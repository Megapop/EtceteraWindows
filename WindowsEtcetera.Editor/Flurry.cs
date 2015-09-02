using System;
using System.Collections.Generic;

namespace Megapop
{
    public static class Flurry
    {
        public enum Gender
        {
            Unknown = -1,
            Female = 0,
            Male = 1,
        }

        public static void StartSession(string apiKey)
        {}
        public static void EndSession()
        {}
        public static void LogError(string eventName, Exception exception)
        {}
        public static void LogPageView()
        {}
        public static void SetAge(int age)
        {}
        public static void SetGender(Gender gender)
        {}
        public static void SetLocation(double latitude, double longitude, float accuracy)
        {}
        public static void SetReportUrl(string serverURL)
        {}
        public static void SetSecureTransportEnabled()
        {}
        public static void SetSessionContinueSeconds(int seconds)
        {}
        public static void SetUserId(string userId)
        {}
        public static void SetVersion(string version)
        {}
        public static void LogEvent(string eventName, Dictionary<string, string> param)
        {}
        public static void LogEvent(string eventName, string paramName = null, string paramValue = null, string paramName2 = null, string paramValue2 = null, string paramName3 = null, string paramValue3 = null, string paramName4 = null, string paramValue4 = null)
        {}
        public static void LogTimedEvent(string eventName, Dictionary<string, string> param)
        {}
        public static void LogTimedEvent(string eventName, string paramName = null, string paramValue = null, string paramName2 = null, string paramValue2 = null, string paramName3 = null, string paramValue3 = null, string paramName4 = null, string paramValue4 = null)
        {}
        public static void EndTimedEvent(string eventName)
        {}
        public static void EndTimedEvent(string eventName, Dictionary<string, string> param)
        {}
    }
}
