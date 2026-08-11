using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : BaseEntity;

        Task<int> SaveChangesAsync(CancellationToken ct = default);

    }
}
