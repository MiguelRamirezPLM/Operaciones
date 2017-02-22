using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a ICD.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ProductPharmaFormEncyclopediaInfo
    {
         #region Constructors

        ///<summary> 
        /// Initializes a new instance of the ICDinfo class.
        ///</summary>

        public ProductPharmaFormEncyclopediaInfo() { }

        #endregion

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaId.
        ///     </para>
        ///     <para>
        ///         Encyclopedia Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EncyclopediaId
        {

            get { return this._encyclopediaId; }
            set { this._encyclopediaId = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         PharmaForm Identifier.
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
        ///       Product Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductId
        {

            get { return this._productId; }
            set { this._productId = value; }

        }


        private int _encyclopediaId;
        private int _pharmaFormId;
        private int _productId;
        
    }
}
