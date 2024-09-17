using System;

namespace SupermarketReceipt.Offers;
public class PercentageDiscount : BaseDiscountStrategy
{
    public PercentageDiscount(Offer offer, ShoppingCartItem shoppingCartItem) : base(offer, shoppingCartItem)
    {
    }

    public override Discount CalculateDiscount()
    {
        var discountAmount = _shoppingCartItem.Quantity * _shoppingCartItem.UnitPrice * _offer.Argument / 100;
        return new Discount(_offer._product, _offer.Argument + "% off", -discountAmount);
    }
}
