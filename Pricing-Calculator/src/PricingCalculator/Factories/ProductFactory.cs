namespace PricingCalculator.Factories
{
    using System;
    using Domain;

    public class ProductFactory
    {
        // This product list would be read from a configuration file or database as not to change the code 
        // everytime a product was added/amended/removed
        public Product CreateProduct(string productName)
        {
            switch (productName.ToLower())
            {
                case "apple":
                case "apples":
                    return new Product
                    {
                        ProductId = 1,
                        Name = "Apples",
                        UnitPrice = 0.35m
                    };
                case "banana":
                case "bananas":
                    return new Product
                    {
                        ProductId = 2,
                        Name = "Bananas",
                        UnitPrice = 0.20m
                    };
                case "melon":
                case "melons":
                    return new Product
                    {
                        ProductId = 3,
                        Name = "Melons",
                        UnitPrice = 0.50m
                    };
                case "lime":
                case "limes":
                    return new Product
                    {
                        ProductId = 4,
                        Name = "Limes",
                        UnitPrice = 0.15m
                    };
                default:
                    throw new NotSupportedException($"Unrecognized product name : {productName.ToLower()}");
            }
        }
    }
}
