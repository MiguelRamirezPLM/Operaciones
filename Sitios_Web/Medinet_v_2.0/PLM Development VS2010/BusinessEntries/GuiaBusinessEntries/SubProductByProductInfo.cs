﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class SubProductByProductInfo
    {
        #region Constructors

        public SubProductByProductInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int SubProductId
        {
            get { return this._subProductId; }
            set { this._subProductId = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public int ClientId
        {
            get { return this._clientId; }
            set { this._clientId = value; }
        }

        [DataMember]
        public byte ClientTypeId
        {
            get { return this._clientTypeId; }
            set { this._clientTypeId = value; }
        }

        [DataMember]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }

        

        #endregion

        private int _subProductId;
        private string _description;
        private int _clientId;
        private byte _clientTypeId;
        private string _companyName;
        
    }
}