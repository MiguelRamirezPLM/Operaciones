using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionCountryByDivision
    {
        #region Constructor
        public SessionCountryByDivision(int id, int ed, int ad)
        {
            BaseId = id;
            CountryId = ed;
            DivisionId = ad;
        }
        #endregion
        #region Member
        private int BaseId;
        private int CountryId;
        private int DivisionId;
        #endregion
        #region Properties
        public int id
        {
            get
            {
                return BaseId;
            }
            set
            {
                BaseId = value;
            }
        }
        public int ed
        {
            get
            {
                return CountryId;
            }
            set
            {
                CountryId = value;
            }
        }
        public int ad
        {
            get
            {
                return DivisionId;
            }
            set
            {
                DivisionId = value;
            }
        }
        #endregion
    }
}