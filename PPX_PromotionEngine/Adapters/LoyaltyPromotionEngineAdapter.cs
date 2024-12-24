using Loyalty;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PPX_PromotionEngine
{
    public class LoyaltyPromotionEngineAdapter : IPromotionEngine, IDisposable
    {
        private readonly LoyaltyPromotionEngine _engine;
        private readonly CacheHelper<HashSet<int>> _cacheHelper;

        public LoyaltyPromotionEngineAdapter() : this(new LoyaltyPromotionEngine())
        {
        }

        public LoyaltyPromotionEngineAdapter(LoyaltyPromotionEngine engine)
        {
            _engine = engine;
            _cacheHelper = new CacheHelper<HashSet<int>>(() => GetDiscountableItemIdsInternal());
        }

        public double GetItemDiscount(int id, double price)
        {
            return _engine.GetItemDiscount(id, price);
        }

        public HashSet<int> GetDiscountableItemIds()
        {
            return _cacheHelper.GetCachedValue() ?? new HashSet<int>();
        }

        private HashSet<int> GetDiscountableItemIdsInternal()
        {
            try
            {
                return _engine.GetDiscountableItemIds().ToHashSet();
            }
            catch (Exception)
            {
                return new HashSet<int>();
            }
        }

        public void Dispose()
        {
            _cacheHelper.Dispose();
        }
    }
}