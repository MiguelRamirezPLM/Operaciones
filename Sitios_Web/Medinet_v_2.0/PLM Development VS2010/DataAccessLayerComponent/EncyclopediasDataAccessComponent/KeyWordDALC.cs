using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;
using System.Data;
using System.Data.Common;

namespace EncyclopediasDataAccessComponent
{
    public class KeyWordDALC : EncyclopediasDataAccessAdapter<KeywordInfo>
    {
        #region Constructors

        private KeyWordDALC() { }

        #endregion

        #region Properties
        public override KeywordInfo getOne(int pk)
        {
            DbCommand dbCmd = KeyWordDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDKeyWords");

            // Add the parameters:
            KeyWordDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            KeyWordDALC.MedInstanceDatabase.AddParameter(dbCmd, "@keywordId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = KeyWordDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override List<KeywordInfo> getAll()
        {
            DbCommand dbCmd = KeyWordDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDKeyWords");
            List<KeywordInfo> bCollection = new List<KeywordInfo>();
            // Add the parameters:
            KeyWordDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = KeyWordDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<KeywordInfo> getAllByEdition(string isbn)
        {
            DbCommand dbCmd = KeyWordDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDKeyWords");
            List<KeywordInfo> bCollection = new List<KeywordInfo>();
            // Add the parameters:
            KeyWordDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            KeyWordDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = KeyWordDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<KeywordInfo> getKeyWordsByText(string search)
        {
            DbCommand dbCmd = KeyWordDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetKeyWordsByText");
            List<KeywordInfo> bCollection = new List<KeywordInfo>();
            // Add the parameters:
            KeyWordDALC.MedInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = KeyWordDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<KeywordInfo> getKeyWordsByTextByEdition(string search, string isbn)
        {
            DbCommand dbCmd = KeyWordDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetKeyWordsByText");
            List<KeywordInfo> bCollection = new List<KeywordInfo>();
            // Add the parameters:
            KeyWordDALC.MedInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);
            KeyWordDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = KeyWordDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        #endregion

        #region protected
        protected override KeywordInfo getFromDataReader(IDataReader current)
        {
            KeywordInfo keyword = new KeywordInfo();

            keyword.KeyWord=current["KeyWord"].ToString();
            keyword.KeyWordId = Convert.ToInt32(current["KeyWordId"]);
            keyword.Active=  Convert.ToBoolean(current["Active"]);

            return keyword;
        }


        public static readonly KeyWordDALC Instance = new KeyWordDALC();
        #endregion
    }
}
