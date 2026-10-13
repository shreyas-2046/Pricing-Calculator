namespace PricingCalculator.Helpers
{
    using System.Collections.Generic;
    using Discounts;
    using Domain;
    using System;
    using System.Linq;
    using Factories;
    using PricingCalculator.Builders;

    public static class Utilities
    {
        public static string ToCurrencyString(this decimal value)
        {
            return value < 1 ? $"{(int) (value * 100)}p" : $"{value:C}";            
        }

        // This discounts list would be read from a configuration file or database as not to change the code 
        // everytime a discount was added/amended/removed
        public static IEnumerable<IDiscount> CreateDiscounts()
        {
            return new List<IDiscount>
            {
                new MultiBuyDiscount(new DiscountedProduct { ProductId = 3, Name = "Melons" }, 2, 1),
                new MultiBuyDiscount(new DiscountedProduct { ProductId = 4, Name = "Limes" }, 3, 1)
            };
        }

        public static IEnumerable<ProductQuantity> CreateProducts(IEnumerable<string> products)
        {
            var productsAndQuantities = new List<ProductQuantity>();

            var productFactory = new ProductFactory();

            foreach (var product in products)
            {
                var resolvedProduct = productFactory.CreateProduct(product);
                var existProduct = productsAndQuantities.SingleOrDefault(item =>
                    item.Product.ProductId == resolvedProduct.ProductId);

                if (existProduct == null)
                {
                    productsAndQuantities.Add(new ProductQuantity
                    {
                        Product = resolvedProduct,
                        Quantity = 1
                    });
                }

                if (existProduct != null)
                    existProduct.Quantity += 1;
            }

            return productsAndQuantities;            
        }
    }
}
