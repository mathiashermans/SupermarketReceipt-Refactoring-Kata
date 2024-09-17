namespace SupermarketReceipt;
public class ShoppingCartItem
{
    public Product Product { get; }
    public double Quantity { get; private set; }
    public double UnitPrice { get; private set; }
    public int QuantityAsInt => (int)Quantity;

    public ShoppingCartItem(Product product)
    {
        Product = product;
        Quantity = 1;
    }

    public void AddItemQuantity(double quantity) 
    {
        Quantity += quantity;
    }

    public void SetUnitPrice(double unitPrice)
    {
        UnitPrice = unitPrice;
    }
}
