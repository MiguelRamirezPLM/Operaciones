using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the ProductSymptom information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ProductSymptomInfo
    {
   
        #region Constructors

        /// <summary>
        ///     Constructor that initializes the properties. Not receive parameters.
        /// </summary>
        public ProductSymptomInfo()
        {
            this._symptomId = -1;
            this._productId = -1;
            this._pharmaFormId = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SymptomId.
        ///     </para>
        ///     <para>
        ///         Symptom Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SymptomId
        {
            get { return this._symptomId; }
            set { this._symptomId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Symptom.
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
        ///         Property which gets or sets a value for pharmaFormId.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaFormId
        {
            get { return this._pharmaFormId; }
            set { this._pharmaFormId = value; }
        }

        #endregion

        private int _symptomId;
        private int _productId;
        private int _pharmaFormId;
    }
}
