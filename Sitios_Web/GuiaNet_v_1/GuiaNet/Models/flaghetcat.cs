using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class flaghetcat
    {
         #region Constructor
        public flaghetcat(bool flag, string param)
        {

            flagg = flag;
            search = param;
        }

        #endregion

        #region Member

        public bool flagg;
        public string search;

        #endregion

        #region Properties

        public bool flag
        {
            get
            {
                return flagg;
            }
            set
            {
                flagg = value;
            }
        }

        public string param
        {
            get
            {
                return search;
            }
            set
            {
                search = value;
            }
        }

        #endregion
    }
}