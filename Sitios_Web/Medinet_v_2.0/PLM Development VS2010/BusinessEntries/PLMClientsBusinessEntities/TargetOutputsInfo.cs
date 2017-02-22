using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the information Output mediums.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class TargetOutputsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TargetOutputsInfo class. Not receive parameters.
        /// </summary>
        public TargetOutputsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetId.
        ///     </para>
        ///     <para>
        ///         Output medium identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TargetId
        {
            get
            {
                return this._targetId;
            }
            set
            {
                this._targetId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetName.
        ///     </para>
        ///     <para>
        ///         Output medium name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TargetName
        {
            get
            {
                return this._targetName;
            }
            set
            {
                this._targetName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Output medium is enabled or disabled.
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

        private byte _targetId;
        private string _targetName;
        private bool _active;

    }
}
