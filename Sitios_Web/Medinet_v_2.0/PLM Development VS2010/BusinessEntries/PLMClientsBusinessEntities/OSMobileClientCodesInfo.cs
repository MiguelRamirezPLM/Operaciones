using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between OSMobiles and Clients and Codes.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CodeInfo"/>
    [DataContract]
    public class OSMobileClientCodesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the OSMobileClientCodesInfo class. Not receive parameters.
        /// </summary>
        public OSMobileClientCodesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OSMobileId.
        ///     </para>
        ///     <para>
        ///         OSMobile identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte OSMobileId
        {
            get
            {
                return this._OSMobileId;
            }
            set
            {
                this._OSMobileId = value;
            }
        }

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
        ///         Property which gets or sets a value for IMEI.
        ///     </para>
        ///     <para>
        ///         International Mobile Equipment Identity.
        ///     </para>
        /// </summary>
        [DataMember]
        public string IMEI
        {
            get
            {
                return this._IMEI;
            }
            set
            {
                this._IMEI = value;
            }
        }

        #endregion

        private byte _OSMobileId;
        private int _clientId;
        private int _codeId;
        private string _IMEI;

    }
}
