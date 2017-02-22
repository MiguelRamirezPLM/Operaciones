using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EncyclopediasBussinesEntries
{
    /// <summary>
    ///     Class which represents information about a EncyclopediasICD.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class EncyclopediaICDInfo
    {
        #region Constructors

        ///<summary> 
        /// Initializes a new instance of the EncyclopediasICDInfo class.
        ///</summary>
        public EncyclopediaICDInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ICDId.
        ///     </para>
        ///     <para>
        ///         ICD Identifier.
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
        ///         Property which gets or sets a value for ICDKey.
        ///     </para>
        ///     <para>
        ///         ICDKey for the ICD.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ICDKey
        {

            get { return this._icdKey; }
            set { this._icdKey = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ICDName.
        ///     </para>
        ///     <para>
        ///         Name for the ICD.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ICDName
        {

            get { return this._icdName; }
            set { this._icdName = value; }

        }

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
        ///         Property which gets or sets a value for EncyclopediaName.
        ///     </para>
        ///     <para>
        ///         Encyclopedia Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string EncyclopediaName
        {

            get { return this._encyclopediaName; }
            set { this._encyclopediaName = value; }

        }

        #endregion

        private int _icdId;
        private string _icdKey;
        private string _icdName;
        private int _encyclopediaId;
        private string _encyclopediaName;
        
    }
}
