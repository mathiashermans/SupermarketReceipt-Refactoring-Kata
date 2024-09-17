namespace SupermarketReceipt;
public class ShoppingCartItem
{
    public Product Product { get; }
    public double Quantity { get; private set; }
    public double UnitPrice { get; private set; }
    public int QuantityAsInt => (int)Quantity;

    public ShoppingCartItem(Product product, double unitPrice, double initialQuantity = 1.0)
    {
        Product = product;
        UnitPrice = unitPrice;
        Quantity = initialQuantity;
    }

    public void AddItemQuantity(double quantity) 
    {
        Quantity += quantity;
    }
}
