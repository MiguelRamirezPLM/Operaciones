using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace GuiaBusinessEntries
{
    [DataContract]
    public class SearchResultsInfo
    {
        #region Constructors
        /// <summary>
        ///     Constructor that initializes the properties. Not receive parameters.
        /// </summary>
        public SearchResultsInfo()
        {
            this._products = new List<ProductsByEditionInfo>();
            this._brands = new List<BrandsByEditionInfo>();
            this._clients = new List<ClientInfo>();
            
        }

      
        [DataMember]
        public List<ProductsByEditionInfo> Products
        {
            get { return this._products; }
            set
            {
                foreach (ProductsByEditionInfo product in value)
                    this._products.Add(product);
            }
        }

        [DataMember]
        public List<BrandsByEditionInfo> Brands
        {
            get { return this._brands; }
            set
            {
                foreach (BrandsByEditionInfo brand in value)
                    this._brands.Add(brand);
            }
        }

        [DataMember]
        public List<ClientInfo> Clients
        {
            get { return this._clients; }
            set
            {
                foreach (ClientInfo client in value)
                    this._clients.Add(client);
            }
        }


        #endregion

        private List<ProductsByEditionInfo> _products;
        private List<BrandsByEditionInfo> _brands;
        private List<ClientInfo> _clients;
        
    }
}
