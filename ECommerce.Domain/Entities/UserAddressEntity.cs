using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;

namespace ECommerce.Domain.Entities
{
    public sealed class UserAddressEntity : BaseEntity
    {
        public const int MaxNameLength = 100;
        public const int MaxPhoneLength = 32;
        public const int MaxCountryLength = 100;
        public const int MaxCityLength = 100;
        public const int MaxStreetLength = 200;
        public const int MaxPostalCodeLength = 20;

        private UserAddressEntity()
        {
        }

        public Guid UserId { get; private set; }
        public string RecipientFirstName { get; private set; } = null!;
        public string RecipientLastName { get; private set; } = null!;
        public string PhoneNumber { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string Street { get; private set; } = null!;
        public string PostalCode { get; private set; } = null!;
        public bool IsDefault { get; private set; }

        public static Result<UserAddressEntity> Create(
            Guid id,
            Guid userId,
            string recipientFirstName,
            string recipientLastName,
            string phoneNumber,
            string country,
            string city,
            string street,
            string postalCode,
            bool isDefault = false)
        {
            if (id == Guid.Empty)
                return Result<UserAddressEntity>.Failure(UserAddressErrors.InvalidId);

            if (userId == Guid.Empty)
                return Result<UserAddressEntity>.Failure(UserAddressErrors.InvalidUserId);

            if (string.IsNullOrWhiteSpace(recipientFirstName) ||
                string.IsNullOrWhiteSpace(recipientLastName))
            {
                return Result<UserAddressEntity>.Failure(UserAddressErrors.InvalidName);
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
                return Result<UserAddressEntity>.Failure(UserAddressErrors.InvalidPhone);

            if (string.IsNullOrWhiteSpace(country) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(street))
            {
                return Result<UserAddressEntity>.Failure(UserAddressErrors.InvalidLocation);
            }

            if (string.IsNullOrWhiteSpace(postalCode))
                return Result<UserAddressEntity>.Failure(UserAddressErrors.InvalidPostalCode);

            return Result<UserAddressEntity>.Success(new UserAddressEntity
            {
                Id = id,
                UserId = userId,
                RecipientFirstName = recipientFirstName.Trim(),
                RecipientLastName = recipientLastName.Trim(),
                PhoneNumber = phoneNumber.Trim(),
                Country = country.Trim(),
                City = city.Trim(),
                Street = street.Trim(),
                PostalCode = postalCode.Trim(),
                IsDefault = isDefault,
                CreatedAt = DateTimeOffset.UtcNow
            });
        }
    }
}
