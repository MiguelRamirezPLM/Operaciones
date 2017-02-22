using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class ProjectInfo
    {
        /// <summary>
        ///     Initializes a new instance of the ProjectInfo class. 
        ///     Not receive parameters.
        /// </summary>
        #region Constructors
        public ProjectInfo()
        {
            this._initialDate = Convert.ToDateTime("01/01/1900");
            this.FinalDate = Convert.ToDateTime("01/01/1900");
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
            get  {  return this._projetId;  }
            set  { this._projetId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProjectName.
        ///     </para>
        ///     <para>
        ///         Project name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProjectName
        {
            get { return this._projectName; }
            set { this._projectName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProjectDescription.
        ///     </para>
        ///     <para>
        ///         Project Description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProjectDescription
        {
            get
            {
                return this._projectDescription;
            }
            set
            {
                this._projectDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InitialDate.
        ///     </para>
        ///     <para>
        ///         Initial Date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime InitialDate
        {
            get { return this._initialDate; }
            set { this._initialDate = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FinalDate.
        ///     </para>
        ///     <para>
        ///         Final Date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime FinalDate
        {
            get { return this._finalDate; }
            set { this._finalDate = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Project is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InvestedTime.
        ///     </para>
        ///     <para>
        ///         Indicates Invested Time on Project.
        ///     </para>
        /// </summary>
        [DataMember]
        public int InvestedTime
        {
            get { return this._investedTime; }
            set { this._investedTime = value; }
        }

        #endregion

        private int _projetId;
        private string _projectName;
        private string _projectDescription;
        private DateTime _initialDate;
        private DateTime _finalDate;
        private bool _active;
        private int _investedTime;
    }
}
