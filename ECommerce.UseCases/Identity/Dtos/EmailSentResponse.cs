using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Identity.Dtos
{
    public sealed record EmailSentResponse(
    string Email,
    bool VerificationCodeResent,
    string Message);
}
