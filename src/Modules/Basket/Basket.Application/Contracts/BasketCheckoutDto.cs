using System;

namespace Basket.Application.Contracts;

public class BasketCheckoutDto
{
    public string UserName { get; init; }
    public Guid CustomerId { get; init; } 
    public decimal TotalPrice { get; init; } 
    public string FirstName { get; init; } 
    public string LastName { get; init; } 
    public string EmailAddress { get; init; } 
    public string AddressLine { get; init; }
    public string Country { get; init; } 
    public string State { get; init; } 
    public string ZipCode { get; init; }
    public string CardName { get; init; } 
    public string CardNumber { get; init; } 
    public string Expiration { get; init; }
    public string Cvv { get; init; }
    public int PaymentMethod { get; init; }

    public void Deconstruct(out string UserName, out Guid CustomerId, out decimal TotalPrice,
        // Shipping and BillingAddress
        out string FirstName, out string LastName, out string EmailAddress, out string AddressLine, out string Country,
        out string State, out string ZipCode,
        //Payment
        out string CardName, out string CardNumber, out string Expiration, out string Cvv, out int PaymentMethod)
    {
        UserName = this.UserName;
        CustomerId = this.CustomerId;
        TotalPrice = this.TotalPrice;
        FirstName = this.FirstName;
        LastName = this.LastName;
        EmailAddress = this.EmailAddress;
        AddressLine = this.AddressLine;
        Country = this.Country;
        State = this.State;
        ZipCode = this.ZipCode;
        CardName = this.CardName;
        CardNumber = this.CardNumber;
        Expiration = this.Expiration;
        Cvv = this.Cvv;
        PaymentMethod = this.PaymentMethod;
    }
}