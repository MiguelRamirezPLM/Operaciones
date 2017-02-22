using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class BrandsByEditionInfo
    {
        #region Constructors

        public BrandsByEditionInfo() { }

        #endregion

        #region Properties

      

        [DataMember]
        public int BrandId
        {
            get
            {
                return this._brandId;
            }
            set
            {
                this._brandId = value;
            }

        }

        [DataMember]
        public string BrandName
        {
            get
            {
                return this._brandName;
            }
            set
            {
                this._brandName = value;
            }

        }

        [DataMember]
        public string Logo
        {
            get
            {
                return this._logo;
            }
            set
            {
                this._logo = value;
            }

        }

        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        

        #endregion

        
        private int _brandId;
        private string _brandName;
        private string _logo;
        private string _baseUrl;
        

    }
}
