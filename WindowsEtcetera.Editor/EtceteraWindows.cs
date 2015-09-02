
using System;
using System.Reflection;

namespace Megapop
{
    public static class EtceteraWindows
    {
        public static void AskForReviewNow(string title, string message)
        { }

        public static bool IsInternet()
        {
            return false;
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            return type.GetProperty(propertyName);
        }
    }
}
