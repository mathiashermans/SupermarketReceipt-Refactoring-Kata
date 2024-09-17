namespace SupermarketReceipt.Offers;
public class ThreeForTwoDiscount : BaseDiscountStrategy
{
    public ThreeForTwoDiscount(Offer offer, int quantity, double unitPrice) : base(offer, quantity, unitPrice)
    {
    }

    public override Discount CalculateDiscount()
    {
        if (_quantity < 3)
            return null;

        var numberOfXs = _quantity / 3;
        var discountAmount = _unitPrice * _quantity - (numberOfXs * 2 * _unitPrice + (_quantity % 3) * _unitPrice);
        return new Discount(_offer._product, "3 for 2", -discountAmount);
    }
}
