using SupermarketReceipt.Offers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SupermarketReceipt;

public class ShoppingCart
{
    private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
    private readonly Dictionary<Product, double> _productQuantities = new Dictionary<Product, double>();
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");


    public List<ProductQuantity> GetItems()
    {
        return new List<ProductQuantity>(_items);
    }

    public void AddItem(Product product)
    {
        AddItemQuantity(product, 1.0);
    }


    public void AddItemQuantity(Product product, double quantity)
    {
        _items.Add(new ProductQuantity(product, quantity));
        if (_productQuantities.ContainsKey(product))
        {
            var newAmount = _productQuantities[product] + quantity;
            _productQuantities[product] = newAmount;
        }
        else
        {
            _productQuantities.Add(product, quantity);
        }
    }

    public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog)
    {
        foreach (var p in _productQuantities.Keys)
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

        var quantity = _productQuantities[product];
        var quantityAsInt = (int)quantity;
        var offer = offers[product];
        var unitPrice = catalog.GetUnitPrice(product);
        Discount discount = null;
        IDiscountStrategy discountStrategy = null;

        switch (offer.OfferType)
        {
            case SpecialOfferType.ThreeForTwo:
                discountStrategy = new ThreeForTwoDiscount(product, quantityAsInt, unitPrice);
                break;
            case SpecialOfferType.TenPercentDiscount:
                discountStrategy = new TenPercentDiscount(product, quantityAsInt, unitPrice);
                break;
            case SpecialOfferType.TwoForAmount:
                discountStrategy = new TwoForAmountDiscount(product, quantityAsInt, unitPrice, offer);
                break;
            case SpecialOfferType.FiveForAmount:
                discountStrategy = new FiveForAmountDiscount(product, quantityAsInt, unitPrice, offer);
                break;
            default:
                break;
        }

        

        return discountStrategy.CalculateDiscount();
    }

}