using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class ActivitySessionInfo
    {
        #region Constructors


        public ActivitySessionInfo() { }


        #endregion


        #region Properties

        [DataMember]
        public int ActivitySessionId
        {
            get { return _activitySessionId; }
            set { this._activitySessionId = value; }
        }

        [DataMember]
        public int ApplicationId
        {
            get { return _applicationId; }
            set { this._applicationId = value; }
        }

        [DataMember]
        public int UserId
        {
            get { return _userId; }
            set { this._userId = value; }
        }

        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set { this._date = value; }
        }


        #endregion


        private int _activitySessionId;
        private int _applicationId;
        private int _userId;
        private DateTime _date;



    }
}
