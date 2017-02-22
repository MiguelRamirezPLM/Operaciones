using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class VersionInfo
    {
        #region Constructors

        public VersionInfo()
        {
            this._lastUpdate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public int VersionId
        {
            get { return this._versionId; }
            set { this._versionId = value; }
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
        public int PackCount
        {
            get { return this._packCount; }
            set { this._packCount = value; }
        }

        [DataMember]
        public string VersionNumber
        {
            get { return this._versionNumber; }
            set { this._versionNumber = value; }
        }

        [DataMember]
        public DateTime LastUpdate
        {
            get { return this._lastUpdate; }
            set { this._lastUpdate = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _versionId;
        private int _editionId;
        private int _dbId;
        private int _packCount;
        private string _versionNumber;
        private DateTime _lastUpdate;
        private bool _active;

    }
}
