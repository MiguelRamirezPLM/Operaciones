using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class BrandDetailInfo
    {
        #region Constructors

        public BrandDetailInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int BrandId
        {
            get { return this._brandId; }
            set { this._brandId = value; }
        }

        [DataMember]
        public string BrandName
        {
            get { return this._brandName; }
            set { this._brandName = value; }
        }

        [DataMember]
        public string Logo
        {
            get { return this._logo; }
            set { this._logo = value; }
        }

        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        [DataMember]
        public int ClientId
        {
            get { return this._clientId; }
            set { this._clientId = value; }
        }

        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion
        
        private int _brandId;
        private string _brandName;
        private string _logo;
        private string _baseUrl;
        private int _clientId;
        private int _editionId;
        private bool _active;

    }
}
