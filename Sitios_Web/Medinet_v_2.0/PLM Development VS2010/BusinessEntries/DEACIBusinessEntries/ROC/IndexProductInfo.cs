using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
   [DataContract]
  public class IndexProductInfo
  {
      #region Constructor

       public IndexProductInfo() { }

      #endregion

       #region Properties
       
       [DataMember]
       public int SectionId
       {
           get { return this._sectionId; }
           set { this._sectionId = value; }
       }

       [DataMember]
       public string Description
       {
           get { return this._description; }
           set { this._description = value; }
       }

       [DataMember]
       public int RowNumber
       {
           get { return this._rowNumber; }
           set { this._rowNumber = value; }
       }

       [DataMember]
       public int Total
       {
           get { return this._total; }
           set { this._total = value; }
       }

       #endregion

       private int _sectionId;
       private string _description;
       private int _rowNumber;
       private int _total;

  }
}
