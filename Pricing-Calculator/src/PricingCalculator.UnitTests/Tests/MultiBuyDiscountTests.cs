namespace PricingCalculator.UnitTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PricingCalculator;
    using PricingCalculator.Discounts;
    using PricingCalculator.Domain;
    using PricingCalculator.Enums;
    using PricingCalculator.Factories;
    using PricingCalculator.Helpers;

    [TestClass]
    public class MultiBuyDiscountTests
    {
        [TestMethod]
        public void Melons_BuyOneGetOneFree_EverySecondMelonIsFree()
        {
            var basket = CreateBasket("Melon", "Melon", "Melon");

            Assert.AreEqual(1.50m, basket.SubTotal);
            Assert.AreEqual(0.50m, basket.GetBasketDiscounts().Sum(discount => discount.Amount));
            Assert.AreEqual(1.00m, basket.SubTotal - basket.GetBasketDiscounts().Sum(discount => discount.Amount));
        }

        [TestMethod]
        public void Limes_ThreeForPriceOfTwo_OneLimeIsFreePerGroupOfThree()
        {
            var basket = CreateBasket("Lime", "Lime", "Lime", "Lime", "Lime", "Lime", "Lime");

            Assert.AreEqual(1.05m, basket.SubTotal);
            Assert.AreEqual(0.30m, basket.GetBasketDiscounts().Sum(discount => discount.Amount));
            Assert.AreEqual(0.75m, basket.SubTotal - basket.GetBasketDiscounts().Sum(discount => discount.Amount));
        }

        [TestMethod]
        public void MixedBasket_AppliesPricesAndBothOffers()
        {
            var basket = CreateBasket("Apple", "Apple", "Banana", "Melon", "Melon", "Lime", "Lime", "Lime");

            Assert.AreEqual(2.35m, basket.SubTotal);
            Assert.AreEqual(0.65m, basket.GetBasketDiscounts().Sum(discount => discount.Amount));
            Assert.AreEqual(1.70m, basket.SubTotal - basket.GetBasketDiscounts().Sum(discount => discount.Amount));
        }

        [TestMethod]
        public void IncompleteOfferGroups_DoNotEarnDiscount()
        {
            var basket = CreateBasket("Melon", "Lime", "Lime");

            Assert.AreEqual(0.80m, basket.SubTotal);
            Assert.AreEqual(0m, basket.GetBasketDiscounts().Sum(discount => discount.Amount));
        }

        [TestMethod]
        public void ProductFactory_AcceptsSingularAndPluralNames()
        {
            var factory = new ProductFactory();

            Assert.AreEqual(0.35m, factory.CreateProduct("Apple").UnitPrice);
            Assert.AreEqual(0.35m, factory.CreateProduct("Apples").UnitPrice);
            Assert.AreEqual(0.20m, factory.CreateProduct("Banana").UnitPrice);
            Assert.AreEqual(0.20m, factory.CreateProduct("Bananas").UnitPrice);
            Assert.AreEqual(0.50m, factory.CreateProduct("Melon").UnitPrice);
            Assert.AreEqual(0.50m, factory.CreateProduct("Melons").UnitPrice);
            Assert.AreEqual(0.15m, factory.CreateProduct("Lime").UnitPrice);
            Assert.AreEqual(0.15m, factory.CreateProduct("Limes").UnitPrice);
        }

        private static ShoppingBasket CreateBasket(params string[] productNames)
        {
            var basket = new ShoppingBasket(Utilities.CreateDiscounts());
            basket.AddProducts(Utilities.CreateProducts(productNames));
            return basket;
        }
    }
}
