using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionCompanyEditions
    {
        #region Constructor
        public SessionCompanyEditions(int id, int ed, int _CompanyTypeId)
        {
            EditionId = id;
            CompanyId = ed;
            CompanyTypeId = _CompanyTypeId;
        }
        #endregion
        #region Member
        private int EditionId;
        private int CompanyId;
        private int CompanyTypeId;
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
        public int _CompanyTypeId
        {
            get
            {
                return CompanyTypeId;
            }
            set
            {
                CompanyTypeId = value;
            }
        }
        #endregion
    }
}