using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between the interaction's product and pharmacological group.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <see cref="MedinetBusinessEntries.ProductContentsInfo"/>
    /// <see cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <see cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <see cref="MedinetBusinessEntries.PharmacologicalGroupsInfo"/>
    [DataContract]
    public class PharmaGroupInteractionsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmaGroupInteractionsInfo class. Not receive parameters.
        /// </summary>
        public PharmaGroupInteractionsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductContentId.
        ///     </para>
        ///     <para>
        ///         Product content's identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductContentId
        {
            get
            {
                return this._productContentId;
            }
            set
            {
                this._productContentId = value;
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
        ///         Property which gets or sets a value for ActiveSubstanceId.
        ///     </para>
        ///     <para>
        ///         Substance identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ActiveSubstanceId
        {
            get
            {
                return this._activeSubstanceId;
            }
            set
            {
                this._activeSubstanceId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaGroupId.
        ///     </para>
        ///     <para>
        ///         Pharmacological group identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaGroupId
        {
            get
            {
                return this._pharmaGroupId;
            }
            set
            {
                this._pharmaGroupId = value;
            }
        }

        #endregion

        private int _productContentId;
        private int _productId;
        private int _activeSubstanceId;
        private int _pharmaGroupId;

    }
}
