using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class ProjectsBLC
    {
        #region Constructors

        private ProjectsBLC() { }

        #endregion

        #region Public Methods

        public List<ProjectInfo> getProjects()
        {
            return PLMUsersDataAccessComponent.ProjectsDALC.Instance.getProjects();
        }

        public List<ProjectInfo> getProjectsByUser(int userId)
        {
            return PLMUsersDataAccessComponent.ProjectsDALC.Instance.getProjectsByUser(userId);
        }

        public void insert(ProjectInfo businessEntity)
        {
            PLMUsersDataAccessComponent.ProjectsDALC.Instance.insert(businessEntity);
        }

        public ProjectInfo getOne(int projectId)
        {
            if (projectId <= 0)
                throw new ArgumentException("The project does not exist.");

            return
                PLMUsersDataAccessComponent.ProjectsDALC.Instance.getOne(projectId);
        }

        #endregion
        
        public static readonly ProjectsBLC Instance = new ProjectsBLC();
    }
}
