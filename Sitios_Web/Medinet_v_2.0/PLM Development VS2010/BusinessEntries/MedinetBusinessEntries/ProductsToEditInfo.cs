using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory, Category, Pharmaceutical Form and Editions of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    [DataContract] 
    public class ProductsToEditInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductsToEditInfo class. Not receive parameters.
        /// </summary>
        public ProductsToEditInfo() { }

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
        ///         Property which gets or sets a value for ProductType.
        ///     </para>
        ///     <para>
        ///         Product type.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProductType
        {
            get { return this._productType; }
            set { this._productType = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductType.
        ///     </para>
        ///     <para>
        ///         Product type.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Symptoms
        {
            get { return this._symptoms; }
            set { this._symptoms = value; }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductActive.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool ProductActive
        {
            get { return this._productActive; }
            set { this._productActive = value; }
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
        public int? PharmaFormId
        {
            get { return this._pharmaFormId; }
            set { this._pharmaFormId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaForm.
        ///     </para>
        ///     <para>
        ///         Name of the PharmaceuticalForm associated with the Product.
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
        ///         Property which gets or sets a value for PharmaActive.
        ///     </para>
        ///     <para>
        ///         Indicates if the PharmaceuticalForm is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool PharmaActive
        {
            get { return this._pharmaActive; }
            set { this._pharmaActive = value; }
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
        public int? DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionShortName.
        ///     </para>
        ///     <para>
        ///         Short Name of the Division.
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
        ///         Property which gets or sets a value for CategoryId.
        ///     </para>
        ///     <para>
        ///         Category Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? CategoryId
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
        ///         Property which gets or sets a value for Editions.
        ///     </para>
        ///     <para>
        ///         Editions associated with the Product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Editions
        {
            get { return this._editions; }
            set { this._editions = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductDescription.
        ///     </para>
        ///     <para>
        ///         Product description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProductDescription
        {
            get
            {
                return this._productDescription;
            }
            set
            {
                this._productDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AdDescription.
        ///     </para>
        ///     <para>
        ///         Product advertising.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AdDescription
        {
            get
            {
                return this._adDescription;
            }
            set
            {
                this._adDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Participant.
        ///     </para>
        ///     <para>
        ///         It indicates if the product is participant.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Participant
        {
            get
            {
                return this._participant;
            }
            set
            {
                this._participant = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Mentionated.
        ///     </para>
        ///     <para>
        ///         It indicates if the product is mentionated.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Mentionated
        {
            get
            {
                return this._mentionated;
            }
            set
            {
                this._mentionated = value;
            }
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ContentFamilyId.
        ///     </para>
        ///     <para>
        ///         Identifier for the content family.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ContentFamilyId
        {
            get
            {
                return this._contentFamilyId;
            }
            set
            {
                this._contentFamilyId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ContentFamilyString.
        ///     </para>
        ///     <para>
        ///         Name for the content family.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ContentFamilyString
        {
            get
            {
                return this._contentFamilyString;
            }
            set
            {
                this._contentFamilyString = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PSFamilyId.
        ///     </para>
        ///     <para>
        ///         Identifier for the photographic identification system family.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? PSFamilyId
        {
            get
            {
                return this._psFamilyId;
            }
            set
            {
                this._psFamilyId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PSFamilyString.
        ///     </para>
        ///     <para>
        ///         Name for the photographic identification system family.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PSFamilyString
        {
            get
            {
                return this._psFamilyString;
            }
            set
            {
                this._psFamilyString = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Countries.
        ///     </para>
        ///     <para>
        ///         Countries of the product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Countries
        {
            get
            {
                return this._countryCodesId;
            }
            set
            {
                this._countryCodesId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Images.
        ///     </para>
        ///     <para>
        ///         Images of the product
        ///     </para>
        /// </summary>
        [DataMember]
        public string Image
        {
            get
            {
                return this._image;
            }
            set
            {
                this._image = value;
            }
        }
        #endregion

        private int _productId;
        private string _brand;
        private bool _productActive;
        private int? _pharmaFormId;
        private string _pharmaForm;
        private bool _pharmaActive;
        private int? _divisionId;
        private string _divisionShortName;
        private int? _categoryId;
        private string _categoryName;
        private string _editions;
        private string _productDescription;
        private string _adDescription;
        private bool _participant;
        private bool _mentionated;
        private bool _newProduct;
        private bool _modifiedContent;
        private bool _sidef;
        private int? _contentFamilyId;
        private string _contentFamilyString;
        private int? _psFamilyId;
        private string _psFamilyString;
        private string _productType;
        private string _symptoms;
        private string _countryCodesId;
        private string _image;
        
    }
}
