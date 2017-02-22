using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the PharmacovigilanceType information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class PharmacovigilanceTypeInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceTypeInfo class. Not receive parameters.
        /// </summary>
        public PharmacovigilanceTypeInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaTypeId.
        ///     </para>
        ///     <para>
        ///         PharmaType identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PharmaTypeId
        {
            get { return this._pharmaTypeId; }
            set { this._pharmaTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaType.
        ///     </para>
        ///     <para>
        ///         PharmaType name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PharmaType
        {
            get { return this._pharmaType; }
            set { this._pharmaType = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the PharmaType is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _pharmaTypeId;
        private string _pharmaType;
        private bool _active;
    }
}
