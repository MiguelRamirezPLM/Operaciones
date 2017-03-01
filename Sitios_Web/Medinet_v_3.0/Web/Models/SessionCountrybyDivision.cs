using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionCountryByDivision
    {
        #region Constructor
        public SessionCountryByDivision(int id, int ed, int ad, int ud)
        {
            CountryId = id;
            BookId = ed;
            EditionId = ad;
            DivisionId = ud;
        }
        #endregion
        #region Member
        public int CountryId;
        public int BookId;
        public int EditionId;
        public int DivisionId;
        #endregion
        #region Properties
        public int id
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
        public int ed
        {
            get
            {
                return BookId;
            }
            set
            {
                BookId = value;
            }
        }
        public int ad
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
        public int ud
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