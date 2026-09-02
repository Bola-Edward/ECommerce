using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Identity
{
    public class ECommerceIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly DbContextOptions<ECommerceIdentityDbContext> _options;
        public ECommerceIdentityDbContext(DbContextOptions<ECommerceIdentityDbContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ECommerceIdentityDbContext).Assembly,
                type => type.Namespace == "ECommerce.Infrastructure.Identity.Configurations");
        }
    }
}
