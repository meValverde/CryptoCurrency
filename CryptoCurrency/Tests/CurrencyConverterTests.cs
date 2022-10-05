using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace CryptoCurrency.Tests
{
    public class CurrencyConverterTests
    {
        [Fact]
        public void CanInsertNewCurrency()
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

    }
}