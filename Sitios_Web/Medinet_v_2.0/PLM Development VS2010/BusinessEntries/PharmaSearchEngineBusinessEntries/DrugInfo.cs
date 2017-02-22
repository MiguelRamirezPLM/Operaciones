using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory, Category and Pharmaceutical Form of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    [DataContract]
    public class DrugInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DrugInfo class. Not receive parameters.
        /// </summary>
        public DrugInfo() { }

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
        ///         Property which gets or sets a value for ProductDescription.
        ///     </para>
        ///     <para>
        ///         Product Description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProductDescription
        {
            get { return this._productDescription; }
            set { this._productDescription = value; }
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
        ///         Property which gets or sets a value for PharmaForms.
        ///     </para>
        ///     <para>
        ///         Is an list of objects of type <see cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> PharmaForms
        {
            get { return this._pharmaForms; }
            set
            {
                foreach (MedinetBusinessEntries.PharmaceuticalFormInfo pharma in value)
                {
                    this._pharmaForms.Add(pharma);
                }
            }
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
        ///         Property which gets or sets a value for DivisionActive.
        ///     </para>
        ///     <para>
        ///         Indicates if the Division is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool DivisionActive
        {
            get { return this._divisionActive; }
            set { this._divisionActive = value; }
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
        ///         Property which gets or sets a value for CategoryActive.
        ///     </para>
        ///     <para>
        ///         Indicates if the Category is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool CategoryActive
        {
            get { return this._categoryActive; }
            set { this._categoryActive = value; }
        }

        #endregion

        private int _productId;
        private string _brand;
        private string _productDescription;
        private bool _productActive;
        private List<MedinetBusinessEntries.PharmaceuticalFormInfo> _pharmaForms = new List<MedinetBusinessEntries.PharmaceuticalFormInfo>();
        private int _divisionId;
        private string _divisionName;
        private string _divisionShortName;
        private bool _divisionActive;
        private int _categoryId;
        private string _categoryName;
        private bool _categoryActive;
    }
}
