using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory, Indications, ActiveSubstances, ICDs and Therapeutics of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.ProductByEditionShortDetailInfo"/>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.DivisionByEditionInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ICDInfo"/>
    [DataContract]
    public class SearchResultsShortDetailInfo
    {
        #region Constructors

        /// <summary>
        ///     Constructor that initializes the properties. Not receive parameters.
        /// </summary>
        public SearchResultsShortDetailInfo() 
        {
            this._labs = new List<DivisionByEditionInfo>();
            this._products = new List<ProductByEditionShortDetailInfo>();
            this._substances = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();
            this._icds = new List<MedinetBusinessEntries.ICDInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Products.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="ProductByEditionShortDetailInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<ProductByEditionShortDetailInfo> Products
        {
            get { return this._products; }
            set 
            {
                foreach (ProductByEditionShortDetailInfo product in value)
                    this._products.Add(product);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Labs.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="DivisionByEditionInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<DivisionByEditionInfo> Labs
        {
            get { return this._labs; }
            set 
            {
                foreach (DivisionByEditionInfo lab in value)
                    this._labs.Add(lab);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Substances.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.ActiveSubstanceInfo> Substances
        {
            get { return this._substances; }
            set 
            {
                foreach (MedinetBusinessEntries.ActiveSubstanceInfo substance in value)
                    this._substances.Add(substance);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ICDs.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="MedinetBusinessEntries.ICDInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.ICDInfo> ICD
        {
            get { return this._icds; }
            set
            {
                foreach (MedinetBusinessEntries.ICDInfo icd in value)
                    this._icds.Add(icd);
            }
        }
        
        #endregion

        private List<ProductByEditionShortDetailInfo> _products;
        private List<DivisionByEditionInfo> _labs;
        private List<MedinetBusinessEntries.ActiveSubstanceInfo> _substances;
        private List<MedinetBusinessEntries.ICDInfo> _icds;
    }
}
