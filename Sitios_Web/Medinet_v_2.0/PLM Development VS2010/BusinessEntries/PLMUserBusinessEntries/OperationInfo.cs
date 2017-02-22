using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class OperationInfo
    {
        private int _operationId;
        private string _description;
        private bool _active;

        #region Constructors

        public OperationInfo() 
        {

        }

        #endregion

        #region Properties
        
        [DataMember]
        public int OperationId
        {
            get { return this._operationId; }
            set { this._operationId = value; }
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
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion
    }
}
