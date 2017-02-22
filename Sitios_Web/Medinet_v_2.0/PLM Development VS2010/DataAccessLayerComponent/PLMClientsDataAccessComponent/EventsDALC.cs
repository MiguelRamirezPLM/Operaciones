using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class EventsDALC : PLMClientsDataAccessAdapter<EventsDetailInfo>
    {

        #region Constructors

        private EventsDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByType(byte typeId)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> BECollection = new List<EventsDetailInfo>();

            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEvents");

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@typeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, typeId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;

        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByTypeByProfession(byte typeId, short professionId)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> BECollection = new List<EventsDetailInfo>();

            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEvents");

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@typeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, typeId);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@professionId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, professionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByTypeBySpeciality(byte typeId, short specialityId)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> BECollection = new List<EventsDetailInfo>();

            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEvents");

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@typeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, typeId);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByTypeByMonth(byte typeId, short month)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> BECollection = new List<EventsDetailInfo>();

            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEvents");

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@typeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, typeId);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@month", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, month);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByStateByCategory(string prefix, byte targetId, int stateId, byte categoryId)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> BECollection = new List<EventsDetailInfo>();

            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEventsByStateByCategory");

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByPrefix(string prefix, byte targetId)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> BECollection = new List<EventsDetailInfo>();

            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEventsByPrefix");

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.EventCategoryDetailInfo> getEventCategoriesByState(string prefix, byte targetId, int stateId)
        {
            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEventCategoriesByState");

            List<EventCategoryDetailInfo> BECollection = new List<EventCategoryDetailInfo>();

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    EventCategoryDetailInfo record = new EventCategoryDetailInfo();

                    record.CategoryId = Convert.ToByte(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();
                    
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.StateInfo> getOtherEventStates(string prefix, byte targetId, int stateId)
        {
            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOtherEventStates");

            List<StateInfo> BECollection = new List<StateInfo>();

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    StateInfo record = new StateInfo();

                    record.StateId = Convert.ToInt32(dataReader["StateId"]);
                    record.StateName = dataReader["StateName"].ToString();
                    record.CountryId = Convert.ToByte(dataReader["CountryId"]);
                    record.ShortName = dataReader["ShortName"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.CompanyEventsDetailInfo> getCompaniesByEvent(int eventId)
        {
            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCompaniesByEvent");

            List<CompanyEventsDetailInfo> BECollection = new List<CompanyEventsDetailInfo>();

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@eventId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, eventId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    CompanyEventsDetailInfo record = new CompanyEventsDetailInfo();

                    record.CompanyClientId = Convert.ToInt32(dataReader["CompanyClientId"]);
                    record.CompanyName = dataReader["CompanyName"].ToString();

                    if (dataReader["CompanyImage"] != System.DBNull.Value)
                        record.CompanyImage = dataReader["CompanyImage"].ToString();

                    if (dataReader["BaseUrl"] != System.DBNull.Value)
                        record.BaseUrl = dataReader["BaseUrl"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }



        public List<PLMClientsBusinessEntities.EventCategoryDetailInfo> getEventCategories(string prefix, byte targetId)
        {
            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEventCategories");

            List<EventCategoryDetailInfo> BECollection = new List<EventCategoryDetailInfo>();

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    EventCategoryDetailInfo record = new EventCategoryDetailInfo();

                    record.CategoryId = Convert.ToByte(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }


        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByCategory(string prefix, byte targetId, byte categoryId)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> BECollection = new List<EventsDetailInfo>();

            DbCommand dbCmd = EventsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEventsByCategory");

            // Add the parameters:
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            EventsDALC.InstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EventsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }


        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.EventsDetailInfo getFromDataReader(IDataReader current)
        {
            EventsDetailInfo record = new EventsDetailInfo();

            record.EventId = Convert.ToInt32(current["EventId"]);
            record.EventName = current["EventName"].ToString();
            record.TypeId = Convert.ToByte(current["TypeId"]);
            record.TypeName = current["TypeName"].ToString();
            record.Site = current["Site"].ToString();
            record.InitialDate = Convert.ToDateTime(current["InitialDate"]);
            record.FinalDate = Convert.ToDateTime(current["FinalDate"]);
            record.Organizer = current["Organizer"].ToString();
            record.WebPage = current["WebPage"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            record.ProfessionName = current["ProfessionName"].ToString();
            record.SpecialityName = current["SpecialityName"].ToString();

            if (current["AddressId"] != System.DBNull.Value)
                record.AddressId = Convert.ToInt32(current["AddressId"]);

            if (current["Street"] != System.DBNull.Value)
                record.Street = current["Street"].ToString();

            if (current["InternalNumber"] != System.DBNull.Value)
                record.InternalNumber = current["InternalNumber"].ToString();

            if (current["Suburb"] != System.DBNull.Value)
                record.Suburb = current["Suburb"].ToString();

            if (current["ZipCode"] != System.DBNull.Value)
                record.ZipCode = current["ZipCode"].ToString();

            if (current["Location"] != System.DBNull.Value)
                record.Location = current["Location"].ToString();

            if (current["CountryId"] != System.DBNull.Value)
                record.CountryId = Convert.ToByte(current["CountryId"]);

            if (current["CountryName"] != System.DBNull.Value)
                record.CountryName = current["CountryName"].ToString();

            if (current["StateId"] != System.DBNull.Value)
                record.StateId = Convert.ToInt32(current["StateId"]);

            if (current["StateName"] != System.DBNull.Value)
                record.StateName = current["StateName"].ToString();

            if (current["Lada"] != System.DBNull.Value)
                record.Lada = current["Lada"].ToString();

            if (current["PhoneOne"] != System.DBNull.Value)
                record.PhoneOne = current["PhoneOne"].ToString();

            if (current["PhoneTwo"] != System.DBNull.Value)
                record.PhoneTwo = current["PhoneTwo"].ToString();

            if (current["Ext"] != System.DBNull.Value)
                record.Ext = current["Ext"].ToString();

            if (current["Latitude"] != System.DBNull.Value)
                record.Latitude = Convert.ToDecimal(current["Latitude"]);

            if (current["Longitude"] != System.DBNull.Value)
                record.Longitude = Convert.ToDecimal(current["Longitude"]);

            return record;
        }

        #endregion

        public static readonly EventsDALC Instance = new EventsDALC();

    }
}
