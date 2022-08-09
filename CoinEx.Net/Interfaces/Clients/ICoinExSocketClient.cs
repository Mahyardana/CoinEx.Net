using CoinEx.Net.Interfaces.Clients.SpotApi;
using CoinEx.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.Interfaces;

namespace CoinEx.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the CoinEx websocket API
    /// </summary>
    public interface ICoinExSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        public ICoinExSocketClientSpotStreams SpotStreams { get; }
        public ICoinExSocketClientFuturesStreams FuturesStreams { get; }
    }
}