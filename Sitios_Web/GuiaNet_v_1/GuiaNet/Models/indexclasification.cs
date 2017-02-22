using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class indexclasification
    {
         #region Constructor
        public indexclasification(int CId, int ClId, int EId, int BId,bool flag,string param)
        {
            country = CId;
            client = ClId;
            edition = EId;
            book = BId;
            flagg = flag;
            search = param;
        }

        #endregion

        #region Member

        private int country;
        private int client;
        public int edition;
        public int book;
        public bool flagg;
        public string search;

        #endregion

        #region Properties

        public bool flag
        {
            get
            {
                return flagg;
            }
            set
            {
                flagg = value;
            }
        }
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

        public string param
        {
            get
            {
                return search;
            }
            set
            {
                search = value;
            }
        }

        #endregion
    }
}