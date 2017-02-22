using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
    public class ParticipantProductInfo
    {

        #region Constructor

        public ParticipantProductInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int EditionId
        {
            get
            {
                return this._editionId;
            }
            set
            {
                this._editionId = value;
            }
        }

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
        public int PharmaFormId
        {
            get
            {
                return this._pharmaFormId;
            }
            set
            {
                this._pharmaFormId = value;
            }
        }

        [DataMember]
        public int DivisionId
        {
            get
            {
                return this._divisionId;
            }
            set
            {
                this._divisionId = value;
            }
        }

        [DataMember]
        public int CategoryId
        {
            get
            {
                return this._categoryId;
            }
            set
            {
                this._categoryId = value;
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

        [DataMember]
        public string Page
        {
            get
            {
                return this._page;
            }
            set
            {
                this._page = value;
            }
        }
        
        #endregion

        private int _editionId;
        private int _productId;
        private int _pharmaFormId;
        private int _divisionId;
        private int _categoryId;
        private string _htmlContent;
        private string _page;

    }
}
