using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class MenuInfo
    {
        #region Constructors

        public MenuInfo() {}

        #endregion

        #region Properties

        [DataMember]
        public int MenuId
        {
            get { return this._menuId; }
            set { this._menuId = value; }
        }

        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        [DataMember]
        public string MenuName
        {
            get { return this._menuName; }
            set { this._menuName = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _menuId;
        private int? _parentId;
        private string _menuName;
        private bool _active;

    }
}
