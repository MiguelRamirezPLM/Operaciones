using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;

namespace VetBusinessLogicComponenet
{
     public sealed class SubstancesBLC
    {
        #region Constructors

         private SubstancesBLC() { }

        #endregion

        #region Public Methods


         public List<ActiveSubstanceInfo> getSubstances(String code, int editionId, string searchText)
         {
             if (editionId <= 0)
                 throw new ArgumentException("The edition does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                 {

                     List<ActiveSubstanceInfo> substancesList = VetDataAccessComponent.ActiveSubstancesDALC.Instance.getSubstancesByText(editionId, searchText);


                     return substancesList;
                 }

                 else
                     throw new ApplicationException("El código no es válido o se encuentra inactivo.");

             }

         }



         #endregion


         #region Private Methods


         #endregion

         public static readonly SubstancesBLC Instance = new SubstancesBLC();



    }
}
