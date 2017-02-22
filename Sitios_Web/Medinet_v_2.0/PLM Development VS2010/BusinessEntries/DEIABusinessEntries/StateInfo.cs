using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries
{
    [DataContract]
    public class StateInfo
    {

        #region Construntors

        public StateInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int StateId
        {
            get { return this._stateId; }
            set { this._stateId = value; }
        }

        [DataMember]
        public byte  CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        [DataMember]
        public string StateName
        {
            get { return this._stateName; }
            set { this._stateName = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion




        private int _stateId;
        private byte  _countryId;
        private string _stateName;
        private bool _active;

    }

}
