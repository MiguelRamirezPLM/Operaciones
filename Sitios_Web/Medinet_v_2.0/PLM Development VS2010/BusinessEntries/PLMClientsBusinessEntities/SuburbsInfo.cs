using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Suburb information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class SuburbsInfo
    {
         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SuburbsInfo class. Not receive parameters.
        /// </summary>
        public SuburbsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SuburbId.
        ///     </para>
        ///     <para>
        ///         Suburb identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SuburbId
        {
            get
            {
                return this._suburbId;
            }
            set
            {
                this._suburbId = value;
            }
        }
    
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SuburbName.
        ///     </para>
        ///     <para>
        ///         Suburb name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SuburbName
        {
            get
            {
                return this._SuburbName;
            }
            set
            {
                this._SuburbName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Suburb is enabled or disabled.
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

        private int _suburbId;
        private string _SuburbName;
        private bool _active;
    }
}
