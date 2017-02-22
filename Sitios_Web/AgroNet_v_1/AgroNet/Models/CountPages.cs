using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class count
    {
           #region Constructor
        public count(int count1)
        {
            Countt = count1;
        }

        #endregion

        #region Member

        private int Countt;

        #endregion

        #region Properties

        public int count1
        {
            get
            {
                return Countt;
            }
            set
            {
                Countt = value;
            }
        }       
        #endregion
    }
}