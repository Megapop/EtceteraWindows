using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.UI.Core;

namespace Megapop.Roaming
{
    public static class RoamingSettings
    {

        private static ApplicationDataContainer settings;

        private static ApplicationDataContainer Settings
        {
            get {

                if (settings == null)
                {
                    if (ApplicationData.Current != null)
                    {
                        settings = ApplicationData.Current.RoamingSettings;
                    }
                }

                return settings;
            }
        }

        static RoamingSettings()
        {
            ApplicationData.Current.DataChanged += CurrentOnDataChanged;
        }

        public static Action<object, Exception> DataChanged;

        private static async void CurrentOnDataChanged(ApplicationData sender, object args)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (DataChanged != null)
                    {
                        DataChanged(args, null);
                    }
                });
            }
            catch (Exception ex)
            {
                if (DataChanged != null)
                {
                    DataChanged(null, ex);
                }
            }
        }

        public static void SetString(string key, string data)
        {
            if (Settings != null)
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
        }

        public static string GetString(string key, string defaultData = null)
        {
            if (Settings != null)
            {
                if (Settings.Values.ContainsKey(key))
                {
                    return Settings.Values[key] as string;
                }
            }
            return defaultData;
        }

        public static async void GetWindowsUser(Action<WindowsUser, Exception> onComplete)
        {
            try
            {
                if (onComplete != null)
                {
                    var user = new WindowsUser();

                    user.DisplayName = await UserInformation.GetDisplayNameAsync();
                    user.FirstName = await UserInformation.GetFirstNameAsync();
                    user.LastName = await UserInformation.GetLastNameAsync();

                    var image = UserInformation.GetAccountPicture(AccountPictureKind.SmallImage) as StorageFile;
                    if (image != null)
                    {
                        IRandomAccessStream myMemoryStream = await image.OpenReadAsync();
                        if (myMemoryStream != null)
                        {
                            var bytes = new byte[myMemoryStream.Size];

                            await myMemoryStream.ReadAsync(bytes.AsBuffer(), (uint)myMemoryStream.Size, InputStreamOptions.None);

                            user.AccountPicture = bytes;
                        }
                    
                    }
                
                    onComplete(user, null);
                }
            }
            catch (Exception ex)
            {
                if (onComplete != null)
                {
                    onComplete(null, ex);
                }
            }
        }

        
    }
}
