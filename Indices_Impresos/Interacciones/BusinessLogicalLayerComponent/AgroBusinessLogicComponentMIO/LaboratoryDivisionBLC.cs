using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;
using AgroDataAccessComponent;

namespace AgroBusinessLogicComponent
{
    public class LaboratoryDivisionBLC
    {
      #region Constructor

        private LaboratoryDivisionBLC() { }

      #endregion

        #region Public Methods

        public List<LaboratoriesInfo> getAll()
        {
           // return AgroPLMBusinessLogicComponent.LaboratoryDALC.Instance.getAll();
            return AgroDataAccessComponent.LaboratoryDivisionDALC.Instance.getAll();
        }


        public void AddLaboratory(LaboratoriesInfo businessEntity)
        {
            AgroDataAccessComponent.LaboratoryDivisionDALC.Instance.insert(businessEntity);
        }

        public void update(LaboratoriesInfo businessEntity)
        {
            AgroDataAccessComponent.LaboratoryDivisionDALC.Instance.updateLab(businessEntity);

             
        }


        public AgroBusinessEntries.LaboratoriesInfo  getLaboratories(int laboratoryId)
        {
              return AgroDataAccessComponent.LaboratoryDivisionDALC.Instance.getOne(laboratoryId);
        }


        public List<DivisionAddressInfo> getDivisionLab(int laboratoryId, int countryId)
        {
            return AgroDataAccessComponent.DivisionAddresDALC.Instance.getDivisionLab(laboratoryId, countryId);
        }


        #endregion

        public static readonly LaboratoryDivisionBLC Instance = new LaboratoryDivisionBLC();
    }
}
