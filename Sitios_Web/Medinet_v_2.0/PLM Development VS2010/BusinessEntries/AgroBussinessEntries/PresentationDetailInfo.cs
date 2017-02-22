using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents the presentation detail associated with a product.
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
    public class PresentationDetailInfo
    {
  
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PresentationDetailInfo class. Not receive parameters.
        /// </summary>
        public PresentationDetailInfo() { }

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
        ///         Property which gets or sets a value for DivisionName.
        ///     </para>
        ///     <para>
        ///         Division name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionName
        {
            get
            {
                return this._divisionName;
            }
            set
            {
                this._divisionName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionShortName.
        ///     </para>
        ///     <para>
        ///         Division short name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionShortName
        {
            get
            {
                return this._divisionShortName;
            }
            set
            {
                this._divisionShortName = value;
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
        ///         Property which gets or sets a value for Brand.
        ///     </para>
        ///     <para>
        ///         Product name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Brand
        {
            get
            {
                return this._brand;
            }
            set
            {
                this._brand = value;
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
        ///         Property which gets or sets a value for PharmaForm.
        ///     </para>
        ///     <para>
        ///         Pharmaceutical form description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PharmaForm
        {
            get
            {
                return this._pharmaForm;
            }
            set
            {
                this._pharmaForm = value;
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
        ///         Property which gets or sets a value for ExternalPackName.
        ///     </para>
        ///     <para>
        ///         External pack name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ExternalPackName
        {
            get
            {
                return this._externalPackName;
            }
            set
            {
                this._externalPackName = value;
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
        ///         Property which gets or sets a value for InternalPackName.
        ///     </para>
        ///     <para>
        ///         Internal pack name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string InternalPackName
        {
            get
            {
                return this._internalPackName;
            }
            set
            {
                this._internalPackName = value;
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
        ///         Property which gets or sets a value for ContentUnitName.
        ///     </para>
        ///     <para>
        ///         Content unit name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ContentUnitName
        {
            get
            {
                return this._contentUnitName;
            }
            set
            {
                this._contentUnitName = value;
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
        ///         Property which gets or sets a value for WeightUnitName.
        ///     </para>
        ///     <para>
        ///         Weight unit name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string WeightUnitName
        {
            get
            {
                return this._weightUnitName;
            }
            set
            {
                this._weightUnitName = value;
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
        ///         Property which gets or set a value for Editions.
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation es participant in mora than one edition.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Editions
        {
            get
            {
                return this._editions;
            }
            set
            {
                this._editions = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value Image's presentation
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation has image
        ///     </para>
        /// </summary>
        [DataMember]
        public string Imagen
        {
            get
            {
                return this._Imagen;
            }
            set
            {
                this._Imagen = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a the name of Image's presentation
        ///     </para>
        ///     <para>
        ///         It indicates if the presentation has image
        ///     </para>
        /// </summary>
       
        [DataMember]
        public int ImageId
        {
            get
            {
                return this._imageId;
            }
            set
            {
                this._imageId = value;
            }
        }

        /// <summary>
        /// <para>
        /// Property which gets or sets the sizes currently associated with an image
        /// </para>
        /// <para>
        /// It indicates in what sizes the image has been uploaded
        /// </para>
        /// </summary>
        [DataMember]
        public string ImageSizes
        {
            get
            {
                return this._imageSizes;
            }
            set
            {
                this._imageSizes = value;
            }
        }
       

        /// <summary>
        /// <para>
        /// Property which gets or sets the sizes 
        /// </para>
        /// <para>
        /// It indicates in what sizes are in DB
        /// </para>
        /// </summary>
        [DataMember]
        public string Sizes
        {
            get
            {
                return this._sizes;
            }
            set
            {
                this._sizes = value;
            }
        }
        #endregion
       

        private int _presentationId;
        private int _divisionId;
        private string _divisionName;
        private string _divisionShortName;
        private int _categoryId;
        private string _categoryName;
        private int _productId;
        private string _brand;
        private int _pharmaFormId;
        private string _pharmaForm;
        private int? _qtyExternalPack;
        private int? _externalPackId;
        private string _externalPackName;
        private int? _qtyInternalPack;
        private int? _internalPackId;
        private string _internalPackName;
        private string _qtyContentUnit;
        private int? _contentUnitId;
        private string _contentUnitName;
        private string _qtyWeightUnit;
        private int? _weightUnitId;
        private string _weightUnitName;
        private string _presentation;
        private bool _active;
        private string _editions;
        private string _Imagen;
        private int _imageId;
        private string _imageSizes;
        private string _sizes;

    }
}
