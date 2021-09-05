using System;
namespace PromotionUnit
{
    public class CartItem
    {
        public char ProductId
        {
            get; set;
        }
        public int Count
        {
            get; set;
        }
        public double PriceAfterPromotion
        {
            get; set;
        }
        public bool PromotionApplied

        {
            get; set;
        }
        public CartItem()
        {
            PriceAfterPromotion = 0;
            PromotionApplied = false;
        }

    }
}
