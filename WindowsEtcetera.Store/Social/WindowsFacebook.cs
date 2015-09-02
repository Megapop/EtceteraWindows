using System;
using Windows.ApplicationModel.Core;
using Windows.Security.Authentication.Web;
using Windows.UI.Core;

namespace Megapop.Social
{
    public class WindowsFacebook
    {
        public static string AppId;
        private const string FaceBookAuth = "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope={2}&response_type=token";

        public static DateTime TokenExpiry { get; set; }

        public static string AccessToken { get; set; }

        public static async void Login(string[] permissions, Action<WebAuthenticationResult, Exception> onComplete)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    var scope = string.Join(",", permissions);

                    var facebookRedirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().AbsoluteUri;

                    var requestUri = new Uri(string.Format(FaceBookAuth, AppId, facebookRedirectUri, scope));
                    var callbackUri = new Uri(facebookRedirectUri);

                    var asyncOperation = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, requestUri, callbackUri);
                    if (onComplete != null)
                    {
                        var authResult = new WebAuthenticationResult();
                        if (asyncOperation != null)
                        {
                            authResult.ResponseData = asyncOperation.ResponseData;
                            authResult.ResponseErrorDetail = asyncOperation.ResponseErrorDetail;
                            authResult.ResponseStatus = (WebAuthenticationStatus) asyncOperation.ResponseStatus;
                        }
                        onComplete(authResult, null);
                    }
                });
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
