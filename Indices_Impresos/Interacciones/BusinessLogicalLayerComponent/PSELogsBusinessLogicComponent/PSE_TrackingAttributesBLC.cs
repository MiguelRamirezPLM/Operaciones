using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSELogsBusinessLogicComponent
{
    public class PSE_TrackingAttributesBLC
    {
        #region Constructors

        private PSE_TrackingAttributesBLC() { }

        #endregion

        #region Public Methods

        public void addAttribute(PSELogsBusinessEntities.PSE_TrackingAttributeInfo attribute)
        {
            PSELogsDataAccessComponent.PSE_TrackingAttributesDALC.Instance.insert(attribute);
        }

        #endregion

        public static readonly PSE_TrackingAttributesBLC Instance = new PSE_TrackingAttributesBLC();
    }
}
