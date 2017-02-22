using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Consequence information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class ConsequenceInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ConsequenceInfo class. Not receive parameters.
        /// </summary>
        public ConsequenceInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ConsequenceId.
        ///     </para>
        ///     <para>
        ///         Consequence identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ConsequenceId
        {
            get { return this._consequenceId; }
            set { this._consequenceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Consequence.
        ///     </para>
        ///     <para>
        ///         Consequence name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Consequence
        {
            get { return this._consequence; }
            set { this._consequence = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Consequence is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _consequenceId;
        private string _consequence;
        private bool _active;
    }
}
