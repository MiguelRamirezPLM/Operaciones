using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
 [DataContract]
  public class BrandByCompanyInfo
  {

      #region Constructor

     public BrandByCompanyInfo() { } 

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

     #endregion

     private int _brandId;
     private string _brandName;

  }
}
