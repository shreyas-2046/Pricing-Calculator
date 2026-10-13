namespace PricingCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;

    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine("Usage: PricingCalculator <product> [product ...]");
                return 1;
            }

            try
            {
                var products = new List<string>(args);
                var basket = new ShoppingBasket(Utilities.CreateDiscounts());
                basket.AddProducts(Utilities.CreateProducts(products));

                var discounts = basket.GetBasketDiscounts().ToArray();
                var total = basket.SubTotal - discounts.Sum(discount => discount.Amount);

                Console.WriteLine($"Subtotal: {basket.SubTotal.ToCurrencyString()}");
                foreach (var discount in discounts)
                {
                    Console.WriteLine(discount.Text);
                }

                Console.WriteLine($"Total: {total.ToCurrencyString()}");
                return 0;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine($"Error: {exception.Message}");
                return 1;
            }
        }
    }
}
