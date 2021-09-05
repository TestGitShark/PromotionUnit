using System;
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
                    if (IsMultibuyPromotionApplicable(cartItem, dictionaryMultibuyPromotion))
                    {

                        MultibuyPromotion applicablePromotion = new MultibuyPromotion();
                        applicablePromotion = dictionaryMultibuyPromotion[cartItem.ProductId];
                        double unitPrice = dictionaryUnitPrice[cartItem.ProductId];
                        cartItem.PriceAfterPromotion = CalculateMultibuyPromotionalPrice(cartItem, applicablePromotion, unitPrice);
                        cartItem.PromotionApplied = true;

                        Console.WriteLine($"Multibuy promotion applicable for{cartItem.ProductId } ");
                        Console.WriteLine($"Price of {cartItem.Count } {cartItem.ProductId } after promotion - {cartItem.PriceAfterPromotion }");

                    }

                }

                }

                catch (Exception e)

                {
                        Console.WriteLine(e.Message);
                }

        }

        public static bool IsMultibuyPromotionApplicable(CartItem cartItem, Dictionary<char, MultibuyPromotion> dictionaryMultibuyPromotion)
        {
            try { 
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
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }


        }


        public static double CalculateMultibuyPromotionalPrice(CartItem cartItem, MultibuyPromotion multibuyPromotion, double unitPrice)

        {

            double priceAfterPromotion = (int)(cartItem.Count / multibuyPromotion.Count) * multibuyPromotion.Price +
                        (cartItem.Count % multibuyPromotion.Count) * unitPrice;




            return priceAfterPromotion;
        }

        public static bool IsDuoComboPromotionApplicable(CartItem currentItem, List<CartItem> shoppingCart, Dictionary<char, DuoComboPromotion> dictionaryDuoCombo)

        {
            //if there is an active promotion for the current item & 
            if (dictionaryDuoCombo.ContainsKey(currentItem.ProductId) &&

                dictionaryDuoCombo[currentItem.ProductId].ActiveState &&
                // the combination item exists in the shopping cart
                shoppingCart.Exists(x => x.ProductId == dictionaryDuoCombo[currentItem.ProductId].ProductIdTwo))

                return true;
            else
                return false;

        }

    }
}  //{ }   []
