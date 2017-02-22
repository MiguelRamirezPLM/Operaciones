using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionsearchspeciality
    {
         #region Constructor
        public sessionsearchspeciality(string Description)
        {
            speciality = Description;
        }

        #endregion

        #region Member

        private string speciality;

        #endregion

        #region Properties

        public string Description
        {
            get
            {
                return speciality;
            }
            set
            {
                speciality = value;
            }
        }
        #endregion
    }
}