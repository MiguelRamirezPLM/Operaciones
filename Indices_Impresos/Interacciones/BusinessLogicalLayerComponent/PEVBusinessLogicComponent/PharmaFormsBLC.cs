using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessLogicComponent
{
  public class PharmaFormsBLC
  {
      #region Constructor

      private PharmaFormsBLC() {} 

      #endregion

      #region Methods

      //Retrieves all pharmaforms by product
      public List<PEVBusinessEntries.PharmaFormsByProductInfo> rocGetPharmaFormsByProduct(string code, int productId)
      {
          if (productId <= 0 )
              throw new ArgumentException("The product does no exist");
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                  return PEVDataAccessComponent.PharmaFormsDALC.Instance.rocGetPharmaFormsByProduct(productId);

              else
                  throw new ApplicationException("El código no es válido o se encuentra inactivo.");
          }
      
      }

  
      #endregion

      public static readonly PharmaFormsBLC Instance = new PharmaFormsBLC();
  }
}
