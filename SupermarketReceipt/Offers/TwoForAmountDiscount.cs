namespace SupermarketReceipt.Offers;
public class TwoForAmountDiscount : BaseDiscountStrategy
{
    private readonly Offer _offer;

    public TwoForAmountDiscount(Product product, int quantity, double unitPrice, Offer offer) : base(product, quantity, unitPrice)
    {
        _offer = offer;
    }

    public override Discount CalculateDiscount()
    {
        if (_quantity < 2)
            return null;


        var total = _offer.Argument * (_quantity / 2) + _quantity % 2 * _unitPrice;
        var discountN = _unitPrice * _quantity - total;
        return new Discount(_product, "2 for " + PriceFormatter.FormatPrice(_offer.Argument), -discountN);
    }
}
