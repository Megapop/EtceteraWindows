using System;
using System.Collections.Generic;

namespace Megapop.IAP
{
    /// <summary>
    /// Provides access to the current app's license metadata
    /// 
    /// </summary>
    public class LicenseInformation
    {

        public event Action LicenseChanged = null;

        /// <summary>
        /// Gets the license expiration date and time relative to the system clock
        /// 
        /// </summary>
        public DateTimeOffset ExpirationDate { get; set; }

        /// <summary>
        /// Gets the value that indicates whether the license is active
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the value that indicates whether the license is a trial license
        /// 
        /// </summary>
        public bool IsTrial { get; set; }

        /// <summary>
        /// Gets the associative list of licenses for the app's features that can be bought through an in-app purchase
        /// 
        /// </summary>
        public Dictionary<string, ProductLicense> ProductLicenses { get; set; }

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