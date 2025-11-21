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
    public class ContestService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed = false;

        public ContestService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiConfig.BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ContestService));
            }
        }

        public async Task<string> UploadDocumentAsync(string filePath, string accessToken)
        {
            CheckDisposed();
            try
            {
                Console.WriteLine($"[UploadDocumentAsync] Starting upload for file: {filePath}");
                Console.WriteLine($"[UploadDocumentAsync] Endpoint: {ApiConfig.UploadDocumentEndpoint}");
                
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(GetContentType(filePath));
                    content.Add(fileContent, "document", Path.GetFileName(filePath));

                    Console.WriteLine($"[UploadDocumentAsync] Content-Type: {GetContentType(filePath)}");
                    Console.WriteLine($"[UploadDocumentAsync] File name: {Path.GetFileName(filePath)}");

                    var response = await _httpClient.PostAsync(ApiConfig.UploadDocumentEndpoint, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"[UploadDocumentAsync] Response status: {response.StatusCode}");
                    Console.WriteLine($"[UploadDocumentAsync] Response body: {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        return result?.data?.content?.ToString() ?? "";
                    }
                    else
                    {
                        throw new Exception($"Lỗi upload file: {response.StatusCode} - {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UploadDocumentAsync] Exception: {ex.Message}");
                Console.WriteLine($"[UploadDocumentAsync] StackTrace: {ex.StackTrace}");
                throw new Exception($"Lỗi khi upload tài liệu: {ex.Message}");
            }
        }

        public async Task<GenerateQuestionsResponse> GenerateQuestionsAsync(
            GenerateQuestionsRequest request, 
            string accessToken)
        {
            CheckDisposed();
            try
            {
                Console.WriteLine("[ContestService] GenerateQuestionsAsync - Start");
                Console.WriteLine($"[ContestService] Request - DocumentContent length: {request.DocumentContent?.Length ?? 0}");
                Console.WriteLine($"[ContestService] Request - AiModel: {request.AiModel}");
                Console.WriteLine($"[ContestService] Request - NumMultipleChoice: {request.NumMultipleChoice}");
                Console.WriteLine($"[ContestService] Request - NumEssay: {request.NumEssay}");
                Console.WriteLine($"[ContestService] Request - ContestId: {request.ContestId}");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var json = JsonConvert.SerializeObject(request);
                Console.WriteLine($"[ContestService] Request JSON: {json}");
                
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = ApiConfig.GenerateQuestionBankEndpoint;
                Console.WriteLine($"[ContestService] Calling URL: {url}");

                var response = await _httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"[ContestService] Response Status: {response.StatusCode}");
                Console.WriteLine($"[ContestService] Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<GenerateQuestionsResponse>(responseContent);
                    Console.WriteLine($"[ContestService] Deserialization Success: {result?.Success}");
                    return result;
                }
                else
                {
                    Console.WriteLine($"[ContestService] Request failed with status: {response.StatusCode}");
                    return new GenerateQuestionsResponse
                    {
                        Success = false,
                        Message = $"Lỗi: {response.StatusCode} - {responseContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ContestService] Exception: {ex.Message}");
                Console.WriteLine($"[ContestService] StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[ContestService] InnerException: {ex.InnerException.Message}");
                }
                return new GenerateQuestionsResponse
                {
                    Success = false,
                    Message = $"Lỗi khi tạo câu hỏi: {ex.Message}"
                };
            }
        }

        public async Task<bool> AddQuestionsToContestAsync(
            string contestId,
            MultipleChoiceQuestionModel[] multipleChoiceQuestions,
            EssayQuestionModel[] essayQuestions,
            string accessToken)
        {
            CheckDisposed();
            try
            {
                Console.WriteLine("[ContestService] AddQuestionsToContestAsync - Start");
                Console.WriteLine($"[ContestService] ContestId: {contestId}");
                Console.WriteLine($"[ContestService] Multiple Choice Questions: {multipleChoiceQuestions?.Length ?? 0}");
                Console.WriteLine($"[ContestService] Essay Questions: {essayQuestions?.Length ?? 0}");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var request = new
                {
                    contestId = contestId,
                    multipleChoiceQuestions = multipleChoiceQuestions,
                    essayQuestions = essayQuestions
                };

                var json = JsonConvert.SerializeObject(request);
                Console.WriteLine($"[ContestService] Request JSON length: {json.Length}");

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var url = ApiConfig.AddQuestionsToContestEndpoint;
                Console.WriteLine($"[ContestService] Calling URL: {url}");

                var response = await _httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"[ContestService] Response Status: {response.StatusCode}");
                Console.WriteLine($"[ContestService] Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("[ContestService] Questions added successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine($"[ContestService] Failed: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ContestService] Exception: {ex.Message}");
                Console.WriteLine($"[ContestService] Stack trace: {ex.StackTrace}");
                throw new Exception($"Lỗi khi thêm câu hỏi vào contest: {ex.Message}");
            }
        }

        public async Task<QuestionsListResponse> GetQuestionsByContestAsync(
            string contestId, 
            string accessToken)
        {
            CheckDisposed();
            try
            {
                Console.WriteLine("\n========== [GetQuestionsByContestAsync] BẮT ĐẦU ==========\n");
                Console.WriteLine($"[GetQuestionsByContestAsync] 📋 ContestId: {contestId}");
                Console.WriteLine($"[GetQuestionsByContestAsync] 🔑 Token: {accessToken?.Substring(0, 20)}...");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var url = ApiConfig.GetFullUrl($"/api/contest/questions/{contestId}");
                Console.WriteLine($"[GetQuestionsByContestAsync] 🌐 URL: {url}");

                var response = await _httpClient.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"[GetQuestionsByContestAsync] 📡 Response Status: {response.StatusCode}");
                Console.WriteLine($"[GetQuestionsByContestAsync] 📦 Response Content Length: {responseContent?.Length ?? 0}");
                Console.WriteLine($"[GetQuestionsByContestAsync] 📄 Response Content:\n{responseContent}\n");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[GetQuestionsByContestAsync] ✓ Response successful, deserializing...");
                    var result = JsonConvert.DeserializeObject<QuestionsListResponse>(responseContent);
                    
                    Console.WriteLine($"[GetQuestionsByContestAsync] 📊 Deserialized result:");
                    Console.WriteLine($"   - Success: {result?.Success}");
                    Console.WriteLine($"   - Message: {result?.Message}");
                    Console.WriteLine($"   - Data null?: {result?.Data == null}");
                    if (result?.Data != null)
                    {
                        Console.WriteLine($"   - MultipleChoice count: {result.Data.MultipleChoice?.Count ?? 0}");
                        Console.WriteLine($"   - Essay count: {result.Data.Essay?.Count ?? 0}");
                        Console.WriteLine($"   - Total: {result.Data.Total}");
                    }
                    
                    Console.WriteLine("\n========== [GetQuestionsByContestAsync] KẾT THÚC - SUCCESS ==========\n");
                    return result;
                }
                else
                {
                    Console.WriteLine($"[GetQuestionsByContestAsync] ❌ Response FAILED: {response.StatusCode}");
                    Console.WriteLine("\n========== [GetQuestionsByContestAsync] KẾT THÚC - FAILED ==========\n");
                    return new QuestionsListResponse
                    {
                        Success = false,
                        Message = $"Lỗi: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[GetQuestionsByContestAsync] ❌❌❌ EXCEPTION ❌❌❌");
                Console.WriteLine($"   Type: {ex.GetType().Name}");
                Console.WriteLine($"   Message: {ex.Message}");
                Console.WriteLine($"   StackTrace:\n{ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"   InnerException: {ex.InnerException.Message}");
                }
                Console.WriteLine("\n========================================\n");
                
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
                    ApiConfig.GetFullUrl($"/api/multiple-choice-question/{questionId}"),
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
                    ApiConfig.GetFullUrl($"/api/essay-question/{questionId}"),
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
                    ApiConfig.GetFullUrl($"/api/multiple-choice-question/{questionId}"));

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
                    ApiConfig.GetFullUrl($"/api/essay-question/{questionId}"));

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
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var queryParams = new List<string>();
                if (!string.IsNullOrEmpty(status))
                    queryParams.Add($"status={status}");
                if (!string.IsNullOrEmpty(visibility))
                    queryParams.Add($"visibility={visibility}");

                var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var url = ApiConfig.GetFullUrl($"/api/contest{query}");

                var response = await _httpClient.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    var contestsJson = result.data.ToString();
                                        
                    var contests = JsonConvert.DeserializeObject<List<ContestModel>>(contestsJson);
                                        
                    return contests ?? new List<ContestModel>();
                }
                else
                {
                    return new List<ContestModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ContestService] GetAllContestsAsync - Exception: {ex.Message}");
                Console.WriteLine($"[ContestService] GetAllContestsAsync - StackTrace: {ex.StackTrace}");
                
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

        public async Task<bool> UpdateContestCountQuestionsAsync(
            string contestId,
            int countMultipleChoice,
            int countEssay,
            string accessToken)
        {
            try
            {
                Console.WriteLine($"[ContestService] UpdateContestCountQuestionsAsync - ContestId: {contestId}");
                Console.WriteLine($"[ContestService] UpdateContestCountQuestionsAsync - MC: {countMultipleChoice}, Essay: {countEssay}");
                
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var request = new
                {
                    countMultipleChoiceQuestions = countMultipleChoice,
                    countEssayQuestions = countEssay
                };

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(
                    ApiConfig.GetFullUrl($"/api/contest/{contestId}"),
                    content);

                Console.WriteLine($"[ContestService] UpdateContestCountQuestionsAsync - Status: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ContestService] UpdateContestCountQuestionsAsync - Exception: {ex.Message}");
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
