using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class EditionPackSQLTextInfo
    {
        #region Constructors

        public EditionPackSQLTextInfo()
        {
            this._addedDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public int PackSqlTextId
        {
            get { return this._packSqlTextId; }
            set { this._packSqlTextId = value; }
        }

        [DataMember]
        public int PackUpdId
        {
            get { return this._packUpdId; }
            set { this._packUpdId = value; }
        }

        [DataMember]
        public string SqlText
        {
            get { return this._sqlText; }
            set { this._sqlText = value; }
        }

        [DataMember]
        public DateTime AddedDate
        {
            get { return this._addedDate; }
            set { this._addedDate = value; }
        }

        [DataMember]
        public int PackOrder
        {
            get { return this._packOrder; }
            set { this._packOrder = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _packSqlTextId;
        private int _packUpdId;
        private string _sqlText;
        private DateTime _addedDate;
        private int _packOrder;
        private bool _active;

    }
}
