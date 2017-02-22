using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Source information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class PharmacovigilanceSourceInfoTypeInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceSourceInfoTypeInfo class. Not receive parameters.
        /// </summary>
        public PharmacovigilanceSourceInfoTypeInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaSourceInfoTypeId.
        ///     </para>
        ///     <para>
        ///         PharmaSourceInfoType identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PharmaSourceInfoTypeId
        {
            get { return this._pharmaSourceInfoTypeId; }
            set { this._pharmaSourceInfoTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaSourceInfo.
        ///     </para>
        ///     <para>
        ///         PharmaSourceInfo name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PharmaSourceInfo
        {
            get { return this._pharmaSourceInfo; }
            set { this._pharmaSourceInfo = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the PharmaSourceInfo is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _pharmaSourceInfoTypeId;
        private string _pharmaSourceInfo;
        private bool _active;
    }
}
