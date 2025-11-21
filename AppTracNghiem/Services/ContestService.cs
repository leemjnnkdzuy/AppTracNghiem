using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AppTracNghiem.Config;
using AppTracNghiem.Models;
using AppTracNghiem.Helpers;

namespace AppTracNghiem.Services
{
    public class ContestService
    {
        private readonly HttpClient _httpClient;

        public ContestService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiConfig.BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> UploadDocumentAsync(string filePath, string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(GetContentType(filePath));
                    content.Add(fileContent, "document", Path.GetFileName(filePath));

                    var response = await _httpClient.PostAsync(ApiConfig.UploadDocumentEndpoint, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        return result?.data?.content?.ToString() ?? "";
                    }
                    else
                    {
                        throw new Exception($"Lỗi upload file: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi upload tài liệu: {ex.Message}");
            }
        }

        public async Task<GenerateQuestionsResponse> GenerateQuestionsAsync(
            GenerateQuestionsRequest request, 
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiConfig.GenerateQuestionsEndpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<GenerateQuestionsResponse>(responseContent);
                }
                else
                {
                    return new GenerateQuestionsResponse
                    {
                        Success = false,
                        Message = $"Lỗi: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenerateQuestionsResponse
                {
                    Success = false,
                    Message = $"Lỗi khi tạo câu hỏi: {ex.Message}"
                };
            }
        }

        public async Task<QuestionsListResponse> GetQuestionsByContestAsync(
            string contestId, 
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(
                    ApiConfig.GetFullUrl($"/api/contest/questions/{contestId}"));
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<QuestionsListResponse>(responseContent);
                }
                else
                {
                    return new QuestionsListResponse
                    {
                        Success = false,
                        Message = $"Lỗi: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new QuestionsListResponse
                {
                    Success = false,
                    Message = $"Lỗi khi lấy danh sách câu hỏi: {ex.Message}"
                };
            }
        }

        public async Task<bool> UpdateMultipleChoiceQuestionAsync(
            string questionId,
            MultipleChoiceQuestionModel question,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var json = JsonConvert.SerializeObject(question);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(
                    ApiConfig.GetFullUrl($"/api/contest/multiple-choice/{questionId}"),
                    content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateEssayQuestionAsync(
            string questionId,
            EssayQuestionModel question,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var json = JsonConvert.SerializeObject(question);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(
                    ApiConfig.GetFullUrl($"/api/contest/essay/{questionId}"),
                    content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMultipleChoiceQuestionAsync(
            string questionId,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.DeleteAsync(
                    ApiConfig.GetFullUrl($"/api/contest/multiple-choice/{questionId}"));

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteEssayQuestionAsync(
            string questionId,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.DeleteAsync(
                    ApiConfig.GetFullUrl($"/api/contest/essay/{questionId}"));

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private string GetContentType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };
        }

        public async Task<UserModel> SearchUserByUsernameAsync(string username, string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(
                    ApiConfig.GetFullUrl($"/api/user/search/{username}"));
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        
                        if (result?.data?.user != null)
                        {
                            var userJson = result.data.user.ToString();
                            var user = JsonConvert.DeserializeObject<UserModel>(userJson);
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddMemberToContestAsync(
            string contestId, 
            string userId, 
            string studentId, 
            string fullName, 
            string email,
            string className,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var data = new
                {
                    userId = userId,
                    studentId = studentId,
                    fullName = fullName,
                    email = email,
                    class_name = className
                };

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(
                    ApiConfig.GetFullUrl($"/api/contest/{contestId}/members"),
                    content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveMemberFromContestAsync(
            string contestId,
            string userId,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.DeleteAsync(
                    ApiConfig.GetFullUrl($"/api/contest/{contestId}/members/{userId}"));

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<dynamic>> GetContestMembersAsync(
            string contestId,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(
                    ApiConfig.GetFullUrl($"/api/contest/{contestId}/members"));
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return JsonConvert.DeserializeObject<List<dynamic>>(result.data.ToString());
                }
                else
                {
                    return new List<dynamic>();
                }
            }
            catch
            {
                return new List<dynamic>();
            }
        }

        public async Task<CreateContestResponse> CreateContestAsync(
            CreateContestRequest request,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(
                    ApiConfig.GetFullUrl("/api/contest"),
                    content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<CreateContestResponse>(responseContent);
                }
                else
                {
                    return new CreateContestResponse
                    {
                        Success = false,
                        Message = $"Lỗi: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new CreateContestResponse
                {
                    Success = false,
                    Message = $"Lỗi khi tạo đề thi: {ex.Message}"
                };
            }
        }

        public async Task<bool> UpdateContestAsync(
            string contestId,
            UpdateContestRequest request,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(
                    ApiConfig.GetFullUrl($"/api/contest/{contestId}"),
                    content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ContestModel> GetContestByIdAsync(
            string contestId,
            string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(
                    ApiConfig.GetFullUrl($"/api/contest/{contestId}"));
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    var contestJson = result.data.ToString();
                    return JsonConvert.DeserializeObject<ContestModel>(contestJson);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ContestModel>> GetAllContestsAsync(
            string accessToken,
            string status = null,
            string visibility = null)
        {
            try
            {
                Console.WriteLine("[ContestService] GetAllContestsAsync - Start");
                
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var queryParams = new List<string>();
                if (!string.IsNullOrEmpty(status))
                    queryParams.Add($"status={status}");
                if (!string.IsNullOrEmpty(visibility))
                    queryParams.Add($"visibility={visibility}");

                var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var url = ApiConfig.GetFullUrl($"/api/contest{query}");
                
                Console.WriteLine($"[ContestService] GetAllContestsAsync - URL: {url}");
                
                var response = await _httpClient.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"[ContestService] GetAllContestsAsync - Status: {response.StatusCode}");
                Console.WriteLine($"[ContestService] GetAllContestsAsync - Response: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    var contestsJson = result.data.ToString();
                    
                    Console.WriteLine($"[ContestService] GetAllContestsAsync - Contests JSON: {contestsJson}");
                    
                    var contests = JsonConvert.DeserializeObject<List<ContestModel>>(contestsJson);
                    
                    Console.WriteLine($"[ContestService] GetAllContestsAsync - Deserialized {contests?.Count ?? 0} contests");
                    
                    return contests ?? new List<ContestModel>();
                }
                else
                {
                    Console.WriteLine($"[ContestService] GetAllContestsAsync - Failed with status {response.StatusCode}");
                    return new List<ContestModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ContestService] GetAllContestsAsync - Exception: {ex.Message}");
                Console.WriteLine($"[ContestService] GetAllContestsAsync - StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[ContestService] GetAllContestsAsync - InnerException: {ex.InnerException.Message}");
                }
                return new List<ContestModel>();
            }
        }

        public async Task<bool> DeleteContestAsync(
            string contestId,
            string accessToken)
        {
            try
            {
                Console.WriteLine($"[ContestService] DeleteContestAsync - ContestId: {contestId}");
                
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.DeleteAsync(
                    ApiConfig.GetFullUrl($"/api/contest/{contestId}"));

                Console.WriteLine($"[ContestService] DeleteContestAsync - Status: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ContestService] DeleteContestAsync - Exception: {ex.Message}");
                return false;
            }
        }
    }
}
