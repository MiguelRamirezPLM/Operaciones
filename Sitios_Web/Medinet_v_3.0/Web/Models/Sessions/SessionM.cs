using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Sessions
{
    public class SessionM
    {
        #region Constructor

        public SessionM(int CountryId, int BookId, int EditionTypeId, int EditionId, int DivisionId)
        {
            country = CountryId;
            division = DivisionId;
            edition = EditionId;
            book = BookId;
            editiontype = EditionTypeId;
        }

        #endregion

        #region Member

        public int country;
        public int division;
        public int edition;
        public int book;
        public int editiontype;

        #endregion

        #region Properties

        public int CountryId
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public int DivisionId
        {
            get
            {
                return division;
            }
            set
            {
                division = value;
            }
        }
        public int EditionId
        {
            get
            {
                return edition;
            }
            set
            {
                edition = value;
            }
        }
        public int BookId
        {
            get
            {
                return book;
            }
            set
            {
                book = value;
            }
        }

        public int EditionTypeId
        {
            get
            {
                return editiontype;
            }
            set
            {
                editiontype = value;
            }
        }
        #endregion
    }
}