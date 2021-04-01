using AliceBlueOnlineLibrary;
using AliceBlueOnlineLibrary.Abstractions;
using AliceBlueOnlineLibrary.DataContract.Contracts;
using AliceBlueOnlineLibrary.DataContract.Feeds.Enum;
using AliceBlueOnlineLibrary.DataContract.Token.Request;
using AliceBlueOnlineLibrary.DataContract.Token.Response;
using AliceBlueOnlineLibrary.TokenGenerator;
using AliceBlueOnlineLibrary.TokenGenerator.Cache;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AliceBlueOnlineLibrary.DataContract.Feeds;

namespace MainAliceBlue
{
    public partial class Program
    {
        private static IAliceBlueApi _aliceBlueApi;
        private static IAliceBlueFeeds _aliceBlueFeeds;

        public static async Task Main(string[] args)
        {
            #region Token
            ITokenMemoryCache<TokenResponse> memoryCache = new TokenMemoryCache<TokenResponse>();
            TokenResponse tokenResponse = await memoryCache
                .GetTokenFromCache("Token_",
                    () => TokenGenerator.LoginAndGetAccessTokenAsync(new TokenRequest
                    {
                        UserName = "257471",
                        Password = "vijay@123",
                        AppId = "t5YPgcOvGG",
                        ApiSecret = "1JJkodxap2B3ceTru3N6DTlq3iH0SrcDHp0Tigk84D9B8ziOCpLv7kj8kJANjsCY",
                        RedirectUrl = "https://ant.aliceblueonline.com/plugin/callback/",
                        TwoFa = "a"
                    }))
                .ConfigureAwait(false);
            #endregion

            #region AliceBlueApi
            _aliceBlueApi = new AliceBlueApi(tokenResponse.AccessToken);
            // await FillAliceBlueApiData().ConfigureAwait(false);
            #endregion

            #region Feeds
            _aliceBlueFeeds = new AliceBlueFeeds(_aliceBlueApi, tokenResponse.AccessToken, new List<string> { "NSE", "BSE" });

            await _aliceBlueFeeds.InitializeFeeds().ConfigureAwait(false);
            IList<Instrument> nseInstruments = _aliceBlueFeeds.GetInstrumentsBySymbol("NSE", "TATASTEEL");
            //IList<Instrument> bseInstruments = _aliceBlueFeeds.GetInstrumentsBySymbol("BSE", "ALLCAP");

            _aliceBlueFeeds.StartWebSocket();
            _aliceBlueFeeds.Subscribe(nseInstruments, LiveFeedType.MarketData);
            _aliceBlueFeeds.Subscribe(nseInstruments, LiveFeedType.Compact);
            _aliceBlueFeeds.Subscribe(nseInstruments, LiveFeedType.SnapQuote);
            _aliceBlueFeeds.Subscribe(nseInstruments, LiveFeedType.FullSnapQuote);

            //_aliceBlueFeeds.Unsubscribe(nseInstruments, LiveFeedType.Compact);

            _aliceBlueFeeds.SubscribeExchangeMessages();
            _aliceBlueFeeds.SubscribeMarketStatusMessages();

            Thread.Sleep(20000);


            MarketData marketData = _aliceBlueFeeds.GetMarketData();
            CompactData compactData = _aliceBlueFeeds.GetCompactData();
            SnapQuote snapQuote = _aliceBlueFeeds.GetSnapQuote();
            FullSnapQuote fullSnapQuote = _aliceBlueFeeds.GetFullSnapQuote();
            Dpr dpr = _aliceBlueFeeds.GetDpr();
            OpenInterest openInterest = _aliceBlueFeeds.GetOpenInterest();
            IList<MarketStatus> marketStatusMessages = _aliceBlueFeeds.GetMarketStatusMessages();
            IList<ExchangeMessage> exchangeMessages = _aliceBlueFeeds.GetExchangeMessages();
            
            //_aliceBlueFeeds.Close();
            #endregion
        }
    }
}
