using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class searchEquipment
    {
        #region Constructor

        public searchEquipment(String EquipmentName)
        {
            term = EquipmentName;
        }

        #endregion

        #region Member

        private string term;

        #endregion

        #region Properties

        public string EquipmentName
        {
            get
            {
                return term;
            }
            set
            {
                term = value;
            }
        }
        #endregion
    }
}