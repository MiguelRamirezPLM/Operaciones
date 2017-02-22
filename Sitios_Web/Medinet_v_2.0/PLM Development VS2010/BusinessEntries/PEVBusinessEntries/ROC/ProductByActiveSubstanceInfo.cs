using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
{
    [DataContract]
    public class ProductByActiveSubstanceInfo
    
    {
        #region Constructors

        public ProductByActiveSubstanceInfo() { }

        #endregion

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
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public int CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember]
        public string Participant
        {
            get { return this._participant; }
            set { this._participant = value; }
        }

        [DataMember]
        public string LaboratoryName
        {
            get { return this._laboratoryName; }
            set { this._laboratoryName = value; }
        }

        [DataMember]
        public int LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

        [DataMember]
        public string ImageName
        {
            get { return this._imageName; }
            set { this._imageName = value; }
        }

        [DataMember]
        public string CourtesImage
        {
            get { return this._courtesyImage; }
            set { this._courtesyImage = value; }

        }
        #endregion


        private int _productId;
        private string _productName;
        private string _description;
        private int _countryId;
        private bool _active;
        private string _participant;
        private string _laboratoryName;
        private int _laboratoryId;
        private string _imageName;
        private string _courtesyImage;
       

    }


  
}
