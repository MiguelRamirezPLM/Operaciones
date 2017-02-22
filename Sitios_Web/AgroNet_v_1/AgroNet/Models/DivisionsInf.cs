using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class DivisionsInf
    {
         #region Constructor
        public DivisionsInf(int Id, bool flag)
        {
            DivisionId = Id;
            flags = flag;
        }

        #endregion

        #region Member

        private int DivisionId;
        private bool flags;

        #endregion

        #region Properties

        public int Id
        {
            get
            {
                return DivisionId;
            }
            set
            {
                DivisionId = value;
            }
        }

        public bool flag
        {
            get
            {
                return flags;
            }
            set
            {
                flags = value;
            }
        }
        #endregion
    }
}