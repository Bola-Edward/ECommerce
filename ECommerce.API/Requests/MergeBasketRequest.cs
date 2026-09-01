using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Requests
{
    public sealed class MergeBasketRequest
    {
        [Required]
        public Guid AnonymousBuyerId { get; init; }
    }
}
