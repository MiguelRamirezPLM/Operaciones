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
    public class SourceInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SourceInfo class. Not receive parameters.
        /// </summary>
        public SourceInfo() { }

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
        ///         Property which gets or sets a value for Source.
        ///     </para>
        ///     <para>
        ///         Source name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Source
        {
            get { return this._source; }
            set { this._source = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Source is enabled or disabled.
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
        private string _source;
        private bool _active;
    }
}
