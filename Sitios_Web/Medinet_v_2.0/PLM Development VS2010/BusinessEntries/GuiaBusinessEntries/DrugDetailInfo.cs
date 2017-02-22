using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class DrugDetailInfo
    {
        #region Constructors

        public DrugDetailInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int DrugId
        {
            get { return this._drugId; }
            set { this._drugId = value; }
        }

        [DataMember]
        public int ClientId
        {
            get { return this._clientId; }
            set { this._clientId = value; }
        }

        [DataMember]
        public string DrugName
        {
            get { return this._drugName; }
            set { this._drugName = value; }
        }

        [DataMember]
        public string ActiveSubstance
        {
            get { return this._activeSubstance; }
            set { this._activeSubstance = value; }
        }

        [DataMember]
        public string PharmaceuticForm
        {
            get { return this._pharmaceuticForm; }
            set { this._pharmaceuticForm = value; }
        }

        [DataMember]
        public string Presentation
        {
            get { return this._presentation; }
            set { this._presentation = value; }
        }

        [DataMember]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }

        #endregion

        private int _drugId;
        private string _drugName;
        private string _activeSubstance;
        private string _pharmaceuticForm;
        private string _presentation;
        private int _clientId;
        private string _companyName;

    }
}
