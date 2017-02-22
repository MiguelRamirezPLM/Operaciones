using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class MedicalCalculatorsBLC
    {

        #region Constructors

        private MedicalCalculatorsBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> getCalculatorsByPrefix(string prefix)
        {
            if (prefix == null || prefix.Trim() == "")
                throw new ArgumentException("The Prefix does not exist.");
            else
                return PLMClientsDataAccessComponent.MedicalCalculatorsDALC.Instance.getCalculatorsByPrefix(prefix);
        }

        public List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> getCalculatorsBySpeciality(PLMClientsBusinessEntities.Catalogs.Specialities speciality)
        {
            return PLMClientsDataAccessComponent.MedicalCalculatorsDALC.Instance.getCalculatorsBySpeciality(Convert.ToInt32(speciality));
        }

        public List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> getCalculatorsByPrefixBySpeciality(string prefix, PLMClientsBusinessEntities.Catalogs.Specialities speciality)
        {
            if (prefix == null || prefix.Trim() == "")
                throw new ArgumentException("The Prefix does not exist.");
            else
                return PLMClientsDataAccessComponent.MedicalCalculatorsDALC.Instance.getCalculatorsByPrefixBySpeciality(prefix, Convert.ToInt32(speciality));
        }

        public PLMClientsBusinessEntities.CalculatorsDetailInfo getCalculatorDetail(int calculatorId)
        {
            if (calculatorId <= 0)
                throw new ArgumentException("The calculator does not exist.");
            else
                return PLMClientsDataAccessComponent.MedicalCalculatorsDALC.Instance.getCalculatorDetail(calculatorId);
        }

        public PLMClientsBusinessEntities.ResultsInfo getResultByCalculator(PLMClientsBusinessEntities.CalculatorsDetailInfo calculatorInfo)
        {
            if (calculatorInfo != null)
                return PLMClientsDataAccessComponent.MedicalCalculatorsDALC.Instance.getResultByCalculator(calculatorInfo);
            else
                throw new Exception("Tha calculator does not exist.");
        }

        #endregion

        public static readonly MedicalCalculatorsBLC Instance = new MedicalCalculatorsBLC();

    }
}
