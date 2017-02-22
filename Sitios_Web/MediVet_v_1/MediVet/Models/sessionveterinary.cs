using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class sessionveterinary
    {
        #region Constructor

        public sessionveterinary(int CId, int DId, int EId, int BId)
        {
            country = CId;
            client = DId;
            edition = EId;
            book = BId;
        }

        #endregion

        #region Member

        private int country;
        private int client;
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
        public int DId
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

        #endregion
    }
}