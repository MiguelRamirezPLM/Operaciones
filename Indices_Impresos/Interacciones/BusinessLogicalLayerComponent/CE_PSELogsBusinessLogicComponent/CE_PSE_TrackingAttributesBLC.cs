using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PSELogsBusinessLogicComponent
{
    public class CE_PSE_TrackingAttributesBLC
    {
        #region Constructors

        private CE_PSE_TrackingAttributesBLC() { }

        #endregion

        #region Public Methods

        public void addAttribute(PSELogsBusinessEntities.PSE_TrackingAttributeInfo attribute)
        {
            CE_PSELogsDataAccessComponent.CE_PSE_TrackingAttributesDALC.Instance.insert(attribute);
        }

        public List<PSELogsBusinessEntities.PSE_TrackingAttributeInfo> getLogsAttributes(int trackId)
        {
            return CE_PSELogsDataAccessComponent.CE_PSE_TrackingAttributesDALC.Instance.getLogsAttributes(trackId);
        }

        public void deleteAttributeLogs(PSELogsBusinessEntities.PSE_TrackingAttributeInfo attribute)
        {
            CE_PSELogsDataAccessComponent.CE_PSE_TrackingAttributesDALC.Instance.delete(attribute);
        }

        public void deleteAttributeLogs(string trackIds)
        {
            CE_PSELogsDataAccessComponent.CE_PSE_TrackingAttributesDALC.Instance.delete(trackIds);
        }

        #endregion

        public static readonly CE_PSE_TrackingAttributesBLC Instance = new CE_PSE_TrackingAttributesBLC();

    }
}
