using AppTracNghiem.Models;

namespace AppTracNghiem.Helpers
{
    public static class TokenManager
    {
        private static TokenData? _currentToken;
        private static UserModel? _currentUser;

        public static void SaveToken(TokenData token, UserModel user)
        {
            _currentToken = token;
            _currentUser = user;
        }

        public static string? GetAccessToken()
        {
            return _currentToken?.AccessToken;
        }

        public static string? GetRefreshToken()
        {
            return _currentToken?.RefreshToken;
        }

        public static TokenData? GetTokenData()
        {
            return _currentToken;
        }

        public static UserModel? GetCurrentUser()
        {
            return _currentUser;
        }

        public static bool IsTokenExpired()
        {
            if (_currentToken == null || string.IsNullOrEmpty(_currentToken.ExpiresAt))
                return true;

            if (DateTime.TryParse(_currentToken.ExpiresAt, out DateTime expiresAt))
            {
                return DateTime.Now >= expiresAt;
            }

            return true;
        }

        public static bool IsAuthenticated()
        {
            return _currentToken != null && 
                   !string.IsNullOrEmpty(_currentToken.AccessToken) && 
                   !IsTokenExpired();
        }

        public static void ClearToken()
        {
            _currentToken = null;
            _currentUser = null;
        }
        
        public static void UpdateToken(TokenData newToken)
        {
            if (_currentToken != null)
            {
                _currentToken = newToken;
            }
        }
    }
}
