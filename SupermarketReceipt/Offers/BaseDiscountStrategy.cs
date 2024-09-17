using System;
using System.Text.RegularExpressions;

namespace SupermarketReceipt.Offers;
public abstract class BaseDiscountStrategy : IDiscountStrategy
{
    protected readonly Offer _offer;
    protected readonly int _quantity;
    protected readonly double _unitPrice;

    public BaseDiscountStrategy(Offer offer, int quantity, double unitPrice)
    {
        _offer = offer;
        _quantity = quantity;
        _unitPrice = unitPrice;
    }

    public abstract Discount CalculateDiscount();

}
