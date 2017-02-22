using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models.Sessions
{
    public class SearchProductNameSM
    {
        #region Constructor

        public SearchProductNameSM(string ProductName)
        {
            Product = ProductName;
        }

        #endregion

        #region Member

        public string Product;

        #endregion

        #region Properties

        public string ProductName
        {
            get
            {
                return Product;
            }
            set
            {
                Product = value;
            }
        }

        #endregion
    }
}