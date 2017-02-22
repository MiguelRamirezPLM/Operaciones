using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries.ROC
{
    [DataContract]
   public  class ProductByUseInfo
   {
       #region Constructor

        public ProductByUseInfo() { }

       #endregion

        #region Properties

        [DataMember]
        public int AgrochemicalUseId
        {
            get { return this._agrochemicalUseId; }
            set { this._agrochemicalUseId = value; }
          
        }

        [DataMember]
        public string AgrochemicalUseName
        {
            get { return this._agrochemicalUseName; }
            set { this._agrochemicalUseName = value; }
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
            get { return this._productName;}
            set { this._productName = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public string Register
        {
            get { return this._register; }
            set { this._register = value; }
        }

        [DataMember]
        public int LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        [DataMember]
        public int CategoryId
        {
            get { return this._categoryId; }
            set { this._categoryId = value; }
        }

        #endregion

        private int _agrochemicalUseId;
        private string _agrochemicalUseName;
        private int _productId;
        private string _productName;
        private string _description;
        private string _register;
        private int _laboratoryId;
        private int _divisionId;
        private int _categoryId;

   }
}
