using CoinEx.Net.Converters;
using Newtonsoft.Json;

namespace CoinEx.Net.Objects.Models
{
    /// <summary>
    /// Symbol Info
    /// </summary>
    public class CoinExMarketCloseAll
    {
        /// <summary>
        /// The name of the symbol
        /// </summary>
        [JsonProperty("status")]
        public string status { get; set; }= string.Empty;
    }
}
