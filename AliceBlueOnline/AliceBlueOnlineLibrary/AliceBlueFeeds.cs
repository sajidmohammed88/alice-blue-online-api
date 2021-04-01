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
        private readonly Dictionary<string, IList<Instrument>> _masterContractsDict = new Dictionary<string, IList<Instrument>>();
        private List<string> _exchangeToDownload;
        private List<MarketStatus> _marketStatusList;
        private List<ExchangeMessage> _exchangeMessages;

        public AliceBlueFeeds(IAliceBlueApi aliceBlueApi, string accessToken, List<string> exchangeToDownload)
        {
            _aliceBlueApi = aliceBlueApi;
            _socketEndpoint = $"wss://ant.aliceblueonline.com/hydrasocket/v2/websocket?access_token={accessToken}";
            _exchangeToDownload = exchangeToDownload;
            _subscriptionModes = new Dictionary<LiveFeedType, string>
            {
                {LiveFeedType.Compact, "compact_marketdata"},
                {LiveFeedType.MarketData, "marketdata"},
                {LiveFeedType.SnapQuote, "snapquote"},
                {LiveFeedType.FullSnapQuote, "full_snapquote"}
            };
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
            SendData(JsonConvert.SerializeObject(new { a = "subscribe", v = new[] { 1, 2, 3, 4, 6 }, m = "market_status" }));
        }

        /// <inheritdoc />
        public void SubscribeExchangeMessages()
        {
            SendData(JsonConvert.SerializeObject(new { a = "subscribe", v = new[] { 1, 2, 3, 4, 6 }, m = "exchange_messages" }));
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
            SendData(JsonConvert.SerializeObject(new { a = action, v = arrayOfExchangeAndToken, m = _subscriptionModes[liveFeedType] }));
        }

        private void SendData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                Console.WriteLine("message to send is empty.");
            }

            _webSocket.Send(data);
            if (!_messageReceiveEvent.WaitOne(5000))
            {
                Console.WriteLine("Cannot receive the response. Timeout.");
            }
        }

        private void WebsocketDataReceived(object sender, DataReceivedEventArgs e)
        {
            byte[] data = e.Data;

            switch (data[0])
            {
                case (byte)WsFrameMode.MarketData:
                    {
                        MarketData marketData = MarketData.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.CompactMarketData:
                    {
                        CompactData compactData = CompactData.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.SnapQuote:
                    {
                        SnapQuote snapQuote = SnapQuote.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.FullSnapQuote:
                    {
                        FullSnapQuote fullSnapQuote = FullSnapQuote.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.DPR:
                    {
                        Dpr dpr = Dpr.Deserialize(data.Skip(1).ToArray());
                        break;
                    }
                case (byte)WsFrameMode.OI:
                    {
                        OpenInterest openInterest = OpenInterest.Deserialize(data.Skip(1).ToArray());
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
