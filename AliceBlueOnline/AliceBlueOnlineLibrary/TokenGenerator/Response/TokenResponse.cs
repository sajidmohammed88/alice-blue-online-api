using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.TokenGenerator.Response
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpirationDelay { get; set; }
    }
}
