﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace GuiaBusinessEntries
{
    [DataContract]
   public  class IntProductsByClientInfo
   {
       #region Constructor

        public IntProductsByClientInfo() { }

       #endregion

       #region Properties

        [DataMember]
        public byte? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        [DataMember]
        public int IntProductId
        {
            get { return this._intProductId; }
            set { this._intProductId = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public int CategoryId
        {
            get { return this._categoryId; }
            set { this._categoryId = value; }
        }

       #endregion

        private byte? _parentId;
        private int _intProductId;
        private string _description;
        private int _categoryId;

   }
}