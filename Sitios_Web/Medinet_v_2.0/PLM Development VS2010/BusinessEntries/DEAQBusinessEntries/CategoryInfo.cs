using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
   public class CategoryInfo
   {
       #region Constructor

       public CategoryInfo() { }

       #endregion

       #region Properties

       [DataMember]
       public int CategoryId
       {
           get { return this._categoryId; }
           set { this._categoryId = value; }
       }

       [DataMember]
       public string CategoryName
       {
           get { return this._categoryName; }
           set { this._categoryName = value; }
       }

       [DataMember]
       public bool Active
       {
           get { return this._active; }
           set { this._active = value; }
       }

       #endregion

       private int _categoryId;
       private string _categoryName;
       private bool _active;
   }
}
