using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CE_PSELogsDataAccessComponent
{
    public class CE_PSE_TrackingAttributesDALC : CE_PSELogsDataAccessComponent<PSELogsBusinessEntities.PSE_TrackingAttributeInfo>
    {
        #region Constructors

        private CE_PSE_TrackingAttributesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PSELogsBusinessEntities.PSE_TrackingAttributeInfo businessEntity)
        {
            StringBuilder sb = new StringBuilder();
            int result = 0;

            sb.Append("\nINSERT INTO [PSE_TrackingAttributes]([TrackId],[AttributeId])");
            sb.Append("\nVALUES(" + businessEntity.TrackId + "," + businessEntity.AttributeId + ")");

            CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.CreateConnection();

            result = CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.ExecuteNonQuerySql(sb.ToString());

            CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.CloseSharedConnection();

            return result;
        }

        public List<PSELogsBusinessEntities.PSE_TrackingAttributeInfo> getLogsAttributes(int trackId)
        {
            
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT [TrackId],[AttributeId] ");
            sb.Append("\nFROM PSE_TrackingAttributes ");
            sb.Append("\nWHERE [TrackId] = " + trackId);
            sb.Append("\nORDER BY TrackId");

            List<PSELogsBusinessEntities.PSE_TrackingAttributeInfo> BECollection = new List<PSELogsBusinessEntities.PSE_TrackingAttributeInfo>();

            CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public override void delete(PSELogsBusinessEntities.PSE_TrackingAttributeInfo businessEntity)
        {
            
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE FROM [PSE_TrackingAttributes] WHERE TrackId = " + businessEntity.TrackId + " AND AttributeId = " + businessEntity.AttributeId);

            try
            {
                CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.CreateConnection();
                CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.ExecuteNonQuerySql(sb.ToString());
            }
            catch
            {
            }
            finally
            {
                CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.CloseSharedConnection();
            }
        }

        public void delete(string trackIds)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE FROM [PSE_TrackingAttributes] WHERE TrackId IN (" + trackIds + ")");

            try
            {
                CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.CreateConnection();
                CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.ExecuteNonQuerySql(sb.ToString());
            }
            catch
            {
            }
            finally
            {
                CE_PSE_TrackingAttributesDALC.PSELogsInstanceDatabase.CloseSharedConnection();
            }    
        }

        #endregion

        #region Protected Methods

        protected override PSELogsBusinessEntities.PSE_TrackingAttributeInfo getFromDataReader(IDataReader current)
        {
            PSELogsBusinessEntities.PSE_TrackingAttributeInfo record = new PSELogsBusinessEntities.PSE_TrackingAttributeInfo();

            record.TrackId = Convert.ToInt32(current["TrackId"]);
            record.AttributeId = Convert.ToInt32(current["AttributeId"]);

            return record;
        }

        #endregion

        public static readonly CE_PSE_TrackingAttributesDALC Instance = new CE_PSE_TrackingAttributesDALC();
    }
}
