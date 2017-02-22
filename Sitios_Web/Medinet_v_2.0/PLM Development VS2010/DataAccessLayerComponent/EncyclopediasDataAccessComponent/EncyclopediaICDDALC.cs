using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;
using System.Data;
using System.Data.Common;

namespace EncyclopediasDataAccessComponent
{
    public class EncyclopediaICDDALC : EncyclopediasDataAccessAdapter<EncyclopediaICDInfo>
    {
         #region Constructors

        private EncyclopediaICDDALC() { }

        #endregion

        #region Public
        public List<EncyclopediaICDInfo> getEncyclopediasICDByICDAssociated(int ICDAssociatedId)
        {
            DbCommand dbCmd = EncyclopediaICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasICDByICDAssociated");
            List<EncyclopediaICDInfo> bCollection = new List<EncyclopediaICDInfo>();
            // Add the parameters:

            EncyclopediaICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdAssociatedId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ICDAssociatedId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaICDInfo> getEncyclopediasICDByICDAssociatedByEdition(int ICDAssociatedId,string isbn)
        {
            DbCommand dbCmd = EncyclopediaICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasICDByICDAssociated");
            List<EncyclopediaICDInfo> bCollection = new List<EncyclopediaICDInfo>();
            // Add the parameters:

            EncyclopediaICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdAssociatedId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ICDAssociatedId);
            EncyclopediaICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaICDInfo> getEncyclopediasICDByICDAssociatedByType(int ICDAssociatedId,int encyclopediaTypeId)
        {
            DbCommand dbCmd = EncyclopediaICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasICDByICDAssociatedByType");
            List<EncyclopediaICDInfo> bCollection = new List<EncyclopediaICDInfo>();
            // Add the parameters:

            EncyclopediaICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdAssociatedId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ICDAssociatedId);
            EncyclopediaICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaTypeId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaTypeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<EncyclopediaICDInfo> getEncyclopediasICDByICDAssociatedByTypeByEdition(int ICDAssociatedId,int encyclopediaTypeId, string isbn)
        {
            DbCommand dbCmd = EncyclopediaICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasICDByICDAssociatedByType");
            List<EncyclopediaICDInfo> bCollection = new List<EncyclopediaICDInfo>();
            // Add the parameters:

            EncyclopediaICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdAssociatedId", DbType.Int32,
                  ParameterDirection.Input, string.Empty, DataRowVersion.Current, ICDAssociatedId);
            EncyclopediaICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaTypeId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaTypeId);
            EncyclopediaICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EncyclopediaICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }
        #endregion

        #region protected
        protected override EncyclopediaICDInfo getFromDataReader(IDataReader current)
        {
            EncyclopediaICDInfo encyclopediaICD = new EncyclopediaICDInfo();
            encyclopediaICD.EncyclopediaId = Convert.ToInt32(current["EncyclopediaId"]);
            encyclopediaICD.EncyclopediaName = current["EncyclopediaName"].ToString();
            encyclopediaICD.ICDId= Convert.ToInt32(current["ICDId"]);
            encyclopediaICD.ICDKey = current["ICDKey"].ToString();
            encyclopediaICD.ICDName = current["SpanishDescription"].ToString();

            return encyclopediaICD;
        }
        #endregion

        public static readonly EncyclopediaICDDALC Instance = new EncyclopediaICDDALC();
    }
}
