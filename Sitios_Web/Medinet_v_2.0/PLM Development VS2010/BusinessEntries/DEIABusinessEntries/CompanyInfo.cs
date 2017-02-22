using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries
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
       public int? ParentId
       {
           get { return this._parentId; }
           set { this._parentId = value; }
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
       public byte CompanyTypeId
       {
           get { return this._companyTypeId; }
           set { this._companyTypeId = value; }
       }

       [DataMember]
       public string CommercialBusiness
       {
           get { return this._commercialBusiness; }
           set { this._commercialBusiness = value; }
       }

       [DataMember]
       public string Address
       {
           get { return this._address; }
           set { this._address = value; }
       }

       [DataMember]
       public int? CityId
       {
           get { return this._cityId; }
           set { this._cityId = value; }
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
       public int? ClientId
       {
           get { return this._clientId; }
           set { this._clientId = value; }
       }

       [DataMember]
       public string HtmlFile
       {
           get { return this._htmlFile; }
           set { this._htmlFile = value; }
       }

       [DataMember]
       public int? EmpresaId
       {
           get { return this._empresaId; }
           set { this._empresaId = value; }
       }

       [DataMember]
       public bool Active
       {
           get { return this._active; }
           set { this._active = value; }
       }

       #endregion

       private int _companyId;
       private int? _parentId;
       private string _companyName;
       private string _companyShortName;
       private byte _companyTypeId;
       private string _commercialBusiness;
       private string _address;
       private int? _cityId;
       private string _suburb;
       private string _location;
       private string _zipCode;
       private string _email;
       private string _web;
       private int? _clientId;
       private string _htmlFile;
       private int? _empresaId;
       private bool _active;


   
   }
}
