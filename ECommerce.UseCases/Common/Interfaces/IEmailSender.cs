using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Common.Interfaces
{
    public interface IEmailSender
    {
        Task<Result> SendAsync(
            string toEmail,
            string subject,
            string body,
            CancellationToken cancellationToken = default);
    }
}
