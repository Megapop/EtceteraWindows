using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Megapop.IAP
{
    public class ProductReceipt
    {

        public static List<ProductReceipt> CreateReceipts(string receiptXml)
        {
            var receipts = new List<ProductReceipt>();
            var xml = XDocument.Parse(receiptXml);
            var descendants = xml.Descendants("ProductReceipt").ToList();
            receipts.Capacity = descendants.Count();
            for (int index = 0; index < descendants.Count; index++)
            {
                var descendant = descendants[index];
                if (descendant != null)
                {
                    receipts.Add(Create(descendant));
                }
            }

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
            var xml = XDocument.Parse(receiptXml);
            var descendants = xml.Descendants("ProductReceipt");
            foreach (var descendant in descendants)
            {
                if (descendant != null)
                {
                    return Create(descendant);
                }
            }

            return null;
        }

        private static ProductReceipt Create(XElement descendant)
        {
            var receipt = new ProductReceipt();
            var idAttribute = descendant.Attribute("Id");
            if (idAttribute != null)
            {
                receipt.Id = new Guid(idAttribute.Value);
            }

            var appIdAttribute = descendant.Attribute("AppId");
            if (appIdAttribute != null)
            {
                receipt.AppId = appIdAttribute.Value;
            }

            var productIdAttribute = descendant.Attribute("ProductId");
            if (productIdAttribute != null)
            {
                receipt.ProductId = productIdAttribute.Value;
            }

            var productTypeAttribute = descendant.Attribute("ProductType");
            if (productTypeAttribute != null)
            {
                ProductType type;
                if (Enum.TryParse(productTypeAttribute.Value, out type))
                {
                    receipt.ProductType = type;
                }
            }

            var purchaseDateAttribute = descendant.Attribute("PurchaseDate");
            if (purchaseDateAttribute != null)
            {
                DateTimeOffset date;
                if (DateTimeOffset.TryParse(purchaseDateAttribute.Value, out date))
                {
                    receipt.PurchaseDate = date;
                }
            }
            return receipt;
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
