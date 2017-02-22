using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class EditionInfo
    {
        #region Constructors

        public EditionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int EditionId
        {
            get
            {
                return this._EditionId;
            }
            set
            {
                this._EditionId = value;
                
            }
        }

        [DataMember]
        public byte CountryId
        {
            get
            {
                return this._CountryId;
            }
            set
            {
                this._CountryId = value;
            }
        }

        [DataMember]
        public int? ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                this._ParentId = value;
                 
            }
        }

        [DataMember]
        public string EditionCode
        {
            get
            {
                return this._EditionCode;
            }
            set
            {
                this._EditionCode = value;
            }
        }

        [DataMember]
        public int BookId
        {
            get
            {
                return this._BookId;
            }
            set
            {
                this._BookId = value;
                 
            }
        }

        [DataMember]
        public int NumberEdition
        {
            get
            {
                return this._NumberEdition;
            }
            set
            {
                this._NumberEdition = value;
            }
        }

        [DataMember]
        public string ISBN
        {
            get
            {
                return this._ISBN;
            }
            set
            {
                this._ISBN = value;
                
            }
        }

        [DataMember]
        public string BarCode
        {
            get
            {
                return this._BarCode;
            }
            set
            {
                this._BarCode = value;
                
            }
        }

        [DataMember]
        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                this._Active = value;
               
            }
        }

        #endregion

        private int _EditionId;

        private byte _CountryId;

        private int? _ParentId;

        private string _EditionCode;

        private int _BookId;

        private int _NumberEdition;

        private string _ISBN;

        private string _BarCode;

        private bool _Active;

    }
}
