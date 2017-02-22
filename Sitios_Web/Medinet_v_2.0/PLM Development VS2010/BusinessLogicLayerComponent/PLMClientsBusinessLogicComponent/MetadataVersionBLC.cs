using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class MetadataVersionBLC
    {

        #region Constructors

        private MetadataVersionBLC() { }

        #endregion

        #region Public Methods

        public string getUrlVersion(string codeString)
        {
            return PLMClientsDataAccessComponent.MetadataVersionDALC.Instance.getUrlVersion(codeString);
        }

        #endregion

        public static readonly MetadataVersionBLC Instance = new MetadataVersionBLC();
    }
}
