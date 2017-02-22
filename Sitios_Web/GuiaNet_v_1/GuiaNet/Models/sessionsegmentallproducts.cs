using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionsegmentallproducts
    {
         #region Constructor
        public sessionsegmentallproducts(int CId, String CountryName, int EId, int NumberEdition, int BId, String BookName)
        {
            country = CId;
            CName = CountryName;
            edition = EId;
            NEdition = NumberEdition;
            book = BId;
            BName = BookName;
        }

        #endregion

        #region Member

        private int country;
        private String CName;
        public int edition;
        private int NEdition;
        public int book;
        private String BName;

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

        public String CountryName
        {
            get
            {
                return CName;
            }
            set
            {
                CName = value;
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

        public int NumberEdition
        {
            get
            {
                return NEdition;
            }
            set
            {
                NEdition = value;
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

        public String BookName
        {
            get
            {
                return BName;
            }
            set
            {
                BName = value;
            }
        }

        #endregion
    }
}