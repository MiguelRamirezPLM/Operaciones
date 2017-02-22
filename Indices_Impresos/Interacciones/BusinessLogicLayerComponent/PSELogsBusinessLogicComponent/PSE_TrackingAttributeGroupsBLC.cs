using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSELogsBusinessLogicComponent
{
    public class PSE_TrackingAttributeGroupsBLC
    {

        #region Constructors

        private PSE_TrackingAttributeGroupsBLC() { }

        #endregion

        #region Public Methods

        public void addAttribute(PSELogsBusinessEntities.PSE_TrackingAttributeGroupsInfo attribute)
        {
            PSELogsDataAccessComponent.PSE_TrackingAttributeGroupsDALC.Instance.insert(attribute);
        }

        #endregion

        public static readonly PSE_TrackingAttributeGroupsBLC Instance = new PSE_TrackingAttributeGroupsBLC();

    }
}
