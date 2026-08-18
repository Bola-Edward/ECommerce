using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Interceptors
{
    public class AuditInterceptor : IAuditInterceptor
    {
        public void Apply(ECommerceDbContext context)
        {
            var entries = context.ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.MarkAsCreated();

                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.MarkAsUpdated();

                }
            }
        }
    }
}
