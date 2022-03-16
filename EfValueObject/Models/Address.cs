namespace EfValueObject.Models;

public sealed record class Address
{
    public Address(string country, string city, int postalCode)
    {
        Country = country;
        City = city;
        PostalCode = postalCode;
    }

    public string Country { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public int PostalCode { get; private set; }

    public static Address Default => new("Iran", "Teheran", 69159);
}