using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class EventsBLC
    {

        #region Constructors

        private EventsBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByType(byte typeId)
        {
            return PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByType(typeId);
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByTypeByProfession(byte typeId, short professionId)
        {
            return PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByTypeByProfession(typeId, professionId);
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByTypeBySpeciality(byte typeId, short specialityId)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> events =
                PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByTypeBySpeciality(typeId, specialityId);

            foreach (PLMClientsBusinessEntities.EventsDetailInfo item in events)
            {
                item.CompanyEvents = PLMClientsDataAccessComponent.EventsDALC.Instance.getCompaniesByEvent(item.EventId);

                foreach (PLMClientsBusinessEntities.CompanyEventsDetailInfo company in item.CompanyEvents)
                {
                    if (company.CompanyImage != null)
                        company.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["CompanyEventsBaseUrl"];
                }
            }
            return events;
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByTypeByMonth(byte typeId, short month)
        {
            return PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByTypeByMonth(typeId, month);
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByCategory(string prefix, byte targetId, byte categoryId)
        {
            return PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByCategory(prefix, targetId, categoryId);
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByStateByCategory(string prefix, byte targetId, int stateId, byte categoryId)
        {
            return PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByStateByCategory(prefix, targetId, stateId, categoryId);
        }

        public List<PLMClientsBusinessEntities.EventCongressInfo> getAllEvents(int countryId, int prefixId, int distributionId, int targetId)
        {
            return PLMClientsDataAccessComponent.EventsDistributionDALC.Instance.getAllEvents(countryId, prefixId, distributionId, targetId);
        }

        public List<PLMClientsBusinessEntities.EventCongressInfo> getAllCongress(int countryId, int prefixId, int distributionId, int targetId)
        {
            return PLMClientsDataAccessComponent.EventsDistributionDALC.Instance.getAllCongress(countryId, prefixId, distributionId, targetId);
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByPrefix(string prefix, byte targetId, decimal latitude, decimal longitude)
        {
            List<PLMClientsBusinessEntities.EventsDetailInfo> events = PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByPrefix(prefix, targetId);

            if (events.Count > 0)
            {
                foreach (PLMClientsBusinessEntities.EventsDetailInfo eventItem in events)
                    eventItem.EventDistance = Convert.ToDecimal(this.calculateDistance(Convert.ToDouble(latitude),
                        Convert.ToDouble(longitude), Convert.ToDouble(eventItem.Latitude), Convert.ToDouble(eventItem.Longitude)));

                var eventList = from eventRecord in events
                                orderby eventRecord.EventDistance
                                select new PLMClientsBusinessEntities.EventsDetailInfo
                                {
                                    EventId = eventRecord.EventId,
                                    EventName = eventRecord.EventName,
                                    TypeId = eventRecord.TypeId,
                                    TypeName = eventRecord.TypeName,
                                    Site = eventRecord.Site,
                                    InitialDate = eventRecord.InitialDate,
                                    FinalDate = eventRecord.FinalDate,
                                    Organizer = eventRecord.Organizer,
                                    WebPage = eventRecord.WebPage,
                                    Active = eventRecord.Active,
                                    ProfessionName = eventRecord.ProfessionName,
                                    SpecialityName = eventRecord.SpecialityName,
                                    AddressId = eventRecord.AddressId,
                                    Street = eventRecord.Street,
                                    InternalNumber = eventRecord.InternalNumber,
                                    Suburb = eventRecord.Suburb,
                                    ZipCode = eventRecord.ZipCode,
                                    Location = eventRecord.Location,
                                    CountryId = eventRecord.CountryId,
                                    CountryName = eventRecord.CountryName,
                                    StateId = eventRecord.StateId,
                                    StateName = eventRecord.StateName,
                                    Lada = eventRecord.Lada,
                                    PhoneOne = eventRecord.PhoneOne,
                                    PhoneTwo = eventRecord.PhoneTwo,
                                    Ext = eventRecord.Ext,
                                    Latitude = eventRecord.Latitude,
                                    Longitude = eventRecord.Longitude,
                                    EventDistance = eventRecord.EventDistance
                                };

                IEnumerable<PLMClientsBusinessEntities.EventsDetailInfo> collect;
                collect = eventList;
                events = collect.ToList();
            }
            return events;
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByPrefixByDate(string prefix, byte targetId, decimal latitude, decimal longitude)
        {
            return PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByPrefix(prefix, targetId);
        }

        public List<PLMClientsBusinessEntities.EventCategoryDetailInfo> getEventCategories(string prefix, byte targetId, decimal latitude, decimal longitude)
        {
            if (prefix == null || prefix == "" || targetId <= 0 )
                throw new ArgumentException("The prefix or target  does not exist.");

            List<PLMClientsBusinessEntities.EventCategoryDetailInfo> eventCategories = PLMClientsDataAccessComponent.EventsDALC.Instance.getEventCategories(prefix, targetId);

            //Add Events for Category
            this.addEventsOpcional(eventCategories, prefix, targetId, latitude, longitude);


            return eventCategories;
        }
        
        public List<PLMClientsBusinessEntities.EventCategoryDetailInfo> getEventCategoriesByState(string prefix, byte targetId, int stateId, decimal latitude, decimal longitude)
        {
            if (prefix == null || prefix == "" || targetId <= 0 || stateId <= 0)
                throw new ArgumentException("The prefix or target or state does not exist.");

            List<PLMClientsBusinessEntities.EventCategoryDetailInfo> eventCategories = PLMClientsDataAccessComponent.EventsDALC.Instance.getEventCategoriesByState(prefix, targetId, stateId);

            //Add Events for Category
            this.addEvents(eventCategories, prefix, targetId, stateId, latitude, longitude);

            return eventCategories;
        }

        public List<PLMClientsBusinessEntities.StateInfo> getOtherEventStates(string prefix, byte targetId, int stateId)
        {
            if (prefix == null || prefix == "" || targetId <= 0 || stateId <= 0)
                throw new ArgumentException("The prefix or target or state does not exist.");

            return PLMClientsDataAccessComponent.EventsDALC.Instance.getOtherEventStates(prefix, targetId, stateId);
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsByPrefixByTarget(string prefix, byte targetId)
        {
            return PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsByPrefixByTarget(prefix, targetId);
        }

        public List<PLMClientsBusinessEntities.EventMonthsInfo> getEventMonthsBySpeciality(short specialityId)
        {
            List<PLMClientsBusinessEntities.EventMonthsInfo> eventMonths = new List<EventMonthsInfo>();

            eventMonths = PLMClientsDataAccessComponent.EventsDALC.Instance.getEventMonthsBySpeciality(specialityId);

            if (eventMonths.Count > 0)
            {
                foreach (PLMClientsBusinessEntities.EventMonthsInfo eventDate in eventMonths)
                    eventDate.Events = PLMClientsBusinessLogicComponent.EventsBLC.Instance.getEventsBySpecialityByDate(specialityId, eventDate.Month, eventDate.Year);

                return eventMonths;
            }
            else
                return null;
        }

        public List<PLMClientsBusinessEntities.EventsDetailInfo> getEventsBySpecialityByDate(short specialityId, string month, string year)
        {
            return PLMClientsDataAccessComponent.EventsDALC.Instance.getEventsBySpecialityByDate(specialityId, month, year);
        }

        #endregion

        #region Private Methods

        private void addEvents(List<EventCategoryDetailInfo> eventCategories, string prefix, byte targetId, int stateId, decimal latitude, decimal longitude)
        {
            foreach (EventCategoryDetailInfo category in eventCategories)
            {
                category.Events = PLMClientsBusinessLogicComponent.EventsBLC.Instance.getEventsByStateByCategory(prefix, targetId, stateId, category.CategoryId);

                foreach (PLMClientsBusinessEntities.EventsDetailInfo eventItem in category.Events)
                    eventItem.EventDistance = Convert.ToDecimal(
                        this.calculateDistance(Convert.ToDouble(latitude), Convert.ToDouble(longitude), Convert.ToDouble(eventItem.Latitude), Convert.ToDouble(eventItem.Longitude)));

                var eventList = from eventRecord in category.Events
                                 orderby eventRecord.EventDistance
                                 select new PLMClientsBusinessEntities.EventsDetailInfo
                                 {
                                     EventId = eventRecord.EventId,
                                     EventName = eventRecord.EventName,
                                     TypeId = eventRecord.TypeId,
                                     TypeName = eventRecord.TypeName,
                                     Site = eventRecord.Site,
                                     InitialDate = eventRecord.InitialDate,
                                     FinalDate = eventRecord.FinalDate,
                                     Organizer = eventRecord.Organizer,
                                     WebPage = eventRecord.WebPage,
                                     Active = eventRecord.Active,
                                     ProfessionName = eventRecord.ProfessionName,
                                     SpecialityName = eventRecord.SpecialityName,
                                     AddressId = eventRecord.AddressId,
                                     Street = eventRecord.Street,
                                     InternalNumber = eventRecord.InternalNumber,
                                     Suburb = eventRecord.Suburb,
                                     ZipCode = eventRecord.ZipCode,
                                     Location = eventRecord.Location,
                                     CountryId = eventRecord.CountryId,
                                     CountryName = eventRecord.CountryName,
                                     StateId = eventRecord.StateId,
                                     StateName = eventRecord.StateName,
                                     Lada = eventRecord.Lada,
                                     PhoneOne = eventRecord.PhoneOne,
                                     PhoneTwo = eventRecord.PhoneTwo,
                                     Ext = eventRecord.Ext,
                                     Latitude = eventRecord.Latitude,
                                     Longitude = eventRecord.Longitude,
                                     EventDistance = eventRecord.EventDistance
                                 };

                IEnumerable<PLMClientsBusinessEntities.EventsDetailInfo> collect;
                collect = eventList;
                category.Events = collect.ToList();
            }
        }

        private void addEventsOpcional(List<EventCategoryDetailInfo> eventCategories, string prefix, byte targetId, decimal latitude, decimal longitude)
        {
             foreach (EventCategoryDetailInfo category in eventCategories)
            {
                category.Events = PLMClientsBusinessLogicComponent.EventsBLC.Instance.getEventsByCategory(prefix, targetId, category.CategoryId);

                foreach (PLMClientsBusinessEntities.EventsDetailInfo eventItem in category.Events)
                    eventItem.EventDistance = Convert.ToDecimal(
                        this.calculateDistance(Convert.ToDouble(latitude), Convert.ToDouble(longitude), Convert.ToDouble(eventItem.Latitude), Convert.ToDouble(eventItem.Longitude)));

                var eventList = from eventRecord in category.Events
                                 orderby eventRecord.EventDistance
                                 select new PLMClientsBusinessEntities.EventsDetailInfo
                                 {
                                     EventId = eventRecord.EventId,
                                     EventName = eventRecord.EventName,
                                     TypeId = eventRecord.TypeId,
                                     TypeName = eventRecord.TypeName,
                                     Site = eventRecord.Site,
                                     InitialDate = eventRecord.InitialDate,
                                     FinalDate = eventRecord.FinalDate,
                                     Organizer = eventRecord.Organizer,
                                     WebPage = eventRecord.WebPage,
                                     Active = eventRecord.Active,
                                     ProfessionName = eventRecord.ProfessionName,
                                     SpecialityName = eventRecord.SpecialityName,
                                     AddressId = eventRecord.AddressId,
                                     Street = eventRecord.Street,
                                     InternalNumber = eventRecord.InternalNumber,
                                     Suburb = eventRecord.Suburb,
                                     ZipCode = eventRecord.ZipCode,
                                     Location = eventRecord.Location,
                                     CountryId = eventRecord.CountryId,
                                     CountryName = eventRecord.CountryName,
                                     StateId = eventRecord.StateId,
                                     StateName = eventRecord.StateName,
                                     Lada = eventRecord.Lada,
                                     PhoneOne = eventRecord.PhoneOne,
                                     PhoneTwo = eventRecord.PhoneTwo,
                                     Ext = eventRecord.Ext,
                                     Latitude = eventRecord.Latitude,
                                     Longitude = eventRecord.Longitude,
                                     EventDistance = eventRecord.EventDistance
                                 };

                IEnumerable<PLMClientsBusinessEntities.EventsDetailInfo> collect;
                collect = eventList;
                category.Events = collect.ToList();
            }

        }

        private double calculateDistance(double clientLatitude, double clientLongitud, double eventLatitude, double eventLongitude)
        {
            double PI_OVER_180 = Math.PI / 180.0;
            double a = 6378137;
            double b = 6356752.3142;
            double f = 1 / 298.257223563;

            double L = (eventLongitude - clientLongitud) * PI_OVER_180;
            double U1 = Math.Atan((1 - f) * Math.Tan(clientLatitude * PI_OVER_180));
            double U2 = Math.Atan((1 - f) * Math.Tan(eventLatitude * PI_OVER_180));

            double sinU1 = Math.Sin(U1);
            double cosU1 = Math.Cos(U1);

            double sinU2 = Math.Sin(U2);
            double cosU2 = Math.Cos(U2);

            double lambda = L;
            double lambdaP = 2 * Math.PI;

            double cosSqAlpha = 0;
            double cos2SigmaM = 0;
            double cosSigma = 0;
            double sinSigma = 0;
            double sigma = 0;

            int iterLimit = 20;

            while (Math.Abs(lambda - lambdaP) > Math.Pow(1, -12) && --iterLimit > 0)
            {
                double sinLambda = Math.Sin(lambda);
                double cosLambda = Math.Cos(lambda);
                sinSigma = Math.Sqrt((cosU2 * sinLambda) * (cosU2 * sinLambda) + (cosU1 * sinU2 - sinU1 * cosU2 * cosLambda) * (cosU1 * sinU2 - sinU1 * cosU2 * cosLambda));

                if (sinSigma == 0)
                    return 0;

                cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda;
                sigma = Math.Atan2(sinSigma, cosSigma);
                double sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma;
                cosSqAlpha = 1 - sinAlpha * sinAlpha;
                cos2SigmaM = cosSigma - 2 * sinU1 * sinU2 / cosSqAlpha;
                if (Double.IsNaN(cos2SigmaM))
                    cos2SigmaM = 0;
                double C = f / 16 * cosSqAlpha * (4 + f * (4 - 3 * cosSqAlpha));
                lambdaP = lambda;
                lambda = L + (1 - C) * f * sinAlpha * (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)));
            }

            if (iterLimit == 0)
                return 0;

            double uSq = cosSqAlpha * (a * a - b * b) / (b * b);
            double A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));
            double B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));
            double deltaSigma = B * sinSigma * (cos2SigmaM + B / 4 * (cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM) - B / 6 * cos2SigmaM * (-3 + 4 * sinSigma * sinSigma) * (-3 + 4 * cos2SigmaM * cos2SigmaM)));
            double s = b * A * (sigma - deltaSigma);

            return s / 1000;
        }

        #endregion

        public static readonly EventsBLC Instance = new EventsBLC();

    }
}
