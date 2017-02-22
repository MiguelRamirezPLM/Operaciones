using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class CountriesSs
    {
                 #region Constructor
        public CountriesSs(int LabId)
        {
            LaboratoryId = LabId;
        }

        #endregion

        #region Member

        private int LaboratoryId;

        #endregion

        #region Properties

        public int LabId
        {
            get
            {
                return LaboratoryId;
            }
            set
            {
                LaboratoryId = value;
            }
        }       
        #endregion
    }
}