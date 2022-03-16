namespace EfValueObject.Models;

public sealed class Person
{
    private Person()
    { }

    public Person(
        string firstName, 
        string lastName, 
        Address address,
        IEnumerable<PhoneNumber> phoneNumbers,
        IEnumerable<GiftCard> giftCards
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumbers = phoneNumbers.ToList();
        GiftCards = giftCards.ToList();
    }


    public int Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Address Address { get; private set; } = Address.Default;
    public IReadOnlyCollection<PhoneNumber> PhoneNumbers { get; private set; }
        = new HashSet<PhoneNumber>();
    public IReadOnlyCollection<GiftCard> GiftCards { get; private set; }
        = new HashSet<GiftCard>();
}
