using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroDataAccessComponent
{
   public class StatesDALC
    {
        #region Constructor

       private StatesDALC() { }

        #endregion
        #region Methods
       #region Select
       public List<AgroEntityFramework.States> getStatesByCountry(int countryId){
            
            List<AgroEntityFramework.States> states;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var sta = (from d in context.States
                           where d.CountryId==countryId
                           orderby d.StateName
                           select d);

                states = sta.ToList();
            }
            return states;
        }
        public AgroEntityFramework.States getStatesByIdEstado(int stateId)
        {

            AgroEntityFramework.States states;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var sta = (from d in context.States
                           where d.StateId == stateId
                           
                           select d).Single();

                states = sta;
            }
            return states;
        }

        
        public List<AgroEntityFramework.States> getStates()
            {            
            List<AgroEntityFramework.States> states;
            using (var context = new AgroEntityFramework.DEAQEntites())
                {
                    var sta = (from d in context.States
                               select d);
                    states = sta.ToList();
                }
                return states;
            }

        #endregion
        #endregion
        public static readonly StatesDALC Instance = new StatesDALC();
    }
}
