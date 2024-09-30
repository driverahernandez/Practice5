using Newtonsoft.Json;

namespace Practice5.Test.ApiTests { 
    public class AuthResponse
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
