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
using System;
using System.Threading.Tasks;

namespace AliceBlueOnlineLibrary.Abstractions
{
    public interface IAliceBlueApi : IDisposable
    {
        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <returns>The profile information.</returns>
        Task<ProfileResponse> GetProfile();

        /// <summary>
        /// Places the order.
        /// </summary>
        /// <param name="placeRequest">The place request.</param>
        /// <param name="orderRoute">The order route.</param>
        /// <returns>The placed order.</returns>
        Task<PlaceOrderResponse> PlaceOrder(PlaceRequestBase placeRequest, string orderRoute);

        /// <summary>
        /// Places the basket order.
        /// </summary>
        /// <param name="placeBasketOrderRequest">The place basket order request.</param>
        /// <returns>The placed basket order.</returns>
        Task<PlaceOrderResponse> PlaceBasketOrder(PlaceBasketOrderRequest placeBasketOrderRequest);

        /// <summary>
        /// Modifies the order.
        /// </summary>
        /// <param name="modifyOrderRequest">The modify order request.</param>
        /// <returns>The modified order response.</returns>
        Task<ModifyOrderResponse> ModifyOrder(ModifyOrderRequest modifyOrderRequest);

        /// <summary>
        /// Cancels the order.
        /// </summary>
        /// <param name="omsOrderId">The oms order identifier.</param>
        /// <param name="orderStatus">The order status.</param>
        /// <returns>The canceled order response.</returns>
        Task<CancelOrderResponse> CancelOrder(string omsOrderId, string orderStatus);

        /// <summary>
        /// Gets the order history.
        /// </summary>
        /// <param name="omsOrderId">The oms order identifier.</param>
        /// <returns>The order history.</returns>
        Task<OrderHistoryResponse> GetOrderHistory(string omsOrderId);

        /// <summary>
        /// Gets the order history with tag.
        /// </summary>
        /// <param name="orderTag">The order tag.</param>
        /// <returns>The order history.</returns>
        Task<OrderHistoryResponse> GetOrderHistoryWithTag(string orderTag);

        /// <summary>
        /// Gets the script information.
        /// </summary>
        /// <param name="exchange">The exchange.</param>
        /// <param name="instrumentToken">The instrument token.</param>
        /// <returns>The script information.</returns>
        Task<ScriptInfoResponse> GetScriptInfo(Exchange exchange, int instrumentToken);

        /// <summary>
        /// Gets the order books.
        /// </summary>
        /// <returns>The order book list.</returns>
        Task<OrderBookResponse> GetOrderBooks();

        /// <summary>
        /// Gets the trades.
        /// </summary>
        /// <returns>The trade list.</returns>
        Task<TradeResponse> GetTrades();

        /// <summary>
        /// Gets the cash positions.
        /// </summary>
        /// <returns>The cash positions.</returns>
        Task<CashPositionResponse> GetCashPositions();

        /// <summary>
        /// Gets the daywise or netwise positions.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The daywise or netwise positions.</returns>
        Task<PositionResponse> GetPositions(string url);

        /// <summary>
        /// Gets the holdings.
        /// </summary>
        /// <returns>The holding list.</returns>
        Task<HoldingResponse> GetHoldings();
    }
}
