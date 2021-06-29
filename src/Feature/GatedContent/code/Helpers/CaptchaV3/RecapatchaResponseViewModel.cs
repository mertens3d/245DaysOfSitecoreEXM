using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Feature.GatedContent.Helpers.CaptchaV3
{
    public class RecapatchaResponseViewModel
    {
        public string Action { get; set; }
        [JsonProperty(PropertyName = "challeng_ts")]
        public DateTime ChallengeTime { get; set; }

        [JsonProperty(PropertyName = "error-codes")]
        public IEnumerable<string> ErrorCodes { get; set; }

        public string HostName { get; set; }
        public double Score { get; set; }
        public bool Success { get; set; }
    }
}
