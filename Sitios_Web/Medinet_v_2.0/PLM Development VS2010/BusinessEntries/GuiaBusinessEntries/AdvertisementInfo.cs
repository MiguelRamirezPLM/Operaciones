using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class AdvertisementInfo
    {
        #region Constructors

        public AdvertisementInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int AdvertId
        {
            get { return this._advertId; }
            set { this._advertId = value; }
        }

        [DataMember]
        public int ClientId
        {
            get { return this._clientId; }
            set { this._clientId = value; }
        }

        [DataMember]
        public string AdvertName
        {
            get { return this._adverName; }
            set { this._adverName = value; }
        }

        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        [DataMember]
        public int Orden
        {
            get { return this._orden; }
            set { this._orden = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember]
        public int AdvertTypeId
        {
            get { return this._advertTypeId; }
            set { this._advertTypeId = value; }
        }



        #endregion

        private int _advertId;
        private int _clientId;
        private string _adverName;
        private string _baseUrl;
        private int _orden;
        private bool _active;
        private int _advertTypeId;

    }
}
