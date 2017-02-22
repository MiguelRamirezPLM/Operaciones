using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace VetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory, Substances, Pharmaceutical Form and the content of the attributes associated with a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="VetBusinessEntries.AttributeDetailInfo"/>

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
        ///         Property which gets or sets a value for ProductName.
        ///     </para>
        ///     <para>
        ///         Product Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProductName
        {
            get { return this._productName; }
            set { this._productName = value; }
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
        ///         Property which gets or sets a value for Attributes.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="AttributeDetailInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<AttributeDetailInfo> Attributes
        {
            get { return this._attributes; }
            set
            {
                this._attributes = new List<AttributeDetailInfo>();
                foreach (AttributeDetailInfo attribute in value)
                    this._attributes.Add(attribute);
            }
        }

      

       

        #endregion





        private int _productId;
        private string _productName;
        private int _pharmaFormId;
        private string _pharmaForm;
        private string _productShot;
        private string _baseUrl;
        private string _divisionName;
        
        private List<AttributeDetailInfo> _attributes = new List<AttributeDetailInfo>();





    }
}
