using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionHospitalsSM
    {
        #region Constructor

        public SessionHospitalsSM(int CId, int ClId, int EId, int BId, int? PId, string Letter)
        {
            country = CId;
            client = ClId;
            edition = EId;
            book = BId;
            product = PId;
            ltr = Letter;
        }

        #endregion

        #region Member

        private int country;
        private int client;
        public int edition;
        public int book;
        public int? product;
        public string ltr;

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
        public int ClId
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
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

        public int? PId
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
            }
        }

        public string Letter
        {
            get
            {
                return ltr;
            }
            set
            {
                ltr = value;
            }
        }

        #endregion
    }
}