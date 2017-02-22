using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Product information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.TherapeuticInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.IndicationInfo"/>
    [DataContract]
    public class AssignedProduct
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AssignedProduct class. Not receive parameters.
        /// </summary>
        public AssignedProduct() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }
        //
        [DataMember]
        public string ProductDescription
        {
            get { return this._productDescription; }
            set { this._productDescription = value; }
        }
        //
        
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Brand.
        ///     </para>
        ///     <para>
        ///         Product Commercial Name.
        ///     </para>
        ///     
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
        ///         Pharmaceutical Form Name.
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
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         Pharmaceutical Form identifier.
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
        ///         Short Name of the laboratory which distributes the Product.
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
        ///         Laboratory Identifier.
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
            get
            {
                return this._categoryId;
            }
            set
            {
                this._categoryId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryName.
        ///     </para>
        ///     <para>
        ///         Category name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }
            set
            {
                this._categoryName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ATC.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is associated with a therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool ATC
        {
            get { return this._atc; }
            set { this._atc = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Substance.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is associated with a Active Substance.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Substance
        {
            get { return this._substance; }
            set { this._substance = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Indication.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is associated with a Indication.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Indication
        {
            get { return this._indication; }
            set { this._indication = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for interaction.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is associated with a interaction.
        ///     </para>
        /// </summary>
        [DataMember]
        public  string Interaction
        {
            get { return this._interaction; }
            set { this._interaction = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for symptom.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is associated with a symptom.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Symptom
        {
            get { return this._symptom; }
            set { this._symptom = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for contraindication.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is associated with a contraindication
        ///     </para>
        /// </summary>
        [DataMember]
        public string Contraindication
        {
            get { return this._contraindication; }
            set { this._contraindication = value; }
        } 

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for productType.
        ///     </para>
        ///     <para>
        ///         Indicates productType of the product
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
        ///         Property which gets or sets a value for ProductTypeID.
        ///     </para>
        ///     <para>
        ///         Is the identifier of the productType
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductTypeId
        {
            get { return this._productTypeId; }
            set { this._productTypeId = value; }
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
        ///         Property which gets or sets a value for ICD.
        ///     </para>
        ///     <para>
        ///         It indicates if the product is in the clasification ICD.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool ICD
        {
            get
            {
                return this._ICD;
            }
            set
            {
                this._ICD= value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Encyclopedia.
        ///     </para>
        ///     <para>
        ///         It indicates if the product is in the clasification Encyclopedias.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Encyclopedia
        {
            get
            {
                return this._encyclopedia;
            }
            set
            {
                this._encyclopedia = value;
            }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ATC OMS.
        ///     </para>
        ///     <para>
        ///         It indicates if the product is in the clasification Therapeutic OMS.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool ATCOMS
        {
            get { return this._atcOMS; }
            set { this._atcOMS = value; }
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
        private bool _atc;
        private bool _substance;
        private bool _indication;
        private string _interaction;
        private bool _symptom;
        private string _contraindication;
        private string _productType;
        private int _productTypeId;
        private string _productDescription;
        private bool _newProduct;
        private bool _modifiedContent;
        private bool _sidef;
        private bool _ICD;
        private bool _encyclopedia;
        private bool _atcOMS;

    }
}
