using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
    [DataContract]
    public class ProductByEditionInfo
    {

        #region Constructors

        public ProductByEditionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }

        [DataMember]
        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }

        [DataMember]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        [DataMember]
        public int SectionId
        {
            get
            {
                return this._sectionId;
            }
            set
            {
                this._sectionId = value;
            }
        }

        [DataMember]
        public string HtmlFile
        {
            get
            {
                return this._htmlFile;
            }
            set
            {
                this._htmlFile = value;
            }
        }

        [DataMember]
        public string HtmlContent
        {
            get
            {
                return this._htmlContent;
            }
            set
            {
                this._htmlContent = value;
            }
        }

        #endregion

        private int _productId;
        private string _productName;
        private string _description;
        private int _sectionId;
        private string _htmlFile;
        private string _htmlContent;

    }
}
