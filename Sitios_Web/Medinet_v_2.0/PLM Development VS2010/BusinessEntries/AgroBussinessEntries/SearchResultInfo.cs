using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents the Uses, Crops, and ActiveSubstances of a Product.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class SearchResultInfo
    {
        #region Constructors

        /// <summary>
        ///     Class which represents the information of the Products.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Transmits information between the application layers.
        ///     </para>
        /// </remarks>
        public  SearchResultInfo() {
            this._crops = new List<CropDetailInfo>();
            this._products = new List<ProductDetailByEditionInfo>();
            this._substances = new List<ActiveSubstanceInfo>();
            this._uses = new List<AgrochemicalUseDetailInfo>();
            this._labs = new List<DivisionInfo>();
        }

        #endregion

        #region Properties
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Substances.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="ActiveSubstanceInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<ActiveSubstanceInfo> Substances
        {
            get { return this._substances; }
            set
            {
                foreach (ActiveSubstanceInfo substance in value)
                    this._substances.Add(substance);
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Products.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="ProductDetailByEditionInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<ProductDetailByEditionInfo> Products
        {
            get { return this._products; }
            set
            {
                foreach (ProductDetailByEditionInfo product in value)
                    this._products.Add(product);
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Uses.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="AgrochemicalUseDetailInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<AgrochemicalUseDetailInfo> Uses
        {
            get { return this._uses; }
            set
            {
                foreach (AgrochemicalUseDetailInfo use in value)
                    this._uses.Add(use);
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Crops.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="CropDetailInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<CropDetailInfo> Crops
        {
            get { return this._crops; }
            set
            {
                foreach (CropDetailInfo crop in value)
                    this._crops.Add(crop);
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Divisions.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="DivisionInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<DivisionInfo> Labs
        {
            get { return this._labs; }
            set
            {
                foreach (DivisionInfo div in value)
                    this._labs.Add(div);
            }
        }
        #endregion
        private List<ActiveSubstanceInfo> _substances;
        private List<ProductDetailByEditionInfo> _products;
        private List<AgrochemicalUseDetailInfo> _uses;
        private List<CropDetailInfo> _crops;
        private List<DivisionInfo> _labs;
    }
}
