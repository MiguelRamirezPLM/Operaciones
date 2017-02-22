using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
        /// <summary>
    ///     Class which represents the Laboratory, Substances, PharmaceuticalForm and the content of the attributes associated with a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.AttributeDetailInfo"/>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.EditionProductShotInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    [DataContract]
    public class VendorDetailInfo
    {
         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductByCodeInfo class. Not receive parameters.
        /// </summary>

        public VendorDetailInfo() { }

        #endregion
        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Vendor.
        ///     </para>
        ///     <para>
        ///         Vendor.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Vendor
        {
            get { return this._vendor; }
            set { this._vendor = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         EditionId.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionId.
        ///     </para>
        ///     <para>
        ///         DivisionId.
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
        ///         DivisionName.
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
        ///         DivisionShortName.
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
        ///         Property which gets or sets a value for _productId.
        ///     </para>
        ///     <para>
        ///         _productId.
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
        ///         Property which gets or sets a value for ParmaFormId.
        ///     </para>
        ///     <para>
        ///         ParmaFormId.
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
        ///         Property which gets or sets a value for NewProduct.
        ///     </para>
        ///     <para>
        ///         NewProduct.
        ///     </para>
        /// </summary>
        [DataMember]
        public string NewProduct
        {
            get { return this._newProduct; }
            set { this._newProduct = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParmaForm.
        ///     </para>
        ///     <para>
        ///         ParmaForm.
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
        ///         Property which gets or sets a value for ModifiedContent.
        ///     </para>
        ///     <para>
        ///         ModifiedContent.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ModifiedContent
        {
            get { return this._modifiedContent; }
            set { this._modifiedContent = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductShot.
        ///     </para>
        ///     <para>
        ///         ProductShot.
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
        ///         Property which gets or sets a value for NumberEdition.
        ///     </para>
        ///     <para>
        ///         NumberEdition.
        ///     </para>
        /// </summary>
        [DataMember]
        public int NumberEdition
        {
            get { return this._numberEdition; }
            set { this._numberEdition = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ModuleId.
        ///     </para>
        ///     <para>
        ///         ModuleId.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ModuleId
        {
            get { return this._moduleId; }
            set { this._moduleId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         ShortName.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get { return this._shortName; }
            set { this._shortName = value; }
        }
         #endregion

        private string _vendor;
        private int _editionId;
        private int _divisionId;
        private string _divisionName;
        private string _divisionShortName;
        private int _productId;
        private string _brand;
        private int _pharmaFormId;
        private string _pharmaForm;
        private string _newProduct;
        private string _modifiedContent;
        private string _productShot;
        private int _numberEdition;
        private int _moduleId;
        private string _shortName;
    }
}
