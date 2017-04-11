using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Sessions
{
    public class SessionActiveSubstances
    {
        #region Constructor
        public SessionActiveSubstances(int ActiveSubstanceId)
        {
            ActiveSubstance = ActiveSubstanceId;
        }

        #endregion

        #region Member

        public int ActiveSubstance;

        #endregion

        #region Properties


        public int ActiveSubstanceId
        {
            get
            {
                return ActiveSubstance;
            }
            set
            {
                ActiveSubstance = value;
            }
        }
        #endregion
    }
}