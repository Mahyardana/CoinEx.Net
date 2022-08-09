using System;
using System.Globalization;
using CoinEx.Net.Converters;
using CoinEx.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace CoinEx.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public class CoinExNewPosition
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        public long Id { get; set; }

        [JsonProperty("order_id")]
        private long OrderId { set => Id = value; }

        /// <summary>
        /// Position ID
        /// </summary>
        [JsonProperty("position_id")]
        public long PositionId { get; set; }

        /// <summary>
        /// Stop Order ID
        /// </summary>
        [JsonProperty("stop_id")]
        public long StopId { get; set; }

        /// <summary>
        /// The symbol of the order
        /// </summary>
        [JsonProperty("market")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The transaction type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        [JsonProperty("type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// The transaction type of the order
        /// </summary>
        [JsonConverter(typeof(FuturesOrderSideConverter))]
        [JsonProperty("side")]
        public FuturesOrderSide Side { get; set; }

        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Realized PNL
        /// </summary>
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("deal_profit")]
        public decimal RealizedPNL { get; set; }
        /// <summary>
        /// Position Leverage
        /// </summary>
        [JsonConverter(typeof(IntConverter))]
        [JsonProperty("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }

    }
}
