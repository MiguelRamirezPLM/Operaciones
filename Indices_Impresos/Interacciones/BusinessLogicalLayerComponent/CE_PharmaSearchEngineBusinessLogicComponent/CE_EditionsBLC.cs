using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_EditionsBLC
    {
        #region Constructor

        private CE_EditionsBLC() { }

        #endregion

        #region Public Method

        public MedinetBusinessEntries.EditionsInfo getOne(int pk)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_EditionsDALC.Instance.getOne(pk);
        }

        public List<MedinetBusinessEntries.EditionsInfo> getAll()
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_EditionsDALC.Instance.getAll();
        }

        public MedinetBusinessEntries.EditionsInfo getByISBN(string isbn)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_EditionsDALC.Instance.getByISBN(isbn.Trim());
        }
        
        #endregion

        public static readonly CE_EditionsBLC Instance = new CE_EditionsBLC();

    }
}
