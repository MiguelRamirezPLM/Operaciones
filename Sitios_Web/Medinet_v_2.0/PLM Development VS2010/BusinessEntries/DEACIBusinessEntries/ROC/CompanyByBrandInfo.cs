using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
    [DataContract]
   public  class CompanyByBrandInfo
   {
       #region Constructor

        public CompanyByBrandInfo() { }

       #endregion

       #region Properties

        [DataMember]
        public int BrandId
        {
            get { return this._brandId; }
            set { this._brandId = value; }
        }

        [DataMember]
        public int CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }

        [DataMember]
        public byte CompanyTypeId
        {
            get { return this._companyTypeId; }
            set { this._companyTypeId = value; }
        }

        [DataMember]
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }

        [DataMember]
        public string SubUrb
        {
            get { return this._suburb; }
            set { this._suburb = value; }
        }

        [DataMember]
        public string PostalCode
        {
            get { return this._postalCode; }
            set { this._postalCode = value; }
        }

        [DataMember]
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        [DataMember]
        public string Web
        {
            get { return this._web; }
            set { this._web = value; }
        }

        [DataMember]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }

        [DataMember]
        public string CompanyShortName
        {
            get { return this._companyShortName; }
            set { this._companyShortName = value; }
        }

        [DataMember]
        public int? Client_Id
        {
            get { return this._client_Id; }
            set { this._client_Id = value; }
        }

       #endregion

        private int _brandId;
        private int _companyId;
        private byte _companyTypeId;
        private string _address;
        private string _suburb;
        private string _postalCode;
        private string _email;
        private string _web;
        private string _companyName;
        private string _companyShortName;
        private int? _client_Id;
   }
}
