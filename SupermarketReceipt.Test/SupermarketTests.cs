using System.Collections.Generic;
using Xunit;

namespace SupermarketReceipt.Test;

public class SupermarketTests
{

    [Fact]
    public void NoItemsInCart_ShouldReturnZero()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;

        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, 10);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        Assert.Equal(0, receipt.GetTotalPrice());
    }

    [Fact]
    public void NoOfferApplied_ShouldReturnRegularPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;

        var apple = new Product("apple", ProductUnit.Kilo);
        var applePrice = 1;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        catalog.AddProduct(apple, applePrice);
        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, 10);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(apple);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        Assert.Equal(applePrice, receipt.GetTotalPrice());
    }

    [Fact]
    public void TenPercentDiscount_ShouldReturnDiscountedPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, 10);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        var discount = toothbrushPrice * 0.1;
        Assert.Equal(toothbrushPrice - discount, receipt.GetTotalPrice());
    }

    [Fact]
    public void ThreeForTwoDiscount_WithTwoItems_ShouldReturnRegularPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, 0);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        var totalPrice = toothbrushPrice * 2;
        Assert.Equal(totalPrice, receipt.GetTotalPrice());
    }

    [Fact]
    public void ThreeForTwoDiscount_WithFourItems_ShouldReturnCorrectPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, 0);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        var totalPrice = (toothbrushPrice * 2) + toothbrushPrice;
        Assert.Equal(totalPrice, receipt.GetTotalPrice());
    }

    [Fact]
    public void ThreeForTwoDiscount_WithThreeItems_ShouldReturnDiscountedPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, 0);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        var totalPrice = toothbrushPrice * 2;
        Assert.Equal(totalPrice, receipt.GetTotalPrice());
    }

    [Fact]
    public void TwoForPriceDiscount_WithOneItem_ShouldReturnRegularPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        var discountPrice = 3;
        teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, toothbrush, discountPrice);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        Assert.Equal(toothbrushPrice, receipt.GetTotalPrice());
    }

    [Fact]
    public void TwoForPriceDiscount_WithThreeItems_ShouldReturnCorrectPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        var discountPrice = 3;
        teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, toothbrush, discountPrice);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        Assert.Equal(discountPrice + toothbrushPrice, receipt.GetTotalPrice());
    }

    [Fact]
    public void TwoForPriceDiscount_WithTwoItems_ShouldReturnDiscount()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        var discountPrice = 3;
        teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, toothbrush, discountPrice);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        Assert.Equal(discountPrice, receipt.GetTotalPrice());
    }

    [Fact]
    public void FiveForPriceDiscount_WithThreeItems_ShouldReturnRegularPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        var discountPrice = 8;
        teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, toothbrush, discountPrice);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        Assert.Equal(toothbrushPrice * 3, receipt.GetTotalPrice());
    }

    [Fact]
    public void FiveForPriceDiscount_WithFiveItems_ShouldReturnDiscount()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        var discountPrice = 8;
        teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, toothbrush, discountPrice);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        Assert.Equal(discountPrice, receipt.GetTotalPrice());
    }

    [Fact]
    public void FiveForPriceDiscount_WithSixItems_ShouldReturnCorrectPrice()
    {
        // Arrange
        var catalog = new FakeCatalog();

        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var toothbrushPrice = 2;


        catalog.AddProduct(toothbrush, toothbrushPrice);
        var teller = new Teller(catalog);
        var discountPrice = 8;
        teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, toothbrush, discountPrice);

        var shoppingCart = new ShoppingCart(catalog);

        // Act
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        shoppingCart.AddItem(toothbrush);
        var receipt = teller.ChecksOutArticlesFrom(shoppingCart);

        // Assert
        Assert.Equal(discountPrice + toothbrushPrice, receipt.GetTotalPrice());
    }
}