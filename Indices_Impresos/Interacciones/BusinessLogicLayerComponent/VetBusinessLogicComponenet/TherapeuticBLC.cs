using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using VetBusinessEntries;

namespace VetBusinessLogicComponenet
{
    public sealed class  TherapeuticBLC
    {
       #region Constructors

        private TherapeuticBLC() { }

        #endregion


        #region Public Methods


        public List<TherapeuticInfo> getTherapeutics(String code, int editionId, string searchText)
        {
            if (editionId <= 0)
                throw new ArgumentException("The edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {

                    List<TherapeuticInfo> therapeuticsList = VetDataAccessComponent.TherapeuticsDALC.Instance.getTherapeuticsByText(editionId, searchText);


                    return therapeuticsList;
                }

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");

            }

        }



        #endregion


        #region Private Methods


        #endregion

        public static readonly TherapeuticBLC Instance = new TherapeuticBLC();



    }
}
