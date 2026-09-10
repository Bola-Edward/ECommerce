using ECommerce.Domain.Entities;
using ECommerce.UseCases.DeliveryMethods.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.DeliveryMethods.Specifications
{
    public sealed class DeliveryMethodsListSpecification : Specification<DeliveryMethodEntity, DeliveryMethodResponse>
    {
        public DeliveryMethodsListSpecification(bool availableOnly)
        {
            var query = Query;

            if (availableOnly)
                query.Where(m => m.IsAvailable);

            query
                .OrderBy(m => m.DisplayOrder);

            query.Select(m => new DeliveryMethodResponse(
                m.Id,
                m.Name,
                m.Description,
                m.Price,
                m.EstimatedDeliveryTime,
                m.IsAvailable,
                m.DisplayOrder));
        }
    }


    public sealed class DeliveryMethodByIdSpecification
    : Specification<DeliveryMethodEntity>
    {
        public DeliveryMethodByIdSpecification(Guid id)
        {
            Query.Where(m => m.Id == id);
        }
    }

    public sealed class DeliveryMethodByIdWithProjectionSpecification
    : Specification<DeliveryMethodEntity, DeliveryMethodResponse>
    {
        public DeliveryMethodByIdWithProjectionSpecification(Guid id)
        {
            Query
                .Where(m => m.Id == id)
                .Select(m => new DeliveryMethodResponse(
                    m.Id,
                    m.Name,
                    m.Description,
                    m.Price,
                    m.EstimatedDeliveryTime,
                    m.IsAvailable,
                    m.DisplayOrder));
        }
    }

    public sealed class DeliveryMethodByNameSpecification : Specification<DeliveryMethodEntity, DeliveryMethodResponse>
    {
        public DeliveryMethodByNameSpecification(string name, Guid? excludeId = null)
        {
            var normalized = name.Trim();
            Query.Where(m => m.Name == normalized);

            if (excludeId.HasValue)
                Query.Where(m => m.Id != excludeId.Value);
        }
    }
}
