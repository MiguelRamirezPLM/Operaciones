using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionReports
    {
        #region Constructor

        public SessionReports(int CId, int UId, int EId, int BId)
        {
            country = CId;
            user = UId;
            edition = EId;
            book = BId;
        }

        #endregion

        #region Member

        private int country;
        private int user;
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
        public int UId
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
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