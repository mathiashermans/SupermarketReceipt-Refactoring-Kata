using SupermarketReceipt.Offers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SupermarketReceipt;

public class ShoppingCart
{
    private readonly Dictionary<Product, ShoppingCartItem> _shoppingCartItems = new Dictionary<Product, ShoppingCartItem>();
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");
    private readonly SupermarketCatalog _catalog;

    public ShoppingCart(SupermarketCatalog catalog)
    {
        _catalog = catalog;        
    }

    public List<ShoppingCartItem> GetItems()
    {
        return new List<ShoppingCartItem>(_shoppingCartItems.Values);
    }

    public void AddItem(Product product)
    {
        AddItemQuantity(product, 1.0);
    }


    public void AddItemQuantity(Product product, double quantity)
    {
        if (_shoppingCartItems.ContainsKey(product))
        {
            _shoppingCartItems[product].AddItemQuantity(quantity);
        }
        else
        {
            var shoppingCartItem = new ShoppingCartItem(product, _catalog.GetUnitPrice(product), quantity);
            _shoppingCartItems.Add(product, shoppingCartItem);
        }        
    }

    public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers)
    {
        foreach (var p in _shoppingCartItems.Keys)
        {
            
            if (!offers.ContainsKey(p))
                continue;

            Discount discount = CalculateDiscount(offers, p);

            if (discount != null)
                receipt.AddDiscount(discount);
        }
    }

    private Discount CalculateDiscount(Dictionary<Product, Offer> offers, Product product)
    {
        var discountStrategy = DiscountStrategyFactory.GetDiscountStrategy(offers[product], _shoppingCartItems[product]);

        return discountStrategy.CalculateDiscount();
    }

}