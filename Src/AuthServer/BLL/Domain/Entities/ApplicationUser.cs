using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthGuard.BLL.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        private string phoneNumber;

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
    }
}