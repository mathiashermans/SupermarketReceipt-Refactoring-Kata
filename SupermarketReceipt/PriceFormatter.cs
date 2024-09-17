using System.Globalization;

namespace SupermarketReceipt;
public static class PriceFormatter
{
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public static string FormatPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
}
