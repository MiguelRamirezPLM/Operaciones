using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class SessionActiveTT
    {

        #region Constructor

        public SessionActiveTT(bool active)
        {
            flag = active;
        }

        #endregion

        #region Members

        public bool flag;

        #endregion

        #region Properties

        public bool active
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        #endregion
    }
}