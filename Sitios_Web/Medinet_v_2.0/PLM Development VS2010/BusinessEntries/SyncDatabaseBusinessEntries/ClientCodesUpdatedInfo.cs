using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class ClientCodesUpdatedInfo
    {
        #region Constructors

        public ClientCodesUpdatedInfo()
        {
            this._updateDate = Convert.ToDateTime("01/01/1900");
            this._sentDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public int ClientCodeUpdId
        {
            get { return this._clientCodeUpdId; }
            set { this._clientCodeUpdId = value; }
        }

        [DataMember]
        public int PackSqlTextId
        {
            get { return this._packSqlTextId; }
            set { this._packSqlTextId = value; }
        }

        [DataMember]
        public string CodeString
        {
            get { return this._codeString; }
            set { this._codeString = value; }
        }

        [DataMember]
        public DateTime UpdateDate
        {
            get { return this._updateDate; }
            set { this._updateDate = value; }
        }

        [DataMember]
        public DateTime SentDate
        {
            get { return this._sentDate; }
            set { this._sentDate = value; }
        }

        [DataMember]
        public bool Confirmed
        {
            get { return this._confirmed; }
            set { this._confirmed = value; }
        }

        #endregion

        private int _clientCodeUpdId;
        private int _packSqlTextId;
        private string _codeString;
        private DateTime _sentDate;
        private DateTime _updateDate;
        private bool _confirmed;

    }
}
