using System;
using System.Collections.Generic;

namespace Megapop.IAP
{
    public static class Store
    {
        public static event Action LicenseChangedEvent;

        static Store()
        {}

        private static void OnLicenseChanged()
        {
            if (LicenseChangedEvent != null)
            {
                LicenseChangedEvent();
            }
        }

        /// <summary>
        /// Fetches the license information and returns the isTrial bool
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public static bool IsInTrialMode()
        {
            return false;
        }

        /// <summary>
        /// Fetches the current LicenseInformation. If in testing mode it will be from the app simulator.
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public static LicenseInformation GetLicenseInformation()
        {
            return null;
        }
        /// <summary>
        /// Gets a product license for the given productId. Returns null if no lincese exists for the product.
        /// 
        /// </summary>
        /// <param name="productId"/>
        /// <returns/>
        public static ProductLicense GetProductLicense(string productId)
        {
            return null;
        }

        public static void LoadTest(uint ageRating, string currentMarket, string description, string formattedPrice, string name, Dictionary<string, ProductListing> products)
        {}

        public static void LoadTest(uint ageRating, string currentMarket, string description, string formattedPrice, string name, string xml)
        {}

        /// <summary>
        /// Gets the product receipt for the given productId.
        /// 
        /// </summary>
        /// <param name="productId"/>
        /// <returns/>
        public static void GetProductReceipts(List<string> productIds, Action<List<ProductReceipt>, Exception> onComplete)
        {
            if (onComplete != null)
            {
                onComplete(null, null);
            }
        }


        /// <summary>
        /// Loads listing information and fires completingHandler with the data once loading completes
        /// 
        /// </summary>
        /// <param name="completionHandler"/>
        public static void LoadListingInformation(Action<ListingInformation, Exception> onComplete)
        {
            if (onComplete != null)
            {
                onComplete(null, null);
            }
        }

        /// <summary>
        /// Requests a purchase of the application. completionHandler gets called with either true or false indicating if
        ///             the app was purchased.
        /// 
        /// </summary>
        public static void RequestProductPurchase(string productId, Action<PurchaseResults, Exception> onComplete)
        {
            if (onComplete != null)
            {
                onComplete(null, null);
            }
        }

        /// <summary>
        /// Reports a consumable as fullfilled. You should call this once you have granted the consumable to the player.
        /// 
        /// </summary>
        /// <param name="productId"/><param name="transactionId"/><param name="onComplete"/>
        public static void ReportConsumableFulfillment(string productId, Guid transactionId, Action<FulfillmentResult, Exception> onComplete)
        {
            if (onComplete != null)
            {
                onComplete(FulfillmentResult.ServerError, null);
            }
        }
    }
}
