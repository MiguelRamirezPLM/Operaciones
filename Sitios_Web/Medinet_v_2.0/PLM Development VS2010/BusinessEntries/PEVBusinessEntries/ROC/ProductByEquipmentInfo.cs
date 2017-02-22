using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
{
    [DataContract]
    public class ProductByEquipmentInfo
    {

        #region Constructor

        public ProductByEquipmentInfo() { }

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
        public byte CountryId
        {
            get
            {
                return this._countryId;
            }
            set
            {
                this._countryId = value;
            }
        }

        [DataMember]
        public int LaboratoryId
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
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        [DataMember]
        public int? IdAccess
        {
            get
            {
                return this._idAccess;
            }
            set
            {
                this._idAccess = value;
            }
        }

        [DataMember]
        public string Participant
        {
            get
            {
                return this._participant;
            }
            set
            {
                this._participant = value;
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
        public string ShortName
        {
            get
            {
                return this._shortName;
            }
            set
            {
                this._shortName = value;
            }
        }

        [DataMember]
        public int RowNumber
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
        public int Total
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
        private byte _countryId;
        private int _laboratoryId;
        private string _description;
        private bool _active;
        private int? _idAccess;
        private string _participant;
        private int _divisionId;
        private string _divisionName;
        private string _shortName;
        private int _rowNumber;
        private int _total;

    }
}
