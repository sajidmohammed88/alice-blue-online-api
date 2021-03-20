using AliceBlueOnlineLibrary.Abstractions;
using AliceBlueOnlineLibrary.DataContract.CashPositions;
using AliceBlueOnlineLibrary.DataContract.Enum;
using AliceBlueOnlineLibrary.DataContract.Holdings;
using AliceBlueOnlineLibrary.DataContract.Order;
using AliceBlueOnlineLibrary.DataContract.Order.Request;
using AliceBlueOnlineLibrary.DataContract.Order.Response;
using AliceBlueOnlineLibrary.DataContract.Positions;
using AliceBlueOnlineLibrary.DataContract.Profile;
using AliceBlueOnlineLibrary.DataContract.ScriptInfo;
using AliceBlueOnlineLibrary.DataContract.Trade;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AliceBlueOnlineLibrary
{
    public class AliceBlueApi : IAliceBlueApi
    {
        private bool _disposedValue;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AliceBlueApi"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public AliceBlueApi(string accessToken)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) };
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.ConnectionClose = false;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");

            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
        }

        /// <inheritdoc />
        public async Task<ProfileResponse> GetProfile()
        {
            var responseMessage = await _httpClient.GetAsync(Constants.AliceBlue.ProfileRoute).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to Retrieve profile");
            }

            return JsonConvert.DeserializeObject<ProfileResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<PlaceOrderResponse> PlaceOrder(PlaceRequestBase placeRequest, string orderRoute)
        {
            var responseMessage = await _httpClient.PostAsync(
                orderRoute,
                new StringContent(JsonConvert.SerializeObject(placeRequest, placeRequest.GetType(), _settings), Encoding.UTF8, "application/json")).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to place order");
            }

            return JsonConvert.DeserializeObject<PlaceOrderResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<PlaceOrderResponse> PlaceBasketOrder(PlaceBasketOrderRequest placeBasketOrderRequest)
        {
            var responseMessage = await _httpClient.PostAsync(
                Constants.AliceBlue.BasketOrderRoute,
                new StringContent(JsonConvert.SerializeObject(placeBasketOrderRequest, _settings), Encoding.UTF8, "application/json")).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to place basket order");
            }

            return JsonConvert.DeserializeObject<PlaceOrderResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<ModifyOrderResponse> ModifyOrder(ModifyOrderRequest modifyOrderRequest)
        {
            var responseMessage = await _httpClient.PutAsync(
                Constants.AliceBlue.OrderRoute,
                new StringContent(JsonConvert.SerializeObject(modifyOrderRequest, _settings), Encoding.UTF8, "application/json")).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to modify order");
            }

            return JsonConvert.DeserializeObject<ModifyOrderResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<CancelOrderResponse> CancelOrder(string omsOrderId, string orderStatus)
        {
            var responseMessage = await _httpClient.DeleteAsync($"{Constants.AliceBlue.OrderRoute}?oms_order_id={omsOrderId}&order_status={orderStatus}").ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to cancel order");
            }

            return JsonConvert.DeserializeObject<CancelOrderResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<OrderHistoryResponse> GetOrderHistory(string omsOrderId)
        {
            return await GetOrderHistoryByUri($"{Constants.AliceBlue.OrderRoute}/{omsOrderId}").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<OrderHistoryResponse> GetOrderHistoryWithTag(string orderTag)
        {
            return await GetOrderHistoryByUri($"{Constants.AliceBlue.OrderRoute}/tag/{orderTag}").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ScriptInfoResponse> GetScriptInfo(Exchange exchange, int instrumentToken)
        {
            var responseMessage =
                await _httpClient.GetAsync(string.Format(Constants.AliceBlue.ScriptInfoRoute, exchange, instrumentToken)).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to Retrieve script info");
            }

            return JsonConvert.DeserializeObject<ScriptInfoResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<OrderBookResponse> GetOrderBooks()
        {
            var responseMessage = await _httpClient.GetAsync(Constants.AliceBlue.OrderRoute).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to Retrieve order book");
            }

            return JsonConvert.DeserializeObject<OrderBookResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<TradeResponse> GetTrades()
        {
            var responseMessage = await _httpClient.GetAsync(Constants.AliceBlue.TradeRoute).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to Retrieve trades");
            }

            return JsonConvert.DeserializeObject<TradeResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<CashPositionResponse> GetCashPositions()
        {
            var responseMessage = await _httpClient.GetAsync(Constants.AliceBlue.CashPositionRoute).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to Retrieve cash positions");
            }

            return JsonConvert.DeserializeObject<CashPositionResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<PositionResponse> GetPositions(string url)
        {
            var responseMessage = await _httpClient.GetAsync(url).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to Retrieve net wise or day wise positions");
            }

            return JsonConvert.DeserializeObject<PositionResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        /// <inheritdoc />
        public async Task<HoldingResponse> GetHoldings()
        {
            var responseMessage = await _httpClient.GetAsync(Constants.AliceBlue.HoldingsRoute).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to Retrieve holdings");
            }

            return JsonConvert.DeserializeObject<HoldingResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        private async Task<OrderHistoryResponse> GetOrderHistoryByUri(string uri)
        {
            var responseMessage = await _httpClient.GetAsync(uri).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Not able to Retrieve order history");
            }

            return JsonConvert.DeserializeObject<OrderHistoryResponse>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), _settings);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
