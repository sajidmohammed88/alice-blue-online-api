namespace AliceBlueOnlineLibrary
{
    public static class Constants
    {
        internal const string BaseUrl = "https://ant.aliceblueonline.com";

        internal static class Token
        {
            internal const string AuthorizationRoute = "/oauth2/auth";
            internal const string AccessTokenRoute = "/oauth2/token";
        }

        public static class AliceBlue
        {
            internal const string ProfileRoute = "/api/v2/profile";
            public const string OrderRoute = "/api/v2/order";
            public const string AmoRoute = "/api/v2/amo";
            public const string BracketOrderRoute = "/api/v2/bracketorder";
            internal const string BasketOrderRoute = "/api/v2/basketorder";
            internal const string ScriptInfoRoute = "/api/v2/scripinfo?exchange={0}&instrument_token={1}";
            internal const string TradeRoute = "/api/v2/trade";
            internal const string CashPositionRoute = "/api/v2/cashposition";
            public const string NetWisePositionRoute = "/api/v2/positions?type=netwise";
            public const string DayWisePositionRoute = "/api/v2/positions?type=daywise";
            internal const string HoldingsRoute = "/api/v2/holdings";
        }
    }
}
