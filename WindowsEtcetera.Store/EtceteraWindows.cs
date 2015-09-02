using System;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Networking.Connectivity;
using Windows.System;
using Windows.UI.Popups;

namespace Megapop
{
    public static class EtceteraWindows
    {

        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            var internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        public async static void AskForReviewNow(string title, string message)
        {
            var md = new MessageDialog(message,title);
            var yes = new UICommand("Yes", (async cmd =>
            {
                var familyName = Package.Current.Id.FamilyName;
                var uri = new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", familyName));
                await LaunchUriAsync(uri);
            }));
            var no = new UICommand("No");
            md.Commands.Add(no);
            md.Commands.Add(yes);

            md.CancelCommandIndex = 0;
            md.DefaultCommandIndex = 1;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => { await md.ShowAsync(); });
        }

        private static async Task LaunchUriAsync(Uri uri)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => { await Launcher.LaunchUriAsync(uri); });
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            return type.GetTypeInfo().GetDeclaredProperty(propertyName);
        }
    }
}
