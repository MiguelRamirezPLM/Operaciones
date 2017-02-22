using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class EditionSymptomBLC
    {
        #region Constructors

        private EditionSymptomBLC() { }

        #endregion

        public void addEditionSymptom(MedinetBusinessEntries.EditionSymptomInfo bEntity) {
            MedinetDataAccessComponent.EditionSymptomDALC.Instance.insert(bEntity);
        }

        public void deleteEditionSymptom(MedinetBusinessEntries.EditionSymptomInfo bEntity)
        {
            MedinetDataAccessComponent.EditionSymptomDALC.Instance.delete(bEntity);
        }
        public void updateEditionSymptom(MedinetBusinessEntries.EditionSymptomInfo bEntity)
        {
            MedinetDataAccessComponent.EditionSymptomDALC.Instance.update(bEntity);
        }

        public List<MedinetBusinessEntries.EditionSymptomInfo> getEditionSymptoms(int editionId,string search) {
            return MedinetDataAccessComponent.EditionSymptomDALC.Instance.getEditionSymptoms(editionId, search);
        }
        public static readonly EditionSymptomBLC Instance = new EditionSymptomBLC();
    }
}
