using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.Api;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.SL.Security;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.DAL.DomainStack;
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.SL.Notifications
{
    public class NotificationsWorkflowService : INotificationsWorkflowService
    {
        readonly ISecurityCodesEntityService securityCodesEntityService;
        readonly IUserContext<Guid> userContext;
        readonly INotificationsService notificationsService;
        readonly IUnitOfWork unitOfWork;
        readonly SignInManager<ApplicationUser> signInManager;

        public NotificationsWorkflowService(
            ISecurityCodesEntityService securityCodesEntityService,
            IUserContext<Guid> userContext,
            INotificationsService notificationsService,
            IUnitOfWork unitOfWork,
            SignInManager<ApplicationUser> signInManager)
        {
            this.securityCodesEntityService = securityCodesEntityService;
            this.userContext = userContext;
            this.notificationsService = notificationsService;
            this.unitOfWork = unitOfWork;
            this.signInManager = signInManager;
        }

        public async Task<OperationResult> SendAddLocalProviderNotification(UserNameIm im)
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

        public async Task<OperationResult> SendTwoFactorNotification(LocalProviderIm im)
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return OperationResult.Failed(1, "User is not logged in for 2 factor validation.");
            }

            var securityCode = SecurityCode.Generate(SecurityCodeAction.TwoFactorVerification, SecurityCodeParameterName.UserId, user.Id);
            securityCode.AddParameter(SecurityCodeParameterName.LocalProvider, im.LocalProvider.ToString());
            securityCodesEntityService.Insert(securityCode);

            var parameters = new Dictionary<string, string>
            {
                {"Code", securityCode.Code.ToString()}
            };

            var userName = im.LocalProvider == LocalProvider.Email ? user.Email : user.PhoneNumber;

            var result = await notificationsService.SendMessageAsync(userName, Template.TwoFactorCode, parameters);

            if (result.IsSucceed)
            {
                await unitOfWork.SaveAsync();
            }

            return result;
        }
    }
}