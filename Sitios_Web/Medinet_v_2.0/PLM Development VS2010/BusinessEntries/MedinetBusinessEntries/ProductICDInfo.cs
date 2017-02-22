using System;
using System.Collections.Generic;
using System.Linq;
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
   public class ProductICDInfo
    {
       #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductTherapeuticInfo class. Not receive parameters.
        /// </summary>
       public ProductICDInfo() { }

        #endregion

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
       ///         Property which gets or sets a value for ICD.
       ///     </para>
       ///     <para>
       ///         ICD Form Identifier.
       ///     </para>
       /// </summary>
       [DataMember]
       public int ICDId
       {
           get { return this._icdId; }
           set { this._icdId = value; }
       }

       /// <summary>
       ///     <para>
       ///         Property which gets or sets a value for pharmaFormId.
       ///     </para>
       ///     <para>
       ///         Pharma Form Identifier.
       ///     </para>
       /// </summary>
       [DataMember]
       public int PharmaFormId
       {
           get { return this._pharmaFormId; }
           set { this._pharmaFormId  = value; }
       }


       private int _productId;
       private int _icdId;
       private int _pharmaFormId;
       
    }
}
