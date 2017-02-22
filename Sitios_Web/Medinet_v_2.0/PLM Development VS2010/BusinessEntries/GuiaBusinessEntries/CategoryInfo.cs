using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public  class CategoryInfo
    {
        #region Constructor

        public CategoryInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int CategoryId
        {
            get { return this._categoryId; }
            set { this._categoryId = value; }
        }

        [DataMember]
        public byte? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }


        #endregion

        private int _categoryId;
        private byte? _parentId;
        private string _description;
        private bool _active;

    }
}
