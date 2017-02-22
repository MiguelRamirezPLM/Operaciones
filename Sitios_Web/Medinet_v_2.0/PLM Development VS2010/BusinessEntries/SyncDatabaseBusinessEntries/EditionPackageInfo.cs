using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class EditionPackageInfo
    {
        #region Constructors

        public EditionPackageInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int PackUpdId
        {
            get { return this._packUpdId; }
            set { this._packUpdId = value; }
        }

        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        [DataMember]
        public int DbId
        {
            get { return this._dbId; }
            set { this._dbId = value; }
        }

        [DataMember]
        public byte TableId
        {
            get { return this._tableId; }
            set { this._tableId = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }


        #endregion

        private int _packUpdId;
        private int _editionId;
        private int _dbId;
        private byte _tableId;
        private bool _active;
    }
}
