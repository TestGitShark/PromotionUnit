using System;
using System.Collections.Generic;
//For loading sample data
namespace PromotionUnit
{
    public class DemoDataManager
    {
        public static Dictionary<char, int> LoadUnitPriceData()
        {
            Dictionary<char, int> output = new Dictionary<char, int>();
            output.Add('A', 50);
            output.Add('B', 30);
            output.Add('C', 20);
            output.Add('D', 15);
            //output.Add('E', 10);
            return output;
        }

        public static List<CartItem> LoadShoppingCartData()
        {
            List<CartItem> output = new List<CartItem>();
            output.Add(new CartItem { ProductId = 'A', Count = 3 });
            output.Add(new CartItem { ProductId = 'B', Count = 5 });
            output.Add(new CartItem { ProductId = 'C', Count = 1 });
            output.Add(new CartItem { ProductId = 'D', Count = 1 });
           // output.Add(new CartItem { ProductId = 'E', Count = 1 });
            return output;
        }

        public static Dictionary<char, MultibuyPromotion> LoadMultibuyPromotions()
        {
            Dictionary<char, MultibuyPromotion> output = new Dictionary<char, MultibuyPromotion>();
            output.Add('A', new MultibuyPromotion { Id = 'A', Count = 3, Price = 130, ActiveState = true });
            output.Add('B', new MultibuyPromotion { Id = 'B', Count = 2, Price = 45, ActiveState = true });

            return output;
        }

        public static Dictionary<char, DuoComboPromotion> LoadDuoComboPromotions()
        {

            Dictionary<char, DuoComboPromotion> output = new Dictionary<char, DuoComboPromotion>();
            output.Add('C', new DuoComboPromotion { ProductIdOne = 'C', ProductIdTwo = 'D', Price = 30, ActiveState = true });
            return output;

        }
    }
}
