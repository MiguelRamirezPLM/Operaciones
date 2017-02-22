using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionAddAdverts
    {
        #region Constructor

        public SessionAddAdverts(int? CId, int? EId, int? AId)
        {
            client = CId;
            edition = EId;
            advert = AId;
        }

        #endregion

        #region Member

        private int? client;
        public int? edition;
        public int? advert;

        #endregion

        #region Properties

        public int? CId
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

        public int? EId
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

        public int? AId
        {
            get
            {
                return advert;
            }
            set
            {
                advert = value;
            }
        }

        #endregion
    }
}