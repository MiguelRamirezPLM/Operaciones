using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{

    /// <summary>
    ///     Class which indicates if the product is in photographic identification system.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class EditionProductShotsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EditionProductShotsInfo class. Not receive parameters.
        /// </summary>
        public EditionProductShotsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionProductShotId.
        ///     </para>
        ///     <para>
        ///         Product shot Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionProductShotId
        {
            get
            {
                return this._editionProductShotId;
            }
            set
            {
                this._editionProductShotId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         Edition Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get
            {
                return this._editionId;
            }
            set
            {
                this._editionId = value;
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
        ///         Category Identifier.
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
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         Pharmaceutical Form Identifier.
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
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product Identifier.
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
        ///         Property which gets or sets a value for PSTypeId.
        ///     </para>
        ///     <para>
        ///         Product shot type.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PSTypeId
        {
            get
            {
                return this._psTypeId;
            }
            set
            {
                this._psTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductShot.
        ///     </para>
        ///     <para>
        ///         Product image.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProductShot
        {
            get
            {
                return this._productShot;
            }
            set
            {
                this._productShot = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QtyCells.
        ///     </para>
        ///     <para>
        ///         Cell quantity.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte? QtyCells
        {
            get
            {
                return this._qtyCells;
            }
            set
            {
                this._qtyCells = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the product shot is enabled or disabled.
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

        #endregion

        private int _editionProductShotId;
        private int _editionId;
        private int _divisionId;
        private int _categoryId;
        private int _pharmaFormId;
        private int _productId;
        private byte _psTypeId;
        private string _productShot;
        private byte? _qtyCells;
        private bool _active;

    }
}
