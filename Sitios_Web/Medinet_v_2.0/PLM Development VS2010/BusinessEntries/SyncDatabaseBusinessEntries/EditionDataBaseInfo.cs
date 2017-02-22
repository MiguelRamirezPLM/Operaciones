using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class EditionDataBaseInfo
    {
        #region Constructors

        public EditionDataBaseInfo() { }

        #endregion

        #region Properties

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

        #endregion

        private int _editionId;
        private int _dbId;

    }
}
