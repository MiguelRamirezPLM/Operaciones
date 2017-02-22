using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PLMClientsBusinessLogicComponent
{
    public class ClientsBLC
    {

        #region Constructors

        private ClientsBLC() { }

        #endregion

        #region Public Methods

        #region Utility methods

        public bool checkEmail(string email)
        {
            // Verify if the email variable has any value:
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            return PLMClientsDataAccessComponent.ClientsDALC.Instance.checkEmail(email);
        }

        public int addClient(PLMClientsBusinessEntities.ClientInfo BEntity)
        {
            return PLMClientsDataAccessComponent.ClientsDALC.Instance.insert(BEntity);
        }

        public int addClientComplete(PLMClientsBusinessEntities.ClientInfo BEntity)
        {
            return PLMClientsDataAccessComponent.ClientsDALC.Instance.insertComplete(BEntity);
        }

        public void updateClient(PLMClientsBusinessEntities.ClientInfo BEntity)
        {
            PLMClientsDataAccessComponent.ClientsDALC.Instance.update(BEntity);
        }

        public void updateClientComplete(PLMClientsBusinessEntities.ClientInfo BEntity)
        {
            PLMClientsDataAccessComponent.ClientsDALC.Instance.updateComplete(BEntity);
        }

        public void removeClient(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The client does not exist.");

            PLMClientsDataAccessComponent.ClientsDALC.Instance.delete(clientId);
        }

        public PLMClientsBusinessEntities.ClientInfo getClient(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The client does not exist.");

            return PLMClientsDataAccessComponent.ClientsDALC.Instance.getOne(clientId);
        }

        public PLMClientsBusinessEntities.ClientInfo getByCompleteName(string completeName)
        {
            // Verify if the email variable has any value:
            if (string.IsNullOrEmpty(completeName) || string.IsNullOrWhiteSpace(completeName))
                throw new ArgumentNullException("completeName");

            return PLMClientsDataAccessComponent.ClientsDALC.Instance.getByName(completeName);
        }

        public PLMClientsBusinessEntities.ClientInfo getByEmail(string email)
        {
            // Verify if the email variable has any value:
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            return PLMClientsDataAccessComponent.ClientsDALC.Instance.getByEmail(email);
        }

        public PLMClientsBusinessEntities.ClientInfo getCompleteByEmail(string email)
        {
            // Verify if the email variable has any value:
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            return PLMClientsDataAccessComponent.ClientsDALC.Instance.getCompleteByEmail(email);
        }

        public List<PLMClientsBusinessEntities.ClientAdvisorInfo> getByEntrySource(int entrySource)
        {
            // Verify if the email variable has any value:
            if (entrySource<=0)
                throw new ArgumentNullException("The Entry Source do not Exist");

            List<PLMClientsBusinessEntities.ClientInfo> client = PLMClientsDataAccessComponent.ClientsDALC.Instance.getByEntrySource(entrySource);
            List<PLMClientsBusinessEntities.ClientAdvisorInfo> clientAdvisorList = new List<PLMClientsBusinessEntities.ClientAdvisorInfo>();
            PLMClientsBusinessEntities.ClientAdvisorInfo clientAd;
            foreach (PLMClientsBusinessEntities.ClientInfo cl in client)
            {
                clientAd = new PLMClientsBusinessEntities.ClientAdvisorInfo();
                clientAd.Active = cl.Active;
                clientAd.AddedDate = cl.AddedDate;
                clientAd.Age = cl.Age;
                clientAd.Birthday = cl.Birthday;
                clientAd.ClientId = cl.ClientId;
                clientAd.CompleteName = cl.CompleteName;
                clientAd.CountryId = cl.CountryId;
                clientAd.Email = cl.Email;
                clientAd.EntrySourceId = cl.EntrySourceId;
                clientAd.FirstName = cl.FirstName;
                clientAd.Gender = cl.Gender;
                clientAd.LastName = cl.LastName;
                clientAd.LastUpdate = cl.LastUpdate;
                clientAd.LocationId = cl.LocationId;
                clientAd.Password = cl.Password;
                clientAd.PhoneNumber = cl.PhoneNumber;
                clientAd.SecondLastName = cl.SecondLastName;
                clientAd.StateId = cl.StateId;
                clientAd.SuburbId = cl.SuburbId;
                clientAd.ZipCode = cl.ZipCode;
                clientAd.ZipCodeId = cl.ZipCodeId;
                clientAd.States = PLMClientsDataAccessComponent.StatesDALC.Instance.getByClientAdvisor(clientAd.ClientId);
                clientAdvisorList.Add(clientAd);

            }
            return clientAdvisorList;
        }

        public PLMClientsBusinessEntities.MobileClientInfo getByIMEI(string IMEI)
        {
            if (string.IsNullOrEmpty(IMEI))
                throw new ArgumentNullException("IMEI");

            return PLMClientsDataAccessComponent.ClientsDALC.Instance.getByIMEI(IMEI);
        }

        public PLMClientsBusinessEntities.MobileClientInfo getByIMEI(string IMEI, string prefix)
        {
            if (string.IsNullOrEmpty(IMEI) || string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException("IMEI or Prefix is null");

            return PLMClientsDataAccessComponent.ClientsDALC.Instance.getByIMEI(IMEI, prefix);
        }

        public int? getCountryByID(string countryID)
        {
            PLMClientsBusinessEntities.CountryInfo countryInfo = new PLMClientsBusinessEntities.CountryInfo();

            countryInfo = PLMClientsDataAccessComponent.CountriesDALC.Instance.getByCode(countryID);

            if (countryInfo != null)
                return countryInfo.CountryId;
            else
                return null;

        }

        public int? getStateByShortName(string stateShortName)
        {
            PLMClientsBusinessEntities.StateInfo stateInfo = new PLMClientsBusinessEntities.StateInfo();

            stateInfo = PLMClientsDataAccessComponent.StatesDALC.Instance.getByShortName(stateShortName);

            if (stateInfo != null)
                return stateInfo.StateId;
            else
                return null;

        }

        public int? getStateByCountryByShortName(int countryId, string stateShortName)
        {
            PLMClientsBusinessEntities.StateInfo stateInfo = new PLMClientsBusinessEntities.StateInfo();

            stateInfo = PLMClientsDataAccessComponent.StatesDALC.Instance.getByCountryByShortName(countryId, stateShortName);

            if (stateInfo != null)
                return stateInfo.StateId;
            else
                return null;
        }

        public int saveClientInfo(string firstName, string lastName, string slastName, char gender, DateTime? birthday, string email, string password, 
            string country, string state, PLMClientsBusinessEntities.Catalogs.EntrySources source, string age, string zipCode, string mobile)
        {
            PLMClientsBusinessEntities.CountryInfo countryInfo = new PLMClientsBusinessEntities.CountryInfo();
            countryInfo = PLMClientsDataAccessComponent.CountriesDALC.Instance.getByCode(country);

            return this.saveClient(firstName, lastName, slastName, gender, birthday, email, password, this.getCountryByID(country), 
                (countryInfo != null ? this.getStateByCountryByShortName(countryInfo.CountryId,state) : null), source, age, zipCode, mobile);
        }

        public int saveClientCompleteInfo(string firstName, string lastName, string slastName, char gender, DateTime? birthday, string email, string password,
            string country, string state, PLMClientsBusinessEntities.Catalogs.EntrySources source, string age, string zipCode,int locationId,int suburbId,int zipCodeId)
        {
            PLMClientsBusinessEntities.CountryInfo countryInfo = new PLMClientsBusinessEntities.CountryInfo();
            countryInfo = PLMClientsDataAccessComponent.CountriesDALC.Instance.getByCode(country);

            return this.saveClientComplete(firstName, lastName, slastName, gender, birthday, email, password, this.getCountryByID(country),
                (countryInfo != null ? this.getStateByCountryByShortName(countryInfo.CountryId, state) : null), source, age, zipCode,locationId,suburbId,zipCodeId);
        }

        public void updateClientInfo(PLMClientsBusinessEntities.ClientInfo clientInfo, string firstName, string lastName, string slastName, char gender, DateTime? birthday,
            string email, string password, string country, string state, string age, string zipCode)
        {
            PLMClientsBusinessEntities.CountryInfo countryInfo = new PLMClientsBusinessEntities.CountryInfo();
            countryInfo = PLMClientsDataAccessComponent.CountriesDALC.Instance.getByCode(country);

            this.updateClient(clientInfo, firstName, lastName, slastName, gender, birthday, email, password, this.getCountryByID(country),
                (countryInfo != null ? this.getStateByCountryByShortName(countryInfo.CountryId, state) : null), age, zipCode);
        }

        public void updateClientCompleteInfo(PLMClientsBusinessEntities.ClientInfo clientInfo, string firstName, string lastName, string slastName, char gender, DateTime? birthday,
            string email, string password, string country, string state, string age, string zipCode,int locationId,int suburbId,int zipCodeId)
        {
            PLMClientsBusinessEntities.CountryInfo countryInfo = new PLMClientsBusinessEntities.CountryInfo();
            countryInfo = PLMClientsDataAccessComponent.CountriesDALC.Instance.getByCode(country);

            this.updateClientComplete(clientInfo, firstName, lastName, slastName, gender, birthday, email, password, this.getCountryByID(country),
                (countryInfo != null ? this.getStateByCountryByShortName(countryInfo.CountryId, state) : null), age, zipCode,locationId,suburbId,zipCodeId);
        }
        public void removeSpecialityClient(int clientId)
        {
            // Delete all Residences stored in the database.
            PLMClientsDataAccessComponent.ResidenceClientsDALC.Instance.delete(clientId);

            // Delete all Specialities stored in the database.
            PLMClientsDataAccessComponent.SpecialityClientsDALC.Instance.delete(clientId);
        }

        public void saveProfessionClient(int clientId, PLMClientsBusinessEntities.Catalogs.Professions profession, string otherProfession, string professionalLicense)
        {
            this.saveProfession(clientId, profession, otherProfession, professionalLicense);
        }

        public void saveSpecialityClient(int clientId, PLMClientsBusinessEntities.Catalogs.Professions profession, PLMClientsBusinessEntities.Catalogs.Specialities? speciality, string specialityName)
        {
            this.saveSpeciality(clientId, profession, speciality, specialityName);
        }

        public void associateClientCode(int clientId, int codeId)
        {
            this.associateCode(clientId, codeId);
        }

        public PLMClientsBusinessEntities.WebClientInfo getClientByEmailByPassword(string email, string password)
        {
            return PLMClientsDataAccessComponent.ClientsDALC.Instance.getByEmailByPassword(email, password);
        }

        public PLMClientsBusinessEntities.ClientDetailCodeInfo getClientByEmailByPasswordByPrefix(string email, string password, string prefix)
        {
            PLMCryptographyComponent.CryptographyComponent cryptographyComponent = new PLMCryptographyComponent.CryptographyComponent();

            PLMClientsBusinessEntities.ClientDetailCodeInfo clientCodeInfo = PLMClientsDataAccessComponent.ClientsDALC.Instance.getByEmailByPasswordByPrefix(email, password, prefix);

            if (clientCodeInfo != null)
            {
                PLMClientsBusinessEntities.ClientDetailInfo clientDetailInfo = getClientDetailByEmail(email);
              
                clientCodeInfo.Active = clientDetailInfo.Active;
                clientCodeInfo.AddedDate = clientDetailInfo.AddedDate;
                clientCodeInfo.Age = clientDetailInfo.Age;
                clientCodeInfo.Birthday = clientDetailInfo.Birthday;
                clientCodeInfo.ClientId = clientDetailInfo.ClientId;
                clientCodeInfo.CompleteName = clientDetailInfo.CompleteName;
                clientCodeInfo.CountryId = clientDetailInfo.CountryId;
                clientCodeInfo.CountryName = clientDetailInfo.CountryName;
                clientCodeInfo.Email = clientDetailInfo.Email;
                
                if(clientCodeInfo.CountryId != null)
                    clientCodeInfo.CountryShortName = CountriesBLC.Instance.getCountry((int)clientDetailInfo.CountryId).ID;
                
                clientCodeInfo.EntrySourceId = clientDetailInfo.EntrySourceId;
                clientCodeInfo.FirstName = clientDetailInfo.FirstName;
                clientCodeInfo.Gender = clientDetailInfo.Gender;
                clientCodeInfo.LastName = clientDetailInfo.LastName;
                clientCodeInfo.LastUpdate = clientDetailInfo.LastUpdate;
                clientCodeInfo.OtherProfession = clientDetailInfo.OtherProfession;
                clientCodeInfo.OtherSpeciality = clientDetailInfo.OtherSpeciality;
                clientCodeInfo.Password = clientDetailInfo.Password;
                clientCodeInfo.Profession = clientDetailInfo.Profession;
                clientCodeInfo.ProfessionalLicense = clientDetailInfo.ProfessionalLicense;
                clientCodeInfo.ResidenceId = clientDetailInfo.ResidenceId;
                clientCodeInfo.ResidenceKey = clientDetailInfo.ResidenceKey;
                clientCodeInfo.SecondLastName = clientDetailInfo.SecondLastName;
                clientCodeInfo.Speciality = clientDetailInfo.Speciality;
                clientCodeInfo.StateId = clientDetailInfo.StateId;
                clientCodeInfo.StateName = clientDetailInfo.StateName;
                
                if(clientCodeInfo.StateId != null)
                    clientCodeInfo.StateShortName = StatesBLC.Instance.getState((int)clientCodeInfo.StateId).ShortName;
                
                return clientCodeInfo;

            }
            else
                return null;
             


        }

        public PLMClientsBusinessEntities.ClientDetailInfo getClientDetailByEmail(string email)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, System.Configuration.ConfigurationManager.AppSettings["RegExpression"]))
                throw new Exception("El correo electrónico no tiene el formato correcto.");

            // Does the email client exists in the database?
            PLMClientsBusinessEntities.ClientInfo clientInfo = this.getByEmail(email.Trim());

            if (clientInfo == null)
                //throw new Exception("El usuario no está registrado");
                return null;

            // Create ClientDetailInfo object with ClientInfo members:
            PLMClientsBusinessEntities.ClientDetailInfo clientDetailInfo = new PLMClientsBusinessEntities.ClientDetailInfo(clientInfo);

            // Get profession:
            PLMClientsBusinessEntities.ProfessionClientInfo professionClientInfo = ProfessionClientsBLC.Instance.getProfessionByClient(clientDetailInfo.ClientId);
            if (professionClientInfo != null)
            {
                clientDetailInfo.Profession = PLMClientsDataAccessComponent.ProfessionsDALC.Instance.getOne(professionClientInfo.ProfessionId);
                clientDetailInfo.ProfessionalLicense = professionClientInfo.ProfessionalLicense;
            }

            // Get Speciality:
            PLMClientsBusinessEntities.SpecialityClientInfo specClientInfo = SpecialityClientsBLC.Instance.getSpecialityByClient(clientDetailInfo.ClientId);
            if (specClientInfo != null)
            {
                clientDetailInfo.Speciality = PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getOne(specClientInfo.SpecialityId);
                clientDetailInfo.OtherSpeciality = specClientInfo.OtherSpeciality;
            }

            //Get Residence Level
            PLMClientsBusinessEntities.ResidenceLevelsInfo residenceInfo = ResidenceLevelsBLC.Instance.getResidenceLevelByClient(clientDetailInfo.ClientId);
            if (residenceInfo != null)
            {
                clientDetailInfo.ResidenceId = residenceInfo.ResidenceId;
                clientDetailInfo.ResidenceKey = residenceInfo.ResidenceKey;
            }

            if (clientInfo.CountryId != null)
                clientDetailInfo.CountryName = CountriesBLC.Instance.getCountry((int)clientInfo.CountryId).CountryName;

            if (clientInfo.StateId != null)
            {
                clientDetailInfo.StateName = StatesBLC.Instance.getState((int)clientInfo.StateId).StateName;
                clientDetailInfo.StateShortName = StatesBLC.Instance.getState((int)clientInfo.StateId).ShortName;
            }
            return clientDetailInfo;
        }

        public PLMClientsBusinessEntities.ClientDetailInfo getClientCompleteDetailByEmail(string email)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, System.Configuration.ConfigurationManager.AppSettings["RegExpression"]))
                throw new Exception("El correo electrónico no tiene el formato correcto.");

            // Does the email client exists in the database?
            PLMClientsBusinessEntities.ClientInfo clientInfo = this.getCompleteByEmail(email.Trim());

            if (clientInfo == null)
                //throw new Exception("El usuario no está registrado");
                return null;

            // Create ClientDetailInfo object with ClientInfo members:
            PLMClientsBusinessEntities.ClientDetailInfo clientDetailInfo = new PLMClientsBusinessEntities.ClientDetailInfo(clientInfo);

            // Get profession:
            PLMClientsBusinessEntities.ProfessionClientInfo professionClientInfo = ProfessionClientsBLC.Instance.getProfessionByClient(clientDetailInfo.ClientId);
            if (professionClientInfo != null)
            {
                clientDetailInfo.Profession = PLMClientsDataAccessComponent.ProfessionsDALC.Instance.getOne(professionClientInfo.ProfessionId);
                clientDetailInfo.ProfessionalLicense = professionClientInfo.ProfessionalLicense;
            }

            // Get Speciality:
            PLMClientsBusinessEntities.SpecialityClientInfo specClientInfo = SpecialityClientsBLC.Instance.getSpecialityByClient(clientDetailInfo.ClientId);
            if (specClientInfo != null)
            {
                clientDetailInfo.Speciality = PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getOne(specClientInfo.SpecialityId);
                clientDetailInfo.OtherSpeciality = specClientInfo.OtherSpeciality;
            }

            //Get Residence Level
            PLMClientsBusinessEntities.ResidenceLevelsInfo residenceInfo = ResidenceLevelsBLC.Instance.getResidenceLevelByClient(clientDetailInfo.ClientId);
            if (residenceInfo != null)
            {
                clientDetailInfo.ResidenceId = residenceInfo.ResidenceId;
                clientDetailInfo.ResidenceKey = residenceInfo.ResidenceKey;
            }

            if (clientInfo.CountryId != null)
                clientDetailInfo.CountryName = CountriesBLC.Instance.getCountry((int)clientInfo.CountryId).CountryName;

            if (clientInfo.StateId != null)
            {
                clientDetailInfo.StateName = StatesBLC.Instance.getState((int)clientInfo.StateId).StateName;
                clientDetailInfo.StateShortName = StatesBLC.Instance.getState((int)clientInfo.StateId).ShortName;
            }
            return clientDetailInfo;
        }

        public PLMClientsBusinessEntities.ClientDetailCodeInfo getClientDetailByEmailByPrefix(string email, string prefix)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, System.Configuration.ConfigurationManager.AppSettings["RegExpression"]))
                throw new Exception("El correo electrónico no tiene el formato correcto.");

            // Does the email client exists in the database?
            PLMClientsBusinessEntities.ClientInfo clientInfo = this.getByEmail(email.Trim());

            if (clientInfo == null)
                //throw new Exception("El usuario no está registrado");
                return null;

            // Create ClientDetailInfo object with ClientInfo members:
            PLMClientsBusinessEntities.ClientDetailInfo clientDetailInfo = new PLMClientsBusinessEntities.ClientDetailInfo(clientInfo);

            // Get profession:
            PLMClientsBusinessEntities.ProfessionClientInfo professionClientInfo = ProfessionClientsBLC.Instance.getProfessionByClient(clientDetailInfo.ClientId);
            if (professionClientInfo != null)
            {
                clientDetailInfo.Profession = PLMClientsDataAccessComponent.ProfessionsDALC.Instance.getOne(professionClientInfo.ProfessionId);
                clientDetailInfo.ProfessionalLicense = professionClientInfo.ProfessionalLicense;
            }

            // Get Speciality:
            PLMClientsBusinessEntities.SpecialityClientInfo specClientInfo = SpecialityClientsBLC.Instance.getSpecialityByClient(clientDetailInfo.ClientId);
            if (specClientInfo != null)
            {
                clientDetailInfo.Speciality = PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getOne(specClientInfo.SpecialityId);
                clientDetailInfo.OtherSpeciality = specClientInfo.OtherSpeciality;
            }

            //Get Residence Level
            PLMClientsBusinessEntities.ResidenceLevelsInfo residenceInfo = ResidenceLevelsBLC.Instance.getResidenceLevelByClient(clientDetailInfo.ClientId);
            if (residenceInfo != null)
            {
                clientDetailInfo.ResidenceId = residenceInfo.ResidenceId;
                clientDetailInfo.ResidenceKey = residenceInfo.ResidenceKey;
            }

            if (clientInfo.CountryId != null)
                clientDetailInfo.CountryName = CountriesBLC.Instance.getCountry((int)clientInfo.CountryId).CountryName;

            if (clientInfo.StateId != null)
            {
                clientDetailInfo.StateName = StatesBLC.Instance.getState((int)clientInfo.StateId).StateName;
                clientDetailInfo.StateShortName = StatesBLC.Instance.getState((int)clientInfo.StateId).ShortName;
            }
            // Create ClientDetailCodeInfo object with ClientInfo members:
            PLMClientsBusinessEntities.ClientDetailCodeInfo clientDetailCodeInfo = new PLMClientsBusinessEntities.ClientDetailCodeInfo(clientDetailInfo);

            clientDetailCodeInfo.ClientDetail = clientDetailInfo;
            clientDetailCodeInfo.CodeId = PLMClientsDataAccessComponent.CodesDALC.Instance.getByEmailByPrefix(email, prefix).CodeId;
            clientDetailCodeInfo.Codestring = PLMClientsDataAccessComponent.CodesDALC.Instance.getByEmailByPrefix(email, prefix).CodeString;

            return clientDetailCodeInfo;
        }

        #endregion

        #region Client Server methods

        public string saveClientInfo(string firstName, string lastName, string slastName, string email,
            PLMClientsBusinessEntities.Catalogs.Professions profession, PLMClientsBusinessEntities.Catalogs.Specialities? speciality, string specialityName, string professionalLicense,
            string codePrefix, string codeString, PLMClientsBusinessEntities.Catalogs.EntrySources source, string macAddresses)
        {
            /* 
             * Step (0): Get Client information given by CodeString:
             * 
             * Verify if the given CodeString has an associated client:
             * 
             */
            if (string.IsNullOrEmpty(codeString))
                throw new ArgumentNullException("codeString");

            PLMClientsBusinessEntities.ClientInfo clientInfo = PLMClientsDataAccessComponent.ClientsDALC.Instance.getByCode(codeString);

            if (clientInfo != null)
                throw new Exception("El usuario ya está registrado.");

            /* 
             * Step (1): Client information
             * 
             * If the client does not exist in the database, add to the database, otherwise
             * get it from the database.
             * 
             */
            int clientId = this.saveClient(firstName, lastName, slastName, 'M', null, email, null, null, null, source, null, null, null);

            /*
             * Step (2): Profession information
             * 
             * Verify if the client has a profession stored in the database.
             * 
             */
            this.saveProfession(clientId, profession, string.Empty, professionalLicense);

            /*
             * Step (3): Speciality information
             * 
             */
            this.saveSpeciality(clientId, profession, speciality, specialityName);

            /*
             * Step (4): Code information
             * 
             * Create a new code for the given user.
             * 
             */
            PLMClientsBusinessEntities.CodeInfo codeInfo = this.createCode(clientId, codePrefix);


            /*
             *Step (5): Mac Addresses Information
             *
             * Saves the user's mac address
             */
            this.saveMacAddress(clientId, codeInfo.CodeId, this.cleanMacAddress(macAddresses));


            return codeInfo.CodeString;
        }

        public void updateClientInfo(string firstName, string lastName, string slastName, string email,
            PLMClientsBusinessEntities.Catalogs.Professions profession, PLMClientsBusinessEntities.Catalogs.Specialities? speciality, string specialityName, string professionalLicense,
            string codePrefix, string codeString, PLMClientsBusinessEntities.Catalogs.EntrySources source, string macAddresses)
        {
            // Get client from the database given by codeString:
            PLMClientsBusinessEntities.ClientInfo clientInfo = PLMClientsDataAccessComponent.ClientsDALC.Instance.getByCode(codeString);

            /* 
             * Step (1): CodeString information
             * 
             * Get Client info given by the code stored into the client machine. If the Code is not associated 
             * to any Client throws an exception.
             * 
             */
            if (clientInfo == null)
                throw new Exception("Este código no ha sido registrado.");

            // Guard old email to get the user by email from the database:
            string oldEmail = clientInfo.Email;

            /* 
             * Step (2): Client information
             * 
             * Update client information into the database.
             * 
             */
            this.updateClient(clientInfo, firstName, lastName, slastName, 'M', null, email, string.Empty, null, null, null, null);

            /* 
             * Step (3): Profession information
             * 
             * Update profession information into the database.
             * 
             */
            this.saveProfession(clientInfo.ClientId, profession, string.Empty, professionalLicense);

            /*
             * Step (4): Speciality information
             * 
             */
            this.saveSpeciality(clientInfo.ClientId, profession, speciality, specialityName);

            /*
             * Step (5): Mac Addresses Information
             *
             * Updates the user's mac address
             */
            this.saveMacAddress(clientInfo.ClientId, CodesBLC.Instance.getCode(codeString).CodeId, this.cleanMacAddress(macAddresses)); 
        }

        public PLMClientsBusinessEntities.ClientDetailInfo getByCode(string codeString)
        {
            if (string.IsNullOrEmpty(codeString))
                throw new ArgumentNullException("codeString");

            PLMClientsBusinessEntities.ClientInfo clientInfo = PLMClientsDataAccessComponent.ClientsDALC.Instance.getByCode(codeString);

            if (clientInfo == null)
                throw new Exception("El usuario no está registrado");

            // Create ClientDetailInfo object with ClientInfo members:
            PLMClientsBusinessEntities.ClientDetailInfo clientDetailInfo = new PLMClientsBusinessEntities.ClientDetailInfo(clientInfo);

            // Get profession:
            PLMClientsBusinessEntities.ProfessionClientInfo professionClientInfo = ProfessionClientsBLC.Instance.getProfessionByClient(clientDetailInfo.ClientId);

            if (professionClientInfo == null)
            {
                clientDetailInfo.Profession = null;
                clientDetailInfo.ProfessionalLicense = null;
            }
            else
            {
                clientDetailInfo.Profession = PLMClientsDataAccessComponent.ProfessionsDALC.Instance.getOne(professionClientInfo.ProfessionId);
                clientDetailInfo.ProfessionalLicense = professionClientInfo.ProfessionalLicense;
            }

            // Get Speciality:
            PLMClientsBusinessEntities.SpecialityClientInfo specClientInfo = SpecialityClientsBLC.Instance.getSpecialityByClient(clientDetailInfo.ClientId);

            if (specClientInfo == null)
            {
                clientDetailInfo.Speciality = null;
                clientDetailInfo.OtherSpeciality = null;
            }
            else
            {
                clientDetailInfo.Speciality = PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getOne(specClientInfo.SpecialityId);
                clientDetailInfo.OtherSpeciality = specClientInfo.OtherSpeciality;
            }

            return clientDetailInfo;
        }

        #endregion

        #endregion

        #region Private Methods

        private int saveClient(string firstName, string lastName, string slastName, char gender, DateTime? birthday, string email, string password, 
            int? countryId, int? stateId, PLMClientsBusinessEntities.Catalogs.EntrySources source, string age, string zipCode, string mobile)
        {
            
            //Valid if the email is correct
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, System.Configuration.ConfigurationManager.AppSettings["RegExpression"]))
                throw new Exception("El correo electrónico no tiene el formato correcto.");

            // Does the email client exists in the database?
            PLMClientsBusinessEntities.ClientInfo clientInfo = this.getByEmail(email.Trim());

            // Get complete name:
            StringBuilder completeName = new StringBuilder((firstName.Trim() + " " + lastName.Trim() + " " + slastName.Trim()).Trim());

            // The email variable did not mathc in the database, does client complete name exist in the database?
            if (clientInfo == null)
                clientInfo = this.getByCompleteName(completeName.ToString());

            // The client does not exist:
            if (clientInfo == null)
            {
                clientInfo = new PLMClientsBusinessEntities.ClientInfo();

                clientInfo.FirstName = firstName.Trim().ToUpper();
                clientInfo.LastName = lastName.Trim().ToUpper();
                clientInfo.SecondLastName = slastName.Trim().ToUpper();
                clientInfo.CompleteName = completeName.ToString().ToUpper();
                clientInfo.Gender = gender.ToString().ToUpper()[0];
                clientInfo.Email = email.Trim().ToLower();
                clientInfo.Active = true;

                if (birthday != null)
                    clientInfo.Birthday = birthday;

                if (password != null)
                    clientInfo.Password = password;

                if (countryId != null)
                    clientInfo.CountryId = countryId;

                if (stateId != null)
                    clientInfo.StateId = stateId;

                if (age != null)
                    clientInfo.Age = age;

                if (zipCode != null)
                    clientInfo.ZipCode = zipCode;

                if (mobile != null)
                    clientInfo.Mobile = mobile;
                
                clientInfo.EntrySourceId = (int)source;

                // Add client to the database:
                this.addClient(clientInfo);
            }
            else
            {
                // The client exists, take arguments to be updated:
                clientInfo.FirstName = firstName.Trim().ToUpper();
                clientInfo.LastName = lastName.Trim().ToUpper();
                clientInfo.SecondLastName = slastName.Trim().ToUpper();
                clientInfo.CompleteName = completeName.ToString().ToUpper();
                clientInfo.Gender = gender;
                clientInfo.Email = email;
                clientInfo.Active = true;

                if (birthday != null)
                    clientInfo.Birthday = birthday;

                if (password != null)
                    clientInfo.Password = password;

                if (countryId != null)
                    clientInfo.CountryId = countryId;

                if (stateId != null)
                    clientInfo.StateId = stateId;

                if (age != null)
                    clientInfo.Age = age;

                if (zipCode != null)
                    clientInfo.ZipCode = zipCode;

                if (mobile != null)
                    clientInfo.Mobile = mobile;

                // Update client:
                this.updateClient(clientInfo);

                // Add new client source:
                PLMClientsBusinessEntities.ClientSourceInfo clientSrcInfo = new PLMClientsBusinessEntities.ClientSourceInfo();
                clientSrcInfo.ClientId = clientInfo.ClientId;
                clientSrcInfo.EntrySourceId = (int)source;

                // First, remove record from the database if it exists in the union table:
                ClientSourcesBLC.Instance.removeClientSource(clientSrcInfo);

                // Then, add new record to the union table:
                ClientSourcesBLC.Instance.addClientSource(clientSrcInfo);
            }

            return clientInfo.ClientId;
        }

        private int saveClientComplete(string firstName, string lastName, string slastName, char gender, DateTime? birthday, string email, string password,
            int? countryId, int? stateId, PLMClientsBusinessEntities.Catalogs.EntrySources source, string age, string zipCode,int locationId,int suburbId,int zipCodeId)
        {

            //Valid if the email is correct
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, System.Configuration.ConfigurationManager.AppSettings["RegExpression"]))
                throw new Exception("El correo electrónico no tiene el formato correcto.");

            // Does the email client exists in the database?
            PLMClientsBusinessEntities.ClientInfo clientInfo = this.getCompleteByEmail(email.Trim());

            // Get complete name:
            StringBuilder completeName = new StringBuilder((firstName.Trim() + " " + lastName.Trim() + " " + slastName.Trim()).Trim());

            // The email variable did not mathc in the database, does client complete name exist in the database?
            if (clientInfo == null)
                clientInfo = this.getByCompleteName(completeName.ToString());

            // The client does not exist:
            if (clientInfo == null)
            {
                clientInfo = new PLMClientsBusinessEntities.ClientInfo();

                clientInfo.FirstName = firstName.Trim().ToUpper();
                clientInfo.LastName = lastName.Trim().ToUpper();
                clientInfo.SecondLastName = slastName.Trim().ToUpper();
                clientInfo.CompleteName = completeName.ToString().ToUpper();
                clientInfo.Gender = gender.ToString().ToUpper()[0];
                clientInfo.Email = email.Trim().ToLower();
                clientInfo.Active = true;

                if (birthday != null)
                    clientInfo.Birthday = birthday;

                if (password != null)
                    clientInfo.Password = password;

                if (countryId != null)
                    clientInfo.CountryId = countryId;

                if (stateId != null)
                    clientInfo.StateId = stateId;

                if (age != null)
                    clientInfo.Age = age;

                if (zipCode != null)
                    clientInfo.ZipCode = zipCode;

                if (zipCodeId != null && locationId != null && suburbId != null)
                {
                    clientInfo.ZipCodeId = zipCodeId;
                    clientInfo.LocationId = locationId;
                    clientInfo.SuburbId = suburbId;
                }

                

                clientInfo.EntrySourceId = (int)source;

                // Add client to the database:
                this.addClientComplete(clientInfo);
            }
            else
            {
                // The client exists, take arguments to be updated:
                clientInfo.FirstName = firstName.Trim().ToUpper();
                clientInfo.LastName = lastName.Trim().ToUpper();
                clientInfo.SecondLastName = slastName.Trim().ToUpper();
                clientInfo.CompleteName = completeName.ToString().ToUpper();
                clientInfo.Gender = gender;
                clientInfo.Email = email;
                clientInfo.Active = true;

                if (birthday != null)
                    clientInfo.Birthday = birthday;

                if (password != null)
                    clientInfo.Password = password;

                if (countryId != null)
                    clientInfo.CountryId = countryId;

                if (stateId != null)
                    clientInfo.StateId = stateId;

                if (age != null)
                    clientInfo.Age = age;

                if (zipCode != null)
                    clientInfo.ZipCode = zipCode;

                if (zipCodeId != null && locationId != null && suburbId != null)
                {
                    clientInfo.ZipCodeId = zipCodeId;
                    clientInfo.LocationId = locationId;
                    clientInfo.SuburbId = suburbId;
                }

                // Update client:
                this.updateClientComplete(clientInfo);

                // Add new client source:
                PLMClientsBusinessEntities.ClientSourceInfo clientSrcInfo = new PLMClientsBusinessEntities.ClientSourceInfo();
                clientSrcInfo.ClientId = clientInfo.ClientId;
                clientSrcInfo.EntrySourceId = (int)source;

                // First, remove record from the database if it exists in the union table:
                ClientSourcesBLC.Instance.removeClientSource(clientSrcInfo);

                // Then, add new record to the union table:
                ClientSourcesBLC.Instance.addClientSource(clientSrcInfo);
            }

            return clientInfo.ClientId;
        }

        private void updateClient(PLMClientsBusinessEntities.ClientInfo clientInfo, string firstName, string lastName, string slastName, char gender, DateTime? birthday,
            string email, string password, int? countryId, int? stateId, string age, string zipCode)
        {
            // Get complete name:
            StringBuilder completeName = new StringBuilder((firstName.Trim() + " " + lastName.Trim() + " " + slastName.Trim()).Trim());

            // Set new values to properties:
            clientInfo.FirstName = firstName.Trim().ToUpper();
            clientInfo.LastName = lastName.Trim().ToUpper();
            clientInfo.SecondLastName = slastName.Trim().ToUpper();
            clientInfo.CompleteName = completeName.ToString().ToUpper();
            clientInfo.Gender = gender.ToString().ToUpper()[0];
            clientInfo.Email = email.Trim().ToLower();
            clientInfo.Active = true;

            if (birthday != null)
                clientInfo.Birthday = birthday;

            if (password != null)
                clientInfo.Password = password;

            if (countryId != null)
                clientInfo.CountryId = countryId;

            if (stateId != null)
                clientInfo.StateId = stateId;

            if (age != null)
                clientInfo.Age = age;

            if (zipCode != null)
                clientInfo.ZipCode = zipCode;

            // Update client information:
            this.updateClient(clientInfo);
        }

        private void updateClientComplete(PLMClientsBusinessEntities.ClientInfo clientInfo, string firstName, string lastName, string slastName, char gender, DateTime? birthday,
            string email, string password, int? countryId, int? stateId, string age, string zipCode,int locationId,int suburbId,int zipCodeId)
        {
            // Get complete name:
            StringBuilder completeName = new StringBuilder((firstName.Trim() + " " + lastName.Trim() + " " + slastName.Trim()).Trim());

            // Set new values to properties:
            clientInfo.FirstName = firstName.Trim().ToUpper();
            clientInfo.LastName = lastName.Trim().ToUpper();
            clientInfo.SecondLastName = slastName.Trim().ToUpper();
            clientInfo.CompleteName = completeName.ToString().ToUpper();
            clientInfo.Gender = gender.ToString().ToUpper()[0];
            clientInfo.Email = email.Trim().ToLower();
            clientInfo.Active = true;

            if (birthday != null)
                clientInfo.Birthday = birthday;

            if (password != null)
                clientInfo.Password = password;

            if (countryId != null)
                clientInfo.CountryId = countryId;

            if (stateId != null)
                clientInfo.StateId = stateId;

            if (age != null)
                clientInfo.Age = age;

            if (zipCode != null)
                clientInfo.ZipCode = zipCode;

            if (zipCodeId != null && locationId != null && suburbId != null)
            {
                clientInfo.ZipCodeId = zipCodeId;
                clientInfo.LocationId = locationId;
                clientInfo.SuburbId = suburbId;
            }
            



            // Update client information:
            this.updateClientComplete(clientInfo);
        }

        private void saveProfession(int clientId, PLMClientsBusinessEntities.Catalogs.Professions profession, string otherProfession, string professionalLicense)
        {
            // Does profession exist in the database?
            PLMClientsBusinessEntities.ProfessionClientInfo ProfClientInfo = ProfessionClientsBLC.Instance.getProfessionByClient(clientId);

            // A profession exists in the database, delete it:
            if (ProfClientInfo != null)
                PLMClientsDataAccessComponent.ProfessionClientsDALC.Instance.delete(ProfClientInfo);

            // Add the new Profession to the database:
            ProfClientInfo = new PLMClientsBusinessEntities.ProfessionClientInfo();

            // Add new information:
            ProfClientInfo.ClientId = clientId;
            ProfClientInfo.ProfessionId = (short)profession;
            ProfClientInfo.OtherProfession = profession == PLMClientsBusinessEntities.Catalogs.Professions.OTRA && otherProfession != null ? otherProfession.Trim().ToUpper() : null;
            ProfClientInfo.ProfessionalLicense = string.IsNullOrEmpty(professionalLicense) || string.IsNullOrWhiteSpace(professionalLicense) ? null : professionalLicense.Trim().ToUpper();

            // Insert to the database:
            PLMClientsDataAccessComponent.ProfessionClientsDALC.Instance.insert(ProfClientInfo);
        }

        private void saveSpeciality(int clientId, PLMClientsBusinessEntities.Catalogs.Professions profession, PLMClientsBusinessEntities.Catalogs.Specialities? speciality, string specialityName)
        {
            // Delete all Residences stored in the database.
            PLMClientsDataAccessComponent.ResidenceClientsDALC.Instance.delete(clientId);

            // Delete all specialities stored in the database. There must be only one speciality by client:
            PLMClientsDataAccessComponent.SpecialityClientsDALC.Instance.delete(clientId);

            // If the client is medic, then save speciality into the database:
            if (speciality != null && (profession == PLMClientsBusinessEntities.Catalogs.Professions.MEDICO ||
                                            profession == PLMClientsBusinessEntities.Catalogs.Professions.MEDICO_VETERINARIO))
            {
                PLMClientsBusinessEntities.SpecialityInfo specInfo = null;

                // If user chose option OTRA verify if the SpecialityName exists in Database:
                if (speciality == PLMClientsBusinessEntities.Catalogs.Specialities.OTRA)
                    specInfo = PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getByName(specialityName.Trim());

                // If the given speciality does not exist in the database then store it in SpecialityClients.OtherSpeciality field:
                if (specInfo == null)
                {
                    specInfo = new PLMClientsBusinessEntities.SpecialityInfo();
                    specInfo.SpecialityId = (short)speciality;
                }

                // Add new speciality information:
                PLMClientsBusinessEntities.SpecialityClientInfo specClientInfo = new PLMClientsBusinessEntities.SpecialityClientInfo();
                specClientInfo.ClientId = clientId;
                specClientInfo.SpecialityId = specInfo.SpecialityId;
                specClientInfo.OtherSpeciality = specInfo.SpecialityId == (short)PLMClientsBusinessEntities.Catalogs.Specialities.OTRA ? specialityName : null;

                // Insert in the database:
                PLMClientsDataAccessComponent.SpecialityClientsDALC.Instance.insert(specClientInfo);
            }
        }

        private int saveUser(int clientId, string user, string password, string codePrefix)
        {
            // Get NickNamePrefixInfo object by codePrefix varible:
            PLMClientsBusinessEntities.NickNamePrefixInfo nickNamePrefixInfo = NickNamePrefixesBLC.Instance.getNickNamePrefix(codePrefix);

            // It is compulsory that the entry in the database exists in it:
            if (nickNamePrefixInfo == null)
                throw new Exception("There is not defined an entry in the database given the codePrefix variable");

            // Get UserInfo from database if it exists:
            PLMClientsBusinessEntities.UserInfo userInfo = UsersBLC.Instance.getByNickName(user.Trim());

            // The user does not exist in the database, then add to the database:
            if (userInfo == null)
            {
                userInfo = new PLMClientsBusinessEntities.UserInfo();

                // Fill UserInfo properties:
                userInfo.NickPrefixId = nickNamePrefixInfo.NickPrefixId;
                userInfo.NickName = user.Trim();
                userInfo.Password = new PLMCryptographyComponent.CryptographyComponent().encrypt(password);
                userInfo.Active = true;

                // Add to the database:
                UsersBLC.Instance.addUser(userInfo);
            }
            else
            {
                // Fill UserInfo properties:
                userInfo.NickPrefixId = nickNamePrefixInfo.NickPrefixId;
                userInfo.NickName = user.Trim();

                // If password variable is not null or empty string, save into the database:
                if (!string.IsNullOrEmpty(password.Trim()))
                    userInfo.Password = new PLMCryptographyComponent.CryptographyComponent().encrypt(password);

                userInfo.Active = true;

                // Update to the database:
                UsersBLC.Instance.updateUser(userInfo);
            }

            // Increment the counter:
            ++nickNamePrefixInfo.CurrentNumber;
            NickNamePrefixesBLC.Instance.updateNickPrefix(nickNamePrefixInfo);

            // Update NickNamePrefixInfo.CurrentNumber increment in the database
            NickNamePrefixesBLC.Instance.updateNickPrefix(nickNamePrefixInfo);

            // Associate the user which has just been created or retrieved from the database, to the client:
            PLMClientsBusinessEntities.ClientUserInfo clientUserInfo = new PLMClientsBusinessEntities.ClientUserInfo();
            clientUserInfo.ClientId = clientId;
            clientUserInfo.UserId = userInfo.UserId;

            // If the association exists in the database, delete it:
            ClientUsersBLC.Instance.removeUserToClient(clientUserInfo);

            // Update the association in the database:
            ClientUsersBLC.Instance.addUserToClient(clientUserInfo);

            return userInfo.UserId;
        }

        private PLMClientsBusinessEntities.CodeInfo createCode(int clientId, string codePrefix)
        {
            // Get CodePrefixInfo object by codePrefix varible:
            PLMClientsBusinessEntities.CodePrefixInfo codePrefixInfo = CodePrefixesBLC.Instance.getCodePrefix(codePrefix);

            // Create the new code:
            string codeString = new PLMCryptographyComponent.CryptographyComponent().addHash(codePrefixInfo.PrefixValue.Trim() + codePrefixInfo.CurrentValue.ToString());

            // Add new code to the database as inactive and not assign:
            PLMClientsBusinessEntities.CodeInfo codeInfo = new PLMClientsBusinessEntities.CodeInfo();
            codeInfo.PrefixId = codePrefixInfo.PrefixId;
            codeInfo.CodeString = codeString;
            codeInfo.CodeStatusId = (byte)PLMClientsBusinessEntities.Catalogs.CodeStatus.INACTIVO;
            codeInfo.Assign = false;

            // Add Code to the database:
            CodesBLC.Instance.addCode(codeInfo);

            // Increment the counter:
            ++codePrefixInfo.CurrentValue;

            // Update the increment in the database:
            CodePrefixesBLC.Instance.updateCode(codePrefixInfo);

            // Add the create transaction:
            PLMClientsBusinessEntities.CodeTransactionInfo codeTranInfo = new PLMClientsBusinessEntities.CodeTransactionInfo();
            codeTranInfo.CodeId = codeInfo.CodeId;
            codeTranInfo.TransactionId = (byte)PLMClientsBusinessEntities.Catalogs.CodeTransactions.CREAR;

            // Add the create tran to the database:
            CodeTransactionsBLC.Instance.addTransaction(codeTranInfo);

            //// Associate code to the user, then activate it:
            //PLMClientsBusinessEntities.UserCodeInfo usrCodeInfo = new PLMClientsBusinessEntities.UserCodeInfo();
            //usrCodeInfo.CodeId = codeInfo.CodeId;
            //usrCodeInfo.UserId = userId;
            //usrCodeInfo.InitialDate = DateTime.Now;
            //usrCodeInfo.FinalDate = DateTime.Now.AddYears(1);

            // Associate code to the user, then activate it:
            PLMClientsBusinessEntities.ClientCodesInfo clientCodeInfo = new PLMClientsBusinessEntities.ClientCodesInfo();
            clientCodeInfo.ClientId = clientId;
            clientCodeInfo.CodeId = codeInfo.CodeId;

            // Add user and code association to the database:
            //UserCodesBLC.Instance.addCodeToUser(usrCodeInfo);
            ClientCodesBLC.Instance.insert(clientCodeInfo);

            // Activate the new code which was created:
            codeInfo.Assign = true;
            codeInfo.CodeStatusId = (byte)PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO;

            // Update the new code in the database:
            CodesBLC.Instance.updateCodes(codeInfo);

            // Add activate tran:
            codeTranInfo.TransactionId = (byte)PLMClientsBusinessEntities.Catalogs.CodeTransactions.ACTIVAR;

            // Add activate tran to the database:
            CodeTransactionsBLC.Instance.addTransaction(codeTranInfo);

            //Return the code has been created:
            return codeInfo;
        }

        private void saveOSMobileUserCode(int userId, int codeId, PLMClientsBusinessEntities.Catalogs.OSMobileDevices osMobileId, string IMEI)
        {
            // Create the Business Entity container:
            PLMClientsBusinessEntities.OSMobileUserCodesInfo osMobileUsercodeInfo = new PLMClientsBusinessEntities.OSMobileUserCodesInfo();

            osMobileUsercodeInfo.OSMobileId = (byte)osMobileId;
            osMobileUsercodeInfo.UserId = userId;
            osMobileUsercodeInfo.CodeId = codeId;
            osMobileUsercodeInfo.IMEI = IMEI.ToUpper().Trim();

            // Insert the entry into the database:
            PLMClientsDataAccessComponent.OSMobileUserCodesDALC.Instance.insert(osMobileUsercodeInfo);
        }

        private void saveOSMobileUserCode(int userId, int codeId, PLMClientsBusinessEntities.Catalogs.OSMobileDevices osMobileId, string IMEI, string stateKey)
        {

            // Create the Business Entity container:
            PLMClientsBusinessEntities.OSMobileUserCodesInfo osMobileUsercodeInfo = new PLMClientsBusinessEntities.OSMobileUserCodesInfo();

            osMobileUsercodeInfo.OSMobileId = (byte)osMobileId;
            osMobileUsercodeInfo.UserId = userId;
            osMobileUsercodeInfo.CodeId = codeId;
            osMobileUsercodeInfo.IMEI = IMEI.ToUpper().Trim();

            // Insert the entry into the database:
            PLMClientsDataAccessComponent.OSMobileUserCodesDALC.Instance.insert(osMobileUsercodeInfo);

            this.associateState(osMobileUsercodeInfo.OSMobileId, osMobileUsercodeInfo.CodeId, osMobileUsercodeInfo.UserId, stateKey);

        }

        private PLMClientsBusinessEntities.ClientInfo convertClient(PLMClientsBusinessEntities.MobileClientInfo mClientInfo)
        {
            PLMClientsBusinessEntities.ClientInfo clientInfo = new PLMClientsBusinessEntities.ClientInfo();

            clientInfo.ClientId = mClientInfo.ClientId;
            clientInfo.EntrySourceId = mClientInfo.EntrySourceId;
            clientInfo.FirstName = mClientInfo.FirstName;
            clientInfo.LastName = mClientInfo.LastName;
            clientInfo.SecondLastName = mClientInfo.SecondLastName;
            clientInfo.CompleteName = mClientInfo.CompleteName;
            clientInfo.Email = mClientInfo.Email;
            clientInfo.Gender = mClientInfo.Gender;
            clientInfo.Birthday = mClientInfo.Birthday;
            clientInfo.AddedDate = mClientInfo.AddedDate;
            clientInfo.LastUpdate = mClientInfo.LastUpdate;
            clientInfo.Active = mClientInfo.Active;

            return clientInfo;
        }

        private string cleanMacAddress(string macAddresses)
        {
            string[] macAddsArray = macAddresses.Trim().Split(';');

            StringBuilder macAdds = new StringBuilder();

            for (int x = 0; x < macAddsArray.Length; x++)
            {
                if (!macAddsArray[x].Equals("0000000000000000") && !string.IsNullOrEmpty(macAddsArray[x].Trim()))
                {
                    macAdds.Append(macAddsArray[x] + ";");
                }
            }

            return macAdds.ToString();


        }

        private void saveMacAddress(int clientId, int codeId, string macAddresses)
        {
            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient = new PLMClientsBusinessEntities.TargetClientCodesInfo();

            targetClient.ClientId = clientId;
            targetClient.CodeId = codeId;
            targetClient.TargetId = Convert.ToByte(PLMClientsBusinessEntities.Catalogs.TargetOutputs.Cliente_Servidor);
            targetClient.DeviceId = Convert.ToByte(PLMClientsBusinessEntities.Catalogs.DeviceIdentifiers.NIC);
            targetClient.HWIdentifier = macAddresses;

            // First remove from database:
            TargetClientCodesBLC.Instance.removeCodeToTargetOutput(targetClient);
            
            // Then add to the database:
            TargetClientCodesBLC.Instance.addCodeToTargetOutput(targetClient);
        }

        private bool validCodePrefix(string codeString, string codePrefix)
        {
            //Gets the code information.
            PLMClientsBusinessEntities.CodeInfo code = CodesBLC.Instance.getCode(codeString);

            //Gets the codeprefix information.
            PLMClientsBusinessEntities.CodePrefixInfo codePre = CodePrefixesBLC.Instance.getCodePrefix(codePrefix);

            return code.PrefixId == codePre.PrefixId;

        }

        private PLMClientsBusinessEntities.CodeInfo associateCode(int clientId, int codeId)
        {
            // Associate code to the user, then activate it:
            PLMClientsBusinessEntities.ClientCodesInfo clientCodeInfo = new PLMClientsBusinessEntities.ClientCodesInfo();
            clientCodeInfo.CodeId = codeId;
            clientCodeInfo.ClientId = clientId;

            PLMClientsDataAccessComponent.ClientCodesDALC.Instance.insert(clientCodeInfo);

            PLMClientsBusinessEntities.CodeInfo codeInfo = PLMClientsDataAccessComponent.CodesDALC.Instance.getOne(codeId);

            // Activate the code:
            codeInfo.Assign = true;
            codeInfo.CodeStatusId = (byte)PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO;

            // Update the code in the database:
            CodesBLC.Instance.updateCodes(codeInfo);

            // Add activate tran:
            PLMClientsBusinessEntities.CodeTransactionInfo codeTranInfo = new PLMClientsBusinessEntities.CodeTransactionInfo();
            codeTranInfo.CodeId = codeInfo.CodeId;
            codeTranInfo.TransactionId = (byte)PLMClientsBusinessEntities.Catalogs.CodeTransactions.ACTIVAR;

            // Add activate tran to the database:
            if (CodeTransactionsBLC.Instance.getTransaction(codeTranInfo) == null)
            {
                CodeTransactionsBLC.Instance.addTransaction(codeTranInfo);
            }

            //Return the code has been created:
            return codeInfo;
        }

        private void associateState(byte osMobileId, int codeId, int userId, string stateKey)
        {
            // Check if state exist.
            PLMClientsBusinessEntities.StateInfo state = CatalogsBLC.Instance.getStateByShortName(stateKey);

            if (state == null)
                throw new ArgumentException("El nombre del estado es incorrecto.");

            // Create the Business Entity container:
            PLMClientsBusinessEntities.StateOSMobileUserCodeInfo stateOSMobileUserCodeInfo =
                StateOSMobileUserCodesBLC.Instance.getMobile(osMobileId,
                    codeId, userId);

            if (stateOSMobileUserCodeInfo == null)
                stateOSMobileUserCodeInfo = new PLMClientsBusinessEntities.StateOSMobileUserCodeInfo();
            else
                StateOSMobileUserCodesBLC.Instance.removeMobileToState(stateOSMobileUserCodeInfo);

            stateOSMobileUserCodeInfo.StateId = state.StateId;
            stateOSMobileUserCodeInfo.OSMobileId = osMobileId;
            stateOSMobileUserCodeInfo.CodeId = codeId;
            stateOSMobileUserCodeInfo.UserId = userId;

            StateOSMobileUserCodesBLC.Instance.addMobileToState(stateOSMobileUserCodeInfo);
        }

        #endregion

        public static readonly ClientsBLC Instance = new ClientsBLC();

    }
}
