using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using CurrentApp = Windows.ApplicationModel.Store.CurrentApp;

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
                licenseInformation = new LicenseInformation();
                licenseInformation.ExpirationDate = information.ExpirationDate;
                licenseInformation.IsActive = information.IsActive;
                licenseInformation.IsTrial = information.IsTrial;
                licenseInformation.ProductLicenses = new Dictionary<string, ProductLicense>();
                var dict = information.ProductLicenses;
                foreach (var keyValuePair in dict)
                {
                    var value = keyValuePair.Value;
                    var productLicense = new ProductLicense();
                    productLicense.ExpirationDate = value.ExpirationDate;
                    productLicense.IsActive = value.IsActive;
                    productLicense.ProductId = productLicense.ProductId;
                    licenseInformation.ProductLicenses.Add(keyValuePair.Key, productLicense);
                }
            }
        }



        private static void OnLicenseChanged()
        {
            if (LicenseChangedEvent != null)
            {
                LicenseChangedEvent();
            }

            if (licenseInformation != null)
            {
                licenseInformation.FireLicenseChanged();
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
        public static void LoadTest(uint ageRating, string currentMarket, string description, string formattedPrice, string name, Dictionary<string, ProductListing> products)
        { }

        public static void LoadTest(uint ageRating, string currentMarket, string description, string formattedPrice, string name, string xml)
        { }

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
        /// <param name="productIds"/>
        /// <returns/>
        public static async void GetProductReceipts(List<string> productIds, Action<List<ProductReceipt>, Exception> onComplete)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try
                    {
                        var task = CurrentApp.GetAppReceiptAsync().AsTask();

                        await task;

                        if (onComplete != null)
                        {
                            var receiptXml = task.Result;
                            var receipts = ProductReceipt.CreateReceipts(receiptXml);
                            onComplete(receipts, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (onComplete != null)
                        {
                            onComplete(null, ex);
                        }
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

        /// <summary>
        /// Loads listing information and fires completingHandler with the data once loading completes
        /// 
        /// </summary>
        /// <param name="completionHandler"/>
        public static async void LoadListingInformation(Action<ListingInformation, Exception> onComplete)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try
                    {
                        var task = CurrentApp.LoadListingInformationAsync().AsTask();
                    
                        await task;

                        if (onComplete != null)
                        {
                            ListingInformation listing = null;
                            var information = task.Result;
                            if (information != null)
                            {
                                listing = new ListingInformation();
                                listing.AgeRating = information.AgeRating;
                                listing.CurrentMarket = information.CurrentMarket;
                                listing.Description = information.Description;
                                listing.FormattedPrice = information.FormattedPrice;
                                listing.Name = information.Name;
                                listing.ProductListings = new Dictionary<string, ProductListing>();
                                foreach (var productListing in information.ProductListings)
                                {
                                    var value = productListing.Value;
                                    var product = new ProductListing();
                                    product.ProductId = value.ProductId;
                                    product.ProductType = (ProductType)value.ProductType;
                                    product.FormattedPrice = value.FormattedPrice;
                                    product.Name = value.Name;
                                    listing.ProductListings.Add(productListing.Key, product);
                                }
                            }

                            onComplete(listing, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (onComplete != null)
                        {
                            onComplete(null, ex);
                        }
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

        /// <summary>
        /// Requests a purchase of the application. completionHandler gets called with either true or false indicating if
        ///             the app was purchased.
        /// 
        /// </summary>
        public static async void RequestProductPurchase(string productId, Action<PurchaseResults, Exception> onComplete)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try
                    {
                        var task = CurrentApp.RequestProductPurchaseAsync(productId).AsTask();

                        await task;

                        if (onComplete != null)
                        {
                            Windows.ApplicationModel.Store.PurchaseResults result = task.Result;
                            PurchaseResults purchase = null;
                            if (result != null)
                            {
                                purchase = new PurchaseResults();
                                purchase.OfferId = result.OfferId;
                                purchase.Status = (ProductPurchaseStatus) result.Status;
                                purchase.ReceiptXml = result.ReceiptXml;
                                purchase.ProductReceipt = ProductReceipt.CreateReceipt(result.ReceiptXml);
                                purchase.TransactionId = result.TransactionId;
                            }

                            onComplete(purchase, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (onComplete != null)
                        {
                            onComplete(null, ex);
                        }
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

        /// <summary>
        /// Reports a consumable as fullfilled. You should call this once you have granted the consumable to the player.
        /// 
        /// </summary>
        /// <param name="productId"/><param name="transactionId"/><param name="onComplete"/>
        public static async void ReportConsumableFulfillment(string productId, Guid transactionId, Action<FulfillmentResult, Exception> onComplete)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try
                    {
                        var task = CurrentApp.ReportConsumableFulfillmentAsync(productId, transactionId).AsTask();

                        await task;

                        var result = task.Result;
                        if (onComplete != null)
                        {
                            onComplete((FulfillmentResult) result, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (onComplete != null)
                        {
                            onComplete(FulfillmentResult.ServerError, ex);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                if (onComplete != null)
                {
                    onComplete(FulfillmentResult.ServerError, ex);
                }
            }
        }
    }
}
