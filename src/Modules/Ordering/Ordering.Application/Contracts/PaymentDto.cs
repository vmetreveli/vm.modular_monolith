namespace Ordering.Application.Contracts;
public class PaymentDto
{
    public PaymentDto(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber; 
        Expiration = expiration;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }

    public PaymentDto()
    {
        
    }

    public string CardName { get; init; }
    public string CardNumber { get; init; }
    public string Expiration { get; init; }
    public string Cvv { get; init; }
    public int PaymentMethod { get; init; }

    public void Deconstruct(out string cardName, out string cardNumber, out string expiration, out string cvv, out int paymentMethod)
    {
        cardName = CardName;
        cardNumber = CardNumber;
        expiration = Expiration;
        cvv = Cvv;
        paymentMethod = PaymentMethod;
    }
}