using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class NickNamePrefixInfo
    {
        #region Constructors

        public NickNamePrefixInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int NickPrefixId
        {
            get { return this._nickPrefixId; }
            set { this._nickPrefixId = value; }
        }

        [DataMember]
        public int PrefixId
        {
            get { return this._prefixId; }
            set { this._prefixId = value; }
        }

        [DataMember]
        public string PrefixName
        {
            get { return this._prefixName; }
            set { this._prefixName = value; }
        }

        [DataMember]
        public int InitialValue
        {
            get { return this._initialValue; }
            set { this._initialValue = value; }
        }

        [DataMember]
        public int FinalValue
        {
            get { return this._finalValue; }
            set { this._finalValue = value; }
        }

        [DataMember]
        public int CurrentNumber
        {
            get { return this._currentNumber; }
            set { this._currentNumber = value; }
        }

        [DataMember]
        public string PrefixDescription
        {
            get { return this._prefixDescription; }
            set { this._prefixDescription = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _nickPrefixId;
        private int _prefixId;
        private string _prefixName;
        private int _initialValue;
        private int _finalValue;
        private int _currentNumber;
        private string _prefixDescription;
        private bool _active;

    }
}