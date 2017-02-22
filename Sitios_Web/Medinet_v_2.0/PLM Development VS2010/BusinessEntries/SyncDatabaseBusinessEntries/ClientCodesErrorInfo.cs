using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class ClientCodesErrorInfo
    {
        #region Constructors

        public ClientCodesErrorInfo()
        {
            this._errorDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public int ClientCodeErrorId
        {
            get { return this._clientCodeErrorId; }
            set { this._clientCodeErrorId = value; }
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
        public string ErrorMessage
        {
            get { return this._errorMessage; }
            set { this._errorMessage = value; }
        }

        [DataMember]
        public DateTime ErrorDate
        {
            get { return this._errorDate; }
            set { this._errorDate = value; }
        }

        [DataMember]
        public bool Confirmed
        {
            get { return this._confirmed; }
            set { this._confirmed = value; }
        }

        #endregion

        private int _clientCodeErrorId;
        private int _packSqlTextId;
        private string _codeString;
        private string _errorMessage;
        private DateTime _errorDate;
        private bool _confirmed;

    }
}
