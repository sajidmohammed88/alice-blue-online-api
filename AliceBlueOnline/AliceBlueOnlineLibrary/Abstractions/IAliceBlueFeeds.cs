using System.Collections.Generic;
using System.Threading.Tasks;
using AliceBlueOnlineLibrary.DataContract.Contracts;
using AliceBlueOnlineLibrary.DataContract.Feeds;
using AliceBlueOnlineLibrary.DataContract.Feeds.Enum;

namespace AliceBlueOnlineLibrary.Abstractions
{
    public interface IAliceBlueFeeds
    {
        /// <summary>
        /// Initializes the FEEDS.
        /// </summary>
        Task InitializeFeeds();

        /// <summary>
        /// Gets the instrument by symbol.
        /// </summary>
        /// <param name="exchange">The exchange.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns>The instrument.</returns>
        IList<Instrument> GetInstrumentsBySymbol(string exchange, string symbol);

        /// <summary>
        /// Starts the web socket.
        /// </summary>
        void StartWebSocket();

        /// <summary>
        /// Subscribes to a live feed.
        /// </summary>
        /// <param name="instruments">The instruments.</param>
        /// <param name="liveFeedType">Type of the live feed.</param>
        void Subscribe(IList<Instrument> instruments, LiveFeedType liveFeedType);

        /// <summary>
        /// Subscribe the market status messages.
        /// </summary>
        void SubscribeMarketStatusMessages();

        /// <summary>
        /// Subscribe the exchange messages.
        /// </summary>
        void SubscribeExchangeMessages();

        /// <summary>
        /// Gets the market status messages.
        /// </summary>
        /// <returns>The market status messages.</returns>
        IList<MarketStatus> GetMarketStatusMessages();

        /// <summary>
        /// Gets the exchange messages.
        /// </summary>
        /// <returns>The exchange messages.</returns>
        IList<ExchangeMessage> GetExchangeMessages();

        /// <summary>
        /// Unsubscribe to a live feed.
        /// </summary>
        /// <param name="instruments">The instruments.</param>
        /// <param name="liveFeedType">Type of the live feed.</param>
        void Unsubscribe(IList<Instrument> instruments, LiveFeedType liveFeedType);

        /// <summary>
        /// Closes the websocket.
        /// </summary>
        void Close();
    }
}
