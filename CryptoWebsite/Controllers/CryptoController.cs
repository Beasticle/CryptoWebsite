using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using RestSharp;

namespace CryptoWebsite.Controllers
{
    public class CryptoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> cryptoView(Models.CryptoCoin coin)
        {
            var client = new RestClient("https://cryptowebapp1.azurewebsites.net");

            if (coin.SearchName == null)
            {
                coin = new Models.CryptoCoin();
                coin.SearchName = "BTC";
            }

            var request = new RestRequest($"/CryptoCoin/{coin.SearchName}");
            var response = await client.GetAsync<Models.CoinObject>(request);
            ViewData["name"] = response.Name;
            ViewData["price"] = response.Price;
            ViewData["supply"] = response.Supply;
            ViewData["mktcap"] = response.Mktcap;
            return View();
        }
    }
}