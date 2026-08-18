
namespace ECommerce.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset? UpdatedAt { get; protected set; }
        public bool IsDeleted { get; private set; }

        // todo : Add CreatedBy and UpdatedBy properties for auditing purposes

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void MarkAsUpdated()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void MarkAsCreated()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
