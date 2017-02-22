using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace AgroBusinessEntries
{   
    /// <summary>
    ///     Class which represents the Products information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Is an active ingredient contained in the product.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class ProductDetailByEditionInfo
    {
         #region Constructors

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
        public ProductDetailByEditionInfo() { }

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
        ///         Property which gets or sets a value for DivisionShortName.
        ///     </para>
        ///     <para>
        ///         ShortName of the Division.
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
        ///         Property which gets or sets a value for Product Description.
        ///     </para>
        ///     <para>
        ///         Category Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }
        #endregion

        private int _productId;
        private string _productName;
        private int _divisionId;
        private string _divisionShortName;
        private string _divisionName;
        private int _pharmaFormId;
        private string _pharmaForm;
        private int _categoryId;
        private string _categoryName;
        private string _description;
    }
}
