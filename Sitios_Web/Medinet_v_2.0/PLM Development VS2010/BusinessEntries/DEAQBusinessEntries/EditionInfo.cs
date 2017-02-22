using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
   public class EditionInfo
   {

       #region Constructor

       public EditionInfo() { }

       #endregion

       #region Properties

        [DataMember]
       public int EditionId
       {
           get { return this._editionId; }
           set { this._editionId = value; }
       }

        [DataMember]
        public int? CountryId
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
        public int? BookId
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
            get { return this._barCode; }
            set { this._barCode = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

       #endregion

       private int _editionId;
       private int? _countryId;
       private int? _parentId;
       private int? _bookId;
       private int _numberEdition;
       private string _isbn;
       private string _barCode;
       private bool _active;

   }
}
