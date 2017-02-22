using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_SyncDatabasesBusinessLogicComponent
{
    public class CE_EdtionAssignCodesBLC
    {
        #region Constructors

        private CE_EdtionAssignCodesBLC() { }

        #endregion

        #region Public Methods

        public int add(SyncDatabaseBusinessEntries.EditionAssignCodeInfo businessEntity)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_EdtionAssignCodesDALC.Instance.insert(businessEntity);
        }

        public void remove(SyncDatabaseBusinessEntries.EditionAssignCodeInfo businessEntity)
        {
            CE_SyncDatabasesDataAccessComponent.CE_EdtionAssignCodesDALC.Instance.delete(businessEntity);
        }

        #endregion

        public static readonly CE_EdtionAssignCodesBLC Instance = new CE_EdtionAssignCodesBLC();
    }
}
