namespace SupermarketReceipt.Offers;
public class TwoForAmountDiscount : BaseDiscountStrategy
{

    public TwoForAmountDiscount(Offer offer, int quantity, double unitPrice) : base(offer, quantity, unitPrice)
    {
    }

    public override Discount CalculateDiscount()
    {
        if (_quantity < 2)
            return null;


        var total = _offer.Argument * (_quantity / 2) + _quantity % 2 * _unitPrice;
        var discountN = _unitPrice * _quantity - total;
        return new Discount(_offer._product, "2 for " + PriceFormatter.FormatPrice(_offer.Argument), -discountN);
    }
}
