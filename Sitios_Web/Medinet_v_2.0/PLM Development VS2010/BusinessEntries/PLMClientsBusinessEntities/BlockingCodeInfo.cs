using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Blocking Code information
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class BlockingCodeInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the BlockingCodeInfo class. Not receive parameters.
        /// </summary>
        public BlockingCodeInfo() {}
        
        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BlockingCodeId.
        ///     </para>
        ///     <para>
        ///         BlockingCode identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int BlockingCodeId
        {
            get { return this._blockingCodeId; }
            set { this._blockingCodeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BlockString.
        ///     </para>
        ///     <para>
        ///         BlockCode value.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BlockString
        {
            get { return this._blockString; }
            set { this._blockString = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the BlockingCode is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _blockingCodeId;
        private string _blockString;
        private bool _active;

    }
}
