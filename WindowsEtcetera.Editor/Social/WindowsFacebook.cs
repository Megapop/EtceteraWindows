using System;

namespace Megapop.Social
{
    public class WindowsFacebook
    {
        public static string AppId;
        public static DateTime TokenExpiry { get; set; }

        public static string AccessToken { get; set; }

        public static void Login(string[] permissions, Action<WebAuthenticationResult, Exception> result)
        {
        }

    }
}
