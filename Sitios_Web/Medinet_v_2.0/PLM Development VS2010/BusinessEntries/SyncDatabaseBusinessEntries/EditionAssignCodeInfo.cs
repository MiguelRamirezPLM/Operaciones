using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class EditionAssignCodeInfo
    {
        #region Constructors

        public EditionAssignCodeInfo() { }

        #endregion

        #region Properties
        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        [DataMember]
        public int AssignId
        {
            get { return this._assignId; }
            set { this._assignId = value; }
        }

        #endregion

        private int _editionId;
        private int _assignId;

    }
}
