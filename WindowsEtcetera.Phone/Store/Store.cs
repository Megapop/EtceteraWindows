using System;
using System.Collections.Generic;

#if DEBUG
using MockIAPLib;
using Store = MockIAPLib;
#else
using Windows.ApplicationModel.Store;
#endif

namespace Megapop.IAP
{
    public static class Store
    {
        public static event Action LicenseChangedEvent;
        private static LicenseInformation licenseInformation;

        static Store()
        {
            var information = CurrentApp.LicenseInformation;
            if (information != null)
            {
                information.LicenseChanged += OnLicenseChanged;
                var license = new LicenseInformation();
                license.ExpirationDate = information.ExpirationDate;
                license.IsActive = information.IsActive;
                license.IsTrial = information.IsTrial;
                license.ProductLicenses = new Dictionary<string, ProductLicense>();
            }
        }

        public static void LoadTest(uint ageRating, string currentMarket, string description, string formattedPrice, string name, Dictionary<string,ProductListing> products)
        {
#if DEBUG
            MockIAP.Init();

            MockIAP.RunInMockMode(true);
            MockIAP.SetListingInformation(ageRating, currentMarket, description, formattedPrice, name);

            foreach (var productListing in products)
            {
                var key = productListing.Key;
                var product = productListing.Value;
                // Add some more items manually.
                var p = new MockIAPLib.ProductListing
                {
                    Name = product.Name,
                    ProductId = product.ProductId,
                    ProductType = (Windows.ApplicationModel.Store.ProductType)product.ProductType,
                    Description = product.Description,
                    FormattedPrice = product.FormattedPrice,
                    Tag = string.Empty
                };
                MockIAP.AddProductListing(key, p);
            }
#endif
        }

        public static void LoadTest(uint ageRating, string currentMarket, string description, string formattedPrice, string name, string xml)
        {
#if DEBUG
            MockIAP.Init();

            MockIAP.RunInMockMode(true);
            MockIAP.SetListingInformation(ageRating, currentMarket, description, formattedPrice, name);

            MockIAP.PopulateIAPItemsFromXml(xml);
#endif
        }

