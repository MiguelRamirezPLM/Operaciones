using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class MedicalCalculatorsDALC : PLMClientsDataAccessAdapter<MedicalCalculatorsInfo>
    {

        #region Constructors

        private MedicalCalculatorsDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> getCalculatorsByPrefix(string prefix)
        {
            List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> BECollection = new List<PLMClientsBusinessEntities.MedicalCalculatorsInfo>();

            DbCommand dbCmd = MedicalCalculatorsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMedicalCalculators");

            // Add the parameters:
            MedicalCalculatorsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MedicalCalculatorsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> getCalculatorsBySpeciality(int specialityId)
        {
            List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> BECollection = new List<PLMClientsBusinessEntities.MedicalCalculatorsInfo>();

            DbCommand dbCmd = MedicalCalculatorsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMedicalCalculators");

            // Add the parameters:
            MedicalCalculatorsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MedicalCalculatorsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> getCalculatorsByPrefixBySpeciality(string prefix, int specialityId)
        {
            List<PLMClientsBusinessEntities.MedicalCalculatorsInfo> BECollection = new List<PLMClientsBusinessEntities.MedicalCalculatorsInfo>();

            DbCommand dbCmd = MedicalCalculatorsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMedicalCalculators");

            // Add the parameters:
            MedicalCalculatorsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            MedicalCalculatorsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MedicalCalculatorsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public PLMClientsBusinessEntities.CalculatorsDetailInfo getCalculatorDetail(int calculatorId)
        {
            List<PLMClientsBusinessEntities.CalculatorsDetailInfo> BECollection = new List<CalculatorsDetailInfo>();

            DbCommand dbCmd = MedicalCalculatorsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMedicalCalculators");

            // Add the parameters:
            MedicalCalculatorsDALC.InstanceDatabase.AddParameter(dbCmd, "@calculatorId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, calculatorId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MedicalCalculatorsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    CalculatorsDetailInfo record = new CalculatorsDetailInfo();

                    record.CalculatorId = Convert.ToInt32(dataReader["CalculatorId"]);
                    record.CalculatorName = dataReader["CalculatorName"].ToString();

                    if (dataReader["CalculatorDescription"] != System.DBNull.Value)
                        record.CalculatorDescription = dataReader["CalculatorDescription"].ToString();

                    if (dataReader["CalculatorFormula"] != System.DBNull.Value)
                        record.CalculatorFormula = dataReader["CalculatorFormula"].ToString();

                    if (dataReader["CalculatorReferences"] != System.DBNull.Value)
                        record.CalculatorReferences = dataReader["CalculatorReferences"].ToString();

                    if (dataReader["SQLSyntax"] != System.DBNull.Value)
                        record.SQLSyntax = dataReader["SQLSyntax"].ToString();

                    DbCommand result = MedicalCalculatorsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVariablesCalculator");

                    // Add the parameters:
                    MedicalCalculatorsDALC.InstanceDatabase.AddParameter(result, "@calculatorId", DbType.Int32,
                        ParameterDirection.Input, string.Empty, DataRowVersion.Current, record.CalculatorId);

                    using (IDataReader dataCurrent = MedicalCalculatorsDALC.InstanceDatabase.ExecuteReader(result))
                    {
                        while (dataCurrent.Read())
                        {
                            PLMClientsBusinessEntities.VariablesInfo variableItem = new VariablesInfo();

                            variableItem.VariableId = Convert.ToInt32(dataCurrent["VariableId"]);
                            variableItem.VariableName = dataCurrent["VariableName"].ToString();
                            variableItem.Nomenclature = dataCurrent["Nomenclature"].ToString();
                            variableItem.Active = Convert.ToBoolean(dataCurrent["Active"]);

                            record.VariablesList.Add(variableItem);
                        }
                    }
                    BECollection.Add(record);
                }
            }
            return BECollection.Count > 0 ? BECollection[0] : null;
        }

        public PLMClientsBusinessEntities.ResultsInfo getResultByCalculator(PLMClientsBusinessEntities.CalculatorsDetailInfo calculatorInfo)
        {
            PLMClientsBusinessEntities.ResultsInfo BEntity = new ResultsInfo();

            foreach (PLMClientsBusinessEntities.VariablesInfo variableItem in calculatorInfo.VariablesList)
            {
                if (variableItem.VariableValue.IndexOf(".") < 0)
                    variableItem.VariableValue = variableItem.VariableValue + ".00";

                calculatorInfo.SQLSyntax = calculatorInfo.SQLSyntax.Replace("@@VariableId:" + variableItem.VariableId + "@@", variableItem.VariableValue);
            }

            DbCommand dbCmd = MedicalCalculatorsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetResultByCalculator");

            // Add the parameters:
            MedicalCalculatorsDALC.InstanceDatabase.AddParameter(dbCmd, "@calculatorId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, calculatorInfo.CalculatorId);
            MedicalCalculatorsDALC.InstanceDatabase.AddParameter(dbCmd, "@sqlSyntax", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, calculatorInfo.SQLSyntax);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MedicalCalculatorsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    if (dataReader["ResultId"] != System.DBNull.Value)
                        BEntity.ResultId = Convert.ToInt32(dataReader["ResultId"]);

                    if (dataReader["ResultDescription"] != System.DBNull.Value)
                        BEntity.ResultDescription = dataReader["ResultDescription"].ToString();

                    if (dataReader["LowerRange"] != System.DBNull.Value)
                        //BEntity.LowerRange = Decimal.Parse(dataReader["LowerRange"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        BEntity.LowerRange = Convert.ToDecimal(dataReader["LowerRange"]);

                    if (dataReader["UpperRange"] != System.DBNull.Value)
                        //BEntity.UpperRange = Decimal.Parse(dataReader["UpperRange"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        BEntity.UpperRange = Convert.ToDecimal(dataReader["UpperRange"]);

                    if (dataReader["FinalResult"] != System.DBNull.Value)
                        BEntity.FinalResult = dataReader["FinalResult"].ToString();

                    if (dataReader["Active"] != System.DBNull.Value)
                            BEntity.Active = Convert.ToBoolean(dataReader["Active"]);
                }
            }
            return BEntity;
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.MedicalCalculatorsInfo getFromDataReader(IDataReader current)
        {
            MedicalCalculatorsInfo record = new MedicalCalculatorsInfo();

            record.CalculatorId = Convert.ToInt32(current["CalculatorId"]);
            record.CalculatorName = current["CalculatorName"].ToString();

            if (current["CalculatorDescription"] != System.DBNull.Value)
                record.CalculatorDescription = current["CalculatorDescription"].ToString();

            if (current["CalculatorFormula"] != System.DBNull.Value)
                record.CalculatorFormula = current["CalculatorFormula"].ToString();

            if (current["SQLSyntax"] != System.DBNull.Value)
                record.SQLSyntax = current["SQLSyntax"].ToString();

            if (current["CalculatorReferences"] != System.DBNull.Value)
                record.CalculatorReferences = current["CalculatorReferences"].ToString();

            if (current["WebPage"] != System.DBNull.Value)
                record.WebPage = current["WebPage"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly MedicalCalculatorsDALC Instance = new MedicalCalculatorsDALC();

    }
}
