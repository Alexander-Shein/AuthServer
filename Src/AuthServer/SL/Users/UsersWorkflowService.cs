using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthGuard.Api;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.DAL.QueryRepositories.Users;
using AuthGuard.DAL.Repositories.Security;
using AuthGuard.SL.Notifications;
using AuthGuard.SL.Security;
using AuthGuard.SL.Users.Models.Input;
using AuthGuard.SL.Users.Models.View;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.DAL.DomainStack;
using DddCore.DAL.DomainStack.EntityFramework.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.SL.Users
{
    public class UsersWorkflowService : IUsersWorkflowService
    {
        #region Private Members

        readonly IDataContext context;
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly ISecurityCodesEntityService securityCodesEntityService;
        readonly IUserContext<Guid> userContext;
        readonly IUsersQueryRepository usersQueryRepository;
        readonly ISecurityCodesRepository securityCodesRepository;
        readonly IUnitOfWork unitOfWork;
        readonly IObjectMapper objectMapper;
        readonly INotificationsService notificationsService;

        #endregion

        public UsersWorkflowService(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISecurityCodesEntityService securityCodesEntityService,
            IUserContext<Guid> userContext,
            IUsersQueryRepository usersQueryRepository,
            ISecurityCodesRepository securityCodesRepository,
            IUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            INotificationsService notificationsService)
        {
            this.context = applicationDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.securityCodesEntityService = securityCodesEntityService;
            this.userContext = userContext;
            this.usersQueryRepository = usersQueryRepository;
            this.securityCodesRepository = securityCodesRepository;
            this.unitOfWork = unitOfWork;
            this.objectMapper = objectMapper;
            this.notificationsService = notificationsService;
        }

        #region Public Methods

        public async Task<UserVm> GetCurrentUserAsync()
        {
            var user = await context.Set<ApplicationUser>().FindAsync(userContext.Id.ToString());
            var vm = objectMapper.Map<UserVm>(user);
            return vm;
        }

        public async Task<ApplicationUser> GetUserByEmailOrPhoneAsync(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName)) return null;

            var isEmail = ApplicationUser.IsEmail(userName);
            Expression<Func<ApplicationUser, bool>> predicate;

            if (!isEmail)
            {
                userName = ApplicationUser.CleanPhoneNumber(userName);
                if (!ApplicationUser.IsPhoneNumber(userName)) return null;
                predicate = x => x.PhoneNumber == userName;
            }
            else
            {
                predicate = x => x.Email == userName;
            }

            var user = await context.Set<ApplicationUser>().FirstOrDefaultAsync(predicate);
            return user;
        }

        public async Task<(UserVm User, OperationResult OperationResult)> UpdateAsync(UserIm im)
        {
            var user = await context.Set<ApplicationUser>().FindAsync(userContext.Id.ToString());

            if (im.EmailCode.HasValue)
            {
                var result = await ProcessCode(im.EmailCode.Value, user.PutEmail);
                if (result.IsNotSucceed) return (null, result);
            }

            if (im.Email == String.Empty) user.DeleteEmail();

            if (im.PhoneNumberCode.HasValue)
            {
                var result = await ProcessCode(im.PhoneNumberCode.Value, user.PutPhoneNumber);
                if (result.IsNotSucceed) return (null, result);
            }

            if (im.PhoneNumber == String.Empty) user.DeletePhone();

            if (im.IsTwoFactorEnabled.HasValue && user.TwoFactorEnabled != im.IsTwoFactorEnabled)
            {
                var result = await userManager.SetTwoFactorEnabledAsync(user, im.IsTwoFactorEnabled.Value);
                if (result.Succeeded)
                {
                    user.TwoFactorEnabled = im.IsTwoFactorEnabled.Value;
                }
                else
                {
                    return (null, OperationResult.Failed(2, result.Errors.First().Description));
                }
            }

            context.Set<ApplicationUser>().Update(user);
            await unitOfWork.SaveAsync();
            await signInManager.SignInAsync(user, isPersistent: true);

            var vm = objectMapper.Map<UserVm>(user);
            return (vm, OperationResult.Succeed);
        }

        public async Task<OperationResult> SendCodeToAddLocalProvider(UserNameIm im)
        {
            var securityCode = SecurityCode.Generate(SecurityCodeAction.AddLocalProvider, SecurityCodeParameterName.UserName, im.UserName);
            securityCode.AddParameter(SecurityCodeParameterName.UserName, im.UserName);
            securityCode.AddParameter(SecurityCodeParameterName.UserId, userContext.Id.ToString());
            securityCodesEntityService.Insert(securityCode);

            var parameters = new Dictionary<string, string>
            {
                {"Code", securityCode.Code.ToString()}
            };

            var result = await notificationsService.SendMessageAsync(im.UserName, Template.AddLocalProvider, parameters);

            if (result.IsSucceed)
            {
                await unitOfWork.SaveAsync();
            }

            return result;
        }

        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName)) return false;

            if (ApplicationUser.IsEmail(userName))
            {
                return await usersQueryRepository.IsUserWithEmailExist(userName);
            }

            var phone = ApplicationUser.CleanPhoneNumber(userName);

            if (ApplicationUser.IsPhoneNumber(phone))
            {
                return await usersQueryRepository.IsUserWithPhoneExist(phone);
            }

            return false;
        }

        #endregion

        #region Private Methods

        public async Task<OperationResult> ProcessCode(int code, Func<SecurityCode, OperationResult> callback)
        {
            var securityCode = await securityCodesRepository.ReadByCodeAsync(code);
            if (securityCode == null) return OperationResult.Failed(1, "Invalid code.");
            securityCodesEntityService.Delete(securityCode);

            var result = callback(securityCode);
            return result;
        }

        #endregion
    }
}