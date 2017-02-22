using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class BookInfo
    {
        #region Constructors

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
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _bookId;
        private string _description;
        private bool _active;

    }
}
