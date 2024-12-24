using System.Collections.Generic;
using PPXModel;
using Visa;
using Loyalty;
using System.Linq;
using System;

namespace PPX_PromotionEngine
{
    /// <summary>
    /// PromotionEngine
    /// Assumptions:
    /// Each engine will get the original item price.
    /// 2 or more with the same id will have the same discounts.
    /// 
    /// </summary>
    public class PromotionEngine
    {
        private readonly List<IPromotionEngine> _providers;

        public PromotionEngine()
        {
            _providers = new List<IPromotionEngine>
                    {
                        new LoyaltyPromotionEngineAdapter(),
                        new VisaPromotionEngineAdapter()
                    };
        }

        /// <summary>
        /// GetDiscount method - totalDiscount for each items
        /// multiple items 
        /// </summary>
        /// <param name="items">List of items</param>
        /// <returns></returns>
        public List<(Item item, double newPrice)> GetDiscounts(List<Item> items)
        {
            var result = new List<(Item, double)>();

            foreach (var item in items)
            {
                double newPrice = item.Price;

                // Apply discounts from all providers
                foreach (var provider in _providers)
                {
                    try
                    {
                        if (provider.GetDiscountableItemIds().Contains(item.Id))
                        {
                            var discount = provider.GetItemDiscount(item.Id, newPrice);
                            if (discount < 0 || discount > newPrice)
                            {
                                //log warning
                            }
                            else newPrice -= discount;
                        }
                    }
                    catch (Exception)
                    {
                        //log error or warning
                    }
                }

                // Add item with its final price
                result.Add((item, newPrice));
            }

            return result;
        }
    }
}