using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class DatabaseInfo
    {
        #region Constructors

        public DatabaseInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int DbId
        {
            get { return this._dbId; }
            set { this._dbId = value; }
        }

        [DataMember]
        public string DbName
        {
            get { return this._dbName; }
            set { this._dbName = value; }
        }

        [DataMember]
        public string DbCode
        {
            get { return this._dbCode; }
            set { this._dbCode = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _dbId;
        private string _dbName;
        private string _dbCode;
        private bool _active;

    }
}
