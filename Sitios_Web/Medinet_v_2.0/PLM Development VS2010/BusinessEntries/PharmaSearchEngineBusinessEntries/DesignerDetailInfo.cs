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

    public class DesignerDetailInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DesignerDetailInfo class. Not receive parameters.
        /// </summary>
        public DesignerDetailInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Vendor.
        ///     </para>
        ///     <para>
        ///         Category Designer.
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
        ///         Category Designer.
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
        ///         Category Designer.
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
        ///         Category Designer.
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
        ///         Category Designer.
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
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Category Designer.
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
        ///         Category Designer.
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
        ///         Category Designer.
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
        ///         Category Designer.
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
        ///         Property which gets or sets a value for NewProduct.
        ///     </para>
        ///     <para>
        ///         Category Designer.
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
        ///         Property which gets or sets a value for ModifiedContent.
        ///     </para>
        ///     <para>
        ///         Category Designer.
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
        ///         Property which gets or sets a value for ModifiedAttributes.
        ///     </para>
        ///     <para>
        ///         Category Designer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ModifiedAttributes
        {
            get { return this._modifiedAttributes; }
            set { this._modifiedAttributes = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Presentations.
        ///     </para>
        ///     <para>
        ///         Category Designer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Presentations
        {
            get { return this._presentations; }
            set { this._presentations = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ModuleId.
        ///     </para>
        ///     <para>
        ///         Category Designer.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ModuleId
        {
            get { return this._moduleId; }
            set { this._moduleId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryId.
        ///     </para>
        ///     <para>
        ///         Category Designer.
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
        ///         Property which gets or sets a value for NumberEdition.
        ///     </para>
        ///     <para>
        ///         Category Designer.
        ///     </para>
        /// </summary>
        [DataMember]
        public int NumberEdition
        {
            get { return this._numberedition; }
            set { this._numberedition = value; }
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
        private string _modifiedAttributes;
        private string _presentations;
        private int? _moduleId;
        private int _categoryId;
        private int _numberedition;
    }
}
