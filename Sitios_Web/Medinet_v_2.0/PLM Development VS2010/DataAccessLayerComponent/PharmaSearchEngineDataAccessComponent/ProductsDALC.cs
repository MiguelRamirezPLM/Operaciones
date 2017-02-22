    using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
using PharmaSearchEngineBusinessEntries;


namespace PharmaSearchEngineDataAccessComponent
{
    public sealed class ProductsDALC : PharmaSearchEngineDataAccessAdapter<ProductByEditionInfo>
    {
        #region Constructors

        private ProductsDALC() { }

        #endregion

        #region Public Methods

        public List<ProductByEditionInfo> getAll(byte countryId, int editionId, string brand, byte typeInEdition, byte? numberPage, byte? page)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            if(!string.IsNullOrEmpty(brand))
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);

            if (numberPage != null)
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@numberPage", DbType.Byte,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, numberPage);
            
            if(page != null)
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@page", DbType.Byte,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, page);

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }
                
        public List<ProductByEditionInfo> getByDivision(byte countryId, int editionId, int divisionId)  
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
   
        }

        public List<ProductByEditionInfo> getByActiveSubstance(byte countryId, int editionId, int activeSubstanceId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");
            
            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public List<ProductByEditionInfo> getAloneByActiveSubstance(byte countryId, int editionId, int activeSubstanceId, bool onlyOne, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@onlyOne", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, onlyOne);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");
    
            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductByEditionInfo> getCombinedByActiveSubstance(byte countryId, int editionId, int activeSubstanceId, bool onlyOne, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@onlyOne", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, onlyOne);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductByEditionInfo> getByDivisionByActiveSubstance(byte countryId, int editionId, int divisionId, int activeSubstanceId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public List<ProductByEditionInfo> getAloneByDivisionByActiveSubstance(byte countryId, int editionId, int divisionId, int activeSubstanceId, bool onlyOne, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@onlyOne", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, onlyOne);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductByEditionInfo> getCombinedByDivisionByActiveSubstance(byte countryId, int editionId, int divisionId, int activeSubstanceId, bool onlyOne, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@onlyOne", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, onlyOne);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");
            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductByEditionInfo> getByIndication(byte countryId, int editionId, int indicationId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, indicationId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }


        public List<ProductByEditionInfo> getByICD(byte countryId, int editionId, int icdId, byte typeInEdition)
        {

            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, icdId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();


            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;


        }





        public List<ProductByEditionInfo> getByDivisionByIndication(byte countryId, int editionId, int divisionId, int indicationId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, indicationId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public List<ProductByEditionInfo> getByTherapeutic(byte countryId, int editionId, int therapeuticId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public List<ProductByEditionInfo> getByTherapeuticByActiveSubstance(byte countryId, int editionId, int therapeuticId, int activeSubstanceId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public List<ProductByEditionInfo> getAloneByTherapeuticByActiveSubstance(byte countryId, int editionId, int therapeuticId, int activeSubstanceId, bool onlyOne, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@onlyOne", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, onlyOne);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductByEditionInfo> getCombinedByTherapeuticByActiveSubstance(byte countryId, int editionId, int therapeuticId, int activeSubstanceId, bool onlyOne, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetProductsWithPF");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@onlyOne", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, onlyOne);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductByEditionInfo> getBySymptoms(int editionId, int symptomId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsBySymptom");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@symptomId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, symptomId);

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public ParticipantProductsInfo getContent(int editionId, int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            
            ParticipantProductsInfo record = new ParticipantProductsInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.EditionId =   Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);

                    if (dataReader["HTMLContent"] != DBNull.Value)
                        record.HTMLContent = dataReader["HTMLContent"].ToString();
                    else
                        record.HTMLContent = null;

                    if (dataReader["Page"] != DBNull.Value)
                        record.Page = dataReader["Page"].ToString();
                    else
                        record.Page = null;

                    return record;
                }
                else
                    return null;
            }
        }

        


        public List<ProductByEditionInfo> searchText(byte countryId, int editionId, string attributes, string text)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEProductsSearchText");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@attributes", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, attributes);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@text", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public bool checkContent(int editionId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckContent");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return",DbType.Int32,
                ParameterDirection.ReturnValue,string.Empty,DataRowVersion.Current,0);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            ProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;

        }

        public int getAll(int editionId, byte countryId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetNumberProducts");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            
            List<ProductByEditionInfo> BECollection = new List<ProductByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection.Count;
        }

        public ProductDetailInfo getProductAttributes(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEProductAttributes");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            
            ProductDetailInfo record = new ProductDetailInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.DivisionName = dataReader["DivisionName"].ToString();

                    if (dataReader["ProductShot"] != DBNull.Value)
                        record.ProductShot = dataReader["ProductShot"].ToString();

                    if (dataReader["DivisionImage"] != DBNull.Value)
                        record.DivisionImage = dataReader["DivisionImage"].ToString();

                    if (dataReader["BaseUrl"] != DBNull.Value)
                        record.BaseUrl = dataReader["BaseUrl"].ToString();

                    if (dataReader["ReferenceUrl"] != DBNull.Value)
                        record.ReferenceUrl = dataReader["ReferenceUrl"].ToString();

                    return record;
                }
                else
                    return null;
            }
        }
       
        public ProductCategoriesInfo getproductByCode(string BarCode)
        {          
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEProductByCode");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@barCode", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BarCode);

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReaderCategories(dataReader);
                else
                    return null;
            }
        }

        public EditionsInfo getMaxEdition(int productId, int pharmaFormId, int categoryId, int divisionId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetMaxEdition");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
      ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
      ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
      ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReaderMaxEdition(dataReader);
                else
                    return null;
            }
        }

        public ProductByCodeInfo getProductAttribute(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEProductAttributes");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            ProductByCodeInfo record = new ProductByCodeInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.DivisionName = dataReader["DivisionName"].ToString();

                    if (dataReader["ProductShot"] != DBNull.Value)
                        record.ProductShot = dataReader["ProductShot"].ToString();                    

                    if (dataReader["BaseUrl"] != DBNull.Value)
                        record.BaseUrl = dataReader["BaseUrl"].ToString();

                    if (dataReader["ReferenceUrl"] != DBNull.Value)
                        record.ReferenceUrl = dataReader["ReferenceUrl"].ToString();

                    return record;
                }
                else
                    return null;
            }
        }


        public ProductByEditionInfo getInformation(int editionId, int divisionId, int categoryId, int pharmaFormId, int productId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsInformation");


            // Add the parameters:

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);


            // Get the result set from the stored procedure:

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                if(dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else 
                    return null;
            }
        }


        #endregion

       

        #region Protected Methods

        protected ProductCategoriesInfo getFromDataReaderCategories(IDataReader current)
        {
            ProductCategoriesInfo record = new ProductCategoriesInfo();
            
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.ProductDescription = null;
            record.TechnicalSheet = false;
            return record;
        }

        protected EditionsInfo getFromDataReaderMaxEdition(IDataReader current)
        {
            EditionsInfo record = new EditionsInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.ISBN = current["ISBN"].ToString();
            record.NumberEdition = Convert.ToInt32(current["NumberEdition"]);
            return record;
        }

        protected override ProductByEditionInfo getFromDataReader(IDataReader current)
        {
            ProductByEditionInfo record = new ProductByEditionInfo();
            
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.Brand = current["Brand"].ToString();
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.DivisionName = current["DivisionName"].ToString();

            if (current["DivisionShortName"] != DBNull.Value)
                record.DivisionShortName = current["DivisionShortName"].ToString();

            else
                record.DivisionShortName = null;

            record.CategotyId = Convert.ToInt32(current["CategoryId"]);
            record.CategoryName = current["CategoryName"].ToString();

            return record;
        }

        #endregion

        public static readonly ProductsDALC Instance = new ProductsDALC();
    }
}
