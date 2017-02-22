using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries.ROC
{
    [DataContract]
    public class AdvertisementByDivisionInfo
    {

        #region Constructor

        public AdvertisementByDivisionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int AdId
        {
            get
            {
                return this._adId;
            }
            set
            {
                this._adId = value;
            }
        }

        [DataMember]
        public string AdFile
        {
            get
            {
                return this._adFile;
            }
            set
            {
                this._adFile = value;
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

        [DataMember]
        public int DivisionId
        {
            get
            {
                return this._divisionId;
            }
            set
            {
                this._divisionId = value;
            }
        }

        [DataMember]
        public string BaseUrl
        {
            get
            {
                return this._baseUrl;
            }
            set
            {
                this._baseUrl = value;
            }
        }

        #endregion

        private int _adId;
        private string _adFile;
        private int _editionId;
        private int _divisionId;
        private string _baseUrl;

    }
}
