using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class NickNamePrefixesDALC : PLMClientsDataAccessAdapter<NickNamePrefixInfo>
    {
        #region Constructors

        private NickNamePrefixesDALC() { }

        #endregion

        #region Public Methods

        public override NickNamePrefixInfo getOne(int pk)
        {
            DbCommand dbCmd = NickNamePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDNickNamePrefixes");

            // Add the parameters:
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@nickPrefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = NickNamePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public NickNamePrefixInfo getByPrefixName(string prefixName)
        {
            DbCommand dbCmd = NickNamePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetNickNamePrefixes");

            // Add the parameters:
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixName);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = NickNamePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public override void update(NickNamePrefixInfo businessEntity)
        {
            DbCommand dbCmd = NickNamePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDNickNamePrefixes");

            // Add the parameters:
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@nickPrefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.NickPrefixId);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixName);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@initialValue", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialValue);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@finalValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalValue);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@currentNumber", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CurrentNumber);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixDescription", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixDescription);
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);


            //Update record:
            NickNamePrefixesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public NickNamePrefixInfo getByPrefixId(int prefixId)
        {
            DbCommand dbCmd = NickNamePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetNickNamePrefixes");

            // Add the parameters:
            NickNamePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = NickNamePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override NickNamePrefixInfo getFromDataReader(IDataReader current)
        {
            NickNamePrefixInfo record = new NickNamePrefixInfo();

            record.NickPrefixId = Convert.ToInt32(current["NickPrefixId"]);
            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.PrefixName = current["PrefixName"].ToString();
            record.InitialValue = Convert.ToInt32(current["InitialValue"]);
            record.FinalValue = Convert.ToInt32(current["FinalValue"]);
            record.CurrentNumber = Convert.ToInt32(current["CurrentNumber"]);
            record.PrefixDescription = current["PrefixDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);


            return record;
        }

        #endregion

        public static readonly NickNamePrefixesDALC Instance = new NickNamePrefixesDALC();

    }
}
