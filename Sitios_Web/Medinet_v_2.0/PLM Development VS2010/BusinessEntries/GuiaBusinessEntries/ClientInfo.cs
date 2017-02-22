using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class ClientInfo
    {
        #region Constructors

        public ClientInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
                    
            }
        }

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
        public int? ClientIdParent
        {
            get
            {
                return this._ClientIdParent;
            }
            set
            {
                this._ClientIdParent = value;
            }
        }

        [DataMember]
        public string ClientCode
        {
            get
            {
                return this._ClientCode;
            }
            set
            {
                this._ClientCode = value;
            }
        }

        [DataMember]
        public string CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                this._CompanyName = value;
            }
        }

        [DataMember]
        public string ShortName
        {
            get
            {
                return this._ShortName;
            }
            set
            {
                this._ShortName = value;
                   
            }
        }

        [DataMember]
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this._Address = value;
                    
            }
        }

        [DataMember]
        public string Suburb
        {
            get
            {
                return this._Suburb;
            }
            set
            {
                this._Suburb = value;
                
            }
        }

        [DataMember]
        public string PostalCode
        {
            get
            {
                return this._PostalCode;
            }
            set
            {
                this._PostalCode = value;
                    
            }
        }

        [DataMember]
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this._City = value;
            }
        }

        [DataMember]
        public int? StateId
        {
            get
            {
                return this._StateId;
            }
            set
            {
                this._StateId = value;
            }
        }

        [DataMember]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this._Email = value;
            }
        }

        [DataMember]
        public string Web
        {
            get
            {
                return this._Web;
            }
            set
            {
                this._Web = value;
            }
        }

        [DataMember]
        public byte ClientTypeId
        {
            get
            {
                return this._ClientTypeId;
            }
            set
            {
                this._ClientTypeId = value;
            }
        }

        [DataMember]
        public string Image
        {
            get
            {
                return this._Image;
            }
            set
            {
                this._Image = value;
            }
        }

        [DataMember]
        public string Products
        {
            get
            {
                return this._Products;
            }
            set
            {
                this._Products = value;
            }
        }

        [DataMember]
        public byte? CountryId
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
        public string Page
        {
            get
            {
                return this._Page;
            }
            set
            {
                this._Page = value;
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


        private int _ClientId;

        private int _EditionId;

        private int? _ClientIdParent;

        private string _ClientCode;

        private string _CompanyName;

        private string _ShortName;

        private string _Address;

        private string _Suburb;

        private string _PostalCode;

        private string _City;

        private int? _StateId;

        private string _Email;

        private string _Web;

        private byte _ClientTypeId;

        private string _Image;

        private string _Products;

        private byte? _CountryId;

        private string _Page;

        private bool _Active;

    }
}
