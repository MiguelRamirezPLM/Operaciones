using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionclassif
    {
         #region Constructor
        public sessionclassif(int Categoryid)
        {
            categoryid = Categoryid;
        }

        #endregion

        #region Member

        private int categoryid;

        #endregion

        #region Properties

        public int Categoryid
        {
            get
            {
                return categoryid;
            }
            set
            {
                categoryid = value;
            }
        }   
        #endregion
    }
}