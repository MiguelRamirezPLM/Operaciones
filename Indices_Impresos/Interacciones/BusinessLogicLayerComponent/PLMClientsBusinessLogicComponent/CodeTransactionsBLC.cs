using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CodeTransactionsBLC
    {
        #region Constructors

        private CodeTransactionsBLC() { }

        #endregion

        #region Public Methods

        public void addTransaction(CodeTransactionInfo BEntity)
        {
            PLMClientsDataAccessComponent.CodeTransactionsDALC.Instance.insert(BEntity);
        }

        public void removeTransaction(CodeTransactionInfo BEntity)
        {
            PLMClientsDataAccessComponent.CodeTransactionsDALC.Instance.delete(BEntity);
        }

        public CodeTransactionInfo getTransaction(CodeTransactionInfo BEntity)
        {
            return PLMClientsDataAccessComponent.CodeTransactionsDALC.Instance.getOne(BEntity);
        }

        #endregion

        public static readonly CodeTransactionsBLC Instance = new CodeTransactionsBLC();

    }
}
