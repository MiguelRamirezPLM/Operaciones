using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class UserProjectInfo
    {

        #region Constructors

        public UserProjectInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProjectId.
        ///     </para>
        ///     <para>
        ///         Project identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProjectId
        {
            get { return this._projetId; }
            set { this._projetId = value; }
        }

        [DataMember]
        public int RoleId
        {
            get { return this._roleId; }
            set { this._roleId = value; }
        }
        #endregion

        private int _userId;
        private int _projetId;
        private int _roleId;
    }
}
