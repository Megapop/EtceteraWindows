using System;

namespace Megapop.HockeyApp
{
    public static class HockeyApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId">are not optional</param>
        /// <param name="appVersion">are not optional</param>
        /// <param name="userId">optional submitting an logged on username and contactinfo</param>
        /// <param name="contactInformation">optional submitting an logged on username and contactinfo</param>
        /// <param name="descriptionLoader">is a lambda for submitting additional information like an event log</param>
        /// <param name="apiBase">is https://rink.hockeyapp.net/api/2/ and can be overwritten</param>
        /// <param name="userAgentString"></param>
        public static void Configure(string appId, string appVersion, string userId = null, string contactInformation = null, Func<Exception, string> descriptionLoader = null, string apiBase = "https://rink.hockeyapp.net", string userAgentString = null)
        {
        }

        /// <summary>
        /// The user can be asked, if the crashed should be sent using
        /// </summary>
        public static bool IsCrashesAvailable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The user can be asked, if the crashed should be sent using
        /// </summary>
        public static int CrashesAvailableCount
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Crashes are deleted by SendCrashesNowAsync() when they are submitted to HockeyApp
        /// </summary>
        /// <param name="done"></param>
        public static void SendCrashes(Action done)
        {
            if (done != null)
            {
                done();
            }
        }

        /// <summary>
        /// Posts a new feedback
        /// </summary>
        /// <param name="message"></param>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="name"></param>
        /// <param name="done"></param>
        public static void PostFeedback(string message, string email = null, string subject = null, string name = null, Action done = null)
        {
            if (done != null)
            {
                done();
            }
        }
    }
}
