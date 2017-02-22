using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroNetBusinessEntries;
using AgroNetDataAccessComponent;

namespace AgroNetBusinessLogicalComponent
{
    public class LaboratoryDivisionBLC
    {
        #region Constructor

            private LaboratoryDivisionBLC() { }

         #endregion

        #region Constructor
     
           
        public List<LaboratoriesInfo> getAll()
            {
                return AgroNetDataAccessComponent.LaboratoriesDALC.Instance.getAll();                
            }


        public void delete (int labId)  
        {
            AgroNetDataAccessComponent.LaboratoriesDALC.Instance.delete(labId);
        }
         
        public void update (LaboratoriesInfo businessEntity)
        {
            AgroNetDataAccessComponent.LaboratoriesDALC.Instance.update(businessEntity);
        }


        public int addLaboratories(LaboratoriesInfo businessEntity)
        {
            return AgroNetDataAccessComponent.LaboratoriesDALC.Instance.insert(businessEntity);
        }

        //public int addAddress(PLMClientsBusinessEntities.AddressInfo BEntity)
        //{
        //    return PLMClientsDataAccessComponent.AddressesDALC.Instance.insert(BEntity);
        //}


        #endregion

        public static readonly LaboratoryDivisionBLC Instance = new LaboratoryDivisionBLC();
    }
}
