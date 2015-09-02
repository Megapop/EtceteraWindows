namespace Megapop.Social
{
    public class WebAuthenticationResult
    {
        /// <summary>
        /// Contains the protocol data when the operation successfully completes.
        /// </summary>
        /// 
        /// <returns>
        /// The returned data.
        /// </returns>
        public string ResponseData {  get; set; }
        /// <summary>
        /// Returns the HTTP error code when ResponseStatus is equal to WebAuthenticationStatus.ErrorHttp. This is only available if there is an error.
        /// </summary>
        /// 
        /// <returns>
        /// The specific HTTP error, for example 400.
        /// </returns>
        public uint ResponseErrorDetail { get; set; }
        /// <summary>
        /// Contains the status of the asynchronous operation when it completes.
        /// </summary>
        /// 
        /// <returns>
        /// One of the enumerations.
        /// </returns>
        public WebAuthenticationStatus ResponseStatus { get; set; }
    }
}