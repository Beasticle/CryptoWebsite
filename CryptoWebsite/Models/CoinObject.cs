using System.ComponentModel.DataAnnotations;

namespace CryptoWebsite.Models
{
    public class CoinObject
    {
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public string Price { get; set; }
        public string Supply { get; set; }
        public string Mktcap { get; set; }
    }
}
