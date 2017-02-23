using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class FolioDALC : PLMUsersDataAccesAdapter<FolioInfo >
    {
        #region constructor

        private FolioDALC() { }

        #endregion
        #region public methods

        public override int insert(FolioInfo businessEntity)
        {
            DbCommand dbCmd = FolioDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");
            //Add the parameters: 
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ApplicationId);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@initialValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialValue);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Prefix);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@currentNumber", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CurrentNumber);

            //Insert Record:
            FolioDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.FolioId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.FolioId;

        }

        public override FolioInfo getOne(int pk)
        {
            DbCommand dbCmd = FolioDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");

            // Add the parameters:
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FolioDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public  FolioInfo getByApplication(int application)
        {
            DbCommand dbCmd = FolioDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");

            // Add the parameters:
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Read);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@ApplicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, application);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FolioDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }



        public override void update(FolioInfo businessEntity)
        {
            DbCommand dbCmd = FolioDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");
            //Add the parameters: 
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);

            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FolioId);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ApplicationId);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@initialValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialValue);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Prefix);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@currentNumber", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CurrentNumber);

            //Insert Record:
            FolioDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.FolioId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

       

        }

        public override void delete(FolioInfo businessEntity)
        {
          
            DbCommand dbCmd = FolioDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFolios");

            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            FolioDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FolioId);

            FolioDALC.InstanceDatabase.ExecuteReader(dbCmd);
        }





        #endregion

        #region Protected Methods

        protected override FolioInfo getFromDataReader(IDataReader current)
        {
            FolioInfo record = new FolioInfo();

            record.FolioId = Convert.ToInt32(current["FolioId"]);
            record.ApplicationId = Convert.ToInt32(current["ApplicationId"]);
            record.InitialValue = Convert.ToInt32(current["InitialValue"]); 
            record.Prefix = current["Prefix"].ToString();
            record.CurrentNumber = Convert.ToInt32(current["CurrentNumber"]);
            record.LastUpdate = Convert.ToDateTime(current["LastUpdate"]);

            return record;
        }

        #endregion

        public static readonly FolioDALC Instance = new FolioDALC();
    }
}
