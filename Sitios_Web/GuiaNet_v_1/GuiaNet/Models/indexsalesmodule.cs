using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class indexsalesmodule
    {
        #region Constructor
        public indexsalesmodule(int CId, int ClId, int EId, int BId, int? PId)
        {
            country = CId;
            client = ClId;
            edition = EId;
            book = BId;
            product = PId;
        }

        #endregion

        #region Member

        private int country;
        private int client;
        public int edition;
        public int book;
        public int? product;

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