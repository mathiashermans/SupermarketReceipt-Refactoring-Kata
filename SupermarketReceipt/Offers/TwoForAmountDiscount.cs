namespace SupermarketReceipt.Offers;
public class TwoForAmountDiscount : BaseDiscountStrategy
{

    public TwoForAmountDiscount(Offer offer, ShoppingCartItem shoppingCartItem) : base(offer, shoppingCartItem)
    {
    }

    public override Discount CalculateDiscount()
    {
        if (_shoppingCartItem.QuantityAsInt < 2)
            return null;


        var total = _offer.Argument * (_shoppingCartItem.QuantityAsInt / 2) + _shoppingCartItem.QuantityAsInt % 2 * _shoppingCartItem.UnitPrice;
        var discountN = _shoppingCartItem.UnitPrice * _shoppingCartItem.QuantityAsInt - total;
        return new Discount(_offer._product, "2 for " + PriceFormatter.FormatPrice(_offer.Argument), -discountN);
    }
}
