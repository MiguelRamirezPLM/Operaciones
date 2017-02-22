using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Laboratory, Category and PharmaceuticalForm of an Product .
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class ProductCategoriesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductCategoriesInfo class. Not receive parameters.
        /// </summary>
        public ProductCategoriesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionId.
        ///     </para>
        ///     <para>
        ///         Division Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryId.
        ///     </para>
        ///     <para>
        ///         Category Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CategoryId
        {
            get { return this._categoryId; }
            set { this._categoryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property that gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         Pharmaceutical Form Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaFormId
        {
            get { return this._pharmaFormId; }
            set { this._pharmaFormId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property that gets or sets a value for ProductId.
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
        ///         Property that gets or sets a value for TechnicalSheet.
        ///     </para>
        ///     <para>
        ///         Indicates if the product belongs to Technical Sheet.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool TechnicalSheet
        {
            get { return this._technicalSheet; }
            set { this._technicalSheet = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductDescription.
        ///     </para>
        ///     <para>
        ///         Product advertising.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProductDescription
        {
            get
            { return this._productDescription; }
            set
            { this._productDescription = value; }
        }

        #endregion

        private int _divisionId;
        private int _categoryId;
        private int _pharmaFormId;
        private int _productId;
        private bool _technicalSheet;
        private string _productDescription;
    }
}
