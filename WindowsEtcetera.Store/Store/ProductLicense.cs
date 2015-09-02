using System;

namespace Megapop.IAP
{
    public class ProductLicense
    {
        /// <summary>
        /// Gets the expiration date and time of the license
        /// 
        /// </summary>
        public DateTimeOffset ExpirationDate { get; set; }

        /// <summary>
        /// Gets the value that indicates whether the feature's license is active
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the ID of an app's in-app offer
        /// 
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Override
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public new string ToString()
        {
            return (string)null;
        }
    }
}