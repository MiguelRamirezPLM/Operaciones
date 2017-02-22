using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
   [DataContract]
   public class BrandInfo
   {
       #region Constructor

       public BrandInfo() { }

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

       [DataMember]
       public int? RowNumber
       {
           get { return this._rowNumber; }
           set { this._rowNumber = value; }
       }

       [DataMember]
       public int? Total
       {
           get { return this._total; }
           set { this._total = value; }
       }

       #endregion

       private int _brandId;
       private string _brandName;
       private int? _rowNumber;
       private int? _total;

   }
}
