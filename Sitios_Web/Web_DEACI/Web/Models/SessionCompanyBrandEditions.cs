using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionCompanyBrandEditions
    {
        #region Constructor
        public SessionCompanyBrandEditions(int id, int ed)
        {
            EditionId = id;
            CompanyId = ed;
        }
        #endregion
        #region Member
        private int EditionId;
        private int CompanyId;
        #endregion
        #region Properties
        public int id
        {
            get
            {
                return EditionId;
            }
            set
            {
                EditionId = value;
            }
        }
        public int ed
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
        #endregion 
    }
}