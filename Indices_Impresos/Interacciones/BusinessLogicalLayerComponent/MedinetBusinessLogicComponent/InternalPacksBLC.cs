using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class InternalPacksBLC
    {

        #region Constructors

        private InternalPacksBLC() { }

        #endregion

        #region Public Methods

        public List<InternalPacksInfo> getInternalPacks()
        {
            return MedinetDataAccessComponent.InternalPacksDALC.Instance.getAll();
        }

        #endregion

        public static readonly InternalPacksBLC Instance = new InternalPacksBLC();

    }
}
