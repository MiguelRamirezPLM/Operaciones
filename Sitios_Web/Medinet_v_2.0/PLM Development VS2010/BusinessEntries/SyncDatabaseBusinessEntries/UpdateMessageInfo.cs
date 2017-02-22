using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class UpdateMessageInfo
    {
        #region Constructors

        public UpdateMessageInfo() 
        {
            this._executeDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public bool UpdateStatus
        {
            get { return this._updateStatus; }
            set { this._updateStatus = value; }
        }

        [DataMember]
        public string UpdateMessage
        {
            get { return this._updateMessage; }
            set { this._updateMessage = value; }
        }

        [DataMember]
        public DateTime ExecuteDate
        {
            get { return this._executeDate; }
            set { this._executeDate = value; }
        }

        #endregion

        private bool _updateStatus;
        private string _updateMessage;
        private DateTime _executeDate;

    }
}
