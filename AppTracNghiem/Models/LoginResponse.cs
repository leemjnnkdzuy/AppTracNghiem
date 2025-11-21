using Newtonsoft.Json;

namespace AppTracNghiem.Models
{
    public class LoginResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("data")]
        public LoginData? Data { get; set; }
    }

    public class LoginData
    {
        [JsonProperty("user")]
        public UserModel User { get; set; } = new UserModel();

        [JsonProperty("tokens")]
        public TokenData Tokens { get; set; } = new TokenData();
    }
}
