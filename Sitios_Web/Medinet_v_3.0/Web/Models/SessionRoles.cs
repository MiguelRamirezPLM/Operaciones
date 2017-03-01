using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionRoles
    {
        #region Constructor
        public SessionRoles(int Id, string Des, bool Act)
        {
            RoleId = Id;
            Description = Des;
            Active = Act;
        }
        #endregion

        #region Member

        public int RoleId;
        public string Description;
        public bool Active;
        #endregion

        #region Properties
        public int id
        {
            get
            {
                return RoleId;
            }
            set
            {
                RoleId = value;
            }
        }
        public string Des
        {
            get
            {
                return Description;
            }
            set
            {
                Description = value;
            }
        }
        public bool Act
        {
            get
            {
                return Active;
            }
            set
            {
                Active = value;
            }
        }

        #endregion
    }


    
}