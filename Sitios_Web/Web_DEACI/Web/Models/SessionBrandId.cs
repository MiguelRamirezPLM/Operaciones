using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionBrandId
    {
        #region Constructor
        public SessionBrandId(int id, int ed)
        {
            CompanyId = id;
            BrandId = ed;
        }
        #endregion
        #region Member
        private int CompanyId;
        private int BrandId;
        #endregion
        #region Properties
        public int id
        {
            get
            {
                return CompanyId;
            }
            set
            {
                CompanyId = value;
            }
        }
        public int ed
        {
            get
            {
                return BrandId;
            }
            set
            {
                BrandId = value;
            }
        }
        #endregion
    }
}