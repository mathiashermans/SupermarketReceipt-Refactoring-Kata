namespace SupermarketReceipt;

public enum SpecialOfferType
{
    ThreeForTwo,
    TenPercentDiscount,
    TwoForAmount,
    FiveForAmount
}

public class Offer
{
    public readonly Product _product;

    public Offer(SpecialOfferType offerType, Product product, double argument)
    {
        OfferType = offerType;
        Argument = argument;
        _product = product;
    }

    public SpecialOfferType OfferType { get; }
    public double Argument { get; }
}