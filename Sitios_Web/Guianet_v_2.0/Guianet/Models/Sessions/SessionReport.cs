using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionReport
    {
        #region Constructor

        public SessionReport(string NickName, int UserId)
        {
            name = NickName;
            User = UserId;
        }

        #endregion

        #region Member

        private string name;
        private int User;

        #endregion

        #region Properties

        public string NickName
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

        public int UserId
        {
            get
            {
                return User;
            }
            set
            {
                User = value;
            }
        }

        #endregion
    }
}