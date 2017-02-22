using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class SpecialitiesDALC : PLMClientsDataAccessAdapter<SpecialityInfo>
    {
        #region Constructors

        private SpecialitiesDALC() { }

        #endregion

        #region Public Methods

        public  List<SpecialityGuidesInfo> getByGuides(string prefix)
        {
            List<SpecialityGuidesInfo> BECollection = new List<SpecialityGuidesInfo>();

            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSpecialitiesByGuies");

            // Add the parameters:
            SpecialitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            using (IDataReader dataReader = SpecialitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReaderGuides(dataReader));
            }
            
            return BECollection;
        }


        

        public override List<SpecialityInfo> getAll()
        {
            List<SpecialityInfo> BECollection = new List<SpecialityInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = SpecialitiesDALC.InstanceDatabase.ExecuteReader(
                SpecialitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSpecialitiesByGuies")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<SpecialityInfo> getByProfessionId(short professionId)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSpecialities");

            List<SpecialityInfo> BECollection = new List<SpecialityInfo>();

            // Add the parameters:
            SpecialitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@professionId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, professionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SpecialitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public SpecialityInfo getByName(string specialityName)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSpecialityByName");
         
            // Add the parameters:
            SpecialitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityName);

            using (IDataReader dataReader = SpecialitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public override SpecialityInfo getOne(int pk)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSpecialities");

            // Add the parameters:
            SpecialitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            SpecialitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@SpecialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = SpecialitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public PLMClientsBusinessEntities.LicenseClientInfo getSpecialityById(int specialityId)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSpecialityById");

            // Add the parameters:
            SpecialitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);

            using (IDataReader dataReader = SpecialitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReaderspeciality(dataReader);

                else
                    return null;
            } 
        }

        private LicenseClientInfo getFromDataReaderspeciality(IDataReader current)
        {
            LicenseClientInfo record = new LicenseClientInfo();

            record.Speciality = current["SpecialityName"].ToString();

            return record;
        }




        #endregion

        #region ProtectedMethods

     
        protected override SpecialityInfo getFromDataReader(IDataReader current)
        {
            SpecialityInfo record = new SpecialityInfo();

            record.SpecialityId = Convert.ToInt16(current["SpecialityId"]);
            record.SpecialityName = current["SpecialityName"].ToString();

            if (current["ShortName"] != System.DBNull.Value)
                record.ShortName = current["ShortName"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        protected  SpecialityGuidesInfo getFromDataReaderGuides(IDataReader current)
        {
            SpecialityGuidesInfo record = new SpecialityGuidesInfo();

            record.SpecialityId = Convert.ToInt16(current["SpecialityId"]);
            record.SpecialityName = current["SpecialityName"].ToString();

            if (current["ShortName"] != System.DBNull.Value)
                record.ShortName = current["ShortName"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

      

        #endregion

        public static readonly SpecialitiesDALC Instance = new SpecialitiesDALC();

    }
}
