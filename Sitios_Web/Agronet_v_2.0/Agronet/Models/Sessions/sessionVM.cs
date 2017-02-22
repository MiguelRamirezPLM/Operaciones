using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models.Sessions
{
    public class sessionVM
    {
        #region Constructor
        public sessionVM(int CountryId, int BookId, int EditionId, int DivisionId)
        {
            country = CountryId;
            division = DivisionId;
            edition = EditionId;
            book = BookId;
        }

        #endregion

        #region Member

        public int country;
        public int division;
        public int edition;
        public int book;

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

        #endregion
    }
}