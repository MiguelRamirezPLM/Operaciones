using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class TherapeuticsDALC : MedinetDataAccessAdapter<TherapeuticInfo>
    {
        #region Constructors

        private TherapeuticsDALC() { }

        #endregion

        #region Public methods

        public List<TherapeuticInfo> getAll(int? parentId)
        {
            DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTherapeutics");

            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            List<TherapeuticInfo> BECollection = new List<TherapeuticInfo>();

            using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<TherapeuticInfo> getAllByName(string description)
        {
            DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTherapeuticsByName");

            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            List<TherapeuticInfo> BECollection = new List<TherapeuticInfo>();

            using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public TherapeuticInfo getOne(string description)
        {
            DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTherapeuticsByName");

            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<TherapeuticInfo> getAllByNameSp(string description)
        {
            DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTherapeuticsByNameSp");

            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            List<TherapeuticInfo> BECollection = new List<TherapeuticInfo>();

            using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public TherapeuticInfo getOneSp(string description)
        {
            DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTherapeuticsByNameSp");

            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override TherapeuticInfo getOne(int pk)
        {
            DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTherapeutics");

            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, TherapeuticsDALC.CRUD.Read);
            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<TherapeuticInfo> getAllByProduct(int productId, int pharmaFormId)
        {
            DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTherapeuticByProduct");

            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<TherapeuticInfo> BECollection = new List<TherapeuticInfo>();

            using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public TherapeuticInfo getOneByProduct(int productId, int pharmaFormId)
        {
            DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTherapeuticByProduct");

            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override TherapeuticInfo getFromDataReader(IDataReader current)
        {
            TherapeuticInfo record = new TherapeuticInfo();

            record.TherapeuticId = Convert.ToInt32(current["TherapeuticId"]);

            if(current["ParentId"] != DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.TherapeuticKey = current["TherapeuticKey"].ToString();
            record.Description = current["Description"].ToString();
            record.SpanishDescription = current["SpanishDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            record.TherapeuticSons = Convert.ToInt32(current["TherapeuticSons"]);

            return record;
        }

        #endregion

        public static readonly TherapeuticsDALC Instance = new TherapeuticsDALC();
    }
}
