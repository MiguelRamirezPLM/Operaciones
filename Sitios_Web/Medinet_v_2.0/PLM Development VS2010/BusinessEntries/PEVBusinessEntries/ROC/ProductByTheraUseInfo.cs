using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace PEVBusinessEntries.ROC
{
    [DataContract]
   public  class ProductByTheraUseInfo
    {

# region Constructor

        public ProductByTheraUseInfo(){}

 # endregion

#region Properties

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
        public int DivisinId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        [DataMember]
        public string ShortName
        {
            get { return this._shortName; }
            set { this._shortName = value; }
        }

        [DataMember]
        public string Participant
        {
            get { return this._participant; }
            set { this._participant = value; }
        }

#endregion 

        private int _productId;
        private string  _productName;
        private int  _divisionId;
        private string _shortName;
        private string _participant;
    }
}
