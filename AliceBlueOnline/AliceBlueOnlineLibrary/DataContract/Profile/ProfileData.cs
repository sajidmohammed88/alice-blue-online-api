using Newtonsoft.Json;
using System.Collections.Generic;

namespace AliceBlueOnlineLibrary.DataContract.Profile
{
    public class ProfileData
    {
        [JsonProperty("pan_number")]
        public string PanNumber { get; set; }

        public string Name { get; set; }

        [JsonProperty("login_id")]
        public string LoginId { get; set; }

        public List<string> Exchanges { get; set; }

        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("dp_ids")]
        public List<string> DpIds { get; set; }

        [JsonProperty("broker_name")]
        public string BrokerName { get; set; }

        public List<Bank> Banks { get; set; }

        [JsonProperty("backoffice_enabled")]
        public bool BackOfficeEnabled { get; set; }

    }
}
