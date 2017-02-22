using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries
{
    [DataContract]
   public  class AdvertisementInfo
   {
       #region Constuctor

        public AdvertisementInfo() { }

       #endregion

       #region Properties

        [DataMember]
        public int AdvId
        {
            get { return this._advId; }
            set { this._advId = value; }
        }

        [DataMember]
        public int CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }

        [DataMember]
        public string HiredSpace
        {
            get { return this._hiredSpace; }
            set { this._hiredSpace = value; }
        }

        [DataMember]
        public string AdvFile
        {
            get { return this._advFile; }
            set { this._advFile = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

       #endregion

        private int _advId;
        private int _companyId;
        private string _hiredSpace;
        private string _advFile;
        private bool _active;

   }

   

    
}
