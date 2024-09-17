namespace SupermarketReceipt.Offers;
public abstract class BaseDiscountStrategy : IDiscountStrategy
{
    protected readonly Offer _offer;
    protected readonly ShoppingCartItem _shoppingCartItem;

    public BaseDiscountStrategy(Offer offer, ShoppingCartItem shoppingCartItem)
    {
        _offer = offer;
        _shoppingCartItem = shoppingCartItem;
    }

    public abstract Discount CalculateDiscount();

}
