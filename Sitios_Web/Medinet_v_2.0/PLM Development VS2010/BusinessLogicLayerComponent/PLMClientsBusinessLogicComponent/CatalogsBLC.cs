using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Xml;
namespace PLMClientsBusinessLogicComponent
{
    public class CatalogsBLC
    {
        #region Constructors

        private CatalogsBLC() { }

        #endregion

        #region Public Methods

        #region Countries

        public List<PLMClientsBusinessEntities.CountryInfo> getCountries()
        {
            List<PLMClientsBusinessEntities.CountryInfo> collection = PLMClientsDataAccessComponent.CountriesDALC.Instance.getAll();

            PLMClientsBusinessEntities.CountryInfo record = new PLMClientsBusinessEntities.CountryInfo();

            record.CountryId = (byte)0;
            record.CountryName = "SELECCIONAR";

            collection.Insert(0, record);

            return collection;
        }

        public PLMClientsBusinessEntities.CountryInfo getCountry(int pk)
        {
            if (pk <= 0)
                throw new ArgumentException("The Country does not exist");

            return PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne(pk);
        }

        public PLMClientsBusinessEntities.CountryInfo getCountry(string country)
        {
            return PLMClientsDataAccessComponent.CountriesDALC.Instance.getByCode(country);
        }

        #endregion

        #region States

        public List<PLMClientsBusinessEntities.StateInfo> getStates()
        {
            return PLMClientsDataAccessComponent.StatesDALC.Instance.getAll();
        }

        public List<PLMClientsBusinessEntities.StateInfo> getStatesByCountry(byte countryId)
        {
            if (countryId == 0)
                throw new ArgumentException("The country does not exist.");

            return PLMClientsDataAccessComponent.StatesDALC.Instance.getByCountry(countryId);
        }

        public List<PLMClientsBusinessEntities.StateInfo> getStatesByCountryByName(byte countryId, string prefix)
        {
           return PLMClientsDataAccessComponent.StatesDALC.Instance.getByCountryByName(countryId,prefix);
        }

        public PLMClientsBusinessEntities.StateInfo getStateByCountryByName(byte countryId, string stateName)
        {
            if (countryId == 0)
                throw new ArgumentException("The country does not exist.");

            return PLMClientsDataAccessComponent.StatesDALC.Instance.getByName(countryId, stateName);
        }

        public PLMClientsBusinessEntities.StateInfo getStateByShortName(string shortName)
        {
            return PLMClientsDataAccessComponent.StatesDALC.Instance.getByShortName(shortName);
        }

        public PLMClientsBusinessEntities.StateInfo getState(int stateId)
        {
            if (stateId <= 0)
                throw new ArgumentException("The state does not exist.");

            return PLMClientsDataAccessComponent.StatesDALC.Instance.getOne(stateId);
        }

        #endregion

        private string getFileContent(string path)
        {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8);

            string fileContent = sr.ReadToEnd();
            sr.Close();

            return fileContent;
        }

