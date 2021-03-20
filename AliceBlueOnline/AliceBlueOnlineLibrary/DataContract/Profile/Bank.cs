using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Profile
{
    public class Bank
    {
        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }
}