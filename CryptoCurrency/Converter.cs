using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCurrency
{
    public class Converter
    {
        internal Dictionary<string, double> CurrencyValues = new Dictionary<string, double>();

        /// <summary>
        /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
        /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
        /// bliver den gamle værdi overskrevet af den nye værdi
        /// </summary>
        /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
        /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
        public void SetPricePerUnit(String currencyName, double price)
        {
            if (string.IsNullOrEmpty(currencyName))
            {
                Console.WriteLine("Invalid Currency Name");
            }
            else if (price < 0)
            {
                Console.WriteLine("Invalid currency price");
            }
            
            if (!CurrencyValues.ContainsKey(currencyName))
            {
                CurrencyValues.Add(currencyName, price);
            }

            else
            {
                CurrencyValues.Remove(currencyName);
                CurrencyValues.Add(currencyName, price);
            }
        }

        /// <summary>
        /// Konverterer fra en kryptovaluta til en anden. 
        /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
        /// 
        /// </summary>
        /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
        /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
        /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
        /// <returns>Værdien af beløbet i toCurrencyName</returns>
        public double Convert(String fromCurrencyName, String toCurrencyName, double amount)
        {
            if (string.IsNullOrWhiteSpace(fromCurrencyName) || string.IsNullOrWhiteSpace(toCurrencyName))
            {
                throw new ArgumentException("Invalid argument, currency can't be empty");
            }

            try
            {
                var fromCurrencyValue = CurrencyValues[fromCurrencyName];
                var toCurrencyValue = CurrencyValues[toCurrencyName];
                var exchangeRate = fromCurrencyValue / toCurrencyValue;
                var result = exchangeRate * amount;
                return Math.Round(result, 2);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine($"The key{e.GetType().Name} is invalid", e.Message);
                throw;
            }

            catch (ArgumentException e)
            {
                Console.WriteLine($"The value given to the argument{e.GetType().Name} is not valid", e.Message);
                throw;
            }
        }
    }
}
