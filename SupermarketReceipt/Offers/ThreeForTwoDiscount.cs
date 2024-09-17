namespace SupermarketReceipt.Offers;
public class ThreeForTwoDiscount : BaseDiscountStrategy
{
    public ThreeForTwoDiscount(Offer offer, ShoppingCartItem shoppingCartItem) : base(offer, shoppingCartItem)
    {
    }

    public override Discount CalculateDiscount()
    {
        if (_shoppingCartItem.QuantityAsInt < 3)
            return null;

        var numberOfXs = _shoppingCartItem.QuantityAsInt / 3;
        var discountAmount = _shoppingCartItem.UnitPrice * _shoppingCartItem.QuantityAsInt - (numberOfXs * 2 * _shoppingCartItem.UnitPrice + (_shoppingCartItem.QuantityAsInt % 3) * _shoppingCartItem.UnitPrice);
        return new Discount(_offer._product, "3 for 2", -discountAmount);
    }
}
