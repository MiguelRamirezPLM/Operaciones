using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries
{
    [DataContract]
    public class CityInfo
    {

        #region Constructor

        public CityInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int CityId
        {
            get
            {
                return this._cityId;
            }
            set
            {
                this._cityId = value;
            }
        }

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

        private int _cityId;
        private int _stateId;
        private string _name;
        private bool _active;

    }
}
