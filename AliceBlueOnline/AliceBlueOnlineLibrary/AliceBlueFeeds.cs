using AliceBlueOnlineLibrary.Abstractions;
using AliceBlueOnlineLibrary.DataContract.Contracts;
using AliceBlueOnlineLibrary.DataContract.Feeds.Enum;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AliceBlueOnlineLibrary.DataContract.Feeds;
using WebSocket4Net;

namespace AliceBlueOnlineLibrary
{
    public class AliceBlueFeeds : IAliceBlueFeeds
    {
        private readonly IAliceBlueApi _aliceBlueApi;
        private readonly string _socketEndpoint;

        private readonly AutoResetEvent _messageReceiveEvent = new AutoResetEvent(false);
        private WebSocket _webSocket;

        private readonly Dictionary<LiveFeedType, string> _subscriptionModes;
        private readonly Dictionary<string, IList<Instrument>> _masterContractsDict;
        private List<string> _exchangeToDownload;
        private List<MarketStatus> _marketStatusList;
        private List<ExchangeMessage> _exchangeMessages;
        private MarketData _marketData;
        private CompactData _compactData;
        private SnapQuote _snapQuote;
        private FullSnapQuote _fullSnapQuote;
        private Dpr _dpr;
        private OpenInterest _openInterest;

        public AliceBlueFeeds(IAliceBlueApi aliceBlueApi, string accessToken, List<string> exchangeToDownload)
        {
            _aliceBlueApi = aliceBlueApi;
            _socketEndpoint = $"wss://ant.aliceblueonline.com/hydrasocket/v2/websocket?access_token={accessToken}";
            _exchangeToDownload = exchangeToDownload;
            _subscriptionModes = Helper.Helper.GetSubscriptionModes();
            _masterContractsDict = new Dictionary<string, IList<Instrument>>();
        }

        /// <inheritdoc />
        public async Task InitializeFeeds()
        {
            if (_exchangeToDownload == null || !_exchangeToDownload.Any())
            {
                _exchangeToDownload = (await _aliceBlueApi.GetProfile().ConfigureAwait(false)).Data.Exchanges;
                if (_exchangeToDownload == null || !_exchangeToDownload.Any())
                {
                    throw new ArgumentException("The user don't have enabled exchanges.");
                }
            }

            foreach (string enabledExchange in _exchangeToDownload.Where(enabledExchange => !_masterContractsDict.ContainsKey(enabledExchange)))
            {
                IList<Instrument> instruments = await _aliceBlueApi.GetInstruments(enabledExchange).ConfigureAwait(false);

                if (instruments != null && instruments.Any())
                {
                    _masterContractsDict.Add(enabledExchange, instruments);
                }
            }
        }

