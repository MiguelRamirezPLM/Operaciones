using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Products and Therapeutics.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.TherapeuticInfo"/>
    [DataContract]
    public class ProductTherapeuticInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductTherapeuticInfo class. Not receive parameters.
        /// </summary>
        public ProductTherapeuticInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for TherapeuticId.
        ///     </para>
        ///     <para>
        ///         Therapeutic Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TherapeuticId
        {
            get { return this._therapeuticId; }
            set { this._therapeuticId = value; }
        }

        #endregion

        private int _productId;
        private int _pharmaFormId;
        private int _therapeuticId;
    }
}
