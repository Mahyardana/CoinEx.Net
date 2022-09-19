using CoinEx.Net.Converters;
using Newtonsoft.Json;

namespace CoinEx.Net.Objects.Models
{
    /// <summary>
    /// Symbol Info
    /// </summary>
    public class CoinExMarketList
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("stock")]
        public string Stock { get; set; } = string.Empty;
        [JsonProperty("money")]
        public string Money { get; set; } = string.Empty;
        [JsonProperty("tick_size")]
        public string Tick_size { get; set; } = string.Empty;
        [JsonProperty("leverages")]
        public string[] Leverages { get; set; }
        [JsonProperty("amount_min")]
        public string Amount_min { get; set; }
        [JsonProperty("type")]
        public decimal Type { get; set; }
        [JsonProperty("fee_prec")]
        public decimal Fee_prec { get; set; }
        [JsonProperty("stock_prec")]
        public decimal Stock_prec { get; set; }
        [JsonProperty("money_prec")]
        public decimal Money_prec { get; set; }
        [JsonProperty("multiplier")]
        public decimal Multiplier { get; set; }
        [JsonProperty("amount_prec")]
        public decimal Amount_prec { get; set; }
        [JsonProperty("available")]
        public bool Available { get; set; }
    }
}
