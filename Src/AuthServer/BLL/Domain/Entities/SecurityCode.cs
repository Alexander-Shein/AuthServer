using System;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities
{
    public class SecurityCode : GuidAggregateRootEntityBase
    {
        const int Min = 1000;
        const int Max = 9999;
        static readonly TimeSpan DefaultExpireTime = TimeSpan.FromHours(1);

        public string UserId { get; set; }
        public int Code { get; set; }
        public DateTime ExpiredAt { get; set; }

        public SecurityCodeAction SecurityCodeAction { get; set; }

        public static SecurityCode Generate(SecurityCodeAction action, string userId)
        {
            return Generate(action, userId, DefaultExpireTime);
        }

        public static SecurityCode Generate(SecurityCodeAction action, string userId, TimeSpan expirationTime)
        {
            return new SecurityCode
            {
                Code = GenerateRandomNumber(),
                SecurityCodeAction = action,
                UserId = userId,
                ExpiredAt = DateTime.Now.Add(expirationTime)
            };
        }

        private static int GenerateRandomNumber()
        {
            var rdm = new Random();
            return rdm.Next(Min, Max);
        }
    }
}