using AliceBlueOnlineLibrary.TokenGenerator;
using AliceBlueOnlineLibrary.TokenGenerator.Cache;
using AliceBlueOnlineLibrary.TokenGenerator.Request;
using System.Threading.Tasks;

namespace MainAliceBlue
{
    public class Program
    {
        private static string _accessToken;

        public static async Task Main(string[] args)
        {
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
        }
    }
}
