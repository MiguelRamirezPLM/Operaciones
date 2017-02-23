using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;

namespace VetDataAccessComponent
{
     public sealed class TherapeuticsDALC:VetDataAccessAdapter<TherapeuticInfo>
     {

         #region Constructors

         private TherapeuticsDALC() { }

         #endregion

         #region Public Methods

         public List<TherapeuticInfo> getTherapeuticsByText(int editionId, string searchText)
         {
             List<TherapeuticInfo> BeCollection = new List<TherapeuticInfo>();
             DbCommand dbCmd = TherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETTherapeutics");

             // Add the parameters
             TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
             TherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@searchText", DbType.String,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, searchText);


             using (IDataReader dataReader = TherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
             {

                 while (dataReader.Read())
                 {
                     BeCollection.Add(this.getFromDataReader(dataReader));
                 }

                 return BeCollection;
             }


         }




         #endregion


         #region Protected Methods


         protected override TherapeuticInfo getFromDataReader(IDataReader current)
         {
             TherapeuticInfo record = new TherapeuticInfo();


             record.TherapeuticId = Convert.ToInt32(current["TherapeuticId"]);

             if (current["ParentId"] != DBNull.Value)
                 record.ParentId = Convert.ToInt32(current["ParentId"]);
             record.TherapeuticName = current["TherapeuticName"].ToString();
             record.Active = Convert.ToBoolean(current["Active"]);

             return record;


         }

         #endregion


         public static readonly TherapeuticsDALC Instance = new TherapeuticsDALC();





     }
}
