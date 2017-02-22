using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;
using System.Data;
using System.Data.Common;


namespace EncyclopediasDataAccessComponent
{
    public class ICDDALC : EncyclopediasDataAccessAdapter<ICDInfo>
    {
        #region Constructors

        private ICDDALC() { }

        #endregion

        #region Properties

        public List<ICDInfo> getICDAssociatedByEncyclopediaContentAttribute(int encyclopediaId,int attributeId,int attributeGroupId)
        {
            DbCommand dbCmd = ICDDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetICDAssociateddByEncyclopediaContent");
            List<ICDInfo> bCollection = new List<ICDInfo>();
            // Add the parameters: 
            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaId);
            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@attributeId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, attributeId);
            ICDDALC.MedInstanceDatabase.AddParameter(dbCmd, "@attributeGroupId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, attributeGroupId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ICDDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        #endregion
        #region protected
        protected override ICDInfo getFromDataReader(IDataReader current)
        {
            ICDInfo ICD = new ICDInfo();

            ICD.ICDId = Convert.ToInt32(current["ICDId"]);
            ICD.ICDKey = current["ICDKey"].ToString();
            ICD.SpanishDescription = current["SpanishDescription"].ToString();

            if (current["ParentId"] != DBNull.Value)
                ICD.ParentId = (int)current["ParentId"];
            
            ICD.EnglishDescription = current["EnglishDescription"].ToString();
            ICD.Active = Convert.ToBoolean(current["Active"]);


            return ICD;
        }
        #endregion

        public static readonly ICDDALC Instance = new ICDDALC();
    }
}
