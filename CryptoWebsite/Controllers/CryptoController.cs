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

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }

        public async Task<IActionResult> cryptoView()
        {
            var client = new RestClient("https://cryptowebapp1.azurewebsites.net");
            var request = new RestRequest("/CryptoCoin/BTC");
            var response = await client.GetAsync(request);
            ViewData["coin"] = response.Content;
            return View();
        }
    }
}