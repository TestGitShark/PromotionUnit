using System;
namespace PromotionUnit
{

        public class Promotion
        {
            string Name { get; set; }
            public bool ActiveState { get; set; }
            public double Price { get; set; }
            public Promotion()
            {
                //currently all promotions are active
                ActiveState = true;
            }
    }
}
