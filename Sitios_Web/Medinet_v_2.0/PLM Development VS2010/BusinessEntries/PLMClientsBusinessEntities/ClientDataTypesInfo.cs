using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between the client and data types.
    /// </summary>
    [DataContract]
    public class ClientDataTypesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientDataTypesInfo class. Not receive parameters.
        /// </summary>
        public ClientDataTypesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ClientId.
        ///     </para>
        ///     <para>
        ///         Client identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ClientId
        {
            get
            {
                return this._clientId;
            }
            set
            {
                this._clientId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeId.
        ///     </para>
        ///     <para>
        ///         Code identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CodeId
        {
            get
            {
                return this._codeId;
            }
            set
            {
                this._codeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DeviceId.
        ///     </para>
        ///     <para>
        ///         Device identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte DeviceId
        {
            get
            {
                return this._deviceId;
            }
            set
            {
                this._deviceId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetId.
        ///     </para>
        ///     <para>
        ///         Target identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TargetId
        {
            get
            {
                return this._targetId;
            }
            set
            {
                this._targetId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DataTypeId.
        ///     </para>
        ///     <para>
        ///         Data type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte DataTypeId
        {
            get
            {
                return this._dataTypeId;
            }
            set
            {
                this._dataTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DataValue.
        ///     </para>
        ///     <para>
        ///         Data value.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DataValue
        {
            get
            {
                return this._dataValue;
            }
            set
            {
                this._dataValue = value;
            }
        }

        #endregion

        private int _clientId;
        private int _codeId;
        private byte _deviceId;
        private byte _targetId;
        private byte _dataTypeId;
        private string _dataValue;

    }
}
