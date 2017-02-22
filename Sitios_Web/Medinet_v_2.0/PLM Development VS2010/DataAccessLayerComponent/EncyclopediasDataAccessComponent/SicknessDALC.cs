using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;
using System.Data;
using System.Data.Common;


namespace EncyclopediasDataAccessComponent 
{
    public class SicknessDALC : EncyclopediasDataAccessAdapter<SicknessInfo>
    {
        #region Constructors

        private SicknessDALC() { }

        #endregion

        #region public

        public override SicknessInfo getOne(int pk)
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSickness");

            // Add the parameters:
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@sicknessId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override List<SicknessInfo> getAll()
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSickness");
            List<SicknessInfo> bCollection = new List<SicknessInfo>();
            // Add the parameters:
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<SicknessInfo> getAllByEdition(string isbn)
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSickness");
            List<SicknessInfo> bCollection = new List<SicknessInfo>();
            // Add the parameters:
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<SicknessInfo> getSicknessByText(string search)
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSicknessByText");
            List<SicknessInfo> bCollection = new List<SicknessInfo>();
            // Add the parameters:
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<SicknessInfo> getSicknessByTextByEdition(string search,string isbn)
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSicknessByText");
            List<SicknessInfo> bCollection = new List<SicknessInfo>();
            // Add the parameters:
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<SicknessInfo> getSicknessByEncyclopedia(int encyclopediaId)
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSicknessByEncyclopedia");
            List<SicknessInfo> bCollection = new List<SicknessInfo>();
            // Add the parameters:

            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<SicknessInfo> getSicknessByEncyclopediaByEdition(int sicknessId, string isbn)
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSicknessByEncyclopedia");
            List<SicknessInfo> bCollection = new List<SicknessInfo>();
            // Add the parameters:

            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, sicknessId);
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<SicknessInfo> getSicknessByICD(int ICDId)
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSicknessByICD");
            List<SicknessInfo> bCollection = new List<SicknessInfo>();
            // Add the parameters:

            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ICDId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<SicknessInfo> getSicknessByICDByEdition(int ICDId, string isbn)
        {
            DbCommand dbCmd = SicknessDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSicknessByICD");
            List<SicknessInfo> bCollection = new List<SicknessInfo>();
            // Add the parameters:

            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ICDId);
            SicknessDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SicknessDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        #endregion

        #region protected
        protected override SicknessInfo getFromDataReader(IDataReader current)
        {
            SicknessInfo sickness = new SicknessInfo();


            sickness.SicknessName = current["SicknessName"].ToString();
            sickness.SicknessId = Convert.ToInt32(current["SicknessId"]);
            sickness.Active = Convert.ToBoolean(current["Active"]);

            return sickness;
        }
        #endregion

        public static readonly SicknessDALC Instance = new SicknessDALC();
    }
}
