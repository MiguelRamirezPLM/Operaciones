using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;

namespace EncyclopediasBusinessLogicComponent
{
   public class KeywordsBLC
    {
       #region Constructors

       private KeywordsBLC() { }

       #endregion

       #region Public

       public KeywordInfo getKeyWord(string code, int keyWordId)
       {
           if (keyWordId <= 0)
               throw new ArgumentException("The KeyWordId does not exist");
           else
           {
               PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   return EncyclopediasDataAccessComponent.KeyWordDALC.Instance.getOne(keyWordId);

               else
                   throw new ApplicationException("The code is invalid or inactive");
           }
       }

       public List<KeywordInfo> getKeyWords(string code)
       {
       
               PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   return EncyclopediasDataAccessComponent.KeyWordDALC.Instance.getAll();

               else
                   throw new ApplicationException("The code is invalid or inactive");
       }

       public List<KeywordInfo> getKeyWordsByEdition(string code,string isbn)
       {
           
               PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   return EncyclopediasDataAccessComponent.KeyWordDALC.Instance.getAllByEdition(isbn);

               else
                   throw new ApplicationException("The code is invalid or inactive");
       }

       public List<KeywordInfo> getKeyWordsByText(string code, string searchText)
       {

           if (searchText.Length > 3)
               searchText = "%" + searchText + "%";
           else
               searchText = searchText + "%";

           PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

           if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
               return EncyclopediasDataAccessComponent.KeyWordDALC.Instance.getKeyWordsByText(searchText);

           else
               throw new ApplicationException("The code is invalid or inactive");
       }

       public List<KeywordInfo> getKeyWordsByTextByEdition(string code, string searchText,string isbn)
       {
           if (searchText.Length > 3)
               searchText = "%" + searchText + "%";
           else
               searchText = searchText + "%";

           PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

           if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
               return EncyclopediasDataAccessComponent.KeyWordDALC.Instance.getKeyWordsByTextByEdition(searchText,isbn);

           else
               throw new ApplicationException("The code is invalid or inactive");
       }

       #endregion

       public static readonly KeywordsBLC Instance = new KeywordsBLC();
    }
}
