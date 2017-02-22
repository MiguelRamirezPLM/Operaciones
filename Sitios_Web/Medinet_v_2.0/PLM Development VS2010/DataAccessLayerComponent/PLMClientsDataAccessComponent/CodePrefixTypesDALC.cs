using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class CodePrefixTypesDALC : PLMClientsDataAccessAdapter<CodePrefixTypesInfo>
    {
        #region Constructors

        private CodePrefixTypesDALC() { }

        #endregion

        #region Public Methods

        public override CodePrefixTypesInfo getOne(int pk)
        {
            DbCommand dbCmd = CodePrefixTypesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodePrefixTypes");

            // Add the parameters:
            CodePrefixTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CodePrefixTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodePrefixTypesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public List<CodePrefixTypesInfo> getCodePrefixTypes()
        {
            DbCommand dbCmd = CodePrefixTypesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllCodePrefixTypes");

            List<CodePrefixTypesInfo> BECollection = new List<CodePrefixTypesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodePrefixTypesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override CodePrefixTypesInfo getFromDataReader(IDataReader current)
        {
            CodePrefixTypesInfo record = new CodePrefixTypesInfo();

            record.PrefixTypeId = Convert.ToByte(current["PrefixTypeId"]);
            record.PrefixTypeName = current["PrefixTypeName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CodePrefixTypesDALC Instance = new CodePrefixTypesDALC();
    }
}
