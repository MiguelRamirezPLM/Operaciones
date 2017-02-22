using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class FoliosBLC
    {
        #region Constructors

        private FoliosBLC() { }

        #endregion

        #region Public Methods

        public int addFolio(FolioInfo BEntity)
        {
            return PLMClientsDataAccessComponent.FoliosDALC.Instance.insert(BEntity);
        }

        public void removeFolio(int folioId)
        {
            if (folioId <= 0)
                throw new ArgumentException("The folio does not exist.");

            PLMClientsDataAccessComponent.FoliosDALC.Instance.delete(folioId);
        }

        public void updateFolio(FolioInfo BEntity)
        {
            PLMClientsDataAccessComponent.FoliosDALC.Instance.update(BEntity);
        }

        public FolioInfo getFolio(int folioId)
        {
            if (folioId <= 0)
                throw new ArgumentException("The folio does not exist.");

            return PLMClientsDataAccessComponent.FoliosDALC.Instance.getOne(folioId);
        }

        public bool chekFolio(string folioString)
        {
            return PLMClientsDataAccessComponent.FoliosDALC.Instance.checkFolio(folioString);
        }

        public FolioInfo getFolio(string folioString)
        {
            return PLMClientsDataAccessComponent.FoliosDALC.Instance.getByName(folioString);
        }

        #endregion

        public static readonly FoliosBLC Instance = new FoliosBLC();

    }
}
