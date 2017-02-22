using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroBusinessLogicComponent
{
   public class StatesBLC
    {
          #region Contructors

       private StatesBLC() { }

        #endregion

        #region Methods
      public List<AgroEntityFramework.States> getStatesByCountryStateId(int stateId)
      {
          return AgroDataAccessComponent.StatesDALC.Instance.getStatesByCountry(stateId);
      }

      public AgroEntityFramework.States getStateByStateId(int stateId)
      {
          return AgroDataAccessComponent.StatesDALC.Instance.getStatesByIdEstado(stateId); 
      }

      public List<AgroEntityFramework.States> getStates()
      {
          return AgroDataAccessComponent.StatesDALC.Instance.getStates();
      }

        #endregion

      public static readonly StatesBLC Instance = new StatesBLC(); 
    }
}
