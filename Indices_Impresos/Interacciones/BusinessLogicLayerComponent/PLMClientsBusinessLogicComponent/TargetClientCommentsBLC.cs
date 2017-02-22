using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class TargetClientCommentsBLC
    {

        #region Constructors

        private TargetClientCommentsBLC() { }

        #endregion

        #region Public Methods

        public void addTargetClientComments(PLMClientsBusinessEntities.TargetClientCommentsInfo BEntity)
        {
            PLMClientsDataAccessComponent.TargetClientCommentsDALC.Instance.insert(BEntity);
        }

        #endregion

        public static readonly TargetClientCommentsBLC Instance = new TargetClientCommentsBLC();

    }
}
