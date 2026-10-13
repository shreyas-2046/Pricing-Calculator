namespace PricingCalculator.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Enums;
    using Helpers;

    /// <summary>Applies a fixed number of free items for each qualifying group.</summary>
    public class MultiBuyDiscount : IDiscount
    {
        private readonly DiscountedProduct _product;
        private readonly int _itemsPerGroup;
        private readonly int _freeItemsPerGroup;

        public MultiBuyDiscount(DiscountedProduct product, int itemsPerGroup, int freeItemsPerGroup)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
            if (itemsPerGroup <= 0) throw new ArgumentOutOfRangeException(nameof(itemsPerGroup));
            if (freeItemsPerGroup <= 0 || freeItemsPerGroup >= itemsPerGroup)
                throw new ArgumentOutOfRangeException(nameof(freeItemsPerGroup));

            _itemsPerGroup = itemsPerGroup;
            _freeItemsPerGroup = freeItemsPerGroup;
        }

        public IEnumerable<AppliedDiscount> DiscountsApplicable(IEnumerable<ProductQuantity> items)
        {
            var line = items.SingleOrDefault(item => item.Product.ProductId == _product.ProductId);
            if (line == null) return Enumerable.Empty<AppliedDiscount>();

            var freeQuantity = line.Quantity / _itemsPerGroup * _freeItemsPerGroup;
            if (freeQuantity == 0) return Enumerable.Empty<AppliedDiscount>();

            var amount = line.Product.UnitPrice * freeQuantity;
            var offerText = $"{_product.Name} multi-buy offer ({freeQuantity} free): - {amount.ToCurrencyString()}";
            return new[]
            {
                new AppliedDiscount { Type = DiscountType.MultiBuy, Text = offerText, Amount = amount }
            };
        }
    }
}
