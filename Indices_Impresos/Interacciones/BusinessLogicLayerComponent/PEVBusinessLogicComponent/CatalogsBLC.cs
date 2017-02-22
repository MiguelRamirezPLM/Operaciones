using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessLogicComponent
{
    public class CatalogsBLC
    {

        #region Constructor

        private CatalogsBLC() { }
        
        #endregion

        #region Public Methods

        //Retrieves information about a Country by CountryName
        public PEVBusinessEntries.CountryInfo rocGetCountry(string code, string id)
        {
                
            PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
            validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                return PEVDataAccessComponent.CatalogsDALC.Instance.rocGetCountryById(id);
            }

            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }

        }

        //Retrieves information from the latest Edition
        public List<PEVBusinessEntries.CategoryInfo> rocGetCategories(string code, int divisionId) 
        {
            if (divisionId < 1)
            {
                throw new ArgumentException("The division does not exist");
            }
            else 
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.CatalogsDALC.Instance.rocGetCategoriesByDivisionId(divisionId);
                }
                else 
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Categories by an DivisionId
        public PEVBusinessEntries.EditionInfo rocGetEdition(string code, int countryId) 
        {
            if (countryId < 1)
            {
                throw new ArgumentException("The Country does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.CatalogsDALC.Instance.rocGetEdition(countryId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        public static readonly CatalogsBLC Instance = new CatalogsBLC();
    }
}
