
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class ProjectActivitiesTimeInfo
    {
        #region Constructors

        public ProjectActivitiesTimeInfo() 
        {
        }

        #endregion

        #region Properties

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
        public int TotalTime
        {
            get { return this._totalTime; }
            set { this._totalTime = value; }
        }

        [DataMember]
        public string ProjectName
        {
            get { return this._projectName; }
            set { this._projectName = value; }
        }

        #endregion

        private string _projectName;
        private int _projetId;
        private int _totalTime;  
    }
}