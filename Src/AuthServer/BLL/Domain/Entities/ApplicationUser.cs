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
    }
}