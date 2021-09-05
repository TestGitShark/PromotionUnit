﻿using System;
using System.Collections.Generic;
/*
 Promotion Unit applies currently active promotions on a given shopping cart
ASSUMPTIONS
1. Only one promotion per product
2. On the shopping cart , one entry per product, and total count of the product
 
 */
namespace PromotionUnit
{
    public class Program
    {
        static void Main(string[] args)
        {
                try
                {

            

                    //Load demo data
                    Dictionary<char, int> dictionaryUnitPrice = DemoDataManager.LoadUnitPriceData();
                    List<CartItem> shoppingCart = DemoDataManager.LoadShoppingCartData();
                    Dictionary<char, MultibuyPromotion> dictionaryMultibuyPromotion = DemoDataManager.LoadMultibuyPromotions();
                    Dictionary<char, DuoComboPromotion> dictionaryDuoComboPromotion = DemoDataManager.LoadDuoComboPromotions();


                    foreach (CartItem cartItem in shoppingCart)
                    {
                        

                    }

                }

                catch (Exception e)

                {
                        Console.WriteLine(e.Message);
                }

        }

        public static bool IsMultibuyPromotionApplicable(CartItem cartItem, Dictionary<char, MultibuyPromotion> dictionaryMultibuyPromotion)
        {
            if (dictionaryMultibuyPromotion.ContainsKey(cartItem.ProductId))
            {
                MultibuyPromotion mp = new MultibuyPromotion();
                mp = dictionaryMultibuyPromotion[cartItem.ProductId];
                if (mp.ActiveState && cartItem.Count >= mp.Count)

                    return true;

                else

                    return false;
            }
            else
                return false;

        }
    }
}
