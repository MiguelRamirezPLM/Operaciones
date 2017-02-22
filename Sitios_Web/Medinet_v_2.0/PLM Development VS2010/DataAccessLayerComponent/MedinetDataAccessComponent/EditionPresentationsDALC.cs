using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class EditionPresentationsDALC : MedinetDataAccessAdapter<EditionPresentationsInfo>
    {

        #region Constructors

        private EditionPresentationsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(EditionPresentationsInfo businessEntity)
        {
            DbCommand dbCmd = EditionPresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionPresentations");

            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@presentationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PresentationId);

            EditionPresentationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(EditionPresentationsInfo businessEntity)
        {
            DbCommand dbCmd = EditionPresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionPresentations");

            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@presentationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PresentationId);

            EditionPresentationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public bool chechEditionPresentation(int editionId, int presentationId)
        {
            DbCommand dbCmd = EditionPresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckEditionPresentations");

            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            EditionPresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@presentationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, presentationId);

            EditionPresentationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        #endregion

        public static readonly EditionPresentationsDALC Instance = new EditionPresentationsDALC();

    }
}
