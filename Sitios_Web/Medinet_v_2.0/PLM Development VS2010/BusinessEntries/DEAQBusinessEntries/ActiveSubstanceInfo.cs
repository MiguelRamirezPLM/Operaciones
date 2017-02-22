using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
    public class ActiveSubstanceInfo
    {

        #region Constructor

        public ActiveSubstanceInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ActiveSubstanceId
        {
            get
            {
                return this._activeSubstanceId;
            }
            set
            {
                this._activeSubstanceId = value;
            }
        }

        [DataMember]
        public string ActiveSubstanceName
        {
            get
            {
                return this._activeSubstanceName;
            }
            set
            {
                this._activeSubstanceName = value;
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

        private int _activeSubstanceId;
        private string _activeSubstanceName;
        private bool _active;

    }
}
