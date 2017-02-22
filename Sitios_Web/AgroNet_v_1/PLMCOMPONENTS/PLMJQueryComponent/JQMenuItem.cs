using System;
using System.Collections.Generic;
using System.Text;

namespace PLMJQueryComponent
{
    [Serializable]
    public class JQMenuItem
    {
        #region Costructors

        public JQMenuItem(){}

        #endregion

        #region Properties

        public string Url
        {
            get { return this._url;}
            set { this._url = value; }           
        }
        public string MenuName
        {
            get { return this._menuName; }
            set { this._menuName = value; }
        }


        #endregion

        private string _url;
        private string _menuName;
      

    }
}
