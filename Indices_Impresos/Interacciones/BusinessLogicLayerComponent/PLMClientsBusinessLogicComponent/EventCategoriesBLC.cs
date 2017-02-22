using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class EventCategoriesBLC
    {
         #region Constructors

            private EventCategoriesBLC() { }

            #endregion

        #region Public Methods

            public List<EventCategoriesInfo> getCategoryEvent()
            {
                return PLMClientsDataAccessComponent.EventCategoriesDALC.Instance.getAll();
            }

            public List<PLMClientsBusinessEntities.EventInfo> getEvents(byte typeId, byte categoryId, string organizer )
            {
                return PLMClientsDataAccessComponent.EventDALC.Instance.getEvents(typeId, categoryId, organizer);
            }

            public List<PLMClientsBusinessEntities.EventInfo> getEventsCategory(byte typeId, byte categoryId)
            {
                return PLMClientsDataAccessComponent.EventDALC.Instance.getEventsCategory(typeId, categoryId);
            }

            public List<PLMClientsBusinessEntities.EventDetailInfo> getCongress(byte typeId)
            {
                return PLMClientsDataAccessComponent.EventDetailDALC.Instance.getCongress(typeId);
            }

            public List<PLMClientsBusinessEntities.EventDetailInfo> getOrganizer(byte categoryId, byte typeId)
            {
                return PLMClientsDataAccessComponent.EventDetailDALC.Instance.getOrganizer(categoryId, typeId);
            }

            public int insert(AddressInfo BEntity, int userId, string hash)
            {
                PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
                BEntity.AddressId = PLMClientsDataAccessComponent.AddressEventDALC.Instance.insert(BEntity);

                activityLog.UserId = userId;
                activityLog.HashKey = hash;
                activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Addresses);
                activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
                activityLog.PrimaryKeyAffected = "(AddressId," + BEntity.AddressId + ")";

                PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

                return BEntity.AddressId;
            }

            public int insert(EventDetailInfo BEntity, int userId, string hash)
            {

                PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
                BEntity.EventId = PLMClientsDataAccessComponent.EventDetailDALC.Instance.insert(BEntity);

                activityLog.UserId = userId;
                activityLog.HashKey = hash;
                activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Events);
                activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
                activityLog.PrimaryKeyAffected = "(EventId," + BEntity.EventId + ")";

                PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

                return BEntity.EventId;

            }

            public void update(PLMClientsBusinessEntities.EventDetailInfo businessEntity, int userId, string hash)
            {

                PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

                activityLog.UserId = userId;
                activityLog.HashKey = hash;
                activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Events);
                activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
                activityLog.PrimaryKeyAffected = "(EventName," + businessEntity.EventName + ");(Site," + businessEntity.Site + ") ;(InitialDate," + businessEntity.InitialDate +
                    ");(FinalDate," + businessEntity.FinalDate + ");(Organizer," + businessEntity.Organizer + ");(WebPage," + businessEntity.WebPage +
                    ");(CategoryId," + businessEntity.CategoryId + ")";

                PLMClientsDataAccessComponent.EventDetailDALC.Instance.update(businessEntity);
                PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            }

            public void addEventAddress(EventsAddressInfo BEntity, int userId, string hash)
            {
                PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

                activityLog.UserId = userId;
                activityLog.HashKey = hash;
                activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EventAddresses);
                activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
                activityLog.PrimaryKeyAffected = "(AddressId," + BEntity.AddressId + ");(EventId," + BEntity.EventId + ")";

                PLMClientsDataAccessComponent.EventsAddressDALC.Instance.insert(BEntity);
                PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            }

            public void deleteEventAddress(EventDetailInfo bussinesEntity)
            {
                PLMClientsDataAccessComponent.EventDetailDALC.Instance.delete(bussinesEntity);

            }

            public PLMClientsBusinessEntities.EventInfo getEvent(int eventId)
            {
                return PLMClientsDataAccessComponent.EventDALC.Instance.getOne(eventId);
            }

            public PLMClientsBusinessEntities.EventDetailInfo getEvents(int eventId)
            {
                return PLMClientsDataAccessComponent.EventDetailDALC.Instance.getOne(eventId);
            }

            public PLMClientsBusinessEntities.AddressInfo getEventsAddress(int addressId)
            {
                return PLMClientsDataAccessComponent.AddressEventDALC.Instance.getOne(addressId);
            }

            public void removeUserToClient(ClientUserInfo BEntity)
            {
                PLMClientsDataAccessComponent.ClientUsersDALC.Instance.delete(BEntity);
            }
        #endregion

            public static readonly EventCategoriesBLC Instance = new EventCategoriesBLC();
    }
}
