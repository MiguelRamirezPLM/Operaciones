using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class SearchPages
    {
          #region Constructor
        public SearchPages(string CountId, string EditId, string BbookId, string DDivisionId)
        {
            country = CountId;
            edition = EditId;
            book = BbookId;
            division = DDivisionId;
        }

        #endregion

        #region Member

        private string country;
        public string edition;
        public string book;
        public string division;

        #endregion

        #region Properties

        public string CountId
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

        public string EditId
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

        public string BbookId
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

        public string DDivisionId
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

        #endregion
    }
}