using System;
using System.Collections.Generic;
using System.Linq;
using Visa;

namespace PPX_PromotionEngine
{
    public class VisaPromotionEngineAdapter : IPromotionEngine, IDisposable
    {
        private readonly VisaPromotionEngine _engine;
        private readonly CacheHelper<HashSet<int>> _cacheHelper;

        public VisaPromotionEngineAdapter() : this(new VisaPromotionEngine())
        {
        }

        public VisaPromotionEngineAdapter(VisaPromotionEngine engine)
        {
            _engine = engine;
            _cacheHelper = new CacheHelper<HashSet<int>>(() => _engine.GetDiscountableItemIds().ToHashSet());
        }

        public double GetItemDiscount(int id, double price)
        {
            return _engine.GetItemDiscount(id, price);
        }

        public HashSet<int> GetDiscountableItemIds()
        {
            return _cacheHelper.GetCachedValue() ?? new HashSet<int>();
        }

        public void Dispose()
        {
            _cacheHelper.Dispose();
        }
    }
}