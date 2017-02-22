using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;



namespace VetBusinessLogicComponenet
{
   public sealed class CategoriesBLC
    {

       #region Constructors

        private CategoriesBLC() { }

        #endregion


        #region Public Methods


        public List<CategoryInfo> getCategoryByProductType(string code, int editionId, int divisionId, VetBusinessEntries.Types.ProductTypes typesId)
        {

            if(editionId <= 0 || divisionId <=0)
                throw new ArgumentException("The book edition or substance does not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if( validCode!=null && validCode.CodeStatusId==PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

               {

                   List<CategoryInfo> categoryList =VetDataAccessComponent.CategoriesDALC.Instance.getCategoryByProductType(editionId,divisionId,(byte)typesId);

                   return categoryList;

               }

               else
                   throw new ApplicationException("El código no es válido o se encuentra inactivo.");



            }

        }


        #endregion

        public static readonly CategoriesBLC Instance = new CategoriesBLC();


    }
}
