using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
   [DataContract]
   public  class IntAddressByClientInfo
   {
       #region Constructor

       public IntAddressByClientInfo() { }

       #endregion

       #region Properties

       [DataMember]
       public int IntAddressId
       {
           get { return this._intAddressId; }
           set { this._intAddressId = value; }
       }

       [DataMember]
       public string Address
       {
           get { return this._address; }
           set { this._address = value; }
       }

       [DataMember]
       public string ZipCode
       {
           get { return this._zipCode; }
           set { this._zipCode = value; }
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
       public int CountryId
       {
           get { return this._countryId; }
           set { this._countryId = value; }
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
       public bool Active
       {
           get { return this._active; }
           set { this._active = value; }
       }

       [DataMember]
       public string CountryName
       {
           get { return this._countryName; }
           set { this._countryName = value; }
       }

       #endregion

       private int _intAddressId;
       private string _address;
       private string _zipCode;
       private string _city;
       private string _state;
       private int _countryId;
       private string _email;
       private string _web;
       private bool _active;
       private string _countryName;

   }
}
