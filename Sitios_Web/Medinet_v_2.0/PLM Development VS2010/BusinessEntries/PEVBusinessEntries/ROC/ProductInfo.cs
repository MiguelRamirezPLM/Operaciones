using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
{
    [DataContract]
    public class ProductInfo
    {
    # region Constructors 

        public ProductInfo() { }

    # endregion

        # region Properties

        [DataMember]
        public int? Total
        {
            get { return this._total; }
            set { this._total = value; }

        }

        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        [DataMember]
        public string ProductName
        {
            get { return this._productName; }
            set { this._productName = value; }
        }

        [DataMember]
        public string DivisionName
        {
            get { return this._divisionName; }
            set { this._divisionName = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        [DataMember]
        public string DivisionShortName
        {
            get { return this._divisionShortName; }
            set { this._divisionShortName = value; }
        }

        [DataMember]
        public string Participant
        {
            get { return this._participant; }
            set { this._participant = value; }
        }

        [DataMember]
        public int RowNumber
        {
            get { return this._rowNumber; }
            set { this._rowNumber = value; }
        }
       
        # endregion Propiertes

        private int? _total;
        private int _productId;
        private string _productName;
        private string _divisionName;
        private string _description;
        private int _divisionId;
        private string _divisionShortName;
        private string _participant;
        private int _rowNumber;


        
    }


   
}
