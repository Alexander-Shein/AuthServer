using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using DddCore.Contracts.BLL.Errors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthGuard.BLL.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        static readonly EmailAddressAttribute emailValidator = new EmailAddressAttribute();
        static readonly Regex phoneRegex = new Regex(@"^(\+[0-9]{9,15})$");

        string phoneNumber;

        public override string PhoneNumber
        {
            get => phoneNumber;
            set => phoneNumber = CleanPhoneNumber(value);
        }

        public static string CleanPhoneNumber(string phone)
        {
            if (String.IsNullOrWhiteSpace(phone)) return String.Empty;

            return "+" + new string(phone.Where(Char.IsDigit).ToArray());
        }

        public void PutConfirmedEmail(string email)
        {
            EmailConfirmed = true;
            Email = email.ToLower();
            NormalizedEmail = email.ToUpper();
        }

        public void DeleteEmail()
        {
            Email = null;
            NormalizedEmail = null;
            EmailConfirmed = false;
        }

        public void DeletePhone()
        {
            PhoneNumber = null;
            PhoneNumberConfirmed = false;
        }

        public void PutConfirmedPhone(string phone)
        {
            PhoneNumber = phone;
            PhoneNumberConfirmed = true;
        }

        public static bool IsPhoneNumber(string number)
        {
            return phoneRegex.IsMatch(number);
        }

        public static bool IsEmail(string email)
        {
            return emailValidator.IsValid(email);
        }

        public OperationResult PutEmail(SecurityCode code)
        {
            if (code.SecurityCodeAction == SecurityCodeAction.ConfirmAccount)
            {
                Enum.TryParse(
                    code.GetParameterValue(SecurityCodeParameterName.LocalProvider),
                    true,
                    out LocalProvider localProvider);

                if (localProvider != LocalProvider.Email)
                {
                    return OperationResult.Failed(1, "Invalid security code.");
                }

                var userId = code.GetParameterValue(SecurityCodeParameterName.UserId);

                if (userId != Id)
                {
                    return OperationResult.Failed(1, "Invalid security code.");
                }

                EmailConfirmed = true;
                return OperationResult.Succeed;
            }

            if (code.SecurityCodeAction == SecurityCodeAction.AddLocalProvider)
            {
                var userId = code.GetParameterValue(SecurityCodeParameterName.UserId);

                if (userId != Id)
                {
                    return OperationResult.Failed(1, "Invalid security code.");
                }

                var email = code.GetParameterValue(SecurityCodeParameterName.UserName);

                if (!IsEmail(email))
                {
                    return OperationResult.Failed(1, "Invalid security code.");
                }

                PutConfirmedEmail(email);
            }

            throw new NotSupportedException(code.SecurityCodeAction.ToString());
        }

        public OperationResult PutPhoneNumber(SecurityCode code)
        {
            if (code.SecurityCodeAction == SecurityCodeAction.ConfirmAccount)
            {
                Enum.TryParse(
                    code.GetParameterValue(SecurityCodeParameterName.LocalProvider),
                    true,
                    out LocalProvider localProvider);

                if (localProvider != LocalProvider.Phone)
                {
                    return OperationResult.Failed(1, "Invalid security code.");
                }

                var userId = code.GetParameterValue(SecurityCodeParameterName.UserId);

                if (userId != Id)
                {
                    return OperationResult.Failed(1, "Invalid security code.");
                }

                PhoneNumberConfirmed = true;
                return OperationResult.Succeed;
            }

            if (code.SecurityCodeAction == SecurityCodeAction.AddLocalProvider)
            {
                var userId = code.GetParameterValue(SecurityCodeParameterName.UserId);

                if (userId != Id)
                {
                    return OperationResult.Failed(1, "Invalid security code.");
                }

                var phone = code.GetParameterValue(SecurityCodeParameterName.UserName);
                phone = CleanPhoneNumber(phone);

                if (!IsPhoneNumber(phone))
                {
                    return OperationResult.Failed(1, "Invalid security code.");
                }

                PutConfirmedPhone(phone);
            }

            throw new NotSupportedException(code.SecurityCodeAction.ToString());
        }
    }
}