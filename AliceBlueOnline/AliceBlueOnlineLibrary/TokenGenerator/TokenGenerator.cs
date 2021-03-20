using AliceBlueOnlineLibrary.DataContract.Token.Request;
using AliceBlueOnlineLibrary.DataContract.Token.Response;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AliceBlueOnlineLibrary.TokenGenerator
{
    /// <summary>
    /// The token generator class.
    /// </summary>
    public class TokenGenerator
    {
        /// <summary>
        /// The login and access token retrieval.
        /// </summary>
        /// <param name="tokenRequest">The token request.</param>
        /// <returns>The token response.</returns>
        public static async Task<TokenResponse> LoginAndGetAccessTokenAsync(TokenRequest tokenRequest)
        {
            if (tokenRequest == null)
            {
                return null;
            }

            using (var httpClient = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                var csrTokenAndLoginChallenge = await GetCsrTokenAngLoginChallenge(httpClient, tokenRequest).ConfigureAwait(false);
                var loginContent = await GetLoginContent(httpClient, tokenRequest, csrTokenAndLoginChallenge).ConfigureAwait(false);
                var (code, url) = await GetAuthorizationCode(httpClient, tokenRequest, (csrTokenAndLoginChallenge.Item1, csrTokenAndLoginChallenge.Item2), loginContent).ConfigureAwait(false);

                //get access token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                    $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{tokenRequest.AppId}:{tokenRequest.ApiSecret}"))}");

                var result = await httpClient.PostAsync(
                    $"{Constants.Token.AccessTokenRoute}?client_id={tokenRequest.AppId}&client_secret={tokenRequest.ApiSecret}&grant_type=authorization_code&code={code}" +
                        $"&redirect_uri={tokenRequest.RedirectUrl}&authorization_response={url}",
                    new StringContent(
                        $"code={code}&redirect_uri={tokenRequest.RedirectUrl}&grant_type=authorization_code&client_secret={tokenRequest.ApiSecret}&cliend_id={tokenRequest.UserName}",
                        Encoding.UTF8,
                        "application/x-www-form-urlencoded"
                    ));

                if (!result.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(@"Couldn't get access token");
                }

                return JsonConvert.DeserializeObject<TokenResponse>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        /// <summary>
        /// Get the csrToken and login challenge.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="tokenRequest">The token request</param>
        /// <returns>Typle of csr token and login challenge.</returns>
        private static async Task<(string, string, Uri)> GetCsrTokenAngLoginChallenge(HttpClient httpClient, TokenRequest tokenRequest)
        {
            var authResult = await httpClient
                    .GetAsync($"{Constants.Token.AuthorizationRoute}?response_type=code&state=test_state&client_id={tokenRequest.AppId}&redirect_uri={tokenRequest.RedirectUrl}")
                    .ConfigureAwait(false);

            var authContent = await authResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (authContent.Contains("OAuth 2.0 Error"))
            {
                throw new HttpRequestException("OAuth 2.0 Error occurred. Please verify your api_secret");
            }

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(authContent);

            var csrfToken = htmlDocument.DocumentNode.SelectSingleNode("//input[@name='_csrf_token']")?.Attributes["value"]?.Value;
            var loginChallenge = htmlDocument.DocumentNode.SelectSingleNode("//input[@name='login_challenge']")?.Attributes["value"]?.Value;

            return (csrfToken, loginChallenge, authResult.RequestMessage.RequestUri);
        }

        /// <summary>
        /// Get the login page content.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="tokenRequest">The token request.</param>
        /// <param name="csrTokenAndLoginChallenge">The csr token and login challenge.</param>
        /// <returns>The loin page content.</returns>
        private static async Task<(string, Uri)> GetLoginContent(HttpClient httpClient, TokenRequest tokenRequest, (string, string, Uri) csrTokenAndLoginChallenge)
        {
            var loginResult = await httpClient.PostAsync(csrTokenAndLoginChallenge.Item3,
                    new StringContent($"client_id={tokenRequest.UserName}&password={tokenRequest.Password}&login_challenge={csrTokenAndLoginChallenge.Item2}&_csrf_token={csrTokenAndLoginChallenge.Item1}",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded")
                    ).ConfigureAwait(false);

            var loginContent = await loginResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (loginContent.Contains("Please Enter Valid Password"))
            {
                throw new HttpRequestException("Please enter a valid password");
            }

            if (loginContent.Contains("Not able to Retrieve ValidPwd"))
            {
                throw new HttpRequestException("Not able to Retrieve valid password");
            }

            if (loginContent.Contains("Internal server error"))
            {
                throw new HttpRequestException("Got Internal server error, please try again after sometimes");
            }

            return (loginContent, loginResult.RequestMessage.RequestUri);
        }

        /// <summary>
        /// Get the authorization code.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="tokenRequest">The token request.</param>
        /// <param name="csrTokenAndLoginChallenge">The Typle of csr token and login challenge</param>
        /// <param name="loginContent">The login page content.</param>
        /// <returns>The authorization code.</returns>
        private static async Task<(string, string)> GetAuthorizationCode(HttpClient httpClient, TokenRequest tokenRequest, (string, string) csrTokenAndLoginChallenge, (string, Uri) loginContent)
        {
            var twoFaContent = new StringBuilder();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(loginContent.Item1);

            var index = 1;
            foreach (var item in htmlDocument.DocumentNode.SelectNodes("//input[@name='question_id1']"))
            {
                var value = item.Attributes["value"]?.Value;
                if (value == null)
                {
                    continue;
                }

                twoFaContent.Append($"answer{index++}={tokenRequest.TwoFa}&question_id1={value}&");
            }

            twoFaContent.Append($"login_challenge={csrTokenAndLoginChallenge.Item2}&_csrf_token={csrTokenAndLoginChallenge.Item1}");

            var twoFaResult = await httpClient.PostAsync(loginContent.Item2,
                        new StringContent(twoFaContent.ToString(),
                        Encoding.UTF8,
                        "application/x-www-form-urlencoded")
                        ).ConfigureAwait(false);
            var result = await twoFaResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (result.Contains("Internal server error"))
            {
                throw new HttpRequestException(@"Getting 'Internal server error' while authorizing the app for the first time.");
            }

            var query = twoFaResult.RequestMessage.RequestUri.Query;
            var indexOfEquals = query.IndexOf("=", StringComparison.Ordinal);
            var code = query.Substring(indexOfEquals + 1, query.IndexOf("&", StringComparison.Ordinal) - indexOfEquals - 1);

            return (code, twoFaResult.RequestMessage.RequestUri.AbsoluteUri);
        }
    }
}