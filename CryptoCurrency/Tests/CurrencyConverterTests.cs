using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace CryptoCurrency.Tests
{
    public class CurrencyConverterTests
    {
        [Fact]
        public void CanInsertNewCurrencyAndReplacePrice()
        {
            //Arrange
            var converter = new Converter();
            var values = new Dictionary<string, double>()
            {
                {"Bitcoin",200},
                {"Litecoin",210},
                {"Namecoin",400},
            };
            
            //Act
            foreach (var currency in values)
            {
                converter.SetPricePerUnit(currency.Key, currency.Value);
            }
            converter.SetPricePerUnit("Bitcoin", 180);
            
            //Assert
            converter.CurrencyValues["Bitcoin"].Should().Be(180);

        }
        
        
        [Theory]
        [InlineData("Namecoin","Bitcoin",50,100.00)]
        [InlineData("Litecoin","Namecoin",80,42.00)]
        [InlineData("Litecoin","Bitcoin",150,157.5)]
        public void CanConvertCurrencies(string fromCurrency, string toCurrency, int amount, double expectedResult)
        {
            //Arrange
            var converter = new Converter();
            var values = new Dictionary<string, double>()
            {
                {"Bitcoin",200},
                {"Litecoin",210},
                {"Namecoin",400},
            };
            foreach (var currency in values)
            {
                converter.SetPricePerUnit(currency.Key, currency.Value);
            }
            //Act

            var convertedCurrency = converter.Convert(fromCurrency, toCurrency, amount);

            //Assert
            convertedCurrency.Should().Be(expectedResult);

        }

        [Theory]
        [InlineData("", "notAcoin", typeof(ArgumentException))]
        [InlineData("notACoint", "", typeof(ArgumentException))]
        [InlineData("notACoin", "neitherACoin", typeof(KeyNotFoundException))]
        public void WrongArgumentsThrowDifferentExceptions(string fromCurrency, string toCurrency, Type exceptionType)
        {
            //Arrange
            var converter = new Converter();
          
            //Act + Assert
            Exception ex = Assert.Throws(exceptionType, () => converter.Convert(fromCurrency, toCurrency, 90));
            
        }

    }
}