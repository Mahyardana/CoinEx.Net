using CoinEx.Net.Converters;
using Newtonsoft.Json;

namespace CoinEx.Net.Objects.Models
{
    /// <summary>
    /// Symbol Info
    /// </summary>
    public class CoinExStopLossSettings
    {
        [JsonProperty("market")]
        public string Market { get; set; } = string.Empty;
    }
    public class CoinExTakeProfitSettings
    {
        [JsonProperty("market")]
        public string Market { get; set; } = string.Empty;
    }
}
