using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
   [DataContract]
   public  class CompanyByPageInfo
   {

       #region Constructor

       public CompanyByPageInfo() { }

       #endregion

       #region Properties
       
       [DataMember]
       public int Total
       {
           get { return this._total; }
           set { this._total = value; }
       }

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
       public int RowNumber
       {
           get { return this._rowNumber; }
           set { this._rowNumber = value; }
       }

       #endregion

       private int _total;
       private int _companyId;
       private string _companyName;
       private int _rowNumber;

   }



}
