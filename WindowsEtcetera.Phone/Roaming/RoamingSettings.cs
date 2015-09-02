using System;
using Windows.Storage;

namespace Megapop.Roaming
{
    public static class RoamingSettings
    {
        private static ApplicationDataContainer settings;

        private static ApplicationDataContainer Settings
        {
            get { return settings ?? (settings = ApplicationData.Current.RoamingSettings); }
        }
        public static void GetWindowsUser(Action<WindowsUser, Exception> onComplete)
        {
        }

        public static Action<object, Exception> DataChanged;

        public static void SetString(string key, string data)
        {
            if (Settings.Values.ContainsKey(key))
            {
                Settings.Values[key] = data;
            }
            else
            {
                Settings.Values.Add(key, data);
            }
        }

        public static string GetString(string key, string defaultData = null)
        {
            if (Settings.Values.ContainsKey(key))
            {
                return Settings.Values[key] as string;
            }
            return defaultData;
        }
    }
}
