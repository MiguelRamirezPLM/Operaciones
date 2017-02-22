using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries.ROC
{
    [DataContract]
    public class ProductByEditionInfo
    {

        #region Constructor

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
        public string Register
        {
            get
            {
                return this._register;
            }
            set
            {
                this._register = value;
            }
        }

        [DataMember]
        public int? PharmaFormId
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
        public string PharmaForm
        {
            get
            {
                return this._pharmaForm;
            }
            set
            {
                this._pharmaForm = value;
            }
        }

        [DataMember]
        public int? LaboratoryId
        {
            get
            {
                return this._laboratoryId;
            }
            set
            {
                this._laboratoryId = value;
            }
        }

        [DataMember]
        public string LaboratoryName
        {
            get
            {
                return this._laboratoryName;
            }
            set
            {
                this._laboratoryName = value;
            }
        }

        [DataMember]
        public int? DivisionId
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
        public string DivisionName
        {
            get
            {
                return this._divisionName;
            }
            set
            {
                this._divisionName = value;
            }
        }

        [DataMember]
        public int? CategoryId
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
        public int? EditionId
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
        public int? RowNumber
        {
            get
            {
                return this._rowNumber;
            }
            set
            {
                this._rowNumber = value;
            }
        }

        [DataMember]
        public int? Total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
            }
        }

        #endregion

        private int _productId;
        private string _productName;
        private string _description;
        private string _register;
        private int? _pharmaFormId;
        private string _pharmaForm;
        private int? _laboratoryId;
        private string _laboratoryName;
        private int? _divisionId;
        private string _divisionName;
        private int? _categoryId;
        private int? _editionId;
        private int? _rowNumber;
        private int? _total;

    }
}
