using System;
using System.Globalization;
using CoinEx.Net.Converters;
using CoinEx.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace CoinEx.Net.Objects.Models
{
    /// <summary>
    /// Adjust Leverage info
    /// </summary>
    public class CoinexAdjustLeverage
    {
        [JsonProperty("position_type")]
        public int PositionType { get; set; }

        [JsonProperty("leverage")]
        public string Leverage { get; set; }
    }
}
