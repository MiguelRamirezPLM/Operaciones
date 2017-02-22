using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;
using System.Data;
using System.Data.Common;

namespace EncyclopediasDataAccessComponent
{
    public class AttributeContentDALC : EncyclopediasDataAccessAdapter<AttributeContentInfo>
    {
        #region Constructors

        private AttributeContentDALC() { }

        #endregion

        #region properties
        public List<AttributeContentInfo> getEncyclopediaContents(int encyclopediaId)
        {
            DbCommand dbCmd = AttributeContentDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediaContents");
            List<AttributeContentInfo> bCollection = new List<AttributeContentInfo>();
            // Add the parameters:
            AttributeContentDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AttributeContentDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<AttributeContentInfo> getEncyclopediaContentsByAttributeType(int encyclopediaId,int attributeTypeId)
        {
            DbCommand dbCmd = AttributeContentDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediaContents");
            List<AttributeContentInfo> bCollection = new List<AttributeContentInfo>();
            // Add the parameters:
            AttributeContentDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaId);
            AttributeContentDALC.MedInstanceDatabase.AddParameter(dbCmd, "@attributeTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, attributeTypeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AttributeContentDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<AttributeContentInfo> getEncyclopediaContentsByAttribute(int encyclopediaId, int attributeGroupId)
        {
            DbCommand dbCmd = AttributeContentDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediaContents");
            List<AttributeContentInfo> bCollection = new List<AttributeContentInfo>();
            // Add the parameters:
            AttributeContentDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaId);
            AttributeContentDALC.MedInstanceDatabase.AddParameter(dbCmd, "@attributeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, attributeGroupId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AttributeContentDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        #endregion

        #region protected
        protected override AttributeContentInfo getFromDataReader(IDataReader current)
        {
            AttributeContentInfo EncyclopediaContent = new AttributeContentInfo();

            EncyclopediaContent.AttributeGroupId = Convert.ToInt32(current["AttributeGroupId"]);
            EncyclopediaContent.AttributeGroupName = current["AttributeGroupName"].ToString();
            EncyclopediaContent.AttributeId = Convert.ToInt32(current["AttributeId"]);
            EncyclopediaContent.AttributeName = current["AttributeName"].ToString();
            EncyclopediaContent.AttributeTypeId = Convert.ToInt32(current["EncyclopediaTypeId"]);
            EncyclopediaContent.BaseUrl = current["BaseUrl"].ToString();
            EncyclopediaContent.Content = current["Content"].ToString();
            EncyclopediaContent.EncyclopediaId = Convert.ToInt32(current["EncyclopediaId"]);
            EncyclopediaContent.ImageFile = current["ImageFile"].ToString();

            return EncyclopediaContent;
        }
        #endregion

        public static readonly AttributeContentDALC Instance = new AttributeContentDALC();
    }
}
