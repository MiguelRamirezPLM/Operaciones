using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class SessionListPillBooks
    {
        #region Constructor

        public SessionListPillBooks(List<ListPillBooks> PillBook)
        {
            LSPB = PillBook;

        }

        #endregion

        #region Members

        public List<ListPillBooks> LSPB;

        #endregion

        #region Properties


        public List<ListPillBooks> PillBook
        {
            get
            {
                return LSPB;
            }
            set
            {
                LSPB = value;
            }
        }

        #endregion
    }
}