namespace Megapop.IAP
{
    public class ProductListing
    {    /// <summary>
        /// Gets the app's purchase price with the appropriate formatting for the current market.
        /// 
        /// </summary>
        public string FormattedPrice { get; set; }

        /// <summary>
        /// Gets the descriptive name of the product or feature that can be shown to customers in the current market.
        /// 
        /// </summary>
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Gets the ID of an app's feature or product.
        /// 
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets the type of this product. Possible values are defined by ProductType
        /// 
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary/>
        /// 
        /// <returns/>
        public new string ToString()
        {
            return Name;
        }
    }
}