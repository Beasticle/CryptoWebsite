#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CryptoWebsite.Models;

namespace CryptoWebsite.Data
{
    public class CryptoWebsiteContext : DbContext
    {
        public CryptoWebsiteContext (DbContextOptions<CryptoWebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<CryptoWebsite.Models.CoinObject> CoinObject { get; set; }
    }
}
