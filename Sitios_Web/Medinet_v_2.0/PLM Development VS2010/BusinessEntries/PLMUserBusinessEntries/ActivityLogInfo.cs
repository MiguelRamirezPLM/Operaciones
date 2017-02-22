using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class ActivityLogInfo
    {
        #region Constructors

        public ActivityLogInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ActivityLogId
        {
            get { return this._activityLogId; }
            set { this._activityLogId = value; }
        }

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        [DataMember]
        public int TableId
        {
            get { return this._tableId; }
            set { this._tableId = value; }
        }

        [DataMember]
        public int OperationId
        {
            get { return this._operationId; }
            set { this._operationId = value; }
        }

        [DataMember]
        public DateTime Date
        {
            get { return this._date; }
            set { this._date = value; }
        }

        [DataMember]
        public string PrimaryKeyAffected
        {
            get { return this._primaryKeyAffected; }
            set { this._primaryKeyAffected = value; }
        }

        [DataMember]
        public string FieldsAffected
        {
            get { return this._fieldsAffected; }
            set { this._fieldsAffected = value; }
        }

        [DataMember]
        public string HashKey
        {
            get { return this._hashKey; }
            set { this._hashKey = value; }
        }

        #endregion


        private int _activityLogId;
        private int _userId;
        private int _tableId;
        private int _operationId;
        private DateTime _date;
        private string _primaryKeyAffected;
        private string _fieldsAffected;
        private string _hashKey;
        
    }
}
