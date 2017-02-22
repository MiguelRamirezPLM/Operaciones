using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class BinnaclesInfo
    {
        #region Constructors

        public BinnaclesInfo() 
        {
            _initialDate = Convert.ToDateTime("01/01/1900");
            _finalDate = Convert.ToDateTime("01/01/1900");
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
        public DateTime InitialDate
        {
            get { return this._initialDate; }
            set { this._initialDate = value; }
        }

        [DataMember]
        public DateTime FinalDate
        {
            get { return this._finalDate; }
            set { this._finalDate = value; }
        }

        [DataMember]
        public string Comment
        {
            get { return this._comment; }
            set { this._comment = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _binnacleId;
        private int _userId;
        private byte  _statusId;
        private string _binnacleDescription;
        private DateTime _initialDate;
        private DateTime _finalDate;
        private string _comment;
        private bool _active;
    }
}