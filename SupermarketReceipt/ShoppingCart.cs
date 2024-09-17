using SupermarketReceipt.Offers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SupermarketReceipt;

public class ShoppingCart
{
    //private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
    //private readonly Dictionary<Product, double> _productQuantities = new Dictionary<Product, double>();
    private readonly Dictionary<Product, ShoppingCartItem> _shoppingCartItems = new Dictionary<Product, ShoppingCartItem>();
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");


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
            var shoppingCartItem = new ShoppingCartItem(product);
            _shoppingCartItems.Add(product, shoppingCartItem);
        }

        
    }

    public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog)
    {
        foreach (var p in _shoppingCartItems.Keys)
        {
            
            if (!offers.ContainsKey(p))
                continue;

            Discount discount = CalculateDiscount(offers, catalog, p);

            if (discount != null)
                receipt.AddDiscount(discount);
        }
    }

    private Discount CalculateDiscount(Dictionary<Product, Offer> offers, SupermarketCatalog catalog, Product product)
    {

        var quantity = _shoppingCartItems[product].Quantity;
        var quantityAsInt = (int)_shoppingCartItems[product].Quantity;
        var offer = offers[product];
        _shoppingCartItems[product].SetUnitPrice(catalog.GetUnitPrice(product));
        IDiscountStrategy discountStrategy = null;

        switch (offer.OfferType)
        {
            case SpecialOfferType.ThreeForTwo:
                discountStrategy = new ThreeForTwoDiscount(offer, _shoppingCartItems[product]);
                break;
            case SpecialOfferType.TenPercentDiscount:
                discountStrategy = new PercentageDiscount(offer, _shoppingCartItems[product]);
                break;
            case SpecialOfferType.TwoForAmount:
                discountStrategy = new TwoForAmountDiscount(offer, _shoppingCartItems[product]);
                break;
            case SpecialOfferType.FiveForAmount:
                discountStrategy = new FiveForAmountDiscount(offer, _shoppingCartItems[product]);
                break;
            default:
                break;
        }              

        return discountStrategy.CalculateDiscount();
    }

}