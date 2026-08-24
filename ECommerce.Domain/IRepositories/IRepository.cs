using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Repositories
{
    public interface IRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
