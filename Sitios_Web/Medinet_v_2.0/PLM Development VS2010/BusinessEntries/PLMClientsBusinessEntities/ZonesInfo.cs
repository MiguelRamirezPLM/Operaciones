using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Zone information relationated with a Branch.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.BranchDetailInfo"/>
    [DataContract]
    public class ZonesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ZonesInfo class. Not receive parameters.
        /// </summary>
        public ZonesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZoneId.
        ///     </para>
        ///     <para>
        ///         Zone identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ZoneId
        {
            get
            {
                return this._zoneId;
            }
            set
            {
                this._zoneId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZoneName.
        ///     </para>
        ///     <para>
        ///         Zone name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ZoneName
        {
            get
            {
                return this._zoneName;
            }
            set
            {
                this._zoneName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Zone is enabled or disabled.
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

        private byte _zoneId;
        private string _zoneName;
        private bool _active;

    }
}
