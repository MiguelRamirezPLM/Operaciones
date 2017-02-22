using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Product, Edition and Therapeutic Group.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.TherapeuticGroupInfo"/>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    [DataContract]
    public class TherapeuticGroupProductInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TherapeuticGroupProductInfo class. Not receive parameters.
        /// </summary>
        public TherapeuticGroupProductInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TheraGroupId.
        ///     </para>
        ///     <para>
        ///         Therapeutic Group Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TheraGroupId
        {
            get { return this._theraGroupId; }
            set { this._theraGroupId = value; }
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

        #endregion

        private int _theraGroupId;
        private int _editionId;
        private int _pharmaFormId;
        private int _productId;
    }
}
