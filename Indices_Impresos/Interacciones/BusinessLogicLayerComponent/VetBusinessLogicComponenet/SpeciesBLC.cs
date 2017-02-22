using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;

namespace VetBusinessLogicComponenet
{
    public sealed class SpeciesBLC
    {
        #region Constructors

        private SpeciesBLC() { }

        #endregion


        #region Public Methods


        public List<SpecieInfo> getSpecies(String code, int editionId, string searchText)
        {
            if (editionId <= 0)
                throw new ArgumentException("The edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {

                    List<SpecieInfo> speciesList = VetDataAccessComponent.SpeciesDALC.Instance.getSpeciesByText(editionId, searchText);


                    return speciesList;
                }

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");

            }

        }



        #endregion


        #region Private Methods


        #endregion

        public static readonly SpeciesBLC Instance = new SpeciesBLC();





    }
}
