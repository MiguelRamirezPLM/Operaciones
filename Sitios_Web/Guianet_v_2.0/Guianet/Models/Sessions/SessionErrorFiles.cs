using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionErrorFiles
    {
        #region Constructor
        public SessionErrorFiles(List<string> LS)
        {
            dt = LS;
        }

        #endregion

        #region Member

        private List<string> dt;

        #endregion

        #region Properties

        public List<string> LS
        {
            get
            {
                return dt;
            }
            set
            {
                dt = value;
            }
        }
        #endregion
    }
}