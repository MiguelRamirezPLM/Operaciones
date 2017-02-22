using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class TableInfo
    {
        #region Constructors

        public TableInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public byte TableId
        {
            get { return this._tableId; }
            set { this._tableId = value; }
        }

        [DataMember]
        public string TableName
        {
            get { return this._tableName; }
            set { this._tableName = value; }
        }

        [DataMember]
        public string TableDescription
        {
            get { return this._tableDescription; }
            set { this._tableDescription = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _tableId;
        private string _tableName;
        private string _tableDescription;
        private bool _active;

    }
}
