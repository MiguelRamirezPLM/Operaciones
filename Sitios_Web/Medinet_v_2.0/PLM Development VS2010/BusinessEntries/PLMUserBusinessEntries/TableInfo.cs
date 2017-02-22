using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class TableInfo
    {
        #region Constructors

        public TableInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int TableId
        {
            get { return this._tableId; }
            set { this._tableId = value; }
        }

        [DataMember]
        public int ApplicationId
        {
            get { return this._applicationId; }
            set { this._applicationId = value; }
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


        private int _tableId;
        private int _applicationId;
        private string _description;
        private bool _active;


    }
}
