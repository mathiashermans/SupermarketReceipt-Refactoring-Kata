using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SupermarketReceipt.Test;
public class CatalogTests
{
    [Fact]
    public void ItemIsCorrectlyPriced()
    {
        // Arrange
        SupermarketCatalog catalog = new FakeCatalog();
        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var price = 1.99;

        // Act
        catalog.AddProduct(toothbrush, price);

        // Assert
        Assert.Equal(price, catalog.GetUnitPrice(toothbrush));
    }

    [Fact]
    public void ShouldThrowErrorForUnknownProduct()
    {
        // Arrange
        SupermarketCatalog catalog = new FakeCatalog();
        var toothbrush = new Product("toothbrush", ProductUnit.Each);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => catalog.GetUnitPrice(toothbrush));
    }

    [Fact]
    public void ShouldThrowErrorWhenAddingDuplicate()
    {
        // Arrange
        SupermarketCatalog catalog = new FakeCatalog();
        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        var price = 1.99;

        // Act
        catalog.AddProduct(toothbrush, price);

        // Assert
        Assert.Throws<ArgumentException>(() => catalog.AddProduct(toothbrush, price));
    }
}
