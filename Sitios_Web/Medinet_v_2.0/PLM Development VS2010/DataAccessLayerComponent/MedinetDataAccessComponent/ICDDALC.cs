using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;


namespace MedinetDataAccessComponent
{
    public class ICDDALC : MedinetDataAccessAdapter<ICDInfo>
    {
        #region Constructors

        private ICDDALC() { }

        #endregion


        #region Public Methods


        public List<ICDInfo> getICDByProduct(int editionId, int productId, int pharmaFormId)
        {
            List<ICDInfo> BECollection = new List<ICDInfo>();
            using (IDataReader dataReader = ICDDALC.MedInstanceDatabase.ExecuteReader(
                ICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetICDByProductPharmaform", editionId, productId, pharmaFormId)))
            {

                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public override ICDInfo getOne(int pk)
        {

            DbCommand dbCmd = ICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDICD");


            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = ICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }

        public List<ICDInfo> getICDByText(int editionId, string search)
        {

            List<ICDInfo> BECollection = new List<ICDInfo>();
            DbCommand dbCmd = ICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetICDByText");

            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@search", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);


            using (IDataReader dataReader = ICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;


        }

        public List<ICDInfo> getICDWithOutProduct(int productId, string search, int pharmaFormId)
        {

            List<ICDInfo> BECollection = new List<ICDInfo>();
            DbCommand dbCmd = ICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetICDWithOutProduct");

            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@search", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);
            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);


            using (IDataReader dataReader = ICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;


        }

        public List<ICDInfo> getAll(int? parentId)
        {
            DbCommand dbCmd = ICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetICDs");

            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            List<ICDInfo> BECollection = new List<ICDInfo>();
            ICDInfo record;
            using (IDataReader dataReader = ICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read()) {
                    record = new ICDInfo();
                    record.ICDId = Convert.ToInt32(dataReader["ICDId"]);
                    if (dataReader["ParentId"] != DBNull.Value)
                        record.ParentId = Convert.ToInt32(dataReader["ParentId"]);
                    record.ICDKey = dataReader["ICDKey"].ToString();
                    record.SpanishDescription = dataReader["SpanishDescription"].ToString();
                    record.EnglishDescription = dataReader["EnglishDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.ICDSons = Convert.ToInt32(dataReader["ICDSons"]);
                    BECollection.Add(record);
                
                }
                    
            }

            return BECollection;
        }

        public int getDifferenceICD(int productId, int icd) {
            DbCommand dbCmd = ICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_getICDDifference");

            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdParent", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, icd);

            
            using (IDataReader dataReader = ICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                if (dataReader.Read())
                {
                    return Convert.ToInt32(dataReader["Result"]);
                }
                else { return -1; }
            }
        }


        #endregion



        #region Protected Methods

        protected override ICDInfo getFromDataReader(IDataReader current)
        {
            ICDInfo record = new ICDInfo();

            record.ICDId = Convert.ToInt32(current["ICDId"]);
            if(current["ParentId"]!=DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);
            record.ICDKey = current["ICDKey"].ToString();
            record.SpanishDescription = current["SpanishDescription"].ToString();
            record.EnglishDescription = current["EnglishDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion

        public static readonly ICDDALC Instance = new ICDDALC();




    }
}