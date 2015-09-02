using System;
using System.Collections.Generic;
using FlurryWP8SDK;
using FlurryWP8SDK.Models;

namespace Megapop
{
    public static class Flurry
    {
        private const int MaxVariablesLength = 10;

        public enum Gender
        {
            Unknown = -1,
            Female = 0,
            Male = 1,
        }

        public static void StartSession(string apiKey)
        {
            Api.StartSession(apiKey);
        }

        public static void EndSession()
        {
            Api.EndSession();
        }

        /// <summary>
        /// Use LogError to report application errors.
        /// Flurry will report the last 10 errors to occur in each session. 
        /// (max length 255 chars) 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="exception"></param>
        public static void LogError(string eventName, Exception exception)
        {
            Api.LogError(eventName, exception);
        }

        public static void LogPageView()
        {
            Api.LogPageView();
        }
        public static void SetAge(int age)
        {
            Api.SetAge(age);
        }
        public static void SetGender(Gender gender)
        {
            Api.SetGender((FlurryWP8SDK.Models.Gender)gender);
        }
        public static void SetLocation(double latitude, double longitude, float accuracy)
        {
            Api.SetLocation(latitude, longitude, accuracy);
        }
        public static void SetReportUrl(string serverURL)
        {
            Api.SetReportUrl(serverURL);
        }
        public static void SetSecureTransportEnabled()
        {
            Api.SetSecureTransportEnabled();
        }

        /// <summary>
        /// Pass a value to change the number of seconds for which paused sessions will be continued. 
        /// After this amount of time has passed with no activity, 
        /// a new session is assumed to have started. 
        /// </summary>
        /// <param name="seconds"></param>
        public static void SetSessionContinueSeconds(int seconds)
        {
            Api.SetSessionContinueSeconds(seconds);
        }
        public static void SetUserId(string userId)
        {
            Api.SetUserId(userId);
        }

        /// <summary>
        /// To change the version name your analytic data is reported under. 
        /// If this is not specified, the version name is retrieved from the application descriptor.
        /// </summary>
        /// <param name="version"></param>
        public static void SetVersion(string version)
        {
            Api.SetVersion(version);
        }

        /// <summary>
        /// Use LogEvent to track user events that happen during a session.
        /// You can track how many times each event occurs, what order events happen in, as well as what the most common parameters are for each event.
        /// This can be useful for measuring how often users take various actions, or what sequences of actions they usually perform.
        /// Each project supports a maximum of 100 events.
        /// The timed argument and the parameters argument are both optional.
        /// Each event id, parameter key, and parameter value must be no more than 255 characters in length.
        /// Each event can have no more than 10 parameters.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="param"></param>
        public static void LogEvent(string eventName, Dictionary<string, string> param)
        {
            Log(eventName, false, param);
        }

        /// <summary>
        /// Use LogEvent to track user events that happen during a session.
        /// You can track how many times each event occurs, what order events happen in, as well as what the most common parameters are for each event.
        /// This can be useful for measuring how often users take various actions, or what sequences of actions they usually perform.
        /// Each project supports a maximum of 100 events.
        /// The timed argument and the parameters argument are both optional.
        /// Each event id, parameter key, and parameter value must be no more than 255 characters in length.
        /// Each event can have no more than 10 parameters.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <param name="paramName2"></param>
        /// <param name="paramValue2"></param>
        /// <param name="paramName3"></param>
        /// <param name="paramValue3"></param>
        /// <param name="paramName4"></param>
        /// <param name="paramValue4"></param>
        public static void LogEvent(string eventName, string paramName = null, string paramValue = null, string paramName2 = null, string paramValue2 = null, string paramName3 = null, string paramValue3 = null, string paramName4 = null, string paramValue4 = null)
        {
            Log(eventName, false, paramName, paramValue, paramName2, paramValue2, paramName3, paramValue3, paramName4, paramValue4);
        }

        public static void LogTimedEvent(string eventName, Dictionary<string, string> param)
        {
            Log(eventName, true, param);
        }

        public static void LogTimedEvent(string eventName, string paramName = null, string paramValue = null, string paramName2 = null, string paramValue2 = null, string paramName3 = null, string paramValue3 = null, string paramName4 = null, string paramValue4 = null)
        {
            Log(eventName, true, paramName, paramValue, paramName2, paramValue2, paramName3, paramValue3, paramName4, paramValue4);
        }

        private static void Log(string eventName, bool timed, Dictionary<string, string> param)
        {
            var parameters = new List<Parameter>(param.Count);

            var count = 0;
            foreach (var par in param)
            {
                if (!string.IsNullOrEmpty(par.Key))
                {
                    parameters.Add(new Parameter(par.Key, par.Value));
                }
                count++;

                if (count > MaxVariablesLength)
                {
                    break;
                }
            }

            Api.LogEvent(eventName, parameters, timed);
        }

        private static void Log(string eventName, bool timed, string paramName = null, string paramValue = null, string paramName2 = null, string paramValue2 = null, string paramName3 = null, string paramValue3 = null, string paramName4 = null, string paramValue4 = null)
        {
            var parameters = new List<Parameter>(4);

            if (!string.IsNullOrEmpty(paramName))
            {
                parameters.Add(new Parameter(paramName, paramValue));
            }
            if (!string.IsNullOrEmpty(paramName2))
            {
                parameters.Add(new Parameter(paramName2, paramValue2));
            }
            if (!string.IsNullOrEmpty(paramName3))
            {
                parameters.Add(new Parameter(paramName3, paramValue3));
            }
            if (!string.IsNullOrEmpty(paramName4))
            {
                parameters.Add(new Parameter(paramName4, paramValue4));
            }

            Api.LogEvent(eventName, parameters, timed);
        }

        public static void EndTimedEvent(string eventName)
        {
            Api.EndTimedEvent(eventName);
        }

        public static void EndTimedEvent(string eventName, Dictionary<string, string> param)
        {
            var parameters = new List<Parameter>(param.Count);

            var count = 0;

            foreach (var par in param)
            {
                if (!string.IsNullOrEmpty(par.Key))
                {
                    parameters.Add(new Parameter(par.Key, par.Value));
                }
                count++;

                if (count > MaxVariablesLength)
                {
                    break;
                }
            }

            Api.EndTimedEvent(eventName, parameters);
        }
    }
}
