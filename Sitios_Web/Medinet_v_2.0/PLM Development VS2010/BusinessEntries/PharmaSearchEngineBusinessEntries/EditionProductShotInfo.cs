using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the Edition, Laboratory, Category, Pharmaceutical Form and ProductShot of a Product.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         An product shot is the image of a product which participates in an edition.
    ///     </para>
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class EditionProductShotInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EditionProductShotInfo class. Not receive parameters.
        /// </summary>
        public EditionProductShotInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionProductShotId.
        ///     </para>
        ///     <para>
        ///         Product Shot Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionProductShotId
        {
            get { return this._editionProductShotId; }
            set { this._editionProductShotId = value; }
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
            get { return this._editionId; }
            set { this._editionId = value; }
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
        ///         Property which gets or sets a value for PSTypeId.
        ///     </para>
        ///     <para>
        ///         Type of Product Shot. It can be alone or with family.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PSTypeId
        {
            get { return this._psTypeId; }
            set { this._psTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductShot.
        ///     </para>
        ///     <para>
        ///         Image of the Product Shot.
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
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product Shot is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
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
        private bool _active;
    }
}
