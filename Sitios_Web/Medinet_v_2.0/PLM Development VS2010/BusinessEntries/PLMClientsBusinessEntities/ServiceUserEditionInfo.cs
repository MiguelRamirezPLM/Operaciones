using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class ServiceUserEditionInfo
    {
        #region Constructors

        public ServiceUserEditionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
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
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _userId;
        private int _editionId;
        private DateTime _initialDate;
        private DateTime _finalDate;
        private bool _active;

    }
}
