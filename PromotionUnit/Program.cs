using System;
using System.Collections.Generic;

namespace PromotionUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Load demo data
            Dictionary<char, int> dictionaryUnitPrice = DemoDataManager.LoadUnitPriceData();
            List<CartItem> shoppingCart = DemoDataManager.LoadShoppingCartData();
            Dictionary<char, MultibuyPromotion> dictionaryMultibuyPromotion = DemoDataManager.LoadMultibuyPromotions();
            Dictionary<char, DuoComboPromotion> dictionaryDuoComboPromotion = DemoDataManager.LoadDuoComboPromotions();

        }
    }
}
