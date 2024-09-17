using System;

namespace SupermarketReceipt.Offers;
public class FiveForAmountDiscount : BaseDiscountStrategy
{
    private readonly Offer _offer;
    private readonly string _description = "5 for ";

    public FiveForAmountDiscount(Product product, int quantity, double unitPrice, Offer offer) : base(product, quantity, unitPrice)
    {
        _offer = offer;
    }

    public override Discount CalculateDiscount()
    {
        if (_quantity < 5)
            return null;

        var numberOfXs = _quantity / 5;
        var discountTotal = _unitPrice * _quantity - (_offer.Argument * numberOfXs + _quantity % 5 * _unitPrice);
        return new Discount(_product, _description + PriceFormatter.FormatPrice(_offer.Argument), -discountTotal);
    }
}
