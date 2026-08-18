using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Data.Interceptors
{
    public interface IAuditInterceptor
    {
        public void Apply(ECommerceDbContext context);
    }
}
