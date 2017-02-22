using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class OperationRolesInfo
    {
        private int _roleId;
        private int _operationId;

        #region Constructors

        public OperationRolesInfo()
        {

        }

        #endregion

        #region Properties

        [DataMember]
        public int RoleId
        {
            get { return this._roleId; }
            set { this._roleId = value; }
        }

        [DataMember]
        public int OperationId
        {
            get { return this._operationId; }
            set { this._operationId = value; }
        }

        #endregion

    }
}
