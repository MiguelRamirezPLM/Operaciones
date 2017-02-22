using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Products and OMS Therapeutics.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.TherapeuticOMSInfo"/>
    [DataContract]
    public class ProductTherapeuticOMSInfo
    {
    
     #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductTherapeuticOMSInfo class. Not receive parameters.
        /// </summary>
        public ProductTherapeuticOMSInfo() { }

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
        ///         Property which gets or sets a value for TherapeuticOMSId.
        ///     </para>
        ///     <para>
        ///         Therapeutic OMS Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TherapeuticOMSId
        {
            get { return this._therapeuticOMSId; }
            set { this._therapeuticOMSId = value; }
        }

        #endregion

        private int _productId;
        private int _pharmaFormId;
        private int _therapeuticOMSId;
    }
}
