using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{ 
    [DataContract]
    public class SeedInfo
    {
        #region Constructor

        public SeedInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int SeedId
        {
            get { return this._seedId; }
            set { this._seedId = value; }
        }

        [DataMember]
        public string SeedName
        {
            get { return this._seedName; }
            set { this._seedName = value; }
        }

        [DataMember]
        public string ScientificName
        {
            get { return this._scientificName; }
            set { this._scientificName = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _seedId;
        private string _seedName;
        private string _scientificName;
        private bool _active;
    }
}
