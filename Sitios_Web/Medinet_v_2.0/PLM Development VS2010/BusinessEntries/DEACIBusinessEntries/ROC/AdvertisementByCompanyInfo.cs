using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
   public class AdvertisementByCompanyInfo
   {

       #region Constructor

       public AdvertisementByCompanyInfo() { }

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
       public int EditionId
       {
           get { return this._editionId; }
           set { this._editionId = value; }
       }

       [DataMember]
       public string HTMLFile
       {
           get { return this._htmlFile; }
           set { this._htmlFile = value; }
       }


       [DataMember]
       public int CountryId
       {
           get { return this._countryId; }
           set { this._countryId = value; }
       }

       [DataMember]
       public int? ParentId
       {
           get { return this._parentId; }
           set { this._parentId = value; }
       }

       [DataMember]
       public int BookId
       {
           get { return this._bookId; }
           set { this._bookId = value; }
       }

       [DataMember]
       public int NumberEdition
       {
           get { return this._numberEdition; }
           set { this._numberEdition = value; }
       }

       [DataMember]
       public string ISBN
       {
           get { return this._isbn; }
           set { this._isbn = value; }
       }

       [DataMember]
       public string BarCode
       {
           get { return this._barcode; }
           set { this._barcode = value; }
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
       private int _editionId;
       private string _htmlFile;
       private int _countryId;
       private int? _parentId;
       private int _bookId;
       private int _numberEdition;
       private string _isbn;
       private string _barcode;
     

   }
}
