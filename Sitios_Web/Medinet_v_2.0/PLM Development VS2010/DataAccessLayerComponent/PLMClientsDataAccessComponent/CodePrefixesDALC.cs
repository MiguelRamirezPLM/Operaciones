using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class CodePrefixesDALC : PLMClientsDataAccessAdapter<CodePrefixInfo>
    {

        #region Constructors

        private CodePrefixesDALC() { }

        #endregion

        #region Public Methods

        public override CodePrefixInfo getOne(int pk)
        {
            DbCommand dbCmd = CodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodePrefixes");

            // Add the parameters:
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public CodePrefixInfo getByPrefixName(string prefixName)
        {
            DbCommand dbCmd = CodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodePrefixes");

            // Add the parameters:
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixName);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public override void update(CodePrefixInfo businessEntity)
        {
            DbCommand dbCmd = CodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodePrefixes");

            // Add the parameters:
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            if (businessEntity.ParentId != null)
                CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ParentId);
            
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixTypeId);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompanyClienId);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixName);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixValue", DbType.AnsiString,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixValue);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@currentValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CurrentValue);

            if (businessEntity.FinalValue != null)
                CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@finalValue", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalValue);

            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixDescription", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixDescription);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@addedDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddedDate);

            //Update record:
            CodePrefixesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public CodePrefixInfo getByParentId(int parentId)
        {
            DbCommand dbCmd = CodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodePrefixes");

            // Add the parameters:
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public CodePrefixInfo getByCode(String codeString )
        {
            DbCommand dbCmd = CodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPrefixByCode");

            // Add the parameters:
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public List<CodePrefixInfo> getCodePrefixes() 
        {
            DbCommand dbCmd = CodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllCodePrefixes");

            List<CodePrefixInfo> BECollection = new List<CodePrefixInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        public override int insert(CodePrefixInfo businessEntity)
        {
            DbCommand dbCmd = CodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodePrefixes");

            // Add the parameters:
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ParentId);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixTypeId);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompanyClienId);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixName);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixValue", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixValue);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@currentValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CurrentValue);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@finalValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalValue);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixDescription);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@addedDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddedDate);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            //CodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@old_Id", DbType.Int32,
            //    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Old_Id);


            //Insert record:
            CodePrefixesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.PrefixId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        #endregion

        #region Protected Methods

        protected override CodePrefixInfo getFromDataReader(IDataReader current)
        {
            CodePrefixInfo record = new CodePrefixInfo();

            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);
            record.PrefixTypeId = Convert.ToByte(current["PrefixTypeId"]);
            record.CompanyClienId = Convert.ToInt32(current["CompanyClientId"]);
            record.PrefixName = current["PrefixName"].ToString();
            record.PrefixValue = current["PrefixValue"].ToString();
            record.CurrentValue = Convert.ToInt32(current["CurrentValue"]);
            if (current["FinalValue"] != System.DBNull.Value)
                record.FinalValue = Convert.ToInt32(current["FinalValue"]);
            record.PrefixDescription = current["PrefixDescription"].ToString();
            record.AddedDate = Convert.ToDateTime(current["AddedDate"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            if (current["CountryId"] != System.DBNull.Value)
            record.CountryId = Convert.ToByte(current["CountryId"]);


            return record;
        }

        #endregion

        public static readonly CodePrefixesDALC Instance = new CodePrefixesDALC();

    }
}
