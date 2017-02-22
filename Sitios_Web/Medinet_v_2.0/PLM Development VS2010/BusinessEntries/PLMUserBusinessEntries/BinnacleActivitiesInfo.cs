using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class BinnacleActivitiesInfo
    {
        #region Constructors

        public BinnacleActivitiesInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int BinnacleId
        {
            get { return this._binnacleId; }
            set { this._binnacleId = value; }
        }

        [DataMember]
        public int ActivityId
        {
            get { return this._activityId; }
            set { this._activityId = value; }
        }

        [DataMember]
        public string Comment
        {
            get { return this._comment; }
            set { this._comment = value; }
        }

        #endregion

        private int _activityId;
        private int _binnacleId;
        private string _comment;
    }
}


