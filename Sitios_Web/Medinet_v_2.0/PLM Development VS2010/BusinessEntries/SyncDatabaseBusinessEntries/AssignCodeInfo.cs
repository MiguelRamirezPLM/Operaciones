using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class AssignCodeInfo
    {
        #region Constructors

        public AssignCodeInfo() 
        {
            this._addedDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public int AssignId
        {
            get { return this._assignId; }
            set { this._assignId = value; }
        }

        [DataMember]
        public string CodeString
        {
            get { return this._codeString; }
            set { this._codeString = value; }
        }

        [DataMember]
        public DateTime AddedDate
        {
            get { return this._addedDate; }
            set { this._addedDate = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember]
        public bool AllowUpdates
        {
            get { return this._allowUpdates; }
            set { this._allowUpdates = value; }
        }
        
        #endregion

        private int _assignId;
        private string _codeString;
        private DateTime _addedDate;
        private bool _active;
        private bool _allowUpdates;
    }
}
