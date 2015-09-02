using System;
using System.Windows.Threading;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using GoogleAds;

namespace Megapop.Admob
{
    public static class Admob
    {
        private static InterstitialAd interstitialAd;
        private static string interstitialAdUnit;
        private static InterstitialAd videoAd;
        private static string videoAdUnit;

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
            interstitialAdUnit = adUnitId;
            interstitialAd = new InterstitialAd(adUnitId);
            interstitialAd.DismissingOverlay += (o, a) => InterstitialAdOnDismissingOverlay(o, a, interstitialAdUnit);
            interstitialAd.FailedToReceiveAd += (o, a) => InterstitialAdOnFailedToReceiveAd(o, a, interstitialAdUnit);
            interstitialAd.LeavingApplication += (o, a) => InterstitialAdOnLeavingApplication(o, a, interstitialAdUnit);
            interstitialAd.ReceivedAd += (o, a) => InterstitialAdOnReceivedAd(o, a, interstitialAdUnit);
            interstitialAd.ShowingOverlay += (o, a) => InterstitialAdOnShowingOverlay(o, a, interstitialAdUnit);

            var adRequest = new AdRequest { ForceTesting = enableTestMode };
            interstitialAd.LoadAd(adRequest);
        }

        /// <summary>
        /// Shows an interstitial as long as one is loaded
        /// </summary>
        public static void ShowInterstitial()
        {
            EtceteraWindows.RunOnUIThread(() =>
            {
                if (interstitialAd != null)
                {
                    interstitialAd.ShowAd();
                }
            });
        }

        public static void LoadVideo(string adUnitId, bool enableTestMode = false)
        {
            videoAdUnit = adUnitId;
            videoAd = new InterstitialAd(adUnitId);
            videoAd.DismissingOverlay += (o, a) => InterstitialAdOnDismissingOverlay(o, a, videoAdUnit);
            videoAd.FailedToReceiveAd += (o, a) => InterstitialAdOnFailedToReceiveAd(o, a, videoAdUnit);
            videoAd.LeavingApplication += (o, a) => InterstitialAdOnLeavingApplication(o, a, videoAdUnit);
            videoAd.ReceivedAd += (o, a) => InterstitialAdOnReceivedAd(o, a, videoAdUnit);
            videoAd.ShowingOverlay += (o, a) => InterstitialAdOnShowingOverlay(o, a, videoAdUnit);

            var adRequest = new AdRequest { ForceTesting = enableTestMode };
            interstitialAd.LoadAd(adRequest);
        }

        public static void ShowVideo()
        {
            EtceteraWindows.RunOnUIThread(() =>
            {
                if (videoAd != null)
                {
                    videoAd.ShowAd();
                }
            });
        }

        private static void InterstitialAdOnShowingOverlay(object sender, AdEventArgs adEventArgs, string id)
        {
            if (ShowOverlay != null)
            {
                ShowOverlay(id);
            }
        }

        private static void InterstitialAdOnReceivedAd(object sender, AdEventArgs adEventArgs, string id)
        {
            if (ReceivedAd != null)
            {
                ReceivedAd(id);
            }
        }

        private static void InterstitialAdOnLeavingApplication(object sender, AdEventArgs adEventArgs, string id)
        {
            if (LeavingApplication != null)
            {
                LeavingApplication(id);
            }
        }

        private static void InterstitialAdOnFailedToReceiveAd(object sender, AdErrorEventArgs adErrorEventArgs, string id)
        {
            if (FailedToReceiveAd != null)
            {
                var code = (AdErrorCode)adErrorEventArgs.ErrorCode;
                FailedToReceiveAd(id, code);
            }
            
        }

        private static void InterstitialAdOnDismissingOverlay(object sender, AdEventArgs adEventArgs, string id)
        {
            if (DismissingAd != null)
            {
                DismissingAd(id);
            }
        }
    }
}
