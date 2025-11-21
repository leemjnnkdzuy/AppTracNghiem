using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using AppTracNghiem.Config;
using AppTracNghiem.Models;

namespace AppTracNghiem.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiConfig.BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginResponse?> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Username = username,
                    Password = password
                };

                var json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiConfig.LoginEndpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (responseContent.TrimStart().StartsWith("{") || responseContent.TrimStart().StartsWith("["))
                    {
                        return JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                    }
                    else
                    {
                        return new LoginResponse
                        {
                            Success = false,
                            Message = "Server trả về dữ liệu không đúng định dạng JSON"
                        };
                    }
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return new LoginResponse { Success = false, Message = "Sai mật khẩu hoặc tên đăng nhập!" };
                    }

                    if (responseContent.TrimStart().StartsWith("{"))
                    {
                        var errorResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                        return errorResponse ?? new LoginResponse 
                        { 
                            Success = false, 
                            Message = $"Lỗi: {response.StatusCode}" 
                        };
                    }
                    else
                    {
                        return new LoginResponse
                        {
                            Success = false,
                            Message = $"Lỗi {response.StatusCode}: Server trả về HTML thay vì JSON"
                        };
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = $"Không thể kết nối đến server!\n\nKiểm tra:\n1. Backend đã chạy chưa?\n2. URL trong ApiConfig.cs đúng chưa?\n\nChi tiết: {ex.Message}"
                };
            }
            catch (JsonException ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = $"Server trả về dữ liệu không phải JSON!\n\nCó thể backend chưa chạy hoặc URL sai.\n\nChi tiết: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = $"Lỗi không xác định: {ex.Message}"
                };
            }
        }

		public async Task<UserModel?> GetCurrentUserAsync(string accessToken)
		{
			try
			{
				_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", accessToken);

				var response = await _httpClient.GetAsync(ApiConfig.GetUserEndpoint);
				var responseContent = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var userResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
					return userResponse?.Data?.User;
				}

				return null;
			}
			catch
			{
				return null;
			}
		}
		
        public async Task<TokenData?> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var requestData = new { refreshToken };
                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiConfig.RefreshEndpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                    return tokenResponse?.Data?.Tokens;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> LogoutAsync(string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.PostAsync(ApiConfig.LogoutEndpoint, null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
