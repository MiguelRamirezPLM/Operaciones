using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class AttributeGroupBLC
    {

        #region Constructors

        private AttributeGroupBLC() { }

        #endregion

        #region Public Methods

        public MedinetBusinessEntries.AttributeGroupInfo getAttributeGroup(int attributeGroupId)
        {
            if (attributeGroupId <= 0)
                throw new ArgumentException("The attribute group does not exist.");

            return MedinetDataAccessComponent.AttributeGroupDALC.Instance.getOne(attributeGroupId);
        }

        public List<AttributeGroupInfo> getAttributeGroups()
        {
            return MedinetDataAccessComponent.AttributeGroupDALC.Instance.getAll();
        }

        #endregion

        public static readonly AttributeGroupBLC Instance = new AttributeGroupBLC();

    }
}
