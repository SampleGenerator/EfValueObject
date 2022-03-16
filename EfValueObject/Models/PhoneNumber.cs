namespace EfValueObject.Models;

public sealed record class PhoneNumber
{
    public PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static string Defaule => "09350000000";

    public static implicit operator string(PhoneNumber phoneNumber)
    {
        return phoneNumber.Value;
    }

    public static implicit operator PhoneNumber(string phoneNumber)
    {
        return new PhoneNumber(phoneNumber);
    }

    public override string ToString()
    {
        return Value;
    }
}
