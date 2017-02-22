using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class BinnaclesStatusNameInfo
    {
        #region Constructors

        public BinnaclesStatusNameInfo() 
        {
            _initialDateD = Convert.ToDateTime("01/01/1900");
            _finalDateD = Convert.ToDateTime("01/01/1900");
        }
        #endregion


        #region Properties

        [DataMember]
        public int BinnacleId
        {
            get { return this._binnacleId; }
            set { this._binnacleId = value; }
        }

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        [DataMember]
        public byte StatusId
        {
            get { return this._statusId; }
            set { this._statusId = value; }
        }

        [DataMember]
        public string BinnacleDescription
        {
            get { return this._binnacleDescription; }
            set { this._binnacleDescription = value; }
        }

        [DataMember]
        public DateTime InitialDateD
        {
            get { return this._initialDateD; }
            set { this._initialDateD = value; }
        }

        [DataMember]
        public DateTime FinalDateD
        {
            get { return this._finalDateD; }
            set { this._finalDateD = value; }
        }

        [DataMember]
        public string Comment
        {
            get { return this._comment; }
            set { this._comment = value; }
        }

        [DataMember]
        public string StatusName
        {
            get { return this._statusName; }
            set { this._statusName = value; }
        }

        #endregion

        private int _binnacleId;
        private int _userId;
        private byte  _statusId;
        private string _binnacleDescription;
        private DateTime _initialDateD;
        private DateTime _finalDateD;
        private string _comment;
        private string _statusName;
    }
}