using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries
{
    [DataContract]
    public class StateInfo
    {

        #region Constructor

        public StateInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int StateId
        {
            get
            {
                return this._stateId;
            }
            set
            {
                this._stateId = value;
            }
        }

        [DataMember]
        public int CountryId
        {
            get
            {
                return this._countryId;
            }
            set
            {
                this._countryId = value;
            }
        }

        [DataMember]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _stateId;
        private int _countryId;
        private string _name;
        private bool _active;

    }
}
