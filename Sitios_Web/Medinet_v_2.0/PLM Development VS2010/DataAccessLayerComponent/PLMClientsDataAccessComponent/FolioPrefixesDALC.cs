using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class FolioPrefixesDALC : PLMClientsDataAccessAdapter<FolioPrefixInfo>
    {
        #region Constructors

        private FolioPrefixesDALC() { }

        #endregion

        #region Public Methods

        public override FolioPrefixInfo getOne(int pk)
        {
            DbCommand dbCmd = FolioPrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolioPrefixes");

            // Add the parameters:
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FolioPrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public FolioPrefixInfo getByPrefixName(string prefixName)
        {
            DbCommand dbCmd = FolioPrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetFolioPrefixes");

            // Add the parameters:
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixName);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FolioPrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public override void update(FolioPrefixInfo businessEntity)
        {
            DbCommand dbCmd = FolioPrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolioPrefixes");

            // Add the parameters:
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixName);
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@initialValue", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialValue);
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@finalValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalValue);
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@currentValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CurrentValue);
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixDescription", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixDescription);
            FolioPrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);


            //Update record:
            FolioPrefixesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Protected Methods

        protected override FolioPrefixInfo getFromDataReader(IDataReader current)
        {
            FolioPrefixInfo record = new FolioPrefixInfo();

            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.PrefixName = current["PrefixName"].ToString();
            record.InitialValue = Convert.ToInt32(current["InitialValue"]);
            record.FinalValue = Convert.ToInt32(current["FinalValue"]);
            record.CurrentValue = Convert.ToInt32(current["CurrentValue"]);
            record.PrefixDescription = current["PrefixDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly FolioPrefixesDALC Instance = new FolioPrefixesDALC();

    }
}
