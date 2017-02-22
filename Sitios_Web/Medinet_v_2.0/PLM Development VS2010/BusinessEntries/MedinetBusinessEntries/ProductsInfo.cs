using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the information of the Products.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Product which participates in an edition.
    ///     </para>
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.CountriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.LaboratoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.AlphabetInfo"/>
    [DataContract]
    public class ProductsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductsInfo class. Not receive parameters.
        /// </summary>
        public ProductsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LaboratoryId.
        ///     </para>
        ///     <para>
        ///         Laboratory Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AlphabetId.
        ///     </para>
        ///     <para>
        ///         Identifier of the alphabet letter.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AlphabetId
        {
            get { return this._alphabetId; }
            set { this._alphabetId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Brand.
        ///     </para>
        ///     <para>
        ///         Product Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Brand
        {
            get { return this._brand; }
            set { this._brand = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Description of the product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SanitaryRegistry.
        ///     </para>
        ///     <para>
        ///         Sanitary Registry of the Product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SanitaryRegistry
        {
            get { return this._sanitaryRegistry; }
            set { this._sanitaryRegistry = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductTypeId.
        ///     </para>
        ///     <para>
        ///         Product Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductTypeId
        {
            get { return this._productTypeId; }
            set { this._productTypeId = value; }
        }
        #endregion

        private int _productId;
        private int _countryId;
        private int _laboratoryId;
        private int _alphabetId;
        private string _brand;
        private string _sanitaryRegistry;
        private string _description;
        private int _productTypeId;
        private bool _active;
    }
}
