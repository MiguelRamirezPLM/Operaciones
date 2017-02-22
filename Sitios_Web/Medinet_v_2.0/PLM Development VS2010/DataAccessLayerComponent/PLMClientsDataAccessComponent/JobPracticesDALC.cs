using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class JobPracticesDALC : PLMClientsDataAccessAdapter<JobPracticeInfo>
    {
        #region Constructors

        private JobPracticesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(JobPracticeInfo businessEntity)
        {
            DbCommand dbCmd = JobPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDJobPractices");

            // Add the parameters:
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@practiceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PracticeId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@jobPlaceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.JobPlaceId);

            if (businessEntity.InstitutionName != null)
                JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@institutionName", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InstitutionName);

            if (businessEntity.PositionName != null)
                JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@positionName", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PositionName);
            
            //Insert record:
            JobPracticesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);    
        }

        public override void delete(JobPracticeInfo businessEntity)
        {
            DbCommand dbCmd = JobPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDJobPractices");

            // Add the parameters:
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@practiceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PracticeId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@jobPlaceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.JobPlaceId);
            
            //Delete record:
            JobPracticesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void update(JobPracticeInfo businessEntity)
        {
            DbCommand dbCmd = JobPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDJobPractices");

            // Add the parameters:
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@practiceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PracticeId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@jobPlaceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.JobPlaceId);

            if (businessEntity.InstitutionName != null)
                JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@institutionName", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InstitutionName);

            if (businessEntity.PositionName != null)
                JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@positionName", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PositionName);

            //Update record:
            JobPracticesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public JobPracticeInfo getOne(int clientId, byte practiceId, byte jobPlaceId)
        {
            DbCommand dbCmd = JobPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDJobPractices");

            // Add the parameters:
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@practiceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,practiceId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@jobPlaceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, jobPlaceId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = JobPracticesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public List<JobPracticeInfo> getByClient(int clientId)
        {
            DbCommand dbCmd = JobPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetJobPracticeByClient");

            // Add the parameters:
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            List<JobPracticeInfo> BECollection = new List<JobPracticeInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = JobPracticesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                
                // Iterates through records:
                while (dataReader.Read())
                {
                    //Add BE to Collection
                    BECollection.Add(this.getFromDataReader(dataReader));

                }   
                
            }

            return BECollection;
        }

        public void delete(int clientId, byte practiceId)
        {
            DbCommand dbCmd = JobPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDJobPractices");

            // Add the parameters:
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            JobPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@practiceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, practiceId);
            

            //Delete record:
            JobPracticesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Protected Methods

        protected override JobPracticeInfo getFromDataReader(IDataReader current)
        {
            JobPracticeInfo record = new JobPracticeInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.PracticeId = Convert.ToByte(current["PracticeId"]);
            record.JobPlaceId = Convert.ToByte(current["JobPlaceId"]);

            if (current["InstitutionName"] != System.DBNull.Value)
                record.InstitutionName = current["InstitutionName"].ToString();

            if (current["PositionName"] != System.DBNull.Value)
                record.PositionName = current["PositionName"].ToString();


            return record;

        }

        #endregion

        public static readonly JobPracticesDALC Instance = new JobPracticesDALC();

    }
}
