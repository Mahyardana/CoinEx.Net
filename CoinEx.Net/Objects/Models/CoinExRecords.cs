using System;
using System.Collections.Generic;
using CoinEx.Net.Converters;
using CoinEx.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace CoinEx.Net.Objects.Models
{
    /// <summary>
    /// Withdrawal info
    /// </summary>
    public class CoinExRecords<T>
    {
        [JsonProperty("records")]
        public IEnumerable<T> Records { get; set; } = Array.Empty<T>();
    }
}
