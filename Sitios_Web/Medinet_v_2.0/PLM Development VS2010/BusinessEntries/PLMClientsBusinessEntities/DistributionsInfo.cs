using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Distribution information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class DistributionsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DistributionsInfo class. Not receive parameters.
        /// </summary>
        public DistributionsInfo() 
        {
            this._lastUpdate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionId.
        ///     </para>
        ///     <para>
        ///         Distribution identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DistributionId
        {
            get
            {
                return this._distributionId;
            }
            set
            {
                this._distributionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionName.
        ///     </para>
        ///     <para>
        ///         Distribution name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DistributionName
        {
            get
            {
                return this._distributionName;
            }
            set
            {
                this._distributionName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Distribution description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Version.
        ///     </para>
        ///     <para>
        ///         Distribution version.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Version
        {
            get
            {
                return this._version;
            }
            set 
            {
                this._version = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LastUpdate.
        ///     </para>
        ///     <para>
        ///         Last update.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime LastUpdate
        {
            get
            {
                return this._lastUpdate;
            }
            set
            {
                this._lastUpdate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Distribution is enabled or disabled.
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

        private int _distributionId;
        private string _distributionName;
        private string _description;
        private string _version;
        private DateTime _lastUpdate;
        private bool _active;

    }
}
