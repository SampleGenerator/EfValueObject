using EfValueObject.Models;

namespace EfValueObject.Dtos;

public sealed class PersonDto
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public Address Address { get; init; } = Address.Default;
    public IReadOnlyCollection<string> PhoneNumbers { get; init; }
        = new HashSet<string>();
    public IReadOnlyCollection<GiftCard> GiftCards { get; init; }
        = new HashSet<GiftCard>();
}
