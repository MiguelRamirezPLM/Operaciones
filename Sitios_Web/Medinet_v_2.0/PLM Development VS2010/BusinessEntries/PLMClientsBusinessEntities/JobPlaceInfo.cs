using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents information about the workplace of an customer.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class JobPlaceInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the JobPlaceInfo class. Not receive parameters.
        /// </summary>
        public JobPlaceInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for JobPlaceId.
        ///     </para>
        ///     <para>
        ///         Workplace identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte JobPlaceId
        {
            get { return this._jobPlaceId; }
            set { this._jobPlaceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for JobPlaceName.
        ///     </para>
        ///     <para>
        ///         Workplace name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string JobPlaceName
        {
            get { return this._jobPlaceName; }
            set { this._jobPlaceName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Workplace is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }
        
        #endregion

        private byte _jobPlaceId;
        private string _jobPlaceName;
        private bool _active;

    }
}
