using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the activity types that a client can do.
    /// </summary>
    [DataContract]
    public class ActivityTypesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ActivityTypesInfo class. Not receive parameters.
        /// </summary>
        public ActivityTypesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActivityTypeId.
        ///     </para>
        ///     <para>
        ///         Activity type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ActivityTypeId
        {
            get
            {
                return this._activityTypeId;
            }
            set
            {
                this._activityTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeName.
        ///     </para>
        ///     <para>
        ///         Activity type name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the activity is enabled or disabled.
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

        private byte _activityTypeId;
        private string _typeName;
        private bool _active;

    }
}
