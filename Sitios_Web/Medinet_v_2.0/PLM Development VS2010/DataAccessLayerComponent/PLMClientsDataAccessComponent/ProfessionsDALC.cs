using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ProfessionsDALC : PLMClientsDataAccessAdapter<ProfessionInfo>
    {
        #region Constructors

        private ProfessionsDALC() { }

        #endregion

        #region Public Methods

        public ProfessionInfo getOne(short professionId)
        {
            DbCommand dbCmd = ProfessionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProfessions");

            // Add the parameters:
            ProfessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProfessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@professionId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, professionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProfessionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public override List<ProfessionInfo> getAll()
        {
            List<ProfessionInfo> BECollection = new List<ProfessionInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProfessionsDALC.InstanceDatabase.ExecuteReader(
                ProfessionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProfessions")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProfessionInfo> getByParent(short parentId)
        {
            List<ProfessionInfo> BECollection = new List<ProfessionInfo>();

            DbCommand dbCmd = ProfessionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProfessions");

            // Add the parameters:
            ProfessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProfessionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override ProfessionInfo getFromDataReader(IDataReader current)
        {
            ProfessionInfo record = new ProfessionInfo();

            record.ProfessionId = Convert.ToInt16(current["ProfessionId"]);

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt16(current["ParentId"]);

            record.ProfessionName = current["ProfessionName"].ToString();
            record.ReqProfessionalLicense = Convert.ToBoolean(current["ReqProfessionalLicense"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly ProfessionsDALC Instance = new ProfessionsDALC();

    }
}
