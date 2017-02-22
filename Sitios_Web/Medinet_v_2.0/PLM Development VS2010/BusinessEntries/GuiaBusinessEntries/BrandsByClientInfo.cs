using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class BrandsByClientInfo
    {
        #region Constructors

        public BrandsByClientInfo() { }

        #endregion


        #region Properties

        [DataMember]
        public int Total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
            }

        }

        [DataMember]
        public int BrandId
        {
            get
            {
                return this._brandId;
            }
            set
            {
                this._brandId = value;
            }

        }

        [DataMember]
        public string BrandName
        {
            get
            {
                return this._brandName;
            }
            set
            {
                this._brandName = value;
            }

        }

        [DataMember]
        public string Logo
        {
            get
            {
                return this._logo;
            }
            set
            {
                this._logo = value;
            }

        }

        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        [DataMember]
        public int RowNumber
        {
            get
            {
                return this._rowNumber;
            }
            set
            {
                this._rowNumber = value;
            }

        }

        [DataMember]
        public int ClientId
        {
            get
            {
                return this._clientId;
            }
            set
            {
                this._clientId = value;
            }
        }

        [DataMember]
        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                this._companyName = value;
            }
        }

        #endregion

        private int _total;
        private int _brandId;
        private string _brandName;
        private string _logo;
        private string _baseUrl;
        private int _rowNumber;
        private int _clientId;
        private string _companyName;

    }
}
