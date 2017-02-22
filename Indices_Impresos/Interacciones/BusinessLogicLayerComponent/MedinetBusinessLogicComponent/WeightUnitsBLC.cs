using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class WeightUnitsBLC
    {

        #region Constructors

        private WeightUnitsBLC() { }

        #endregion

        #region Public Methods

        public List<WeightUnitsInfo> getWeightUnits()
        {
            return MedinetDataAccessComponent.WeightUnitsDALC.Instance.getAll();
        }

        #endregion

        public static readonly WeightUnitsBLC Instance = new WeightUnitsBLC();

    }
}
