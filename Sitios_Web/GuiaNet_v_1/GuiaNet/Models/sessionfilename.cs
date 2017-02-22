using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionfilename
    {
        #region Constructor

        public sessionfilename(string filename)
        {
            name = filename;
        }

        #endregion

        #region Member

        private string name;

        #endregion

        #region Properties

        public string filename
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        #endregion
    }
}