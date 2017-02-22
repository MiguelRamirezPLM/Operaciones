using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class RegionInfo
    {
        private int _regionId;
        private string _description;
        private bool _active;

        #region Constructors

        public RegionInfo() 
        {

        }

        #endregion

        #region Properties

        [DataMember]
        public int RegionId
        {
            get { return this._regionId; }
            set { this._regionId = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

    }
}
