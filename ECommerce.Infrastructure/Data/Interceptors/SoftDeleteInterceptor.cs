using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Infrastructure.Data.Interceptors
{
    public class SoftDeleteInterceptor : ISoftDeleteInterceptor
    {
        public void Apply(ECommerceDbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State != EntityState.Deleted)
                {
                    continue;
                }

                entry.State = EntityState.Modified;
                entry.Entity.MarkAsDeleted();
            }
        }
    }
}
