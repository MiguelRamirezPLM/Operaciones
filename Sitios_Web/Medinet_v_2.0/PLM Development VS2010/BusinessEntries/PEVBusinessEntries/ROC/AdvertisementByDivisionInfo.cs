using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
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
        private bool _active;
        private string _baseUrl;

    }
}
