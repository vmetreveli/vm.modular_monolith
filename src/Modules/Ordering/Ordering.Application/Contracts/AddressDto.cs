namespace Ordering.Application.Contracts;
public class AddressDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; } 
    public string AddressLine { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }

    public AddressDto()
    {
    }

    public AddressDto(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public void Deconstruct(out string FirstName, out string LastName, out string EmailAddress, out string AddressLine, out string Country, out string State, out string ZipCode)
    {
        FirstName = this.FirstName;
        LastName = this.LastName;
        EmailAddress = this.EmailAddress;
        AddressLine = this.AddressLine;
        Country = this.Country;
        State = this.State;
        ZipCode = this.ZipCode;
    }
}