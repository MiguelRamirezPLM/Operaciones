using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;


namespace PharmaSearchEngineDataAccessComponent
{
    public sealed class TherapeuticsByProductDALC : PharmaSearchEngineDataAccessAdapter<TherapeuticByProductInfo>
    {

        #region Constructors

        private TherapeuticsByProductDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.TherapeuticInfo> getByName(byte countryId, int editionId, string description, byte typeInEdition)
        {

            DbCommand dbCmd = TherapeuticsByProductDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSETherapeutics");

            // Add the parameters:
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Participante ? "P" :
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<MedinetBusinessEntries.TherapeuticInfo> BECollection = new List<MedinetBusinessEntries.TherapeuticInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TherapeuticsByProductDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    MedinetBusinessEntries.TherapeuticInfo thera = new MedinetBusinessEntries.TherapeuticInfo();

                    thera.TherapeuticId = Convert.ToInt32(dataReader["TherapeuticId"]);
                    
                    if(dataReader["ParentId"] != System.DBNull.Value)
                        thera.ParentId = Convert.ToInt32(dataReader["ParentId"]);

                    thera.TherapeuticKey = dataReader["TherapeuticKey"].ToString();
                    thera.SpanishDescription = dataReader["SpanishDescription"].ToString();
                    thera.Description = dataReader["Description"].ToString();
                    thera.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(thera);

                }
            }

            return BECollection;

        }

        public List<TherapeuticByProductInfo> getAll(byte countryId, int editionId, byte typeInEdition)
        {
            DbCommand dbCmd = TherapeuticsByProductDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSETherapeutics");

            // Add the parameters:
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Participante ? "P" :
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<TherapeuticByProductInfo> BECollection = new List<TherapeuticByProductInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TherapeuticsByProductDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<TherapeuticByProductInfo> getByProduct(byte countryId, int editionId, int productId, byte typeInEdition)
        {
            DbCommand dbCmd = TherapeuticsByProductDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSETherapeutics");

            // Add the parameters:
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Participante ? "P" :
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<TherapeuticByProductInfo> BECollection = new List<TherapeuticByProductInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<TherapeuticByProductInfo> getByProductByPharmaForm(byte countryId, int editionId, int productId, int pharmaFormId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSETherapeutics");

            // Add the parameters:
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Participante ? "P" :
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<TherapeuticByProductInfo> BECollection = new List<TherapeuticByProductInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<TherapeuticByProductInfo> getByProductByTherapeutic(byte countryId, int editionId, int productId, int therapeuticId, byte typeInEdition)
        {
            DbCommand dbCmd = TherapeuticsByProductDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSETherapeutics");

            // Add the parameters:
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);
            TherapeuticsByProductDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Participante ? "P" :
                (TherapeuticsByProductDALC.TypeInEdition)typeInEdition == TherapeuticsByProductDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<TherapeuticByProductInfo> BECollection = new List<TherapeuticByProductInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override TherapeuticByProductInfo getFromDataReader(IDataReader current)
        {
            TherapeuticByProductInfo record = new TherapeuticByProductInfo();

            if (current["TherapeuticId"] != DBNull.Value)
                record.TherapeuticId = Convert.ToInt32(current["TherapeuticId"]);
            else
                record.TherapeuticId = null;

            if (current["TherapeuticKey"] != DBNull.Value)
                record.TherapeuticKey = current["TherapeuticKey"].ToString();
            else
                record.TherapeuticKey = null;

            if (current["Therapeutic"] != DBNull.Value)
                record.Therapeutic = current["Therapeutic"].ToString();

            
            record.ProductId = Convert.ToInt32(current["ProductId"]);      
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);

            return record;

        }

        #endregion

        public static readonly TherapeuticsByProductDALC Instance = new TherapeuticsByProductDALC();
    }
}
