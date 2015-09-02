using System;

namespace Megapop.IAP
{
    public class PurchaseResults
    {
        /// <summary>
        /// A unique ID used to identify a specific in-app purchase item within a large purchase catalog
        /// 
        /// </summary>
        public string OfferId { get; set; }

        /// <summary>
        /// A full receipt that provides a transaction history for the in-app purchase.
        /// 
        /// </summary>
        public string ReceiptXml { get; set; }

        /// <summary>
        /// The receipt
        /// </summary>
        public ProductReceipt ProductReceipt { get; set; }

        /// <summary>
        /// The current state of the in-app purchase
        /// 
        /// </summary>
        public ProductPurchaseStatus Status { get; set; }

        /// <summary>
        /// A unique transaction ID associated with a purchase of the consumable
        /// 
        /// </summary>
        public Guid TransactionId { get; set; }

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