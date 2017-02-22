using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EncyclopediasBussinesEntries;

namespace EncyclopediasDataAccessComponent
{
    public class EncyclopediaDALC : EncyclopediasDataAccessAdapter<EncyclopediaInfo>
    {
        #region Constructors

        private EncyclopediaDALC() { }

        #endregion

        #region Public
        public override EncyclopediaInfo getOne(int pk)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEncyclopedia");

            // Add the parameters:
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override List<EncyclopediaInfo> getAll()
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEncyclopedia");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
           
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public  List<EncyclopediaInfo> getAllByEdition(string isbn)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEncyclopedia");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByText(string search)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByText");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                     bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByTextByEdition(string search,string isbn)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByText");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByType(int encyclopediaTypeId)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByType");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:
            
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaTypeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByTypeByEdition(int encyclopediaTypeId,string isbn)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByType");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:

            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaTypeId);
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByICD(int ICDId)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByICD");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:

            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@ICDId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ICDId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByICDByEdition(int ICDId, string isbn)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByICD");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:

            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@ICDId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ICDId);
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByKeyword(int keywordId)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByKeyword");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:

            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@keywordId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, keywordId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByKeywordByEdition(int keywordId, string isbn)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByKeyword");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:

            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@keywordId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, keywordId);
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasBySickness(int sicknessId)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasBySickness");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:

            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@sicknessId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, sicknessId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasBySicknessByEdition(int sicknessId, string isbn)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasBySickness");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:

            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@sicknessId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, sicknessId);
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaInfo> getEncyclopediasByProduct(int productId,int pharmaFormId)
        {
            DbCommand dbCmd = EncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByProduct");
            List<EncyclopediaInfo> bCollection = new List<EncyclopediaInfo>();
            // Add the parameters:

            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            EncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        #endregion

        #region protected
        protected override EncyclopediaInfo getFromDataReader(IDataReader current)
        {
            EncyclopediaInfo encyclopedia = new EncyclopediaInfo();
            encyclopedia.EncyclopediaId = Convert.ToInt32(current["EncyclopediaId"]);
            encyclopedia.EncyclopediaName = current["EncyclopediaName"].ToString();
            encyclopedia.EncyclopediaTypeId = Convert.ToInt32(current["EncyclopediaTypeId"]);
            encyclopedia.Active = Convert.ToBoolean(current["Active"]);

            return encyclopedia;
        }
        #endregion
        public static readonly EncyclopediaDALC Instance = new EncyclopediaDALC();
    }
}
