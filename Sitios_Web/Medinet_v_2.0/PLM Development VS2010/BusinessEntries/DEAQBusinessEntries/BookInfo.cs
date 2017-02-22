using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
   public class BookInfo
   {
       #region Constructor

       public BookInfo() { }

       #endregion 

       #region Properties

       [DataMember]
       public int BookId
       {
           get { return this._bookId; }
           set { this._bookId = value; }
       }

       [DataMember]
       public string BookName
       {
           get { return this._bookName; }
           set { this._bookName = value; }
       }

       [DataMember]
       public string ShortName
       {
           get { return this._shortName; }
           set { this._shortName = value; }
       }

       [DataMember]
       public bool Active
       {
           get { return this._active; }
           set { this._active = value; }
       }

       #endregion


       private int _bookId;
       private string _bookName;
       private string _shortName;
       private bool _active;

   }
}
