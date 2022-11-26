using System;
using CoinEx.Net.Converters;
using CoinEx.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace CoinEx.Net.Objects.Models
{
    /// <summary>
    /// Withdrawal info
    /// </summary>
    public class CoinExTransferRecord
    {
        /// <summary>
        /// The total quantity of the withdrawal
        /// </summary>
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The name of the asset of the withdrawal
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The time the withdrawal was created
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        [JsonProperty("transfer_type")]
        public string TransferType { get; set; } = string.Empty;
    }
}
