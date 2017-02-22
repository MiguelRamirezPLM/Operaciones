using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public sealed class PharmaceuticalFormsDALC : AgroDataAccessAdapter<PharmaceuticalFormInfo>
    {
        #region Constructors

        private PharmaceuticalFormsDALC() { }

        #endregion

        #region Public methods

        public List<PharmaceuticalFormInfo> getAllByFilter(string filter)
        {
            List<PharmaceuticalFormInfo> BECollection = new List<PharmaceuticalFormInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PharmaceuticalFormsDALC.AgroInstanceDatabase.ExecuteReader(
                PharmaceuticalFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaceuticalFormByName", filter)))
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
            using (IDataReader dataReader = PharmaceuticalFormsDALC.AgroInstanceDatabase.ExecuteReader(
                PharmaceuticalFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaceuticalFormByProduct", productId)))
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
            DbCommand dbCmd = PharmaceuticalFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaceuticalFormByName");

            PharmaceuticalFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, name);

            using (IDataReader dataReader = PharmaceuticalFormsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    PharmaceuticalFormInfo record = new PharmaceuticalFormInfo();

                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.Description = dataReader["PharmaForm"].ToString();
                    //record.EnglishDescription = current["EnglishDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    return record;
                }
                else
                    return null;
            }
        }

       
        public override PharmaceuticalFormInfo getOne(int pk)
        {
            DbCommand dbCmd = PharmaceuticalFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPharmaceuticalForms");

            PharmaceuticalFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            PharmaceuticalFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd,"@pharmaFormId",DbType.Int32,
                ParameterDirection.Input,string.Empty,DataRowVersion.Current,pk);
            
            using (IDataReader dataReader = PharmaceuticalFormsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    PharmaceuticalFormInfo record = new PharmaceuticalFormInfo();

                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.Description = dataReader["PharmaForm"].ToString();
            //record.EnglishDescription = current["EnglishDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

            return record;
                }
                   
                else
                    return null;
            }
        }

        public List<PharmaceuticalFormInfo> getAllWithoutProduct(int productId)
        {
            List<PharmaceuticalFormInfo> BECollection = new List<PharmaceuticalFormInfo>();

            DbCommand dbCmd = PharmaceuticalFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaFormsWithoutProduct");

            // Add the parameters:
            PharmaceuticalFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PharmaceuticalFormsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
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
            //record.EnglishDescription = current["EnglishDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly PharmaceuticalFormsDALC Instance = new PharmaceuticalFormsDALC();
    }
}