        private static void OnLicenseChanged()
        {
            if (LicenseChangedEvent != null)
            {
                EtceteraWindows.RunOnUnityThread(LicenseChangedEvent);
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
            var license = GetLicenseInformation();
            return license != null && license.IsTrial;
        }

        /// <summary>
        /// Fetches the current LicenseInformation. If in testing mode it will be from the app simulator.
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public static LicenseInformation GetLicenseInformation()
        {
            return licenseInformation;
        }
        /// <summary>
        /// Gets a product license for the given productId. Returns null if no lincese exists for the product.
        /// 
        /// </summary>
        /// <param name="productId"/>
        /// <returns/>
        public static ProductLicense GetProductLicense(string productId)
        {
            var license = GetLicenseInformation();
            if (license != null)
            {
                var licenses = license.ProductLicenses;
                if (licenses != null && licenses.ContainsKey(productId))
                {
                    return licenses[productId];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the product receipt for the given productId.
        /// 
        /// </summary>
        /// <param name="productId"/>
        /// <returns/>
        public static async void GetProductReceipts(List<string> productIds, Action<List<ProductReceipt>, Exception> onComplete)
        {
            EtceteraWindows.RunOnUIThread(() => LoadProductReceipts(productIds, onComplete));
        }

        private static async void LoadProductReceipts(List<string> productIds, Action<List<ProductReceipt>, Exception> onComplete)
        {
            var receipts = new List<ProductReceipt>();
            for (int index = 0; index < productIds.Count; index++)
            {
                var productId = productIds[index];

                if (string.IsNullOrEmpty(productId))
                {
                    continue;
                }

                try
                {
#if DEBUG
                    var task = CurrentApp.GetProductReceiptAsync(productId);
#else
                    var task = CurrentApp.GetProductReceiptAsync(productId).AsTask();
#endif
                    await task;

                    var receiptXml = task.Result;

                    var receipt = ProductReceipt.CreateReceipt(receiptXml);

                    receipts.Add(receipt);
                }
                catch (Exception ex)
                {
                    if (onComplete != null)
                    {
                        EtceteraWindows.RunOnUnityThread(() => onComplete(null, ex));
                    }
                    return;
                }
            }

            if (onComplete != null)
            {
                EtceteraWindows.RunOnUnityThread(() =>onComplete(receipts, null));
            }
        }

        /// <summary>
        /// Loads listing information and fires completingHandler with the data once loading completes
        /// 
        /// </summary>
        /// <param name="completionHandler"/>
        public static void LoadListingInformation(Action<ListingInformation, Exception> onComplete)
        {
            EtceteraWindows.RunOnUIThread(() => LoadListing(onComplete));
        }

        private static async void LoadListing(Action<ListingInformation, Exception> onComplete)
        {

            try
            {
#if DEBUG
                var task = CurrentApp.LoadListingInformationAsync();
#else
                var task = CurrentApp.LoadListingInformationAsync().AsTask();
#endif
                await task;

                var information = task.Result;

                if (information != null)
                {
                    var listing = new ListingInformation();
                    listing.AgeRating = information.AgeRating;
                    listing.CurrentMarket = information.CurrentMarket;
                    listing.Description = information.Description;
                    listing.FormattedPrice = information.FormattedPrice;
                    listing.Name = information.Name;
                    listing.ProductListings = new Dictionary<string, ProductListing>();
                    var productListings = information.ProductListings;

                    foreach (var productListing in productListings)
                    {
                        var value = productListing.Value;
                        var product = new ProductListing();
                        product.ProductId = value.ProductId;
                        product.ProductType = (ProductType)value.ProductType;
                        product.FormattedPrice = value.FormattedPrice;
                        product.Name = value.Name;
                        listing.ProductListings.Add(productListing.Key, product);
                    }
                    if (onComplete != null)
                    {
                        EtceteraWindows.RunOnUnityThread(() => onComplete(listing, null));
                    }
                }
                else
                {
                    if (onComplete != null)
                    {
                        EtceteraWindows.RunOnUnityThread(() => onComplete(null, new Exception("ListingInformation is null")));
                    }
                }
            }
            catch (Exception ex)
            {
                if (onComplete != null)
                {
                    EtceteraWindows.RunOnUnityThread(() => onComplete(null, ex));
                }
            }

            
        }

        /// <summary>
        /// Requests a purchase of the application. completionHandler gets called with either true or false indicating if
        ///             the app was purchased.
        /// 
        /// </summary>
        public static async void RequestProductPurchase(string productId, Action<PurchaseResults, Exception> onComplete)
        {
            EtceteraWindows.RunOnUIThread(() => Purchase(productId, onComplete));
        }

        private static async void Purchase(string productId, Action<PurchaseResults, Exception> completionHandler)
        {
            Exception exception = null;
            string result = null;
            try
            {
                result = await CurrentApp.RequestProductPurchaseAsync(productId, true);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (completionHandler != null)
            {
                PurchaseResults purchase = null;
                if (result != null)
                {
                    purchase = new PurchaseResults();
                    //purchase.offerId = result.OfferId;
                    purchase.Status = ProductPurchaseStatus.Succeeded;
                    purchase.ReceiptXml = result;
                    //purchase.transactionId = result.TransactionId;
                }

                EtceteraWindows.RunOnUnityThread(() => completionHandler(purchase, exception));
            }
        }

        /// <summary>
        /// Reports a consumable as fullfilled. You should call this once you have granted the consumable to the player.
        /// 
        /// </summary>
        /// <param name="productId"/><param name="transactionId"/><param name="completionHandler"/>
        
        public static void ReportConsumableFulfillment(string productId, Guid transactionId, Action<FulfillmentResult, Exception> onComplete)
        {
            Exception exception = null;
            try
            {
                CurrentApp.ReportProductFulfillment(productId);
                if (onComplete != null)
                {
                    EtceteraWindows.RunOnUnityThread(() => onComplete(FulfillmentResult.Succeeded, null));
                }
            }
            catch (Exception ex)
            {

                if (onComplete != null)
                {
                    EtceteraWindows.RunOnUnityThread(() => onComplete(FulfillmentResult.ServerError, ex));
                }
            }

        }
    }
}
