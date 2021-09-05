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
    }
}
