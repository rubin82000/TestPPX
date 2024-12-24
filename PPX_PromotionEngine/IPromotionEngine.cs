using System.Collections.Generic;

namespace PPX_PromotionEngine
{
    public interface IPromotionEngine
    {
        double GetItemDiscount(int id, double price);
        HashSet<int> GetDiscountableItemIds();
    }
}