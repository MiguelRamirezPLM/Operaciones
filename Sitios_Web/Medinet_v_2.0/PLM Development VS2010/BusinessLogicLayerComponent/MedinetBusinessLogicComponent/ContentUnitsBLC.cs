using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class ContentUnitsBLC
    {

        #region Constructors

        private ContentUnitsBLC() { }

        #endregion

        #region Public Methods

        public List<ContentUnitsInfo> getContentUnits()
        {
            return MedinetDataAccessComponent.ContentUnitsDALC.Instance.getAll();
        }

        #endregion

        public static readonly ContentUnitsBLC Instance = new ContentUnitsBLC();

    }
}
