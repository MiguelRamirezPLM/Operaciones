using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the presentation information associated with a product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <see cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <see cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <see cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <see cref="MedinetBusinessEntries.ExternalPacksInfo"/>
    /// <see cref="MedinetBusinessEntries.InternalPacksInfo"/>
    /// <see cref="MedinetBusinessEntries.ContentUnitsInfo"/>
    /// <see cref="MedinetBusinessEntries.WeightUnitsInfo"/>
    [DataContract]
    public class PresentationsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PresentationsInfo class. Not receive parameters.
        /// </summary>
        public PresentationsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PresentationId.
        ///     </para>
        ///     <para>
        ///         Presentation identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PresentationId
        {
            get
            {
                return this._presentationId;
            }
            set
            {
                this._presentationId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionId.
        ///     </para>
        ///     <para>
        ///         Division identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DivisionId
        {
            get
            {
                return this._divisionId;
            }
            set
            {
                this._divisionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryId.
        ///     </para>
        ///     <para>
        ///         Category identifier.
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
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         Pharmaceutical form identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaFormId
        {
            get
            {
                return this._pharmaFormId;
            }
            set
            {
                this._pharmaFormId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyExternalPack.
        ///     </para>
        ///     <para>
        ///         External pack quantity.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? QtyExternalPack
        {
            get
            {
                return this._qtyExternalPack;
            }
            set
            {
                this._qtyExternalPack = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ExternalPackId.
        ///     </para>
        ///     <para>
        ///         External pack identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ExternalPackId
        {
            get
            {
                return this._externalPackId;
            }
            set
            {
                this._externalPackId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyInternalPack.
        ///     </para>
        ///     <para>
        ///         Internal pack quantity.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? QtyInternalPack
        {
            get
            {
                return this._qtyInternalPack;
            }
            set 
            {
                this._qtyInternalPack = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InternalPackId.
        ///     </para>
        ///     <para>
        ///         Internal pack identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? InternalPackId
        {
            get
            {
                return this._internalPackId;
            }
            set
            {
                this._internalPackId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyContentUnit.
        ///     </para>
        ///     <para>
        ///         Content unit quantity.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QtyContentUnit
        {
            get
            {
                return this._qtyContentUnit;
            }
            set
            {
                this._qtyContentUnit = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ContentUnitId.
        ///     </para>
        ///     <para>
        ///         Content unit identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ContentUnitId
        {
            get
            {
                return this._contentUnitId;
            }
            set
            {
                this._contentUnitId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyWeightUnit.
        ///     </para>
        ///     <para>
        ///         Weight unit quantity.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QtyWeightUnit
        {
            get
            {
                return this._qtyWeightUnit;
            }
            set
            {
                this._qtyWeightUnit = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for WeightUnitId.
        ///     </para>
        ///     <para>
        ///         Weight unit identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? WeightUnitId
        {
            get
            {
                return this._weightUnitId;
            }
            set
            {
                this._weightUnitId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Presentation.
        ///     </para>
        ///     <para>
        ///         Presentation description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Presentation
        {
            get
            {
                return this._presentation;
            }
            set
            {
                this._presentation = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PackPpfId
        {
            get
            {
                return this._PackPpfId;
            }
            set
            {
                this._PackPpfId= value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ContentName
        {
            get
            {
                return this._ContentName;
            }
            set
            {
                this._ContentName = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get
            {
                return this._ShortName;
            }
            set
            {
                this._ShortName = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TargetUseId
        {
            get
            {
                return this._TargetUseId;
            }
            set
            {
                this._TargetUseId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UseName
        {
            get
            {
                return this._UseName;
            }
            set
            {
                this._UseName= value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Brand
        {
            get
            {
                return this._Brand;
            }
            set
            {
                this._Brand = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PharmaForm
        {
            get
            {
                return this._PharmaForm;
            }
            set
            {
                this._PharmaForm = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PackPpfId
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public OuterPackInfo Packs
        {
            get
            {
                return this._Packs;
            }
            set
            {
                this._Packs = value;
            }
        }

       


        #endregion

        private int _presentationId;
        private int _divisionId;
        private int _categoryId;
        private int _productId;
        private int _pharmaFormId;
        private int? _qtyExternalPack;
        private int? _externalPackId;
        private int? _qtyInternalPack;
        private int? _internalPackId;
        private string _qtyContentUnit;
        private int? _contentUnitId;
        private string _qtyWeightUnit;
        private int? _weightUnitId;
        private string _presentation;
        private bool _active;
        private int _PackPpfId;
        private string _ContentName;
        private string _UnitName;
        private string _ShortName;
        private int _TargetUseId;
        private string _UseName;
        private string _Brand;
            private string _PharmaForm;
            private OuterPackInfo _Packs;
           
    }
}
