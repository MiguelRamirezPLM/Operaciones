using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class StateInfo
    {
        #region Constructors

        public StateInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int StateId
        {
            get
            {
                return this._StateId;
            }
            set
            {
                this._StateId = value;
            }
        }

        [DataMember]
        public byte CountryId
        {
            get
            {
                return this._CountryId;
            }
            set
            {
                this._CountryId = value;
                 
            }
        }

        [DataMember]
        public string StateName
        {
            get
            {
                return this._StateName;
            }
            set
            {
                this._StateName = value;
            }
        }

        [DataMember]
        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                this._Active = value;
            }
        }

        #endregion

        private int _StateId;

        private byte _CountryId;

        private string _StateName;

        private bool _Active;
    }
}
