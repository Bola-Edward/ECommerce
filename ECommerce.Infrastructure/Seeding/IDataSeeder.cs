using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Seeding
{
    public interface IDataSeeder
    {
        int Order { get; }

        Task SeedAsync(CancellationToken ct = default);
    }
}
