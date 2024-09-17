using System;
using System.Text.RegularExpressions;

namespace SupermarketReceipt.Offers;
public abstract class BaseDiscountStrategy : IDiscountStrategy
{
    protected readonly Product _product;
    protected readonly int _quantity;
    protected readonly double _unitPrice;

    public BaseDiscountStrategy(Product product, int quantity, double unitPrice)
    {
        _product = product;
        _quantity = quantity;
        _unitPrice = unitPrice;
    }

    public abstract Discount CalculateDiscount();

}
