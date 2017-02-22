using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class SessionCatch
    {
         #region Constructor

        public SessionCatch(int? PillBookId)
        {
            PBId = PillBookId;

        }

        #endregion

        #region Members

        public int? PBId;

        #endregion

        #region Properties


        public int? PillBookId
        {
            get
            {
                return PBId;
            }
            set
            {
                PBId = value;
            }
        }

        #endregion
    }
}