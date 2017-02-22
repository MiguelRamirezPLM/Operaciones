using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ProfessionsBLC
    {

        #region Constructors

        private ProfessionsBLC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.ProfessionInfo getProfession(int professionId)
        {
            return PLMClientsDataAccessComponent.ProfessionsDALC.Instance.getOne((short)professionId);
        }

        public List<ProfessionInfo> getByParent(short parentId) 
        {
            return PLMClientsDataAccessComponent.ProfessionsDALC.Instance.getByParent(parentId);
        }

        #endregion

        public static readonly ProfessionsBLC Instance = new ProfessionsBLC();

    }
}
