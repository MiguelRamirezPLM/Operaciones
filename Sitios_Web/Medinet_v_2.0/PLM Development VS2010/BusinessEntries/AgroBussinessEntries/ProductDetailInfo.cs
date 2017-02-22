using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory, Substances, PharmaceuticalForm and the content of the attributes associated with a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ProductDetailInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductDetailInfo class. Not receive parameters.
        /// </summary>
        public ProductDetailInfo() { }

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
        ///         Property which gets or sets a value for ProductShot.
        ///     </para>
        ///     <para>
        ///         Product shot is the image of a product which participates in an edition.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProductShot
        {
            get { return this._productShot; }
            set { this._productShot = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Web address where the product image.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionName.
        ///     </para>
        ///     <para>
        ///         Division Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionName
        {
            get { return this._divisionName; }
            set { this._divisionName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionImage.
        ///     </para>
        ///     <para>
        ///         Division Logo.
        ///     </para>
        /// </summary>
        public string DivisionImage
        {
            get { return this._divisionImage; }
            set { this._divisionImage = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ReferenceUrl.
        ///     </para>
        ///     <para>
        ///         Product's Web Reference.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ReferenceUrl
        {
            get
            {
                return this._referenceUrl;
            }
            set
            {
                this._referenceUrl = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Substances.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<ActiveSubstanceInfo> Substances
        {
            get { return this._substances; }
            set
            {
                this._substances = new List<ActiveSubstanceInfo>();
                foreach (ActiveSubstanceInfo substance in value)
                    this._substances.Add(substance);
            }
        }



        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Attributes.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="AttributeDetailInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo> Attributes
        {
            get { return this._attributes; }
            set
            {
                this._attributes = new List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo>();
                foreach (PharmaSearchEngineBusinessEntries.AttributeDetailInfo attribute in value)
                    this._attributes.Add(attribute);
            }
        }

      
        #endregion


        private int _productId;
        private string _brand;
        private int _pharmaFormId;
        private string _pharmaForm;
        private string _productShot;
        private string _baseUrl;
        private string _divisionName;
        private string _divisionImage;
        private string _referenceUrl;
        private List<ActiveSubstanceInfo> _substances = new List<ActiveSubstanceInfo>();
        private List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo> _attributes = new List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo>();
     
    }
}
