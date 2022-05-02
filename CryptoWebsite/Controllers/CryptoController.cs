#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CryptoWebsite.Data;
using CryptoWebsite.Models;
using RestSharp;

namespace CryptoWebsite.Controllers
{
    public class CryptoController : Controller
    {
        private readonly CryptoWebsiteContext _context;

        public CryptoController(CryptoWebsiteContext context)
        {
            _context = context;
        }

        // GET: Crypto
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoinObject.ToListAsync());
        }

        // GET: Crypto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coinObject = await _context.CoinObject
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coinObject == null)
            {
                return NotFound();
            }

            return View(coinObject);
        }

        // GET: Crypto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crypto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CryptoCoin coin)
        {

            //coin = new CoinObject();
            var client = new RestClient("https://cryptowebapp1.azurewebsites.net");

            var request = new RestRequest($"/CryptoCoin/{coin.Name}");
            var response = await client.GetAsync<CoinObject>(request);


            if (ModelState.IsValid)
            {
                _context.Add(response);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coin);
        }

        // GET: Crypto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coinObject = await _context.CoinObject.FindAsync(id);
            if (coinObject == null)
            {
                return NotFound();
            }
            return View(coinObject);
        }

        // POST: Crypto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,Supply,Mktcap")] CoinObject coinObject)
        {
            if (id != coinObject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coinObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoinObjectExists(coinObject.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(coinObject);
        }

        // GET: Crypto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coinObject = await _context.CoinObject
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coinObject == null)
            {
                return NotFound();
            }

            return View(coinObject);
        }

        // POST: Crypto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coinObject = await _context.CoinObject.FindAsync(id);
            _context.CoinObject.Remove(coinObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoinObjectExists(int id)
        {
            return _context.CoinObject.Any(e => e.ID == id);
        }
    }
}
