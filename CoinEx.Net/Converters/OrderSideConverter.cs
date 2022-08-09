using CoinEx.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace CoinEx.Net.Converters
{
    internal class OrderSideConverter : BaseConverter<OrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderSide, string>> Mapping => new List<KeyValuePair<OrderSide, string>>
        {
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell")
        };
    }
    internal class FuturesOrderSideConverter : BaseConverter<FuturesOrderSide>
    {
        public FuturesOrderSideConverter() : this(true) { }
        public FuturesOrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FuturesOrderSide, string>> Mapping => new List<KeyValuePair<FuturesOrderSide, string>>
        {
            new KeyValuePair<FuturesOrderSide, string>(FuturesOrderSide.LongBuy, "2"),
            new KeyValuePair<FuturesOrderSide, string>(FuturesOrderSide.ShortSell, "1")
        };
    }
}
