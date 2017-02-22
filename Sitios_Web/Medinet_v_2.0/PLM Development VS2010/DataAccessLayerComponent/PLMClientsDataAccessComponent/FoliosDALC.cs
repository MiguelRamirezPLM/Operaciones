using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class FoliosDALC : PLMClientsDataAccessAdapter<FolioInfo>
    {
        #region Constructors

        private FoliosDALC() { }

        #endregion

        #region Public Methods

        public override int insert(FolioInfo businessEntity)
        {
            DbCommand dbCmd = FoliosDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");

            // Add the parameters:
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@folioString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FolioString);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Insert record:
            FoliosDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void update(FolioInfo businessEntity)
        {
            DbCommand dbCmd = FoliosDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");

            // Add the parameters:
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FolioId);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@folioString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FolioString);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Update record:
            FoliosDALC.InstanceDatabase.ExecuteNonQuery(dbCmd); 
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = FoliosDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");

            // Add the parameters:
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);
           
            //Delete record:
            FoliosDALC.InstanceDatabase.ExecuteNonQuery(dbCmd); 
        }

        public override FolioInfo getOne(int pk)
        {
            DbCommand dbCmd = FoliosDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");

            // Add the parameters:
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FoliosDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public FolioInfo getByName(string folioString)
        {
            DbCommand dbCmd = FoliosDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetFolio");

            // Add the parameters:
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@folioString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, folioString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FoliosDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public bool checkFolio(string folioString)
        {
            DbCommand dbCmd = FoliosDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckFolio");

            // Add the parameters:
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Byte,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            FoliosDALC.InstanceDatabase.AddParameter(dbCmd, "@folioString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, folioString);

            FoliosDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;

        }

        #endregion

        #region Protected Methods

        protected override FolioInfo getFromDataReader(IDataReader current)
        {
            FolioInfo record = new FolioInfo();

            record.FolioId = Convert.ToInt32(current["FolioId"]);
            record.FolioString = current["FolioString"].ToString();
            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly FoliosDALC Instance = new FoliosDALC();

    }
}
