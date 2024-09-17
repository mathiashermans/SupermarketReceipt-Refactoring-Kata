using System;

namespace SupermarketReceipt.Offers;
public class PercentageDiscount : BaseDiscountStrategy
{
    public PercentageDiscount(Offer offer, int quantity, double unitPrice) : base(offer, quantity, unitPrice)
    {
    }

    public override Discount CalculateDiscount()
    {
        var discountAmount = _quantity * _unitPrice * _offer.Argument / 100;
        return new Discount(_offer._product, _offer.Argument + "% off", -discountAmount);
    }
}
