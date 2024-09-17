using System;

namespace SupermarketReceipt.Offers;
public class FiveForAmountDiscount : BaseDiscountStrategy
{
    private readonly string _description = "5 for ";

    public FiveForAmountDiscount(Offer offer, ShoppingCartItem shoppingCartItem) : base(offer, shoppingCartItem)
    {
    }

    public override Discount CalculateDiscount()
    {
        if (_shoppingCartItem.QuantityAsInt < 5)
            return null;

        var numberOfXs = _shoppingCartItem.QuantityAsInt / 5;
        var discountTotal = _shoppingCartItem.UnitPrice * _shoppingCartItem.QuantityAsInt - (_offer.Argument * numberOfXs + _shoppingCartItem.QuantityAsInt % 5 * _shoppingCartItem.UnitPrice);
        return new Discount(_offer._product, _description + PriceFormatter.FormatPrice(_offer.Argument), -discountTotal);
    }
}
