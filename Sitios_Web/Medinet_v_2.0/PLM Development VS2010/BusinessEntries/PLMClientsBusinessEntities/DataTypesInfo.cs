using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the data types that a client can save.
    /// </summary>
    [DataContract]
    public class DataTypesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DataTypesInfo class. Not receive parameters.
        /// </summary>
        public DataTypesInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for TypeName.
        ///     </para>
        ///     <para>
        ///         Data type name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the data type is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private byte _dataTypeId;
        private string _typeName;
        private bool _active;

    }
}
