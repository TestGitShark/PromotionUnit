using System;
using Xunit;
using PromotionUnit;
using System.Collections.Generic;
namespace PromotionUnit.Tests
{
    public class ProgramTests
    {
        [Theory]
        [InlineData('A', 4, 'A', 3, 120, true)]
        [InlineData('A', 3, 'A', 3, 120, true)]
        [InlineData('B', 3, 'B', 2, 50, true)]
        public void IsMultibuyPromotionApplicable_ReturnsTrue(char cartProductId, int cartProductCount,
                                                            char promotionProductId, int promotionProductCount,
                                                            double promotionProductPrice, bool promotionState)
        {
            //Arrange
            bool expectedResult = true;
            CartItem cartItem = new CartItem { ProductId = cartProductId, Count = cartProductCount };
            Dictionary<char, MultibuyPromotion> dictionaryMultibuyPromotion = new Dictionary<char, MultibuyPromotion>();
            dictionaryMultibuyPromotion.Add(promotionProductId, new MultibuyPromotion { Id = promotionProductId, Count = promotionProductCount, Price = promotionProductPrice, ActiveState = promotionState });
            // dictionaryMultibuyPromotion.Add('B', new MultibuyPromotion { Id = 'B', Count = 2, Price = 50, ActiveState = true });

            //Act

            bool actualResult = Program.IsMultibuyPromotionApplicable(cartItem, dictionaryMultibuyPromotion);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData('A', 2, 'A', 3, 120, true)]
        [InlineData('A', 3, 'A', 3, 120, false)]
        [InlineData('B', 3, 'A', 3, 120, true)]
        public void IsMultibuyPromotionApplicable_ReturnsFalse(char cartProductId, int cartProductCount,
                                                            char promotionProductId, int promotionProductCount,
                                                            double promotionProductPrice, bool promotionState)
        {
            //Arrange
            bool expectedResult = false;
            CartItem cartItem = new CartItem { ProductId = cartProductId, Count = cartProductCount };
            Dictionary<char, MultibuyPromotion> dictionaryMultibuyPromotion = new Dictionary<char, MultibuyPromotion>();
            dictionaryMultibuyPromotion.Add(promotionProductId, new MultibuyPromotion { Id = promotionProductId, Count = promotionProductCount, Price = promotionProductPrice, ActiveState = promotionState });
            // dictionaryMultibuyPromotion.Add('B', new MultibuyPromotion { Id = 'B', Count = 2, Price = 50, ActiveState = true });

            //Act

            bool actualResult = Program.IsMultibuyPromotionApplicable(cartItem, dictionaryMultibuyPromotion);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData('A', 4, 3, 120, 50, 170)]
        [InlineData('A', 3, 3, 120, 50, 120)]
        [InlineData('A', 2, 3, 120, 50, 100)]
        public void CalculateMultibuyPromotionalPrice_ShouldCalculatePromotionalPrice(char productId, int cartProductCount,
                                                              int promotionProductCount,
                                                             double promotionProductPrice,
                                                             double unitPrice, double expectedResult)
        {
            //Arrange
            CartItem cartItem = new CartItem { ProductId = productId, Count = cartProductCount };
            MultibuyPromotion multibuyPromotion = new MultibuyPromotion { Id = productId, Count = promotionProductCount, Price = promotionProductPrice, ActiveState = true };

            //Act
            double actualResult = Program.CalculateMultibuyPromotionalPrice(cartItem, multibuyPromotion, unitPrice);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData('C', 2, 'D', 2, 'C', 'D', 25, true)]
        [InlineData('C', 2, 'D', 1, 'C', 'D', 25, true)]
        public void IsDuoComboPromotionApplicable_ReturnsTrue(char cartProductId, int cartProductCount,
                                                              char cartProductId2, int cartProductCount2,
                                                              char promProductIdOne, char promoProductIdTwo, double promotionPrice,
                                                              bool promotionState)
        {

            //Arrange
            bool expectedResult = true;
            CartItem currentCartItem = new CartItem { ProductId = cartProductId, Count = cartProductCount };
            CartItem secondCartItem = new CartItem { ProductId = cartProductId2, Count = cartProductCount2 };

            //dummy shopping cart with 2 products for testing the duoPromotion
            List<CartItem> shoppingCart = new List<CartItem>();
            shoppingCart.Add(currentCartItem);
            shoppingCart.Add(secondCartItem);


            Dictionary<char, DuoComboPromotion> dictionaryDuComboPromotion = new Dictionary<char, DuoComboPromotion>();
            dictionaryDuComboPromotion.Add(promProductIdOne, new DuoComboPromotion { ProductIdOne = promProductIdOne, ProductIdTwo = promoProductIdTwo, Price = promotionPrice, ActiveState = promotionState });


            //Act
            bool actualResult = Program.IsDuoComboPromotionApplicable(currentCartItem, shoppingCart, dictionaryDuComboPromotion);

            //Assert

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData('C', 2, 'D', 2, 'C', 'D', 25, false)]
        [InlineData('C', 2, 'E', 1, 'C', 'D', 25, true)]
        [InlineData('A', 2, 'E', 1, 'C', 'D', 25, true)]
        public void IsDuoComboPromotionApplicable_ReturnsFalse(char cartProductId, int cartProductCount,
                                                             char cartProductId2, int cartProductCount2,
                                                             char promProductIdOne, char promoProductIdTwo, double promotionPrice,
                                                             bool promotionState)
        {

            //Arrange
            bool expectedResult = false;
            CartItem currentCartItem = new CartItem { ProductId = cartProductId, Count = cartProductCount };
            CartItem secondCartItem = new CartItem { ProductId = cartProductId2, Count = cartProductCount2 };

            //dummy shopping cart with 2 products for testing the duoPromotion
            List<CartItem> shoppingCart = new List<CartItem>();
            shoppingCart.Add(currentCartItem);
            shoppingCart.Add(secondCartItem);


            Dictionary<char, DuoComboPromotion> dictionaryDuComboPromotion = new Dictionary<char, DuoComboPromotion>();
            dictionaryDuComboPromotion.Add(promProductIdOne, new DuoComboPromotion { ProductIdOne = promProductIdOne, ProductIdTwo = promoProductIdTwo, Price = promotionPrice, ActiveState = promotionState });


            //Act
            bool actualResult = Program.IsDuoComboPromotionApplicable(currentCartItem, shoppingCart, dictionaryDuComboPromotion);

            //Assert

            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData('C', 1, 'D', 1, 'C', 'D', 25, 20, 15, 25)]
        [InlineData('C', 2, 'D', 2, 'C', 'D', 25, 20, 15, 50)]
        [InlineData('C', 2, 'D', 3, 'C', 'D', 25, 20, 15, 65)]
        [InlineData('C', 2, 'D', 1, 'C', 'D', 25, 20, 15, 45)]
        public void CalculateDuoComboPromotionalPrice_ShouldCalculateComboPromotionalPrice(char cartProductId, int cartProductCount,
                                                            char cartProductId2, int cartProductCount2,
                                                            char promProductIdOne, char promoProductIdTwo,
                                                            double promotionPrice, double unitPrice1, double unitPrice2, double expectedResult)
        {

            //Arrange

            CartItem currentCartItem = new CartItem { ProductId = cartProductId, Count = cartProductCount };
            CartItem secondCartItem = new CartItem { ProductId = cartProductId2, Count = cartProductCount2 };

            //dummy shopping cart with 2 products for testing the duoPromotion
            List<CartItem> shoppingCart = new List<CartItem>();
            shoppingCart.Add(currentCartItem);
            shoppingCart.Add(secondCartItem);

            DuoComboPromotion comboPromotion = new DuoComboPromotion
            {
                ProductIdOne = promProductIdOne,
                ProductIdTwo = promoProductIdTwo,
                Price = promotionPrice
            };

            //Act

            double actualResult = Program.CalculateDuoComboPromotionalPrice(currentCartItem, shoppingCart, comboPromotion, unitPrice1, unitPrice2);


            //Assert

            Assert.Equal(expectedResult, actualResult);

        }
    }
}
