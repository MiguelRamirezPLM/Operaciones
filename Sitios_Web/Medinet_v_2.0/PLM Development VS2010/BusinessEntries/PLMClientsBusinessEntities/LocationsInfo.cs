using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Location information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class LocationsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the LocationsInfo class. Not receive parameters.
        /// </summary>
        public LocationsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LocationId.
        ///     </para>
        ///     <para>
        ///         Location identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int LocationId
        {
            get
            {
                return this._locationId;
            }
            set
            {
                this._locationId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LocationName.
        ///     </para>
        ///     <para>
        ///         Location name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string LocationName
        {
            get
            {
                return this._locationName;
            }
            set
            {
                this._locationName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Location is enabled or disabled.
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

        private int _locationId;
        private string _locationName;
        private bool _active;
    }
}
