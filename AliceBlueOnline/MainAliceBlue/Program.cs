using System;
using AliceBlueOnlineLibrary;
using AliceBlueOnlineLibrary.DataContract.Enum;
using AliceBlueOnlineLibrary.DataContract.Order;
using AliceBlueOnlineLibrary.DataContract.Order.Enum;
using AliceBlueOnlineLibrary.DataContract.Order.Request;
using AliceBlueOnlineLibrary.DataContract.Token.Request;
using AliceBlueOnlineLibrary.TokenGenerator;
using AliceBlueOnlineLibrary.TokenGenerator.Cache;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainAliceBlue
{
    public class Program
    {
        private static string _accessToken;

        public static async Task Main(string[] args)
        {
            // Token generator
            var tokenMemoryCache = new TokenMemoryCache();
            var tokenRequest = new TokenRequest
            {
                UserName = "257471",
                Password = "vijay@123",
                AppId = "t5YPgcOvGG",
                ApiSecret = "1JJkodxap2B3ceTru3N6DTlq3iH0SrcDHp0Tigk84D9B8ziOCpLv7kj8kJANjsCY",
                RedirectUrl = "https://ant.aliceblueonline.com/plugin/callback/",
                TwoFa = "a"
            };

            _accessToken = await tokenMemoryCache.GetTokenFromCache("Token_",
                () => TokenGenerator.LoginAndGetAccessTokenAsync(tokenRequest)).ConfigureAwait(false);

            //Alice blue API
            var aliceBlueApi = new AliceBlueApi(_accessToken);

            //get profile
            var profile = await aliceBlueApi.GetProfile().ConfigureAwait(false);
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            //place order
            var placeOrderRequest = new PlaceOrderOrAmoRequest
            {
                Exchange = Exchange.NSE,
                OrderType = OrderType.MARKET,
                InstrumentToken = 11460,
                Quantity = 1,
                DisclosedQuantity = 0,
                Price = 10,
                TransactionType = OrderSide.BUY,
                TriggerPrice = 0.0F,
                Validity = "DAY",
                Product = ProductType.CNC,
                Source = "web",
                OrderTag = "order1"
            };
            var placeOrderResponse = await aliceBlueApi.PlaceOrder(placeOrderRequest, Constants.AliceBlue.OrderRoute).ConfigureAwait(false);
            if (placeOrderResponse == null)
            {
                throw new ArgumentNullException(nameof(placeOrderResponse));
            }

            //place amo
            var placeAmoResponse = await aliceBlueApi.PlaceOrder(placeOrderRequest, Constants.AliceBlue.AmoRoute).ConfigureAwait(false);
            if (placeAmoResponse == null)
            {
                throw new ArgumentNullException(nameof(placeAmoResponse));
            }

            //place bracket order
            var placeBracketRequest = new PlaceBracketOrderRequest
            {
                Exchange = Exchange.NFO,
                OrderType = OrderType.LIMIT,
                InstrumentToken = 47308,
                Quantity = 40,
                DisclosedQuantity = 0,
                Price = 27956.50F,
                TransactionType = OrderSide.BUY,
                Validity = "DAY",
                Source = "web",
                OrderTag = "order1",
                SquareOffValue = 28000.50M,
                StopLossValue = 27900.10M,
                TrailingStopLoss = 1
            };
            var placeBracketResponse = await aliceBlueApi.PlaceOrder(placeBracketRequest, Constants.AliceBlue.BracketOrderRoute).ConfigureAwait(false);
            if (placeBracketResponse == null)
            {
                throw new ArgumentNullException(nameof(placeBracketResponse));
            }

            //place basket order
            var placeBasketOrderRequest = new PlaceBasketOrderRequest
            {
                Source = "web",
                Orders = new List<Order>
                {
                    new Order
                    {
                        Exchange = Exchange.NSE,
                        OrderType = OrderType.MARKET,
                        InstrumentToken = 22,
                        Quantity = 6,
                        DisclosedQuantity = 0,
                        Price = 0,
                        TransactionType = OrderSide.BUY,
                        TriggerPrice = 0.0F,
                        Validity = "DAY",
                        Product = ProductType.CNC
                    },
                    new Order
                    {
                        Exchange = Exchange.BSE,
                        OrderType = OrderType.MARKET,
                        InstrumentToken = 500285,
                        Quantity = 3,
                        DisclosedQuantity = 0,
                        Price = 0,
                        TransactionType = OrderSide.BUY,
                        TriggerPrice = 0.0F,
                        Validity = "DAY",
                        Product = ProductType.CNC
                    }
                }
            };
            var placeBasketResponse = await aliceBlueApi.PlaceBasketOrder(placeBasketOrderRequest).ConfigureAwait(false);
            if (placeBasketResponse == null)
            {
                throw new ArgumentNullException(nameof(placeBasketResponse));
            }

            //modify order
            var modifyOrderRequest = new ModifyOrderRequest
            {
                OmsOrderId = "210320000000049",
                Exchange = Exchange.NSE,
                OrderType = OrderType.LIMIT,
                InstrumentToken = 11460,
                Quantity = 2,
                DisclosedQuantity = 0,
                Price = 13.6F,
                TransactionType = OrderSide.BUY,
                TriggerPrice = 0.0F,
                Validity = "DAY",
                Product = ProductType.CNC,
            };

            var modifyOrderResponse = await aliceBlueApi.ModifyOrder(modifyOrderRequest).ConfigureAwait(false);
            if (modifyOrderResponse == null)
            {
                throw new ArgumentNullException(nameof(modifyOrderResponse));
            }

            //cancel order
            var cancelOrderResponse = await aliceBlueApi.CancelOrder("210320000000055", "open").ConfigureAwait(false);
            if (cancelOrderResponse == null)
            {
                throw new ArgumentNullException(nameof(cancelOrderResponse));
            }

            //order history
            var orderHistoryResponse = await aliceBlueApi.GetOrderHistory("210320000001901").ConfigureAwait(false);
            if (orderHistoryResponse == null)
            {
                throw new ArgumentNullException(nameof(orderHistoryResponse));
            }

            //order history with tag
            var orderHistoryResponseWithTag = await aliceBlueApi.GetOrderHistoryWithTag("order1").ConfigureAwait(false);
            if (orderHistoryResponseWithTag == null)
            {
                throw new ArgumentNullException(nameof(orderHistoryResponseWithTag));
            }

            //script info
            var scriptInfoResponse = await aliceBlueApi.GetScriptInfo(Exchange.NSE, 11460).ConfigureAwait(false);
            if (scriptInfoResponse == null)
            {
                throw new ArgumentNullException(nameof(scriptInfoResponse));
            }

            //order book
            var orderBookResponse = await aliceBlueApi.GetOrderBooks().ConfigureAwait(false);
            if (orderBookResponse == null)
            {
                throw new ArgumentNullException(nameof(orderBookResponse));
            }

            //trade
            var tradeResponse = await aliceBlueApi.GetTrades().ConfigureAwait(false);
            if (tradeResponse == null)
            {
                throw new ArgumentNullException(nameof(tradeResponse));
            }

            //cash positions
            var cashPositionResponse = await aliceBlueApi.GetCashPositions().ConfigureAwait(false);
            if (cashPositionResponse == null)
            {
                throw new ArgumentNullException(nameof(cashPositionResponse));
            }

            //day wise position
            var dayWisePositionResponse = await aliceBlueApi.GetPositions(Constants.AliceBlue.DayWisePositionRoute).ConfigureAwait(false);
            if (dayWisePositionResponse == null)
            {
                throw new ArgumentNullException(nameof(dayWisePositionResponse));
            }

            //net wise position
            var netWisePositionResponse = await aliceBlueApi.GetPositions(Constants.AliceBlue.NetWisePositionRoute).ConfigureAwait(false);
            if (netWisePositionResponse == null)
            {
                throw new ArgumentNullException(nameof(netWisePositionResponse));
            }

            //holdings
            var holdingResponse = await aliceBlueApi.GetHoldings().ConfigureAwait(false);
            if (holdingResponse == null)
            {
                throw new ArgumentNullException(nameof(holdingResponse));
            }
        }
    }
}
