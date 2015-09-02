using System;

namespace Megapop.Admob
{
    public static class Admob
    {
        /// <summary>
        /// Raised after an interstitial/overlay is dismissed.
        /// 
        /// </summary>
        public static Action<string, AdErrorCode> FailedToReceiveAd;

        /// <summary>
        /// Raised in case an ad could not be loaded successfully due to either of the following
        ///             reasons:
        ///             - There was a network issue.
        ///             - The request made was determined to be invalid.
        ///             - Successful ad request, but no ad returned due to lack of inventory.
        ///             - Something went wrong internally.
        /// 
        /// </summary>
        public static Action<string> DismissingAd;

        /// <summary>
        /// Raised when the action taken by the user caused an external app to open.
        /// 
        /// </summary>
        public static Action<string> LeavingApplication;


        /// <summary>
        /// Raised when an ad is successfully received.
        /// 
        /// </summary>
        public static Action<string> ReceivedAd;

        /// <summary>
        /// Raised before an interstitial/overlay is shown.
        /// 
        /// </summary>
        public static Action<string> ShowOverlay;

        /// <summary>
        /// Sends out a request to load an interstitial. Note that testMode doesn't yet work in the AdMob SDK.
        /// </summary>
        /// <param name="adUnitId"></param>
        /// <param name="enableTestMode"></param>
        public static void LoadInterstitial(string adUnitId, bool enableTestMode = false)
        {
            if (FailedToReceiveAd != null)
            {
                FailedToReceiveAd(adUnitId, AdErrorCode.InvalidRequest);
            }
        }

        /// <summary>
        /// Shows an interstitial as long as one is loaded
        /// </summary>
        public static void ShowInterstitial()
        {
        }

        public static void LoadVideo(string adUnitId, bool enableTestMode = false)
        {
            if (FailedToReceiveAd != null)
            {
                FailedToReceiveAd(adUnitId, AdErrorCode.InvalidRequest);
            }
        }

        public static void ShowVideo()
        {
        }

    }
}
