using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries
{
    [DataContract]
    public class SpeciesBySpecieIdInfo
    {
        #region Constructor

        public SpeciesBySpecieIdInfo() { }
        
        #endregion

        #region Properties

        [DataMember]
        public int SpecieId
        {
            get
            {
                return this._specieId;
            }
            set
            {
                this._specieId = value;
            }
        }

        [DataMember]
        public string SpecieName
        {
            get
            {
                return this._specieName;
            }
            set
            {
                this._specieName = value;
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

        private int _specieId;
        private string _specieName;
        private bool _active;
    }
}
