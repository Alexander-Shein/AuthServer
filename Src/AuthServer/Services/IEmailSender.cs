﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;

namespace AuthGuard.Services
{
    public interface IEmailSender
    {
        Task<Email> SendEmailAsync(string toEmail, Template template, IDictionary<string, string> parameters);
    }
}