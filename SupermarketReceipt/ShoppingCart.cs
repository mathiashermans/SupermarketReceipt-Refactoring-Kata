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

        switch (offer.OfferType)
        {
            case SpecialOfferType.ThreeForTwo:
                return CalculateThreeForTwoDiscount(product, unitPrice, quantityAsInt);
            case SpecialOfferType.TenPercentDiscount:
                return CalculateTenPercentDiscount(product, unitPrice, quantity, offer);
            case SpecialOfferType.TwoForAmount:
                return CalculateTwoForAmountDiscount(product, unitPrice, quantityAsInt, offer);
            case SpecialOfferType.FiveForAmount:
                return CalculateFiveForAmountDiscount(product, unitPrice, quantityAsInt, offer);
            default:
                break;
        }       

        return discount;
    }

    private Discount CalculateFiveForAmountDiscount(Product product, double unitPrice, int quantityAsInt, Offer offer)
    {
        if (quantityAsInt < 5)
            return null;

        var numberOfXs = quantityAsInt / 5;
        var discountTotal = unitPrice * quantityAsInt - (offer.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
        return new Discount(product, 5 + " for " + PrintPrice(offer.Argument), -discountTotal);
        
    }

    private Discount CalculateTwoForAmountDiscount(Product product, double unitPrice, int quantity, Offer offer)
    {
        if (quantity < 2)
            return null;

        
        var total = offer.Argument * (quantity / 2) + quantity % 2 * unitPrice;
        var discountN = unitPrice * quantity - total;
        return new Discount(product, "2 for " + PrintPrice(offer.Argument), -discountN);
        
    }

    private Discount CalculateTenPercentDiscount(Product product, double unitPrice, double quantity, Offer offer)
    {
        return new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
    }

    private Discount CalculateThreeForTwoDiscount(Product product, double unitPrice, int quantity)
    {
        if (quantity < 3)
            return null;

        var numberOfXs = quantity / 3;
        var discountAmount = unitPrice * quantity - (numberOfXs * 2 * unitPrice + (quantity % 3) * unitPrice);
        return new Discount(product, "3 for 2", -discountAmount);
    }

    private string PrintPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
}