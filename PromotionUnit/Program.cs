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
                    else if (IsDuoComboPromotionApplicable(cartItem, shoppingCart, dictionaryDuoComboPromotion))
                    {

                        Console.WriteLine($"DuoCombo promotion applicable for{cartItem.ProductId } ");
                        DuoComboPromotion applicablePromotion = new DuoComboPromotion();
                        applicablePromotion = dictionaryDuoComboPromotion[cartItem.ProductId];
                        double unitPrice1 = dictionaryUnitPrice[cartItem.ProductId];
                        double unitPrice2 = dictionaryUnitPrice[applicablePromotion.ProductIdTwo];
                        double promotionalPrice = CalculateDuoComboPromotionalPrice(cartItem, shoppingCart, applicablePromotion, unitPrice1, unitPrice2);

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


        public static double CalculateDuoComboPromotionalPrice(CartItem currentCartItem, List<CartItem> shoppingCart, DuoComboPromotion duoComboPromotion,
                                                        double unitPriceProduct1, double unitPriceProduct2)
        {

            //
            int i = shoppingCart.FindIndex(x => x.ProductId == duoComboPromotion.ProductIdTwo);
            double priceAfterPromotion = 0;
            if (currentCartItem.Count == shoppingCart[i].Count)

            {
                priceAfterPromotion = currentCartItem.Count * duoComboPromotion.Price;
                currentCartItem.PriceAfterPromotion = priceAfterPromotion;
                shoppingCart[i].PromotionApplied = true;
                shoppingCart[i].PriceAfterPromotion = 0; // combined price given to the other item
            }
            else if (currentCartItem.Count < shoppingCart[i].Count)
            {
                priceAfterPromotion = (currentCartItem.Count * duoComboPromotion.Price) + (shoppingCart[i].Count - currentCartItem.Count) * unitPriceProduct2;
                currentCartItem.PriceAfterPromotion = currentCartItem.Count * duoComboPromotion.Price;
                shoppingCart[i].PromotionApplied = true;
                shoppingCart[i].PriceAfterPromotion = (shoppingCart[i].Count - currentCartItem.Count) * unitPriceProduct2;
            }
            else
            {
                priceAfterPromotion = (shoppingCart[i].Count * duoComboPromotion.Price) + (currentCartItem.Count - shoppingCart[i].Count) * unitPriceProduct1;

                currentCartItem.PriceAfterPromotion = (shoppingCart[i].Count * duoComboPromotion.Price) + (currentCartItem.Count - shoppingCart[i].Count) * unitPriceProduct1;
                shoppingCart[i].PromotionApplied = true;
                shoppingCart[i].PriceAfterPromotion = 0;

            }
            return priceAfterPromotion;
        }


        public static double CalculatePriceWithoutPromotion(CartItem cartItem, double unitPrice)
        {

            if (!cartItem.PromotionApplied)
            {

                cartItem.PriceAfterPromotion = cartItem.Count * unitPrice;
            }
            return cartItem.PriceAfterPromotion;
        }

    }
}  //{ }   []
