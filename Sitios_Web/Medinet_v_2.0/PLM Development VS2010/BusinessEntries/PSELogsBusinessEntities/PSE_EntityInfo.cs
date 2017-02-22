using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PSELogsBusinessEntities
{

    /// <summary>
    ///     Class which represents the entities which correspond to medical information search.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class PSE_EntityInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PSE_EntityInfo class. Not receives parameters.
        /// </summary>
        public PSE_EntityInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EntityId.
        ///     </para>
        ///     <para>
        ///         Entity identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EntityId
        {
            get { return this._entityId; }
            set { this._entityId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EntityName.
        ///     </para>
        ///     <para>
        ///         Entity Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string EntityName
        {
            get { return this._entityName; }
            set { this._entityName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Entity is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _entityId;
        private string _entityName;
        private bool _active;

    }
}
