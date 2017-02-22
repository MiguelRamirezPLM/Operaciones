using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class CountryInfo
    {
        #region Constructors

        public CountryInfo() { }

        #endregion
        
        #region Properties

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
        public string CountryName
        {
            get
            {
                return this._CountryName;
            }
            set
            {
                this._CountryName = value;
            }
        }

        [DataMember]
        public byte? CountryCode
        {
            get
            {
                return this._CountryCode;
            }
            set
            {
                this._CountryCode = value;
                 
            }
        }

        [DataMember]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
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
        
        private byte _CountryId;

        private string _CountryName;

        private byte? _CountryCode;

        private string _ID;

        private bool _Active;

    }
}
