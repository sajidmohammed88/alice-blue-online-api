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
        /// Get the instrument by symbol.
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
        /// Unsubscribe to a live feed.
        /// </summary>
        /// <param name="instruments">The instruments.</param>
        /// <param name="liveFeedType">Type of the live feed.</param>
        void Unsubscribe(IList<Instrument> instruments, LiveFeedType liveFeedType);

        /// <summary>
        /// Closes the websocket.
        /// </summary>
        void Close();

        /// <summary>
        /// Get the market status messages.
        /// </summary>
        /// <returns>The market status messages.</returns>
        IList<MarketStatus> GetMarketStatusMessages();

        /// <summary>
        /// Get the exchange messages.
        /// </summary>
        /// <returns>The exchange messages.</returns>
        IList<ExchangeMessage> GetExchangeMessages();

        /// <summary>
        /// Get the market data.
        /// </summary>
        /// <returns>The market data.</returns>
        MarketData GetMarketData();

        /// <summary>
        /// Get the compact data.
        /// </summary>
        /// <returns>The compact data.</returns>
        CompactData GetCompactData();


        /// <summary>
        /// Get the snap quote.
        /// </summary>
        /// <returns>The snap quote.</returns>
        SnapQuote GetSnapQuote();

        /// <summary>
        /// Get the full snap quote.
        /// </summary>
        /// <returns>The full snap quote.</returns>
        FullSnapQuote GetFullSnapQuote();

        /// <summary>
        /// Get the DPR.
        /// </summary>
        /// <returns>The DPR.</returns>
        Dpr GetDpr();

        /// <summary>
        /// Get the open interest.
        /// </summary>
        /// <returns>The open interest.</returns>
        OpenInterest GetOpenInterest();
    }
}
