using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class TherapeuticsOMSDALC : MedinetDataAccessAdapter<TherapeuticOMSInfo>
    {
        #region Constructors

        private TherapeuticsOMSDALC() { }

        #endregion

        #region Public methods

        public List<TherapeuticOMSInfo> getAll(int? parentId)
        {
            DbCommand dbCmd = TherapeuticsOMSDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOMSTherapeutics");

            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            List<TherapeuticOMSInfo> BECollection = new List<TherapeuticOMSInfo>();

            using (IDataReader dataReader = TherapeuticsOMSDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<TherapeuticOMSInfo> getAllByName(string description)
        {
            DbCommand dbCmd = TherapeuticsOMSDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOMSTherapeuticOMSByName");

            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            List<TherapeuticOMSInfo> BECollection = new List<TherapeuticOMSInfo>();

            using (IDataReader dataReader = TherapeuticsOMSDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public TherapeuticOMSInfo getOne(string description)
        {
            DbCommand dbCmd = TherapeuticsOMSDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOMSTherapeuticOMSByName");

            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            using (IDataReader dataReader = TherapeuticsOMSDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<TherapeuticOMSInfo> getAllByNameSp(string description)
        {
            DbCommand dbCmd = TherapeuticsOMSDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOMSTherapeuticOMSByNameSp");

            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            List<TherapeuticOMSInfo> BECollection = new List<TherapeuticOMSInfo>();

            using (IDataReader dataReader = TherapeuticsOMSDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public TherapeuticOMSInfo getOneSp(string description)
        {
            DbCommand dbCmd = TherapeuticsOMSDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOMSTherapeuticOMSByNameSp");

            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            using (IDataReader dataReader = TherapeuticsOMSDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override TherapeuticOMSInfo getOne(int pk)
        {
            DbCommand dbCmd = TherapeuticsOMSDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOMSTherapeutics");

            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, TherapeuticsOMSDALC.CRUD.Read);
            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticOMSId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = TherapeuticsOMSDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<TherapeuticOMSInfo> getAllByProduct(int productId, int pharmaFormId)
        {
            DbCommand dbCmd = TherapeuticsOMSDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOMSTherapeuticByProduct");

            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<TherapeuticOMSInfo> BECollection = new List<TherapeuticOMSInfo>();

            using (IDataReader dataReader = TherapeuticsOMSDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public TherapeuticOMSInfo getOneByProduct(int productId, int pharmaFormId)
        {
            DbCommand dbCmd = TherapeuticsOMSDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOMSTherapeuticByProduct");

            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            TherapeuticsOMSDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            using (IDataReader dataReader = TherapeuticsOMSDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override TherapeuticOMSInfo getFromDataReader(IDataReader current)
        {
            TherapeuticOMSInfo record = new TherapeuticOMSInfo();

            record.TherapeuticOMSId = Convert.ToInt32(current["TherapeuticOMSId"]);

            if (current["ParentId"] != DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.TherapeuticKey = current["TherapeuticKey"].ToString();
            record.Description = current["Description"].ToString();
            record.SpanishDescription = current["SpanishDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            record.TherapeuticSons = Convert.ToInt32(current["TherapeuticSons"]);

            return record;
        }

        #endregion

        public static readonly TherapeuticsOMSDALC Instance = new TherapeuticsOMSDALC();
    }

}
