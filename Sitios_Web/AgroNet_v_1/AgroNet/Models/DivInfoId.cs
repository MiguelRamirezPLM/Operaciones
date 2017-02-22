using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class DivInfoId
    {
          #region Constructor
        public DivInfoId(int DivisionId, bool flag)
        {
            DIid = DivisionId;
            flags = flag;
        }

        #endregion

        #region Member

        private int DIid;
        private bool flags;
        #endregion

        #region Properties

        public int DivisionId
        {
            get
            {
                return DIid;
            }
            set
            {
                DIid = value;
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