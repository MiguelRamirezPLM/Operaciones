using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the presentation information associated with a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.AttributesInfo"/>
    [DataContract]
    public class PresentationByProductInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AttributeByProductInfo class. Not receive parameters.
        /// </summary>
        public PresentationByProductInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ExternalPackId.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ExternalPackId
        {
            get { return this._externalPackId; }
            set { this._externalPackId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyExternalPack.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? QtyExternalPack
        {
            get { return this._qtyExternalPack; }
            set { this._qtyExternalPack = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ExternalPackName.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ExternalPackName
        {
            get { return this._externalPackName; }
            set { this._externalPackName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InternalPackId.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? InternalPackId
        {
            get { return this._internalPackId; }
            set { this._internalPackId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyInternalPack.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? QtyInternalPack
        {
            get { return this._qtyInternalPack; }
            set { this._qtyInternalPack = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InternalPackName.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string InternalPackName
        {
            get { return this._internalPackName; }
            set { this._internalPackName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ContentUnitId.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ContentUnitId
        {
            get { return this._contentUnitId; }
            set { this._contentUnitId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyContentUnit.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QtyContentUnit
        {
            get { return this._qtyContentUnit; }
            set { this._qtyContentUnit = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UnitName.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ContentUnitName
        {
            get { return this._UnitName; }
            set { this._UnitName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for WeightUnitId.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? WeightUnitId
        {
            get { return this._weightUnitId; }
            set { this._weightUnitId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyWeightUnit.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QtyWeightUnit
        {
            get { return this._qtyWeightUnit; }
            set { this._qtyWeightUnit = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string WeightShortName
        {
            get { return this._ShortName; }
            set { this._ShortName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PresentationId.
        ///     </para>
        ///     <para>
        ///         Category Presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PresentationId
        {
            get { return this._presentationId; }
            set { this._presentationId = value; }
        }

        #endregion

        private int _presentationId;
        private int? _externalPackId;
        private int? _qtyExternalPack;
        private string _externalPackName;
        private int? _internalPackId;
        private int? _qtyInternalPack;
        private string _internalPackName;
        private int? _contentUnitId;
        private string _qtyContentUnit;
        private string _UnitName;
        private int? _weightUnitId;
        private string _qtyWeightUnit;
        private string _ShortName;


    }
}
