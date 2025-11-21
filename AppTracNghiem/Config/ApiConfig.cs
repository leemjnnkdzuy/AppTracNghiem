namespace AppTracNghiem.Config
{
    public static class ApiConfig
    {
        public const string BaseUrl = "http://localhost:3001";
        
        // User
        public const string LoginEndpoint = "/api/user/login";
        public const string RefreshEndpoint = "/api/user/refresh";
        public const string LogoutEndpoint = "/api/user/logout";
        public const string GetUserEndpoint = "/api/user/me";

        // Contest
        public const string UploadDocumentEndpoint = "/api/contest/upload-document";
        public const string GenerateQuestionsEndpoint = "/api/contest/generate-questions";
        public const string GetQuestionsByContestEndpoint = "/api/contest/questions";

        public static string GetFullUrl(string endpoint)
        {
            return $"{BaseUrl}{endpoint}";
        }
    }
}
