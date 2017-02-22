using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CodePrefixesBLC
    {
        #region Constructors

        private CodePrefixesBLC() { }

        #endregion

        #region Public Methods

        public CodePrefixInfo getCodePrefix(int codePrefixId)
        {
            if (codePrefixId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.CodePrefixesDALC.Instance.getOne(codePrefixId);
        }

        public CodePrefixInfo getCodePrefix(string codePrefix)
        {
            return PLMClientsDataAccessComponent.CodePrefixesDALC.Instance.getByPrefixName(codePrefix);
        }

        public void updateCode(CodePrefixInfo BEntity)
        {
            PLMClientsDataAccessComponent.CodePrefixesDALC.Instance.update(BEntity);
        }

        public CodePrefixInfo getCodePrefixByParent(int parentId)
        {
            if (parentId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.CodePrefixesDALC.Instance.getByParentId(parentId);
        }

        public CodePrefixInfo getByCode(String codeString)
        {
            if (codeString == null)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.CodePrefixesDALC.Instance.getByCode(codeString);
        }

        public List<CodePrefixInfo> getCodePrefixes()
        {
            return PLMClientsDataAccessComponent.CodePrefixesDALC.Instance.getCodePrefixes();
        }

        public void insert(CodePrefixInfo businessEntity)
        {
            PLMClientsDataAccessComponent.CodePrefixesDALC.Instance.insert(businessEntity);
        }

        #endregion

        public static readonly CodePrefixesBLC Instance = new CodePrefixesBLC();

    }
}



