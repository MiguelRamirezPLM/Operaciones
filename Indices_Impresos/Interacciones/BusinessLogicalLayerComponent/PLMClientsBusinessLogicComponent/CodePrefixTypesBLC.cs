using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CodePrefixTypesBLC
    {
        #region Constructors

        private CodePrefixTypesBLC() { }

        #endregion

        #region Public Methods

        public CodePrefixTypesInfo getCodePrefixType(int PrefixTypeId)
        {
            if (PrefixTypeId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.CodePrefixTypesDALC.Instance.getOne(PrefixTypeId);
        }

        public List<CodePrefixTypesInfo> getCodePrefixTypes()
        {
            return PLMClientsDataAccessComponent.CodePrefixTypesDALC.Instance.getCodePrefixTypes();
        }

        #endregion

        public static readonly CodePrefixTypesBLC Instance = new CodePrefixTypesBLC();
    }
}
