using System;

namespace Megapop.Roaming
{
    public static class RoamingSettings
    {
        public static Action<object, Exception> DataChanged;

        public static void GetWindowsUser(Action<WindowsUser, Exception> onComplete)
        {
        }
        public static void SetString(string key, string data)
        {        }

        public static string GetString(string key, string defaultData = null)
        {
            return defaultData;
        }
    }
}
