using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class PharmaceuticalFormsDALC : MedinetDataAccessAdapter<PharmaceuticalFormInfo>
    {
        #region Constructors

        private PharmaceuticalFormsDALC() { }

        #endregion

        #region Public methods

        public List<PharmaceuticalFormInfo> getAllByFilter(string filter)
        {
            List<PharmaceuticalFormInfo> BECollection = new List<PharmaceuticalFormInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PharmaceuticalFormsDALC.MedInstanceDatabase.ExecuteReader(
                PharmaceuticalFormsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaceuticalFormByName", filter)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;      
        }

        public List<PharmaceuticalFormInfo> getAllByProduct(int productId)
        {
            List<PharmaceuticalFormInfo> BECollection = new List<PharmaceuticalFormInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PharmaceuticalFormsDALC.MedInstanceDatabase.ExecuteReader(
                PharmaceuticalFormsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaceuticalFormByProduct", productId)))
            {
                // Iterates through records:
                
                while (dataReader.Read())
                {                    
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public PharmaceuticalFormInfo getOneByName(string name)
        {
            DbCommand dbCmd = PharmaceuticalFormsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaceuticalFormByName");

            PharmaceuticalFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, name);

            using (IDataReader dataReader = PharmaceuticalFormsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public string getPresentationsByProduct(int productId)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetSqlStringCommand("SELECT dbo.plm_dfGetPharmaFormByProduct (" + productId + ")");

            return IndicationsDALC.MedInstanceDatabase.ExecuteScalar(dbCmd).ToString();
        }

        public List<PharmaTherapeuticInfo> getPharmaTherapeutic(int productId)
        {
            DbCommand dbCmd = PharmaceuticalFormsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaTherapeutic");

            PharmaceuticalFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            List<PharmaTherapeuticInfo> recordCollection = new List<PharmaTherapeuticInfo>();

            using (IDataReader dataReader = PharmaceuticalFormsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                PharmaTherapeuticInfo record;

                while (dataReader.Read())
                {
                    record = new PharmaTherapeuticInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    if (dataReader["PharmaFormId"] != System.DBNull.Value)
                        record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    if (dataReader["PharmaForm"] != System.DBNull.Value)
                        record.PharmaForm = dataReader["PharmaForm"].ToString();
                    if (dataReader["TherapeuticId"] != System.DBNull.Value) 
                        record.TherapeuticId = Convert.ToInt32(dataReader["TherapeuticId"]);
                    if (dataReader["Therapeutic"] != System.DBNull.Value) 
                        record.Therapeutic = dataReader["Therapeutic"].ToString();
                    if (dataReader["Active"] != System.DBNull.Value) 
                        record.Active = Convert.ToBoolean(dataReader["Active"]);
                    recordCollection.Add(record);
                }

                return recordCollection;


            }

        }

        public override PharmaceuticalFormInfo getOne(int pk)
        {
            DbCommand dbCmd = PharmaceuticalFormsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPharmaceuticalForms");

            PharmaceuticalFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            PharmaceuticalFormsDALC.MedInstanceDatabase.AddParameter(dbCmd,"@pharmaFormId",DbType.Int32,
                ParameterDirection.Input,string.Empty,DataRowVersion.Current,pk);
            
            using (IDataReader dataReader = PharmaceuticalFormsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<PharmaceuticalFormInfo> getAllWithoutProduct(int productId)
        {
            List<PharmaceuticalFormInfo> BECollection = new List<PharmaceuticalFormInfo>();

            DbCommand dbCmd = PharmaceuticalFormsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaFormsWithoutProduct");

            // Add the parameters:
            PharmaceuticalFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PharmaceuticalFormsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override PharmaceuticalFormInfo getFromDataReader(System.Data.IDataReader current)
        {
            PharmaceuticalFormInfo record = new PharmaceuticalFormInfo();

            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.Description = current["Description"].ToString();
            record.EnglishDescription = current["EnglishDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly PharmaceuticalFormsDALC Instance = new PharmaceuticalFormsDALC();
    }
}
