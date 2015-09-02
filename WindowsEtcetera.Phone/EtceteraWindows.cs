using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Threading;
using Windows.Networking.Connectivity;

namespace Megapop
{
    public static class EtceteraWindows
    {
        public static Dispatcher Dispatcher;
        private static Action<Action> thread;

        public static void Initialize(Dispatcher unityDispatcher, Action<Action> unityThread)
        {
            Dispatcher = unityDispatcher;
            thread = unityThread;
        }

        public static void RunOnUnityThread(Action action)
        {
            Debug.WriteLine("invoking method on Unity thread");
            try
            {
                if (thread != null)
                {
                    thread(action);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("exception running Action on Unity thread: " + (object)ex);
            }
        }
        public static void RunOnUIThread(Action action)
        {
            Debug.WriteLine("invoking method on Unity UI thread");

            Dispatcher.BeginInvoke(action);
        }

        public static void AskForReviewNow(string title, string message)
        {}

        public static bool IsInternet()
        {
            var networkInformation = NetworkInformation.GetConnectionProfiles();
            return networkInformation.Count >= 0;
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            return type.GetTypeInfo().GetDeclaredProperty(propertyName);
        }
    }
}
