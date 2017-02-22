using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
    public class MentionatedProductInfo
    {

        #region Constructor

        public MentionatedProductInfo() { }

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

        #endregion

        private int _editionId;
        private int _productId;
        private int _pharmaFormId;
        private int _divisionId;
        private int _categoryId;

    }
}
