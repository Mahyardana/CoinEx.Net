using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Interfaces;
using CoinEx.Net.Objects.Internal;

namespace CoinEx.Net
{
    internal class CoinExAuthenticationProvider : AuthenticationProvider
    {
        private readonly INonceProvider _nonceProvider;

        private readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        public long GetNonce() => _nonceProvider.GetNonce();

        public CoinExAuthenticationProvider(ApiCredentials credentials, INonceProvider? nonceProvider) : base(credentials)
        {
            _nonceProvider = nonceProvider ?? new CoinExNonceProvider();
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            if (apiClient.GetType() == typeof(CoinEx.Net.Clients.FuturesApi.CoinExClientFuturesApi))
            {
                uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
                bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            }
            else
            {
                uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
                bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            }
            headers = new Dictionary<string, string>();

            if (!auth)
                return;

            var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;
            if (apiClient.GetType() == typeof(CoinEx.Net.Clients.FuturesApi.CoinExClientFuturesApi))
            {
                apiClient.requestBodyFormat = RequestBodyFormat.FormData;
                parameters.Add("timestamp", GetMillisecondTimestamp(apiClient));
                var str = (uri.SetParameters(parameters, arraySerialization).Query.Replace("?", "") + "&secret_key=") + Credentials.Secret!.GetString();
                headers.Add("Authorization", SignSHA256(str).ToLower());
                headers.Add("AccessId", Credentials.Key!.GetString());
            }
            else
            {
                apiClient.requestBodyFormat = RequestBodyFormat.Json;
                parameters.Add("tonce", _nonceProvider.GetNonce());
                parameters.Add("access_id", Credentials.Key!.GetString());
                var str = uri.SetParameters(parameters, arraySerialization).Query.Replace("?", "") + "&secret_key=" + Credentials.Secret!.GetString();
                headers.Add("Authorization", SignMD5(str));

            }
        }

        public override string Sign(string toSign)
        {
            return SignMD5(toSign).ToUpper();
        }

        public string SignSHA(string toSign)
        {
            return SignSHA256(toSign).ToLower();
        }
    }
}
