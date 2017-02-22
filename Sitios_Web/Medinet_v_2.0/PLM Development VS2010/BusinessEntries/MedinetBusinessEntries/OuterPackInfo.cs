using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Packs and Products.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.PackInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class OuterPackInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the OuterPackInfo class. Not receive parameters.
        /// </summary>
        public OuterPackInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId.
        ///     </para>
        ///     <para>
        ///         Identifier of the association between package and product.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PackPpfId
        {
            get { return this._packPpfId; }
            set { this._packPpfId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Indicates if the Package have an parent package.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InnerPack.
        ///     </para>
        ///     <para>
        ///         Is an object of type <see cref="MedinetBusinessEntries.PackInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public PackInfo InnerPack
        {
            get { return this._innerPack; }
            set { this._innerPack = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackId.
        ///     </para>
        ///     <para>
        ///         Package Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PackId
        {
            get { return this._packId; }
            set { this._packId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackName.
        ///     </para>
        ///     <para>
        ///         Name or description of the package.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PackName
        {
            get { return this._packName; }
            set { this._packName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackTypeId.
        ///     </para>
        ///     <para>
        ///         Package type identifier. it can be primary or secondary package.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PackTypeId
        {
            get { return this._packTypeId; }
            set { this._packTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeName.
        ///     </para>
        ///     <para>
        ///         Name the type of Package.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TypeName
        {
            get { return this._typeName; }
            set { this._typeName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyItems.
        ///     </para>
        ///     <para>
        ///         Number of items contained in the package.
        ///     </para>
        /// </summary>
        [DataMember]
        public int QtyItems
        {
            get { return this._qtyItems; }
            set { this._qtyItems = value; }
        }

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
        ///         Pproduct Name.
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
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the association between Product and package is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _packPpfId;
        private int? _parentId;
        private PackInfo _innerPack = new PackInfo();
        private int _packId;
        private string _packName;
        private int _packTypeId;
        private string _typeName;
        private int _qtyItems;
        private int _productId;
        private string _brand;
        private int _pharmaFormId;
        private string _pharmaForm;
        private bool _active;

    }
}
