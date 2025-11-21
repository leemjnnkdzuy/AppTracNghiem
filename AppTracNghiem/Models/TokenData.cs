using Newtonsoft.Json;

namespace AppTracNghiem.Models
{
    public class TokenData
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonProperty("expiresAt")]
        public string ExpiresAt { get; set; } = string.Empty;
    }
}
