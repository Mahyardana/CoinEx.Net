using System;
using CoinEx.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace CoinEx.Net.Objects.Models
{
    /// <summary>
    /// Balance info
    /// </summary>
    public class CoinExAssetQuery
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the asset that is available
        /// </summary>
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity of the asset not currently available
        /// </summary>
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Frozen { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Transfer { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Balance_Total { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Margin { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Profit_Unreal { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}
