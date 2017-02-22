using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
    public class CropInfo
    {
        #region Constructor

        public CropInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int CropId
        {
            get { return this._cropId; }
            set { this._cropId = value; }
        }

        [DataMember]
        public string CropName
        {
            get { return this._cropName; }
            set { this._cropName = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember]
        public string ScientificName
        {
            get { return this._scientificName; }
            set { this._scientificName = value; }
        }
        #endregion

        private int _cropId;
        private string _cropName;
        private string _scientificName;
        private bool _active;
    }
}
