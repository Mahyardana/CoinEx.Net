﻿using CoinEx.Net.Converters;
using CoinEx.Net.Enums;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoinEx.Net.Objects.Models;
using CoinEx.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.CommonObjects;

namespace CoinEx.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class CoinExClientFuturesApiTrading : ICoinExClientFuturesApiTrading
    {
        private const string PlaceLimitOrderEndpoint = "order/limit";
        private const string PlaceMarketOrderEndpoint = "order/put_market";
        private const string PlaceStopLimitOrderEndpoint = "order/stop/limit";
        private const string PlaceStopMarketOrderEndpoint = "order/stop/market";
        private const string PlaceImmediateOrCancelOrderEndpoint = "order/ioc";
        private const string FinishedOrdersEndpoint = "order/finished";
        private const string OpenPositionsEndpoint = "position/pending";
        private const string PositionStopLossSettingsEndpoint = "position/stop_loss";
        private const string PositionTakeProfitSettingsEndpoint = "position/take_profit";
        private const string OpenStopOrdersEndpoint = "order/stop/pending";
        private const string OrderStatusEndpoint = "order/status";
        private const string OrderDetailsEndpoint = "order/deals";
        private const string UserTransactionsEndpoint = "order/user/deals";
        private const string CancelOrderEndpoint = "order/pending";
        private const string MarketCloseAllEndpoint = "position/market_close";
        private const string CancelStopOrderEndpoint = "order/stop/pending";
        private const string AdjustLeverageEndpoint = "market/adjust_leverage";
        private const string MarketListEndpoint = "market/list";

        private readonly CoinExClientFuturesApi _baseClient;

        internal CoinExClientFuturesApiTrading(CoinExClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExNewPosition>> PlaceOrderAsync(
            string symbol,
            FuturesOrderSide side,
            OrderType type,
            decimal quantity,

            decimal? price = null,
            int? leverage = null,
            decimal? stopPrice = null,
            bool? immediateOrCancel = null,
            OrderOption? orderOption = null,
            string? clientOrderId = null,
            string? sourceId = null,
            CancellationToken ct = default)
        {
            symbol.ValidateCoinExSymbol();

            var endpoint = "";
            if (type == OrderType.Limit)
                endpoint = PlaceLimitOrderEndpoint;
            else if (type == OrderType.Market)
                endpoint = PlaceMarketOrderEndpoint;
            else if (type == OrderType.StopLimit)
                endpoint = PlaceStopLimitOrderEndpoint;
            else if (type == OrderType.StopMarket)
                endpoint = PlaceStopMarketOrderEndpoint;

            if (immediateOrCancel == true)
            {
                if (type != OrderType.Limit)
                    throw new ArgumentException("ImmediateOrCancel only valid for limit orders");

                endpoint = PlaceImmediateOrCancelOrderEndpoint;
            }

            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
                { "side", (int)side },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("option", orderOption.HasValue ? JsonConvert.SerializeObject(orderOption, new OrderOptionConverter(false)) : null);
            parameters.AddOptionalParameter("stop_price", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("client_id", clientOrderId);
            parameters.AddOptionalParameter("source_id", sourceId);

            var result = await _baseClient.Execute<CoinExNewPosition>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId { SourceObject = result.Data, Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });

            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinExPosition>>> GetOpenPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            symbol?.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>();

            parameters.AddOptionalParameter("market", symbol);

            return await _baseClient.Execute<IEnumerable<CoinExPosition>>(_baseClient.GetUrl(OpenPositionsEndpoint), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// position type 1 Isolated Margin 2 Cross Margin
        public async Task<WebCallResult<CoinexAdjustLeverage>> AdjustLeverageAsync(string symbol,int leverage,int positiontype, CancellationToken ct = default)
        {
            symbol?.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>();

            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("leverage", leverage.ToString());
            parameters.AddOptionalParameter("position_type", positiontype);

            return await _baseClient.Execute<CoinexAdjustLeverage>(_baseClient.GetUrl(AdjustLeverageEndpoint), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// position type 1 Isolated Margin 2 Cross Margin
        public async Task<WebCallResult<CoinExStopLossSettings>> PositionStopLossSettings(string symbol, long position_id, int stop_type,decimal stop_loss_price, CancellationToken ct = default)
        {
            symbol?.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>();

            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("position_id", position_id);
            parameters.AddOptionalParameter("stop_type", stop_type);
            parameters.AddOptionalParameter("stop_loss_price", stop_loss_price.ToString());

            return await _baseClient.Execute<CoinExStopLossSettings>(_baseClient.GetUrl(PositionStopLossSettingsEndpoint), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<CoinExTakeProfitSettings>> PositionTakeProfitSettings(string symbol, long position_id, int stop_type, decimal take_profit_price, CancellationToken ct = default)
        {
            symbol?.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>();

            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("position_id", position_id);
            parameters.AddOptionalParameter("stop_type", stop_type);
            parameters.AddOptionalParameter("take_profit_price", take_profit_price.ToString());

            return await _baseClient.Execute<CoinExTakeProfitSettings>(_baseClient.GetUrl(PositionTakeProfitSettingsEndpoint), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinExMarketList>>> GetMarketListAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<CoinExMarketList>>(_baseClient.GetUrl(MarketListEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPagedResult<CoinExOrder>>> GetOpenStopOrdersAsync(string symbol, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            symbol?.ValidateCoinExSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 100);
            var parameters = new Dictionary<string, object>
            {
                { "page", page ?? 1 },
                { "limit", limit ?? 100 }
            };
            parameters.AddOptionalParameter("market", symbol);
            return await _baseClient.ExecutePaged<CoinExOrder>(_baseClient.GetUrl(OpenStopOrdersEndpoint), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPagedResult<CoinExOrder>>> GetClosedOrdersAsync(string symbol, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateCoinExSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 100);
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
                { "page", page ?? 1 },
                { "limit", limit ?? 100 }
            };

            return await _baseClient.ExecutePaged<CoinExOrder>(_baseClient.GetUrl(FinishedOrdersEndpoint), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExOrder>> GetOrderAsync(string symbol, long orderId, CancellationToken ct = default)
        {
            symbol.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
                { "id", orderId }
            };

            return await _baseClient.Execute<CoinExOrder>(_baseClient.GetUrl(OrderStatusEndpoint), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPagedResult<CoinExOrderTrade>>> GetOrderTradesAsync(long orderId, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 100);
            var parameters = new Dictionary<string, object>
            {
                { "id", orderId },
                { "page", page ?? 1 },
                { "limit", limit ?? 100  }
            };

            return await _baseClient.ExecutePaged<CoinExOrderTrade>(_baseClient.GetUrl(OrderDetailsEndpoint), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPagedResult<CoinExOrderTradeExtended>>> GetUserTradesAsync(string symbol, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateCoinExSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 100);
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
                { "page", page ?? 1 },
                { "limit", limit ?? 100 }
            };

            return await _baseClient.ExecutePaged<CoinExOrderTradeExtended>(_baseClient.GetUrl(UserTransactionsEndpoint), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExOrder>> CancelOrderAsync(string symbol, long orderId, CancellationToken ct = default)
        {
            symbol.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
                { "id", orderId }
            };

            var result = await _baseClient.Execute<CoinExOrder>(_baseClient.GetUrl(CancelOrderEndpoint), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExMarketCloseAll>> MarketCloseAllAsync(string symbol, long positionID, CancellationToken ct = default)
        {
            symbol.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
                { "position_id", positionID }
            };

            var result = await _baseClient.Execute<CoinExMarketCloseAll>(_baseClient.GetUrl(MarketCloseAllEndpoint), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
            };

            return await _baseClient.Execute(_baseClient.GetUrl(CancelOrderEndpoint), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllStopOrdersAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateCoinExSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
            };

            return await _baseClient.Execute(_baseClient.GetUrl(CancelStopOrderEndpoint), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
