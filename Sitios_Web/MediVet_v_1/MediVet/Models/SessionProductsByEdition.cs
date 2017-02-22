using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class SessionProductsByEdition
    {
        #region Constructor

        public SessionProductsByEdition(int CId, int EId, int BId)
        {
            country = CId;
            edition = EId;
            book = BId;
        }

        #endregion

        #region Member

        private int country;
        public int edition;
        public int book;

        #endregion

        #region Properties

        public int CId
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

        public int EId
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

        public int BId
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