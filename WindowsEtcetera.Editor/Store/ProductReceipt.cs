using System;
using System.Collections.Generic;

namespace Megapop.IAP
{
    public class ProductReceipt
    {

        public static List<ProductReceipt> CreateReceipts(string receiptXml)
        {
            var receipts = new List<ProductReceipt>();
            return receipts;
        }
        public static ProductReceipt CreateReceipt(string receiptXml)
        {
            //string data = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Receipt Version=\"1.0\" ReceiptDate=\"2012-11-24T12:22:20Z\" CertificateId=\"\" ReceiptDeviceId=\"aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa\">
            //<ProductReceipt Id=\"bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb\" 
            //AppId=\"MyNamespace.MyApp_t7396xywk1mky\" 
            //ProductId=\"Upgrade\" 
            //PurchaseDate=\"2012-11-24T12:22:20Z\" 
            //ProductType=\"Durable\"  /></Receipt>";

            return null;
        }

        /// <summary>
        ///  Identifies the purchase.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Identifies the app through which the user made the purchase.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Identifies the product purchased.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Determines the product type. Currently only supports a value of Durable.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Date when the purchase occurred.
        /// </summary>
        public DateTimeOffset PurchaseDate { get; set; }
    }
}
