using System;

namespace SupermarketReceipt.Offers;
public class TenPercentDiscount : BaseDiscountStrategy
{
    private readonly double _discount = 0.1;
    private readonly string _description = "10% off"; 
    public TenPercentDiscount(Product product, int quantity, double unitPrice) : base(product, quantity, unitPrice)
    {
    }

    public override Discount CalculateDiscount()
    {
        return new Discount(_product, _description, -_quantity * _unitPrice * _discount);
    }
}
