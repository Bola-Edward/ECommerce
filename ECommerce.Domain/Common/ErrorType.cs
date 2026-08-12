using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Common
{
    public enum ErrorType
    {
        Validation,
        NotFound,
        Conflict,
        UnAuthorized,
        Forbidden,
        Failure,
    }
}