        /// <inheritdoc />
        public IList<Instrument> GetInstrumentsBySymbol(string exchange, string symbol)
        {
            if (!_masterContractsDict.ContainsKey(exchange))
            {
                throw new ArgumentException(
                    $"Cannot find exchange { exchange } in master contract. " +
                    "Please ensure if that exchange is enabled in your profile and downloaded the master contract for the same");
            }

            return _masterContractsDict[exchange]
                .Where(i => i.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <inheritdoc />
        public void StartWebSocket()
        {
            _webSocket = new WebSocket(_socketEndpoint);
            _webSocket.Opened += WebsocketOpened;
            _webSocket.Closed += WebsocketClosed;
            _webSocket.Error += WebsocketError;
            _webSocket.DataReceived += WebsocketDataReceived;

            _webSocket.Open();
            while (_webSocket.State == WebSocketState.Connecting)
            {
            }

            if (_webSocket.State != WebSocketState.Open)
            {
                throw new Exception("Connection is not opened.");
            }
        }

        /// <inheritdoc />
        public void Subscribe(IList<Instrument> instruments, LiveFeedType liveFeedType)
        {
            Send("subscribe", instruments, liveFeedType);
        }

        /// <inheritdoc />
        public void SubscribeMarketStatusMessages()
        {
            SendData(JsonConvert.SerializeObject(new { a = "subscribe", v = new[] { 1, 2, 3, 4, 6 }, m = "market_status" }), "subscribe");
        }

        /// <inheritdoc />
        public void SubscribeExchangeMessages()
        {
            SendData(JsonConvert.SerializeObject(new { a = "subscribe", v = new[] { 1, 2, 3, 4, 6 }, m = "exchange_messages" }), "subscribe");
        }

        /// <inheritdoc />
        public void Unsubscribe(IList<Instrument> instruments, LiveFeedType liveFeedType)
        {
            Send("unsubscribe", instruments, liveFeedType);
        }

        /// <inheritdoc />
        public IList<MarketStatus> GetMarketStatusMessages()
        {
            return _marketStatusList;
        }

        /// <inheritdoc />
        public IList<ExchangeMessage> GetExchangeMessages()
        {
            return _exchangeMessages;
        }

        /// <inheritdoc />
        public MarketData GetMarketData()
        {
            return _marketData;
        }

        /// <inheritdoc />
        public CompactData GetCompactData()
        {
            return _compactData;
        }

        /// <inheritdoc />
        public SnapQuote GetSnapQuote()
        {
            return _snapQuote;
        }

        /// <inheritdoc />
        public FullSnapQuote GetFullSnapQuote()
        {
            return _fullSnapQuote;
        }

        /// <inheritdoc />
        public Dpr GetDpr()
        {
            return _dpr;
        }

        /// <inheritdoc />
        public OpenInterest GetOpenInterest()
        {
            return _openInterest;
        }

        /// <inheritdoc />
        public void Close()
        {
            _webSocket.Close();
        }

        private void Send(string action, IList<Instrument> instruments, LiveFeedType liveFeedType)
        {
            if (instruments == null || !_subscriptionModes.ContainsKey(liveFeedType))
            {
                return;
            }

            int[][] arrayOfExchangeAndToken = instruments
                .Select(instrument => new[] { (int)instrument.Exchange, instrument.Token })
                .ToArray();
            SendData(JsonConvert.SerializeObject(new { a = action, v = arrayOfExchangeAndToken, m = _subscriptionModes[liveFeedType] }), action);
        }

        private void SendData(string data, string action)
        {
            if (string.IsNullOrEmpty(data))
            {
                Console.WriteLine("message to send is empty.");
            }

            _webSocket.Send(data);
            if (!_messageReceiveEvent.WaitOne(5000))
            {
                Console.WriteLine($"Cannot receive the response. Timeout for the action : {action}");
            }
        }

        private void WebsocketDataReceived(object sender, DataReceivedEventArgs e)
        {
            byte[] data = e.Data;

            switch (data[0])
            {
                case (byte)WsFrameMode.MarketData:
                    {
                        _marketData = MarketData.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.CompactMarketData:
                    {
                        _compactData = CompactData.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.SnapQuote:
                    {
                        _snapQuote = SnapQuote.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.FullSnapQuote:
                    {
                        _fullSnapQuote = FullSnapQuote.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.DPR:
                    {
                        _dpr = Dpr.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.OI:
                    {
                        _openInterest = OpenInterest.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.MarketStatus:
                    {
                        MarketStatus marketStatus = MarketStatus.Deserialize(data.Skip(1).ToArray());
                        if (_marketStatusList == null)
                        {
                            _marketStatusList = new List<MarketStatus>();
                        }
                        _marketStatusList.Add(marketStatus);
                        break;
                    }
                case (byte)WsFrameMode.ExchangeMessages:
                    {
                        ExchangeMessage exchangeMessage = ExchangeMessage.Deserialize(data.Skip(1).ToArray());
                        if (_exchangeMessages == null)
                        {
                            _exchangeMessages = new List<ExchangeMessage>();
                        }
                        _exchangeMessages.Add(exchangeMessage);
                        break;
                    }
            }

            _messageReceiveEvent.Set();
        }

        private static void WebsocketError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        private static void WebsocketClosed(object sender, EventArgs e)
        {
            Console.WriteLine("Websocket is closed.");
        }

        private static void WebsocketOpened(object sender, EventArgs e)
        {
            Console.WriteLine("Websocket is opened.");
        }
    }
}
