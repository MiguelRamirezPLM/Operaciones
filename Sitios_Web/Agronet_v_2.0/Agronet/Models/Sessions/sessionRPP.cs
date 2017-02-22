using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models.Sessions
{
    public class sessionRPP
    {
        #region Constructor

        public sessionRPP(int Count)
        {
            Quantity = Count;
        }

        #endregion

        #region Member

        public int Quantity;

        #endregion

        #region Properties

        public int Count
        {
            get
            {
                return Quantity;
            }
            set
            {
                Quantity = value;
            }
        }

        #endregion
    }
}