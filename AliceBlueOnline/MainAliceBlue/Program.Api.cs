using AliceBlueOnlineLibrary;
using AliceBlueOnlineLibrary.DataContract.Enum;
using AliceBlueOnlineLibrary.DataContract.Order;
using AliceBlueOnlineLibrary.DataContract.Order.Enum;
using AliceBlueOnlineLibrary.DataContract.Order.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainAliceBlue
{
    public partial class Program
    {
        public static async Task FillAliceBlueApiData()
        {
            //get profile
            var profile = await _aliceBlueApi.GetProfile().ConfigureAwait(false);
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
                Price = 0.0F,
                TransactionType = OrderSide.BUY,
                TriggerPrice = 0.0F,
                Validity = "DAY",
                Product = ProductType.CNC,
                Source = "web",
                OrderTag = "order1"
            };
            var placeOrderResponse = await _aliceBlueApi.PlaceOrder(placeOrderRequest, Constants.AliceBlue.OrderRoute).ConfigureAwait(false);
            if (placeOrderResponse == null)
            {
                throw new ArgumentNullException(nameof(placeOrderResponse));
            }

            //place amo
            var placeAmoResponse = await _aliceBlueApi.PlaceOrder(placeOrderRequest, Constants.AliceBlue.AmoRoute).ConfigureAwait(false);
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
            var placeBracketResponse = await _aliceBlueApi.PlaceOrder(placeBracketRequest, Constants.AliceBlue.BracketOrderRoute).ConfigureAwait(false);
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
            var placeBasketResponse = await _aliceBlueApi.PlaceBasketOrder(placeBasketOrderRequest).ConfigureAwait(false);
            if (placeBasketResponse == null)
            {
                throw new ArgumentNullException(nameof(placeBasketResponse));
            }

            //modify order
            var modifyOrderRequest = new ModifyOrderRequest
            {
                OmsOrderId = "210320000000049",
                Exchange = Exchange.NSE,
                OrderType = OrderType.MARKET,
                InstrumentToken = 11460,
                Quantity = 2,
                DisclosedQuantity = 0,
                Price = 13.6F,
                TransactionType = OrderSide.BUY,
                TriggerPrice = 0.0F,
                Validity = "DAY",
                Product = ProductType.CNC,
                NestRequestId = "1"
            };

            var modifyOrderResponse = await _aliceBlueApi.ModifyOrder(modifyOrderRequest).ConfigureAwait(false);
            if (modifyOrderResponse == null)
            {
                throw new ArgumentNullException(nameof(modifyOrderResponse));
            }

            //cancel order
            var cancelOrderResponse = await _aliceBlueApi.CancelOrder("210320000000055", "open").ConfigureAwait(false);
            if (cancelOrderResponse == null)
            {
                throw new ArgumentNullException(nameof(cancelOrderResponse));
            }

            //order history
            var orderHistoryResponse = await _aliceBlueApi.GetOrderHistory("210320000001901").ConfigureAwait(false);
            if (orderHistoryResponse == null)
            {
                throw new ArgumentNullException(nameof(orderHistoryResponse));
            }

            //order history with tag
            var orderHistoryResponseWithTag = await _aliceBlueApi.GetOrderHistoryWithTag("order1").ConfigureAwait(false);
            if (orderHistoryResponseWithTag == null)
            {
                throw new ArgumentNullException(nameof(orderHistoryResponseWithTag));
            }

            //script info
            var scriptInfoResponse = await _aliceBlueApi.GetScriptInfo(Exchange.NSE, 11460).ConfigureAwait(false);
            if (scriptInfoResponse == null)
            {
                throw new ArgumentNullException(nameof(scriptInfoResponse));
            }

            //order book
            var orderBookResponse = await _aliceBlueApi.GetOrderBooks().ConfigureAwait(false);
            if (orderBookResponse == null)
            {
                throw new ArgumentNullException(nameof(orderBookResponse));
            }

            //trade
            var tradeResponse = await _aliceBlueApi.GetTrades().ConfigureAwait(false);
            if (tradeResponse == null)
            {
                throw new ArgumentNullException(nameof(tradeResponse));
            }

            //cash positions
            var cashPositionResponse = await _aliceBlueApi.GetCashPositions().ConfigureAwait(false);
            if (cashPositionResponse == null)
            {
                throw new ArgumentNullException(nameof(cashPositionResponse));
            }

            //day wise position
            var dayWisePositionResponse = await _aliceBlueApi.GetPositions(Constants.AliceBlue.DayWisePositionRoute).ConfigureAwait(false);
            if (dayWisePositionResponse == null)
            {
                throw new ArgumentNullException(nameof(dayWisePositionResponse));
            }

            //net wise position
            var netWisePositionResponse = await _aliceBlueApi.GetPositions(Constants.AliceBlue.NetWisePositionRoute).ConfigureAwait(false);
            if (netWisePositionResponse == null)
            {
                throw new ArgumentNullException(nameof(netWisePositionResponse));
            }

            //holdings
            var holdingResponse = await _aliceBlueApi.GetHoldings().ConfigureAwait(false);
            if (holdingResponse == null)
            {
                throw new ArgumentNullException(nameof(holdingResponse));
            }
        }
    }
}
