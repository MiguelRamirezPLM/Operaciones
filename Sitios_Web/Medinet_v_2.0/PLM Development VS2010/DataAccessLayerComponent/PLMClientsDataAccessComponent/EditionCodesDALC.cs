using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class EditionCodesDALC : PLMClientsDataAccessAdapter<EditionCodeInfo>
    {

        #region Constructors

        private EditionCodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(EditionCodeInfo businessEntity)
        {
            DbCommand dbCmd = UserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionCodes");

            // Add the parameters:
            EditionCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            EditionCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            EditionCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            EditionCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            if (businessEntity.InitialDate != null)
                EditionCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@initialDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialDate);

            if (businessEntity.FinalDate != null)
                EditionCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@finalDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalDate);

            //Insert record:
            EditionCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        #endregion

        public static readonly EditionCodesDALC Instance = new EditionCodesDALC();
    }
}
