using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;

namespace EncyclopediasBusinessLogicComponent
{
    public class EncyclopediaBLC
    {
         #region Constructors

        private EncyclopediaBLC() { }

        #endregion

        #region Public
        
        public EncyclopediaInfo getEncyclopedia(string code, int encyclopediaId) {
            if (encyclopediaId <= 0)
                throw new ArgumentException("The encyclopedia does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getOne(encyclopediaId);

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
        }

        public List<EncyclopediaInfo> getEncyclopedias(string code)
        {


            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getAll();

            else
                throw new ApplicationException("The code is invalid or inactive");
        }

        public List<EncyclopediaInfo> getEncyclopediasByEdition(string code,string isbn)
        {

            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getAllByEdition(isbn);

            else
                throw new ApplicationException("The code is invalid or inactive");
        }

        public List<EncyclopediaInfo> getEncyclopediasByText(string code,string textSearch) {

            if (textSearch.Length > 3)
                textSearch = "%" + textSearch + "%";
            else
                textSearch = textSearch + "%";
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByText(textSearch);

                else
                    throw new ApplicationException("The code is invalid or inactive");
        
        }

        public List<EncyclopediaInfo> getEncyclopediasByTextByEdition(string code, string textSearch,string isbn)
        {
            if (textSearch.Length > 3)
                textSearch = "%" + textSearch + "%";
            else
                textSearch = textSearch + "%";

            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByTextByEdition(textSearch,isbn);

            else
                throw new ApplicationException("The code is invalid or inactive");
        }

         public List<EncyclopediaInfo> getEncyclopediasByType(string code, int encyclopediaTypeId)
         {
             if (encyclopediaTypeId <= 0)
                 throw new ArgumentException("The encyclopediaType does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByType(encyclopediaTypeId);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }

         public List<EncyclopediaInfo> getEncyclopediasByTypeByEdition(string code, int encyclopediaTypeId,string isbn)
         {
             if (encyclopediaTypeId <= 0)
                 throw new ArgumentException("The encyclopediaType does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByTypeByEdition(encyclopediaTypeId,isbn);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }

         public List<EncyclopediaInfo> getEncyclopediasByICD(string code, int ICDId)
         {
             if (ICDId <= 0)
                 throw new ArgumentException("The ICD does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByICD(ICDId);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }

         public List<EncyclopediaInfo> getEncyclopediasByICDByEdition(string code, int ICDId,string isbn)
         {
             if (ICDId <= 0)
                 throw new ArgumentException("The ICD does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByICDByEdition(ICDId,isbn);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }

         public List<EncyclopediaInfo> getEncyclopediasByKeyWord(string code, int keyWordId)
         {
             if (keyWordId <= 0)
                 throw new ArgumentException("The KeyWord does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByKeyword(keyWordId);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }

         public List<EncyclopediaInfo> getEncyclopediasByKeyWordByEdition(string code, int keyWordId, string isbn)
         {
             if (keyWordId <= 0)
                 throw new ArgumentException("The KeyWord does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByKeywordByEdition(keyWordId,isbn);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }

         public List<EncyclopediaInfo> getEncyclopediasBySickness(string code, int sicknessId)
         {
             if (sicknessId <= 0)
                 throw new ArgumentException("The sickness does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasBySickness(sicknessId);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }

         public List<EncyclopediaInfo> getEncyclopediasBySicknessByEdition(string code, int sicknessId,string isbn)
         {
             if (sicknessId <= 0)
                 throw new ArgumentException("The sickness does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasBySicknessByEdition(sicknessId,isbn);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }

         public List<EncyclopediaInfo> getEncyclopediasByProduct(string code,int productId, int pharmaFormId)
         {
             if (productId <= 0 || pharmaFormId <=0)
                 throw new ArgumentException("The Product or PharmaForm does not exist");
             else
             {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                 if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                     return EncyclopediasDataAccessComponent.EncyclopediaDALC.Instance.getEncyclopediasByProduct(productId,pharmaFormId);

                 else
                     throw new ApplicationException("The code is invalid or inactive");
             }
         }


        #endregion

         public static readonly EncyclopediaBLC Instance = new EncyclopediaBLC();
    }
}