        public void sendLicenseByEmail(PLMClientsBusinessEntities.LicensesInfo licenseInfo, PLMClientsBusinessEntities.ClientInfo clientInfo ,string link, string fileName, string senderHTML, string passwordHTML)
        {
            string htmlContent = this.getFileContent(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\HtmlTemplates\\" + fileName + "");
            htmlContent = htmlContent.Replace("@@@CompletaName@@@", clientInfo.CompleteName);
            htmlContent = htmlContent.Replace("@@@licencia@@@", licenseInfo.LicenseKey);

            PLMClientsBusinessEntities.CodeInfo code = CodesBLC.Instance.getCodeByLicenseKey(licenseInfo.LicenseKey);

            string url = MetadataVersionBLC.Instance.getUrlVersion(code.CodeString);
           // string url = "http://www.plmconnection.com/plmservices/prescripciontotal/mexico/suscripciones/update.xml";
            
            if (fileName == "sendLicenseByDownloadPT.htm")
            {
                XmlDocument archivoxml = new XmlDocument();

                archivoxml.Load(url);

                XmlNodeList trabajadores = archivoxml.GetElementsByTagName("update");

                XmlNodeList dmg = archivoxml.GetElementsByTagName("dmg");

                foreach (XmlElement trab in dmg)
                {
                    XmlNodeList dmgXml = trab.GetElementsByTagName("url");
                    htmlContent = htmlContent.Replace("@@@linkMac@@@", dmgXml[0].InnerText);
                }
                XmlNodeList exe = archivoxml.GetElementsByTagName("exe");

                foreach (XmlElement trab in exe)
                {
                    XmlNodeList salario = trab.GetElementsByTagName("url");
                    htmlContent = htmlContent.Replace("@@@link@@@", salario[0].InnerText);
                }
            }
            else
                htmlContent = htmlContent.Replace("@@@link@@@", "");

            htmlContent = htmlContent.Replace("@@@email@@@", "descargaplm@plmlatina.com");

            bool sendaMail = this._email.sendHTMLMail(clientInfo.Email, "PLM", "Licencia", htmlContent, true,senderHTML,passwordHTML);
            
            if (sendaMail == false)
            {
                new ExecutionEngineException("Error al enviar el mensaje");
            }
        }

        public List<PLMClientsBusinessEntities.SpecialityInfo> getSpecialities()
        {
            return PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getAll();
        }

        public List<PLMClientsBusinessEntities.SpecialityInfo> getSpecialities(short professionId)
        {
            if(professionId <= 0)
                throw new ArgumentException("The profession does not exist.");

            List<PLMClientsBusinessEntities.SpecialityInfo> collection = PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getByProfessionId(professionId);

            PLMClientsBusinessEntities.SpecialityInfo record = new PLMClientsBusinessEntities.SpecialityInfo();

            record.SpecialityId = -1;
            record.SpecialityName = "SELECCIONAR";

            collection.Insert(0,record);

            return collection;
        }

        public List<PLMClientsBusinessEntities.ProfessionInfo> getProfessions()
        {
            List<PLMClientsBusinessEntities.ProfessionInfo> collection = PLMClientsDataAccessComponent.ProfessionsDALC.Instance.getAll();

            PLMClientsBusinessEntities.ProfessionInfo record = new PLMClientsBusinessEntities.ProfessionInfo();

            record.ProfessionId = -1;
            record.ProfessionName = "SELECCIONAR";

            collection.Insert(0, record);

            return collection;
        }

        public PLMClientsBusinessEntities.ProfessionInfo getProfession(short professionId)
        {
            return PLMClientsDataAccessComponent.ProfessionsDALC.Instance.getOne(professionId);
        }

        public List<PLMClientsBusinessEntities.ProfessionalPracticeInfo> getPractices()
        {
            return PLMClientsDataAccessComponent.ProfessionalPracticesDALC.Instance.getAll();
        }
        
        public List<PLMClientsBusinessEntities.JobPlaceInfo> getJobPlaces()
        {
            return PLMClientsDataAccessComponent.JobPlacesDALC.Instance.getAll();
        }

        public PLMClientsBusinessEntities.MessageInfo getMessage(int messageId)
        {
            if (messageId <= 0)
                throw new ArgumentException("The message does not exist.");

            return PLMClientsDataAccessComponent.MessagesDALC.Instance.getOne(messageId);
        }

        public PLMClientsBusinessEntities.MessageInfo getMessageByCode(int messageCode)
        {
            if (messageCode == 0)
                throw new ArgumentException("The message does not exist.");

            return PLMClientsDataAccessComponent.MessagesDALC.Instance.getByCode(messageCode);
        }

        public PLMClientsBusinessEntities.EditionInfo getEdition(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("The user does not exist.");

            return PLMClientsDataAccessComponent.EditionsDALC.Instance.getByUserId(userId);
        }

        public PLMClientsBusinessEntities.EditionInfo getEdition(string code)
        {
            return PLMClientsDataAccessComponent.EditionsDALC.Instance.getByCode(code);
        }

        public PLMClientsBusinessEntities.ContactInfo getContactByCountry(string country)
        {
            if (country == null || country == "" || country.Length > 3)
                throw new ArgumentException("The Country ID does not exist.");

            return PLMInfoDataAccessComponent.ContactDALC.Instance.getContactInfoByCountry(country);
        }

        public PLMClientsBusinessEntities.TermsInfo getTerms()
        {
            PLMClientsBusinessEntities.TermsInfo terms = new PLMClientsBusinessEntities.TermsInfo();
            terms.TextFile = System.Configuration.ConfigurationManager.AppSettings["TermsTxt"];
            terms.PdfFile = System.Configuration.ConfigurationManager.AppSettings["TermsPDF"];

            return terms;
        }

        #endregion

        public static readonly CatalogsBLC Instance = new CatalogsBLC();

        private PLMEmailComponent.EmailComponent _email = new PLMEmailComponent.EmailComponent();
    }
}
