using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace InfoLogsBusinessEntries
{

    /// <summary>
    ///     Class which represents the query sources of electronic tools.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class Info_SourceInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Info_SourceInfo class. Not receives parameters.
        /// </summary>
        public Info_SourceInfo() { }

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
            get
            {
                return this._sourceId;
            }
            set
            {
                this._sourceId = value;
            }
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
            get
            {
                return this._sourceName;
            }
            set
            {
                this._sourceName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the query Source is enabled or disabled.
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

        private byte _sourceId;
        private string _sourceName;
        private bool _active;

    }
}
