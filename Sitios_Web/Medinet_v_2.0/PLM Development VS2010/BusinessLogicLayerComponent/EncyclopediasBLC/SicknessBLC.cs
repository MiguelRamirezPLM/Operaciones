using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;

namespace EncyclopediasBusinessLogicComponent
{
  public class SicknessBLC
    {
      #region Constructors

      private SicknessBLC() { }

      #endregion

      #region public

      public SicknessInfo getSickness(string code, int sicknessId)
      {
          if (sicknessId <= 0)
              throw new ArgumentException("The Sickness does not exist");
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                  return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getOne(sicknessId);

              else
                  throw new ApplicationException("The code is invalid or inactive");
          }
      }

      public List<SicknessInfo> getSicknesses(string code)
      {
          
              PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                  return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getAll();

              else
                  throw new ApplicationException("The code is invalid or inactive");
      }

      public List<SicknessInfo> getSicknessesByText(string code,string textSearch)
      {
          if (textSearch.Length > 3)
              textSearch = "%" + textSearch + "%";
          else
              textSearch = textSearch + "%";

          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
              return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getSicknessByText(textSearch);

          else
              throw new ApplicationException("The code is invalid or inactive");
      }

      public List<SicknessInfo> getSicknessesByTextByEdition(string code, string textSearch,string isbn)
      {
          if (textSearch.Length > 3)
              textSearch = "%" + textSearch + "%";
          else
              textSearch = textSearch + "%";

          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
              return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getSicknessByTextByEdition(textSearch,isbn);

          else
              throw new ApplicationException("The code is invalid or inactive");
      }

      public List<SicknessInfo> getSicknessesByEdition(string code,string isbn)
      {

          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
              return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getAllByEdition(isbn);

          else
              throw new ApplicationException("The code is invalid or inactive");
      }

      public List<SicknessInfo> getSicknessesByEncyclopedia(string code, int encyclopediaId)
      {

          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
              return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getSicknessByEncyclopedia(encyclopediaId);

          else
              throw new ApplicationException("The code is invalid or inactive");
      }

      public List<SicknessInfo> getSicknessesByEncyclopediaByEdition(string code, int encyclopediaId,string isbn)
      {

          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
              return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getSicknessByEncyclopediaByEdition(encyclopediaId,isbn);

          else
              throw new ApplicationException("The code is invalid or inactive");
      }

      public List<SicknessInfo> getSicknessesByICD(string code, int icdId)
      {

          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
              return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getSicknessByICD(icdId);

          else
              throw new ApplicationException("The code is invalid or inactive");
      }

      public List<SicknessInfo> getSicknessesByICDByEdition(string code, int icdId,string isbn)
      {

          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
              return EncyclopediasDataAccessComponent.SicknessDALC.Instance.getSicknessByICDByEdition(icdId,isbn);

          else
              throw new ApplicationException("The code is invalid or inactive");
      }

      #endregion

      public static readonly SicknessBLC Instance = new SicknessBLC();
    }
}
