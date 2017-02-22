using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
    [DataContract]
   public class CompanyInfo
   {
       #region Constructor

       public CompanyInfo() { }
       
       #endregion
   


    #region Properties
        
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
        public int? CompanyIdParent
        {
            get { return this._companyIdParent; }
            set { this._companyIdParent = value; }
        }

        [DataMember]
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }

        [DataMember]
        public string Suburb
        {
            get { return this._suburb; }
            set { this._suburb = value; }
        }

        [DataMember]
        public string Ubication
        {
            get { return this._ubication; }
            set { this._ubication = value; }
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
        public string Contact
        {
            get { return this._contact; }
            set { this._contact = value; }
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
        public int? CityId
        {
            get { return this._cityId; }
            set { this._cityId = value; }
        }

        [DataMember]
        public int? ClientID
        {
            get { return this._client_ID; }
            set { this._client_ID = value; }
       
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember]
        public string CityName
        {
            get { return this._cityName; }
            set { this._cityName = value; }
        }

        [DataMember]
        public int StateId
        {
            get { return this._stateId; }
            set { this._stateId = value; }
        }

        [DataMember]
        public string StateName
        {
            get { return this._stateName; }
            set { this._stateName = value; }
        }


        [DataMember]
        public int Total
        {
            get { return this._total; }
            set { this._total = value; }
        }

        [DataMember]
        public int RowNumber
        {
            get { return this._rowNumber; }
            set { this._rowNumber = value; }
        }

        #endregion

        private int _companyId;
        private byte _companyTypeId;
        private int? _companyIdParent;
        private string _address;
        private string _suburb;
        private string _ubication;
        private string _postalCode;
        private string _email;
        private string _web;
        private string _contact;
        private string _companyName;
        private string _companyShortName;
        private int? _cityId;
        private int? _client_ID;
        private bool _active;
        private string _cityName;
        private int _stateId;
        private string _stateName;
        private int _total;
        private int _rowNumber;

    }


}
