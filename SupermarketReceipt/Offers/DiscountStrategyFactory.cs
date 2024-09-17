namespace SupermarketReceipt.Offers;
public class DiscountStrategyFactory
{
    public static IDiscountStrategy GetDiscountStrategy(Offer offer, ShoppingCartItem shoppingCartItem)
    {
        IDiscountStrategy discountStrategy = null;

        switch (offer.OfferType)
        {
            case SpecialOfferType.ThreeForTwo:
                discountStrategy = new ThreeForTwoDiscount(offer, shoppingCartItem);
                break;
            case SpecialOfferType.TenPercentDiscount:
                discountStrategy = new PercentageDiscount(offer, shoppingCartItem);
                break;
            case SpecialOfferType.TwoForAmount:
                discountStrategy = new TwoForAmountDiscount(offer, shoppingCartItem);
                break;
            case SpecialOfferType.FiveForAmount:
                discountStrategy = new FiveForAmountDiscount(offer, shoppingCartItem);
                break;
        }

        return discountStrategy;
    }
}
