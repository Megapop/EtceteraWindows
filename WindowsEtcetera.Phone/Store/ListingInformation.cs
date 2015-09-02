using System.Collections.Generic;

namespace Megapop.IAP
{
    public class ListingInformation
    {
        /// <summary>
        /// Gets the age rating for the app.
        /// 
        /// </summary>
        public uint AgeRating { get; set; }

        /// <summary>
        /// Gets the value that indicates the customer's market, such as en-us, that will be used for transactions in the current session.
        /// 
        /// </summary>
        public string CurrentMarket { get; set; }

        /// <summary>
        /// Gets the app's description in the current market.
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the app's purchase price formatted for the current market and currency.
        /// 
        /// </summary>
        public string FormattedPrice { get; set; }

        /// <summary>
        /// Gets the app's name in the current market.
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets information about the features and products that can be bought by making in-app purchases.
        /// 
        /// </summary>
        public Dictionary<string, ProductListing> ProductListings { get; set; }

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