using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries.ROC
{
    [DataContract]
    public  class DivisionByCountryInfo
    {
        #region Constructor

        public DivisionByCountryInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        [DataMember]
        public string DivisionName
        {
            get { return this._divisionName; }
            set { this._divisionName = value; }
        }

        [DataMember]
        public string ShortName
        {
            get { return this._shortName; }
            set { this._shortName = value; }
        }

        [DataMember]
        public int? LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

        [DataMember]
        public int? CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember]
        public int DivisionInformationId
        {
            get { return this._divisionInformationId; }
            set { this._divisionInformationId = value; }
        }

        [DataMember]
        public string Image
        {
            get { return this._image; }
            set { this._image = value; }
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
        public string Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        [DataMember]
        public string ZipCode
        {
            get { return this._zipCode; }
            set { this._zipCode = value; }
        }

        [DataMember]
        public string Telephone
        {
            get { return this._telephone; }
            set { this._telephone = value; }
        }

        [DataMember]
        public string Lada
        {
            get { return this._lada; }
            set { this._lada = value; }
        }

        [DataMember]
        public string Fax
        {
            get { return this._fax; }
            set { this._fax = value; }
        }

        [DataMember]
        public string Web
        {
            get { return this._web; }
            set { this._web = value; }
        }

        [DataMember]
        public string City
        {
            get { return this._city; }
            set { this._city = value; }
        }

        [DataMember]
        public string State
        {
            get { return this._state; }
            set { this._state = value; }
        }

        [DataMember]
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

       

        #endregion

        private int _divisionId;
        private int? _parentId;
        private string _divisionName;
        private string _shortName;
        private int? _laboratoryId;
        private int? _countryId;
        private bool _active;
        private int _divisionInformationId;
        private string _image;
        private string _address;
        private string _suburb;
        private string _location;
        private string _zipCode;
        private string _telephone;
        private string _lada;
        private string _fax;
        private string _web;
        private string _city;
        private string _state;
        private string _email;
        
 

    }
}
