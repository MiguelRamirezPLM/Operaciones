using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
    [DataContract]
    public class AdvertisementByEditionInfo
    {

        #region Constructor

        public AdvertisementByEditionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int AdvId
        {
            get
            {
                return this._advId;
            }
            set
            {
                this._advId = value;
            }
        }

        [DataMember]
        public string HiredSpace
        {
            get
            {
                return this._hiredSpace;
            }
            set
            {
                this._hiredSpace = value;
            }
        }

        [DataMember]
        public string AdvFile
        {
            get
            {
                return this._advFile;
            }
            set
            {
                this._advFile = value;
            }
        }

        [DataMember]
        public int CompanyId
        {
            get
            {
                return this._companyId;
            }
            set
            {
                this._companyId = value;
            }
        }

        [DataMember]
        public int EditionId
        {
            get
            {
                return this._editionId;
            }
            set
            {
                this._editionId = value;
            }
        }

        #endregion

        private int _advId;
        private string _hiredSpace;
        private string _advFile;
        private int _companyId;
        private int _editionId;

    }
}
