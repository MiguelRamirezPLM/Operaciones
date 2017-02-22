using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SearchDate
    {
        #region Constructor
        public SearchDate(string Date)
        {
            dt = Date;
        }

        #endregion

        #region Member

        private string dt;

        #endregion

        #region Properties

        public string Date
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