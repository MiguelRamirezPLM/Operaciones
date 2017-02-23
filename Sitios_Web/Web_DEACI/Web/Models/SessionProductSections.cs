using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionProductSections
    {
        #region Constructor
        public SessionProductSections(int ed)
        {
            ProductId = ed;
        }
        #endregion
        #region Member
        private int ProductId;
        #endregion
        #region Properties
        public int ed
        {
            get
            {
                return ProductId;
            }
            set
            {
                ProductId = value;
            }
        }
        #endregion
    }
}