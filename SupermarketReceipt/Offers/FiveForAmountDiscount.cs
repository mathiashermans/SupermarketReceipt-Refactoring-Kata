using System;

namespace SupermarketReceipt.Offers;
public class FiveForAmountDiscount : BaseDiscountStrategy
{
    private readonly string _description = "5 for ";

    public FiveForAmountDiscount(Offer offer, int quantity, double unitPrice) : base(offer, quantity, unitPrice)
    {
    }

    public override Discount CalculateDiscount()
    {
        if (_quantity < 5)
            return null;

        var numberOfXs = _quantity / 5;
        var discountTotal = _unitPrice * _quantity - (_offer.Argument * numberOfXs + _quantity % 5 * _unitPrice);
        return new Discount(_offer._product, _description + PriceFormatter.FormatPrice(_offer.Argument), -discountTotal);
    }
}
