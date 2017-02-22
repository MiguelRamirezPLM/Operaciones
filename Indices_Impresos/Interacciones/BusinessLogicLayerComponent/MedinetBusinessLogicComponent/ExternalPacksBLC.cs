using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class ExternalPacksBLC
    {

        #region Constructors

        private ExternalPacksBLC() { }

        #endregion

        #region Public Methods

        public List<ExternalPacksInfo> getExternalPacks()
        {
            return MedinetDataAccessComponent.ExternalPacksDALC.Instance.getAll();
        }

        #endregion

        public static readonly ExternalPacksBLC Instance = new ExternalPacksBLC();

    }
}
