using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CryptoWebsite.Data;
using System;
using System.Linq;
using RestSharp;

namespace CryptoWebsite.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CryptoWebsiteContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CryptoWebsiteContext>>()))
            {
                // Look for any movies.
                if (context.CoinObject.Any())
                {
                    return;   // DB has been seeded
                }



                context.CoinObject.AddRange(
                    new CoinObject
                    {
                        Name = GetCryptoData("BTC").Result.Name,
                        Price = GetCryptoData("BTC").Result.Price,
                        Supply = GetCryptoData("BTC").Result.Supply,
                        Mktcap = GetCryptoData("BTC").Result.Mktcap

                    },

                    new CoinObject
                    {
                        Name = GetCryptoData("ETH").Result.Name,
                        Price = GetCryptoData("ETH").Result.Price,
                        Supply = GetCryptoData("ETH").Result.Supply,
                        Mktcap = GetCryptoData("ETH").Result.Mktcap
                    },

                    new CoinObject
                    {
                        Name = GetCryptoData("BNB").Result.Name,
                        Price = GetCryptoData("BNB").Result.Price,
                        Supply = GetCryptoData("BNB").Result.Supply,
                        Mktcap = GetCryptoData("BNB").Result.Mktcap
                    },

                    new CoinObject
                    {
                        Name = GetCryptoData("XRP").Result.Name,
                        Price = GetCryptoData("XRP").Result.Price,
                        Supply = GetCryptoData("XRP").Result.Supply,
                        Mktcap = GetCryptoData("XRP").Result.Mktcap
                    },
                    new CoinObject
                    {
                        Name = GetCryptoData("SOL").Result.Name,
                        Price = GetCryptoData("SOL").Result.Price,
                        Supply = GetCryptoData("SOL").Result.Supply,
                        Mktcap = GetCryptoData("SOL").Result.Mktcap
                    }
                ) ;
                context.SaveChanges();
            }
        }

        private static async Task<CoinObject> GetCryptoData(string name)
        {
            var client = new RestClient("https://cryptowebapp1.azurewebsites.net");

            var request = new RestRequest($"/CryptoCoin/{name}");
            var response = await client.GetAsync<CoinObject>(request);
            return response;
        }
    }
}