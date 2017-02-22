using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory, Category, PharmaceuticalForm and page of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    [DataContract]
    public class PagedProductInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PagedProductInfo class. Not receive parameters.
        /// </summary>
        public PagedProductInfo() { }

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
        ///         Property which gets or sets a value for PharmaForm.
        ///     </para>
        ///     <para>
        ///         Name or description of the PharmaceuticalForm.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PharmaForm
        {
            get { return this._pharmaForm; }
            set { this._pharmaForm = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates of the Product is enabled.
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
        ///         Property which gets or sets a value for PharmaFormId.
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
        ///         Property which gets or sets a value for DivisionShortName.
        ///     </para>
        ///     <para>
        ///         Division Short Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionShortName
        {
            get { return this._divisionShortName; }
            set { this._divisionShortName = value; }
        }

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
        ///         Property which gets or sets a value for CategoryName.
        ///     </para>
        ///     <para>
        ///         Category Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CategoryName
        {
            get { return this._categoryName; }
            set { this._categoryName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Page.
        ///     </para>
        ///     <para>
        ///         Page on which the drug is in the print edition.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Page
        {
            get { return this._page; }
            set { this._page = value; }
        }

        [DataMember]
        public string ProductDescription 
        {
            get { return this._productDescription; }
            set { this._productDescription = value; }
        }

        [DataMember]
        public string ProductType
        {
            get { return this._productType; }
            set { this._productType = value; }
        }
        [DataMember]
        public string Symptoms
        {
            get { return this._symptoms; }
            set { this._symptoms = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for NewProduct.
        ///     </para>
        ///     <para>
        ///         It indicates if the product is new.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool NewProduct
        {
            get
            {
                return this._newProduct;
            }
            set
            {
                this._newProduct = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ModifiedContent.
        ///     </para>
        ///     <para>
        ///         It indicates if the product is modified.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool ModifiedContent
        {
            get
            {
                return this._modifiedContent;
            }
            set
            {
                this._modifiedContent = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Sidef.
        ///     </para>
        ///     <para>
        ///         It indicates if the product is in photographic identification system.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Sidef
        {
            get
            {
                return this._sidef;
            }
            set
            {
                this._sidef = value;
            }
        }

        #endregion

        private int _productId;
        private string _brand;
        private string _pharmaForm;
        private bool _active;
        private int _pharmaFormId;
        private string _divisionShortName;
        private int _divisionId;
        private int _categoryId;
        private string _categoryName;
        private string _page;
        private string _productDescription;
        private string _productType;
        private string _symptoms;
        private bool _newProduct;
        private bool _modifiedContent;
        private bool _sidef;
    }
}
