using System;
using System.Collections.Generic;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class JobPracticesBLC
    {
        #region Constructors

        private JobPracticesBLC() { }

        #endregion

        #region Public Methods

        public void addJobPractice(PLMClientsBusinessEntities.JobPracticeInfo BEntity)
        {
            PLMClientsDataAccessComponent.JobPracticesDALC.Instance.insert(BEntity);
        }

        public void removeJobPractice(PLMClientsBusinessEntities.JobPracticeInfo BEntity)
        {
            PLMClientsDataAccessComponent.JobPracticesDALC.Instance.delete(BEntity);
        }

        public void removeJobPractice(int clientId, byte practiceId)
        {
            PLMClientsDataAccessComponent.JobPracticesDALC.Instance.delete(clientId, practiceId);
        }

        public void updateJobPractice(PLMClientsBusinessEntities.JobPracticeInfo BEntity)
        {
            PLMClientsDataAccessComponent.JobPracticesDALC.Instance.update(BEntity);
        }

        public PLMClientsBusinessEntities.JobPracticeInfo getJobPracticeByClient(int clientId, byte practiceId, byte jobPlaceId)
        {
            if (clientId <= 0 || practiceId == 0 || jobPlaceId == 0)
                throw new ArgumentException("The client, practice or job place does not exist.");

            return PLMClientsDataAccessComponent.JobPracticesDALC.Instance.getOne(clientId, practiceId, jobPlaceId);

        }

        public List<PLMClientsBusinessEntities.JobPracticeInfo> getJobPracticeByClient(int clientId)
        {
            return PLMClientsDataAccessComponent.JobPracticesDALC.Instance.getByClient(clientId);
        }

        #endregion

        public static readonly JobPracticesBLC Instance = new JobPracticesBLC();

    }
}
