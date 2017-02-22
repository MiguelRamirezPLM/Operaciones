using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PSELogsBusinessEntities
{

    /// <summary>
    ///     Class which represents the search sources of medical information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class PSE_SourceInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PSE_SourceInfo class. Not receives parameters.
        /// </summary>
        public PSE_SourceInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SourceId.
        ///     </para>
        ///     <para>
        ///         Source identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte SourceId
        {
            get { return this._sourceId; }
            set { this._sourceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SourceName.
        ///     </para>
        ///     <para>
        ///         Source name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SourceName
        {
            get { return this._sourceName; }
            set { this._sourceName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the search source is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _sourceId;
        private string _sourceName;
        private bool _active;

    }
}
