using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class SearchClass
    {
          #region Constructor
        public SearchClass(string CId, string DId, string EId, string PId, string BId)
        {
            country = CId;
            division = DId;
            edition = EId;
            product = PId;
            book = BId;
        }

        #endregion

        #region Member

        private string country;
        private string division;
        public string edition;
        public string product;
        public string book;

        #endregion

        #region Properties

        public string CId
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
        public string DId
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

        public string EId
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

        public string PId
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

        public string BId
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