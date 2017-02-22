using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class ApplicationUserInfo
    {
        private int _userId;
        private int _applicationId;
        private int _roleId;

        #region Constructors

        public ApplicationUserInfo() 
        {

        }

        #endregion

        #region Properties

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId=value; }
        }

        [DataMember]
        public int ApplicationId
        {
            get { return this._applicationId; }
            set { this._applicationId=value; }
        }

        [DataMember]
        public int RoleId
        {
            get { return this._roleId; }
            set { this._roleId = value; }
        }

        #endregion

    }
}
