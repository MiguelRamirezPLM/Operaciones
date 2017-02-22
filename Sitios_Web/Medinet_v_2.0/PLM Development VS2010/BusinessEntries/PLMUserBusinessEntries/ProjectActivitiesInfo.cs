using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class ProjectActivitiesInfo
    {
        #region Constructors

        public ProjectActivitiesInfo() 
        {
            _activityDate = Convert.ToDateTime("01/01/1900"); 
            _addDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public int ActivityId
        {
            get { return this._activityId; }
            set { this._activityId = value; }
        }

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProjectId.
        ///     </para>
        ///     <para>
        ///         Project identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProjectId
        {
            get { return this._projetId; }
            set { this._projetId = value; }
        }

        [DataMember]
        public int RoleId
        {
            get { return this._roleId; }
            set { this._roleId = value; }
        }

        [DataMember]
        public int BinnacleId
        {
            get { return this._binnacleId; }
            set { this._binnacleId = value; }
        }

        [DataMember]
        public DateTime AddDate
        {
            get { return this._addDate; }
            set { this._addDate = value; }
        }

        [DataMember]
        public DateTime ActivityDate
        {
            get { return this._activityDate; }
            set { this._activityDate = value; }
        }

        [DataMember]
        public byte InvestedTime
        {
            get { return this._investedTime; }
            set { this._investedTime = value; }
        }

        [DataMember]
        public string ActivityDescription
        {
            get { return this._activityDescription; }
            set { this._activityDescription = value; }
        }

        [DataMember]
        public string Comment
        {
            get { return this._comment; }
            set { this._comment = value; }
        }

        #endregion

        private int _activityId;
        private int _userId;
        private int _projetId;
        private int _roleId;
        private int _binnacleId;
        private DateTime _addDate;
        private DateTime _activityDate;
        private byte _investedTime;
        private string _activityDescription;
        private string _comment;    
    }
}
