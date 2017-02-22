using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class CodePrefixEditionsDALC : PLMClientsDataAccessAdapter<CodePrefixEditionInfo>
    {
        #region Constructors

        private CodePrefixEditionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(CodePrefixEditionInfo businessEntity)
        {
            DbCommand dbCmd = CodePrefixEditionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodePrefixEditions");

            // Add the parameters:
            CodePrefixEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CodePrefixEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CodePrefixEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            CodePrefixEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
                        

            //Insert record:
            CodePrefixEditionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public CodePrefixEditionInfo getOne(CodePrefixEditionInfo businessEntity)
        {
            DbCommand dbCmd = CodePrefixEditionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodePrefixEditions");

            // Add the parameters:
            CodePrefixEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CodePrefixEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            CodePrefixEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);

            //Read record:
            using (IDataReader dataReader = CodePrefixEditionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    return this.getFromDataReader(dataReader);
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Protected Methods

        protected override CodePrefixEditionInfo  getFromDataReader(IDataReader current)
        {
            CodePrefixEditionInfo record = new CodePrefixEditionInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.PrefixId = Convert.ToInt32(current["PrefixID"]);

            return record;
        
        }

        #endregion

        public static readonly CodePrefixEditionsDALC Instance = new CodePrefixEditionsDALC();
    }
}
