using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class pagessearch
    {
        #region Constructor
        public pagessearch(string ProductName)
        {
            search = ProductName;
        }

        #endregion

        #region Member

        private string search;

        #endregion

        #region Properties

        public string ProductName
        {
            get
            {
                return search;
            }
            set
            {
                search = value;
            }
        }
        #endregion
    }
}