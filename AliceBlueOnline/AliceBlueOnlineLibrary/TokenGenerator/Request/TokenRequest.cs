namespace AliceBlueOnlineLibrary.TokenGenerator.Request
{
    public class TokenRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string AppId { get; set; }

        public string ApiSecret { get; set; }

        public string RedirectUrl { get; set; }

        public string TwoFa { get; set; }
    }
}
