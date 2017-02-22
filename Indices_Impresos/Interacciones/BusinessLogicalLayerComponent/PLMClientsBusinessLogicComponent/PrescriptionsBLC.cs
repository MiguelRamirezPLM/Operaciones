using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class PrescriptionsBLC
    {
        #region Constructors

        private PrescriptionsBLC() { }

        #endregion

        #region Public Methods

        public void savePrescriptionDrugs(PLMClientsBusinessEntities.PrescriptionsInfo prescription, List<PLMClientsBusinessEntities.DrugInfo> drugs)
        {
            string drugsList = "";

            if (drugs.Count != 0)
            {
                foreach (PLMClientsBusinessEntities.DrugInfo items in drugs)
                {
                    drugsList += "'" + items.Brand.ToString() 
                            + "','" + items.PharmaceuticalForm.ToString() 
                            + "','" + items.Laboratory.ToString() + "','"
                            + items.Category.ToString() + "','" 
                            + items.Presentation.ToString() + "'|";
                }
                drugsList = drugsList.Substring(0, drugsList.Length - 1);
            }

            PLMClientsDataAccessComponent.PrescriptionsDALC.Instance.savePrescriptionDrugs(prescription, drugsList);
        }

        #endregion

        public static readonly PrescriptionsBLC Instance = new PrescriptionsBLC();
    }
}
