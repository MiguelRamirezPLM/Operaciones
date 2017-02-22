using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
   [DataContract]
   public class CompanyBySectionInfo
   {

       #region Constructor

       public CompanyBySectionInfo() { } 

       #endregion

       #region Properties

       [DataMember]
       public int CompanyId
       {
           get { return this._companyId; }
           set { this._companyId = value; }
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
       public string HtmlFile
       {
           get { return this._htmlFile; }
           set { this._htmlFile = value; }
       }

       [DataMember]
       public string HtmlContent
       {
           get
           {
               return this._htmlContent;
           }
           set
           {
               this._htmlContent = value;
           }
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
       public string ZipCode
       {
           get { return this._zipCode; }
           set { this._zipCode = value; }
       }

       [DataMember]
       public int CityId
       {
           get { return this._cityId; }
           set { this._cityId = value; }
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
       public string Web
       {
           get { return this._web; }
           set { this._web = value; }
       }

       [DataMember]
       public string CommercialBusiness
       {
           get { return this._commercialBusiness; }
           set { this._commercialBusiness = value; }
       }

     
       #endregion

       private int _companyId;
       private string _companyName;
       private string _companyShortName;
       private string _htmlFile;
       private string _htmlContent;
       private string _address;
       private string _suburb;
       private string _zipCode;
       private int _cityId;
       private string _cityName;
       private int _stateId;
       private string _stateName;
       private string _web;
       private string _commercialBusiness;
  
   }
}
