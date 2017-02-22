using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionproduction
    {
         #region Constructor
        public sessionproduction(int CId, int ClId, int EId, int BId, int? PId)
        {
            country = CId;
            client = ClId;
            edition = EId;
            book = BId;
        }

        #endregion

        #region Member

        private int country;
        private int client;
        public int edition;
        public int book;
        public int? productid;

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
                return productid;
            }
            set
            {
                productid = value;
            }
        }

        #endregion
    }
}