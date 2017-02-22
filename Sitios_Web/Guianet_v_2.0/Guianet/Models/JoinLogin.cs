using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class JoinLogin
    {
        public int UsrUserId { get; set; }
        public bool UserActive { get; set; }
        public int UserCountryId { get; set; }
        public string UserEmail { get; set; }
        public string UserLastName { get; set; }
        public string UserName { get; set; }
        public string UserNickName { get; set; }
        public string UserPassword { get; set; }

        public int UserApplicationId { get; set; }
        public int UserRoleId { get; set; }
        public int UserUserId { get; set; }

        public bool RoleActive { get; set; }
        public string RoleDescription { get; set; }
        public int RoleId { get; set; }

        public int ApplicationsApplicationId { get; set; }
        public bool ApplicationsActive { get; set; }
        public string ApplicationsDescription { get; set; }
        public string ApplicationsHashKey { get; set; }
    }
}