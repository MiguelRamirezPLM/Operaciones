using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class RoleInfo
    {
        private int _roleId;
        private string _description;
        private bool _active;

        #region Constructors

        public RoleInfo() 
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
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active;}
            set { this._active = value;}
        }

        #endregion
    }
}
