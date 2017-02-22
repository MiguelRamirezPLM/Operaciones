using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuiaBusinessEntries;
using System.Data;
using System.Data.Common;
namespace GuiaDataAccessComponent
{
    public class Queries : GuiaDataAccessAdapter<CountryInfo>
    {
        #region Constructors

        private Queries() { }
        
        #endregion

        #region Countries

        public CountryInfo getCountryById(byte countryId)
        {
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountryById");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    CountryInfo record = new CountryInfo();

                    record.CountryId = Convert.ToByte(dataReader["CountryId"]);
                    record.CountryName = dataReader["CountryName"].ToString();
                    record.ID = dataReader["ID"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    return record;
                }
                else{
                    return null;
                }
            }
            /*
            GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var countryVar = from countryRows in db.Countries
                             where countryRows.Active == true && countryRows.CountryId == countryId
                             select countryRows;

            List<Countries> country = countryVar.ToList();

            CountryInfo countryInfo = new CountryInfo();

            countryInfo.CountryId = country[0].CountryId;
            countryInfo.CountryName = country[0].CountryName;
            countryInfo.CountryCode = country[0].CountryCode;
            countryInfo.ID = country[0].ID;
            countryInfo.Active = country[0].Active;

            return countryInfo;*/
        }
        
        #endregion

        #region States

        public StateInfo getStateById(int stateId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var stateVar = from stateRows in db.States
                           where stateRows.Active == true && stateRows.StateId == stateId
                           select stateRows;

            List<States> state = stateVar.ToList();

            StateInfo stateInfo = new StateInfo();

            stateInfo.StateId = state[0].StateId;
            stateInfo.CountryId = state[0].CountryId;
            stateInfo.StateName = state[0].StateName;
            stateInfo.Active = state[0].Active;

            return stateInfo;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStateById");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    StateInfo record = new StateInfo();

                    record.CountryId = Convert.ToByte(dataReader["CountryId"]);
                    record.StateId = Convert.ToInt32(dataReader["StateId"]);
                    record.StateName = dataReader["StateName"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    return record;
                }
                else
                {
                    return null;
                }
            }


        }

        #endregion

        #region Drugs

        public List<DrugInfo> getDrugsByClientId(int clientId)
        {
            /* 
            GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var drugVar = from drugRows in db.roc_spGetDrugsByClientId(clientId)
                          select new GuiaBusinessEntries.DrugInfo
                          {
                              DrugId = drugRows.DrugId,
                              ClientId = drugRows.ClientId,
                              DrugName = drugRows.DrugName,
                              ActiveSubstance = drugRows.ActiveSubstance,
                              PharmaceuticForm = drugRows.PharmaceuticForm,
                              Presentación = drugRows.Presentation,
                              Active =drugRows.Active
                          };

            List<DrugInfo> drugInformation = drugVar.ToList();

            return drugInformation.Count() > 0 ? drugInformation : null;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDrugsByClientId");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            List<DrugInfo> drugInformation = new List<DrugInfo>();
            DrugInfo record;
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new DrugInfo();

                    record.DrugId = Convert.ToInt32(dataReader["DrugId"]);
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.DrugName = dataReader["DrugName"].ToString();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.PharmaceuticForm = dataReader["PharmaForm"].ToString();
                    record.Presentación = dataReader["Presentation"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    drugInformation.Add(record);

                }
                
            }
            return drugInformation;

        }
        

        #endregion

        #region Clients

        public List<ClientTypeInfo> getClientTypes()
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientTypeVar = from clientTypeRows in db.ClientTypes
                                orderby clientTypeRows.ClientTypeId
                                select clientTypeRows;

            List<ClientTypes> clientTypes = clientTypeVar.ToList();

            List<ClientTypeInfo> clientTypesInf = new List<ClientTypeInfo>();

            foreach (ClientTypes client in clientTypes)
            {
                ClientTypeInfo cliType = new ClientTypeInfo();

                cliType.ClientTypeId = client.ClientTypeId;
                cliType.TypeName = client.TypeName;
                cliType.Active = client.Active;
                
                clientTypesInf.Add(cliType);
            }

            return clientTypesInf;*/
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientTypes");
            
            List<ClientTypeInfo> clientTypesInf = new List<ClientTypeInfo>();
            ClientTypeInfo record;

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new ClientTypeInfo();

                    record.ClientTypeId = Convert.ToByte(dataReader["ClientTypeId"]);
                    record.TypeName= dataReader["TypeName"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    clientTypesInf.Add(record);
                }
                
            }

            return clientTypesInf;
        }

        public ClientInfo getClientById(int clientId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientVar = from clientRows in db.Clients
                            where clientRows.ClientId == clientId
                            select clientRows;

            //List<Clients> client = clientVar.ToList();

            //return this.setClientInfo(client[0]);
            //List<Clients> client = 

            return this.setClientInfo(clientVar.ToList()[0]);
            */
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientById");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }


        }
        
        public int getNumberOfClientsByEditionByType(int editionId, byte clientTypeId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.Clients
                             where clientRows.Active == true && 
                             clientRows.EditionId == editionId && clientRows.ClientTypeId == clientTypeId
                             select clientRows.ClientId;

            return clientsVar.ToList().Count();
            */
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetNumberOfClientsByType");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientTypeId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return Convert.ToInt32(dataReader["Total"]);
                else
                    return 0;
            }

        }

        public List<ClientInfo> getClientsByEditionByType(int editionId, byte clientTypeId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.Clients
                             where clientRows.Active == true &&
                             clientRows.EditionId == editionId && clientRows.ClientTypeId == clientTypeId
                             orderby clientRows.CompanyName
                             select clientRows;

            List<Clients> clients = clientsVar.ToList();

            List<ClientInfo> companies = new List<ClientInfo>();

            foreach (Clients client in clients)
            {
                companies.Add(this.setClientInfo(client));
            }


            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByEditionByType");
            List<ClientInfo> clientList = new List<ClientInfo>();
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientTypeId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    clientList.Add(this.getFromDataReader(dataReader));
             }

            return clientList;

        }

        public List<ClientInfo> getClientsByName(int editionId, byte clientTypeId, string letter)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.Clients
                             where clientRows.Active == true && clientRows.CompanyName.StartsWith(letter) &&
                             clientRows.EditionId == editionId && clientRows.ClientTypeId == clientTypeId
                             orderby clientRows.CompanyName
                             select clientRows;

            List<Clients> clients = clientsVar.ToList();
            
            List<ClientInfo> companies = new List<ClientInfo>();

            foreach (Clients client in clients)
            {
                companies.Add(this.setClientInfo(client));
            }


            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByEditionByName");
            List<ClientInfo> clientList = new List<ClientInfo>();
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientTypeId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@letter", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, letter);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    clientList.Add(this.getFromDataReader(dataReader));
            }

            return clientList;


        }

        public List<ClientInfo> getClientsByText(int editionId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.Clients
                             where clientRows.Active == true && clientRows.CompanyName.Contains(text) &&
                             clientRows.EditionId == editionId && clientRows.ClientTypeId == clientTypeId
                             orderby clientRows.CompanyName
                             select clientRows;

            List<Clients> clients = clientsVar.ToList();

            List<ClientInfo> companies = new List<ClientInfo>();

            foreach (Clients client in clients)
            {
                companies.Add(this.setClientInfo(client));
            }


            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsByText");
            List<ClientInfo> clientList = new List<ClientInfo>();
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    clientList.Add(this.getFromDataReader(dataReader));
            }

            return clientList;

        }

        public List<ClientInfo> getClientsByTextByType(int editionId, byte clientTypeId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.Clients
                             where clientRows.Active == true && clientRows.CompanyName.Contains(text) &&
                             clientRows.EditionId == editionId && clientRows.ClientTypeId == clientTypeId
                             orderby clientRows.CompanyName
                             select clientRows;

            List<Clients> clients = clientsVar.ToList();

            List<ClientInfo> companies = new List<ClientInfo>();

            foreach (Clients client in clients)
            {
                companies.Add(this.setClientInfo(client));
            }


            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsByText");
            List<ClientInfo> clientList = new List<ClientInfo>();
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientTypeId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    clientList.Add(this.getFromDataReader(dataReader));
            }

            return clientList;

        }

        public List<PhoneTypeInfo> getPhoneTypes()
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var phoneTypeVar = from phoneTypeRows in db.PhoneTypes
                                orderby phoneTypeRows.PhoneTypeId
                                select phoneTypeRows;

            List<PhoneTypes> phoneTypes = phoneTypeVar.ToList();

            List<PhoneTypeInfo> phoneTypesInf = new List<PhoneTypeInfo>();

            foreach (PhoneTypes phone in phoneTypes)
            {
                PhoneTypeInfo phoType = new PhoneTypeInfo();

                phoType.PhoneTypeId = phone.PhoneTypeId;
                phoType.Description = phone.Description;
                phoType.Active = phone.Active;

                phoneTypesInf.Add(phoType);
            }

            return phoneTypesInf;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPhoneTypes");
            List<PhoneTypeInfo> PhoneTypeList = new List<PhoneTypeInfo>();
            PhoneTypeInfo phoneType;
            // Add the parameters:
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    phoneType = new PhoneTypeInfo();
                    phoneType.Active = Convert.ToBoolean(dataReader["Active"]);
                    phoneType.Description = dataReader["Description"].ToString();
                    phoneType.PhoneTypeId = Convert.ToByte(dataReader["PhoneTypeId"]);
                    PhoneTypeList.Add(phoneType);
                }
            }

            return PhoneTypeList;
        }

        public List<PhonesByClientInfo> getPhonesByClient(int clientId)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var phonesVar = (from clientPhoneRows in db.ClientPhones
                            join phoneTypeRows in db.PhoneTypes on clientPhoneRows.PhoneTypeId equals phoneTypeRows.PhoneTypeId
                            where clientPhoneRows.ClientId == clientId
                            select new PhonesByClient
                            {
                                PhoneTypeId = phoneTypeRows.PhoneTypeId,
                                Number = clientPhoneRows.Number,
                                Lada = clientPhoneRows.Lada,
                                PhoneType = phoneTypeRows.Description
                            }).Distinct();

            List<PhonesByClient> phones = phonesVar.ToList();

            List<PhonesByClientInfo> phoneClients = new List<PhonesByClientInfo>();

            foreach (PhonesByClient phone in phones)
            {
                PhonesByClientInfo tel = new PhonesByClientInfo();

                tel.PhoneTypeId = phone.PhoneTypeId;
                tel.Number = phone.Number;
                tel.Lada = phone.Lada;
                tel.PhoneType = phone.PhoneType;

                phoneClients.Add(tel);
            }

            return phoneClients;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPhonesByClient");
            List<PhonesByClientInfo> phonesList = new List<PhonesByClientInfo>();
            PhonesByClientInfo record;

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read()) 
                {
                    record = new PhonesByClientInfo();
                    record.PhoneTypeId = Convert.ToByte(dataReader["PhoneTypeId"]);
                    record.Number = dataReader["Number"].ToString();
                    
                    if (dataReader["Lada"]!=DBNull.Value)
                    record.Lada = dataReader["Lada"].ToString();

                    record.PhoneType = dataReader["PhoneType"].ToString();
                    phonesList.Add(record);
                
                }
                    
            }

            return phonesList;


        }

        public List<BrandDetailInfo> getBrandsByClient(int editionId, int clientId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var brandsVar = from brandsRows in db.plm_spGetBrandsByClient(editionId, clientId)
                            select new BrandDetailInfo
                            {
                                BrandId = brandsRows.BrandId,
                                BrandName = brandsRows.BrandName,
                                Logo = brandsRows.Logo,
                                BaseUrl = brandsRows.BaseUrl,
                                ClientId = brandsRows.ClientId,
                                EditionId = brandsRows.EditionId,
                                Active = brandsRows.Active
                            };

            List<BrandDetailInfo> brands = brandsVar.ToList();
            
            return brands;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBrandsByClient");
            List<BrandDetailInfo> BrandsList = new List<BrandDetailInfo>();
            BrandDetailInfo record;

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new BrandDetailInfo();
                    record.BrandId = Convert.ToInt32(dataReader["BrandId"]);
                    record.BrandName = dataReader["BrandName"].ToString();

                    if (dataReader["Logo"]!=DBNull.Value)
                    record.Logo= dataReader["Logo"].ToString();

                    if (dataReader["BaseUrl"] != DBNull.Value)
                    record.BaseUrl = dataReader["BaseUrl"].ToString();

                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.EditionId= Convert.ToInt32(dataReader["EditionId"]);
                    record.Active= Convert.ToBoolean(dataReader["Active"]);
                    BrandsList.Add(record);

                }
            }

            return BrandsList;
        }

        public List<DrugDetailInfo> getDrugs(int editionId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var drugsVar = from drugsRows in db.plm_spGetDrugs(editionId, text)
                           select new DrugDetailInfo
                            {
                                DrugId = drugsRows.DrugId,
                                DrugName = drugsRows.DrugName,
                                PharmaceuticForm = drugsRows.PharmaceuticForm,
                                ActiveSubstance = drugsRows.ActiveSubstance,
                                Presentation = drugsRows.Presentation,
                                ClientId = drugsRows.ClientId,
                                CompanyName = drugsRows.CompanyName
                            };

            List<DrugDetailInfo> drugs = drugsVar.ToList();

            return drugs;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDrugs");
            List<DrugDetailInfo> DrugList = new List<DrugDetailInfo>();
            DrugDetailInfo record;
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new DrugDetailInfo();
                    record.DrugId = Convert.ToInt32(dataReader["ProductId"]);
                    record.DrugName = dataReader["ProductName"].ToString();
                    record.PharmaceuticForm = dataReader["PharmaForm"].ToString();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.Presentation = dataReader["Description"].ToString();
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.CompanyName = dataReader["CompanyName"].ToString();
                    DrugList.Add(record);
                }
                 
            }

            return DrugList;

        }

        #endregion

        #region Editions

        public EditionInfo getEditionByISBN(string isbn)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var editionVar = from editionRows in db.Editions
                             where editionRows.ISBN.Equals(isbn)
                             select editionRows;


            List<Editions> edition = editionVar.ToList();

            EditionInfo editionInfo = new EditionInfo();

            editionInfo.EditionId = edition[0].EditionId;
            editionInfo.ParentId = edition[0].ParentId;
            editionInfo.NumberEdition = edition[0].NumberEdition;
            editionInfo.EditionCode = edition[0].EditionCode;
            editionInfo.BookId = edition[0].BookId;
            editionInfo.CountryId = edition[0].CountryId;
            editionInfo.BarCode = edition[0].BarCode;
            editionInfo.ISBN = edition[0].ISBN;
            editionInfo.Active = edition[0].Active;

            return editionInfo;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditionByISBN");
            EditionInfo editionInfo = new EditionInfo();
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read()) {
                    editionInfo.EditionId = Convert.ToInt32(dataReader["EditionId"]);

                    if (dataReader["ParentId"]!=DBNull.Value)
                    editionInfo.ParentId = Convert.ToInt32(dataReader["ParentId"]);

                    editionInfo.NumberEdition = Convert.ToInt32(dataReader["NumberEdition"]);

                    if (dataReader["EditionCode"] != DBNull.Value)
                    editionInfo.EditionCode = dataReader["EditionCode"].ToString();
                    
                    editionInfo.BookId = Convert.ToInt32(dataReader["BookId"]);
                    editionInfo.CountryId = Convert.ToByte(dataReader["CountryId"]);

                    if (dataReader["BarCode"] != DBNull.Value)
                    editionInfo.BarCode = dataReader["BarCode"].ToString();

                    if (dataReader["ISBN"] != DBNull.Value)
                    editionInfo.ISBN = dataReader["ISBN"].ToString();

                    editionInfo.Active = Convert.ToBoolean(dataReader["Active"]);
                }
            }

            return editionInfo;

        }

        #endregion

        #region Private Methods

        private ClientInfo setClientInfo(Clients client)
        {
            ClientInfo clientInfo = new ClientInfo();
            
            clientInfo.ClientId = client.ClientId;
            clientInfo.ClientIdParent = client.ClientIdParent;
            clientInfo.ClientTypeId = client.ClientTypeId;
            clientInfo.ClientCode = client.ClientCode;
            clientInfo.CompanyName = client.CompanyName;
            clientInfo.ShortName = client.ShortName;
            clientInfo.Address = client.Address;
            clientInfo.Suburb = client.Suburb;
            clientInfo.PostalCode = client.PostalCode;
            clientInfo.City = client.City;
            clientInfo.StateId = client.StateId;
            clientInfo.CountryId = client.CountryId;
            clientInfo.Email = client.Email;
            clientInfo.Web = client.Web;
            clientInfo.Products = client.Products;
            clientInfo.Image = client.Image;
            clientInfo.Page = client.Page;
            clientInfo.EditionId = client.EditionId;
            clientInfo.Active = client.Active;

            return clientInfo;
        }

        #endregion

        #region ROC

        public List<ClientsByEditionInfo> getClientsByTypeByEdition(int editionId, byte clientTypeId)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();


            var clientsVar = from clientRows in db.roc_spGetClientsByType(editionId, clientTypeId, numberPage, page)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode,
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };



            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsByType");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientTypeId);
            
            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }
                
            }

            return clientList;

        }

        public List<ClientsByEditionInfo> getClientsByTypeByLetter(int editionId, byte clientTypeId, string letter)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsByLetter(editionId, clientTypeId, letter, numberPage, page)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode,
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                                  
                             };

            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsByLetter");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientTypeId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@letter", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, letter);
            
            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }
           
            return clientList;


        }

        public List<ClientsByEditionInfo> getClientsByTypeByText(int editionId, byte clientTypeId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();
            
            var clientsVar = from clientRows in db.roc_spGetClientsByText(editionId, clientTypeId, text, numberPage, page)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode,
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 StateName = clientRows.StateName,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };


            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;*/
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsByTypeByText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientTypeId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            

            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }

            return clientList;
        }

        public List<ProductsByClientInfo> getProductsByClient(int clientId, int? top)
        {
            /*
            GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var productsVar = from productRows in db.roc_spGetProductsByClient(clientId, top)
                              select new ProductsByClientInfo
                              {
                                 ClientProductId = (int)productRows.ClientProductId,
                                 ClientId = (int)productRows.ClientId,
                                 Page = productRows.Page,
                                 ProductClientActive = (bool)productRows.ProductClientActive,
                                 ProductId = (int)productRows.ProductId,
                                 ProductName = productRows.ProductName,
                                 ProductActive = (bool)productRows.ProductActive,
                                 SubProductId = productRows.SubProductId,
                                 SubProductName = productRows.SubProductName,
                                 SubProductActive = productRows.SubProductActive
                              };



            List<ProductsByClientInfo> prodClients = productsVar.ToList();

            
            return prodClients;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByClient");
            List<ProductsByClientInfo> productList = new List<ProductsByClientInfo>();
            ProductsByClientInfo record;
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@top", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, top);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new ProductsByClientInfo();

                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.Page = dataReader["Page"].ToString();
                    if (dataReader["ProductActive"]!=DBNull.Value)
                    record.ProductActive = Convert.ToBoolean(dataReader["ProductActive"]);

                    record.ProductClientActive = Convert.ToBoolean(dataReader["Active"]);
                    if (dataReader["ProductId"] != DBNull.Value)
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ProductName = dataReader["ProductName"].ToString();
                    record.SubProductActive = Convert.ToBoolean(dataReader["SubProductActive"]);
                    record.SubProductId = Convert.ToInt32(dataReader["SubProductId"]);
                    record.SubProductName = dataReader["SubProductName"].ToString();
                    
                    productList.Add(record);
                }
                
            }
            return productList;
        }

        public List<ProductsByEditionInfo> getProductsByText(int editionId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var productsVar = from productsRows in db.roc_spGetProductsByText(editionId, text, numberPage, page)
                              select new ProductsByEditionInfo
                              {
                                  ProductId = (int)productsRows.ProductId,
                                  ProductName = productsRows.ProductName,
                                  SubProductId = productsRows.SubProductId,
                                  SubProductName = productsRows.Description,
                                  Total = (int)productsRows.TOTAL,
                                  RowNumber = (int)productsRows.RowNumber
                              };

            List<ProductsByEditionInfo> products = productsVar.ToList();

            return products;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByText");
            List<ProductsByEditionInfo> productList = new List<ProductsByEditionInfo>();
            ProductsByEditionInfo record;
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new ProductsByEditionInfo();

                    if (dataReader["ProductId"]!=DBNull.Value)
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);

                    record.ProductName = dataReader["ProductName"].ToString();

                    if (dataReader["SubProductId"] != DBNull.Value)
                    record.SubProductId = Convert.ToInt32(dataReader["SubProductId"]);
                    record.SubProductName = dataReader["SubProductName"].ToString();
                    productList.Add(record);
                }

            }
          
            return productList;

        }

        public List<BrandsByEditionInfo> getBrandsByText(int editionId, string text)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var brandsVar = from brandRows in db.roc_spGetBrandsByText(editionId, text, numberPage, page)
                            select new BrandsByEditionInfo
                            {
                                BrandId = brandRows.BrandId,
                                BrandName = brandRows.BrandName,
                                Logo = brandRows.Logo,
                                BaseUrl = brandRows.BaseUrl,
                                Total = (int)brandRows.TOTAL,
                                RowNumber = (int)brandRows.RowNumber
                            };


            List<BrandsByEditionInfo> brands = brandsVar.ToList();

            return brands;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBrandsByText");
            List<BrandsByEditionInfo> brandList = new List<BrandsByEditionInfo>();
            
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    brandList.Add(this.getFromDataReaderBrandsByEditionInfo(dataReader));
                }

            }
          
            return brandList;

        }

        public List<SpecialityInfo> getSpecialities()
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var specialitiesVar = from specialityRows in db.roc_spGetSpecialities()
                                  select new SpecialityInfo
                                  {
                                      SpecialityId = specialityRows.SpecialityId,
                                      Description = specialityRows.Description,
                                      Active = specialityRows.Active
                                  };

            List<SpecialityInfo> specialities = specialitiesVar.ToList();

            return specialities;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSpecialities");
            List<SpecialityInfo> specialList = new List<SpecialityInfo>();
            SpecialityInfo record;
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new SpecialityInfo();

                    record.SpecialityId = Convert.ToInt32(dataReader["SpecialityId"]);
                    record.Description = dataReader["Description"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    specialList.Add(record);
                }
            }

            return specialList;

        }

        public List<ClientsByEditionInfo> getClientsBySpeciality(int editionId, int specialityId)
        {
            /*
            GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsBySpeciality(editionId, specialityId, numberPage, page)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode.ToString(),
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };

            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;
            */
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsBySpeciality");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);
            

            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }
           
            return clientList;
        }

        public List<ClientsByEditionInfo> getClientsBySpecByStateByText(int editionId, int specialityId, int stateId, string text)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsBySpecByStateByText(editionId, specialityId, stateId, numberPage, page, text)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode.ToString(),
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 StateName = string.Empty,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };

            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies*/;
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsBySpecByStateByText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
          ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }
            
            return clientList;


        }

        public List<ClientsByEditionInfo> getClientsBySpecByState(int editionId, int specialityId, int stateId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsBySpecByState(editionId, specialityId, numberPage, page, stateId)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode.ToString(),
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };

            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsBySpecByState");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);
            

            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }
           
            return clientList;

        }

        public List<ClientsByEditionInfo> getClientsBySpecByText(int editionId, int specialityId, string text)
        {
            /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsBySpecByText(editionId, specialityId, numberPage, page, text)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode.ToString(),
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 StateName = string.Empty,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };

            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsBySpecByText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);


            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }
           
            return clientList;


        }

        public List<StateInfo> getStatesByCountry(byte countryId)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var stateVar = from stateRows in db.roc_spGetStatesByCountry(countryId)
                           select new StateInfo
                           {
                               StateId = stateRows.StateId,
                               StateName = stateRows.StateName,
                               CountryId = stateRows.CountryId,
                               Active = stateRows.Active
                           };

            List<StateInfo> states = stateVar.ToList();

            return states;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStatesByCountry");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            List<StateInfo> states = new List<StateInfo>();
            StateInfo record;

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while(dataReader.Read())
                {
                     record = new StateInfo();

                    record.CountryId = Convert.ToByte(dataReader["CountryId"]);
                    record.StateName = dataReader["StateName"].ToString();
                    record.StateId = Convert.ToInt32(dataReader["StateId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    states.Add(record);
                    
                }
            }
            return states;

        }

        public List<BrandsByEditionInfo> getBrands(int editionId)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var brandsVar = from brandsRows in db.roc_spGetBrands(editionId,numberPage,page)
                            select new BrandsByEditionInfo
                            {
                                BrandId = brandsRows.BrandId,
                                BrandName = brandsRows.BrandName,
                                Logo = brandsRows.Logo,
                                BaseUrl = brandsRows.BaseUrl,
                                RowNumber = (int)brandsRows.RowNumber,
                                Total = (int)brandsRows.TOTAL
                            };

            List<BrandsByEditionInfo> brands = brandsVar.ToList();

            return brands;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBrands");
            List<BrandsByEditionInfo> brandList = new List<BrandsByEditionInfo>();
            
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    brandList.Add(this.getFromDataReaderBrandsByEditionInfo(dataReader));
                }

            }
            
            return brandList;

        }

        public List<BrandsByEditionInfo> getBrandsByLetter(int editionId, string letter)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var brandsVar = from brandsRows in db.roc_spGetBrandsByLetter(editionId,numberPage,page,letter)
                            select new BrandsByEditionInfo
                            {
                                BrandId = brandsRows.BrandId,
                                BrandName = brandsRows.BrandName,
                                Logo = brandsRows.Logo,
                                BaseUrl = brandsRows.BaseUrl,
                                RowNumber = (int)brandsRows.RowNumber,
                                Total = (int)brandsRows.TOTAL
                            };

            List<BrandsByEditionInfo> brands = brandsVar.ToList();

            return brands;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.spGetBrandsByLetter");
            List<BrandsByEditionInfo> brandList = new List<BrandsByEditionInfo>();

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@letter", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, letter);
          

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    brandList.Add(this.getFromDataReaderBrandsByEditionInfo(dataReader));
                }

            }
           
            return brandList;

        }

        public List<BrandsByEditionInfo> getBrandsByEditionByText(int editionId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var brandsVar = from brandsRows in db.roc_spGetBrandsByEditionText(editionId,numberPage,page,text)
                            select new BrandsByEditionInfo
                            {
                                BrandId = brandsRows.BrandId,
                                BrandName = brandsRows.BrandName,
                                Logo = brandsRows.Logo,
                                BaseUrl = brandsRows.BaseUrl,
                                RowNumber = (int)brandsRows.RowNumber,
                                Total = (int)brandsRows.TOTAL
                            };

            List<BrandsByEditionInfo> brands = brandsVar.ToList();

            return brands;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBrandsByEditionText");
            List<BrandsByEditionInfo> brandList = new List<BrandsByEditionInfo>();

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    brandList.Add(this.getFromDataReaderBrandsByEditionInfo(dataReader));
                }

            }
         
            return brandList;


        }

        public List<ProductInfo> getProducts(int editionId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var productsVar = from productRows in db.roc_spGetProducts(editionId, numberPage, page)
                              select new ProductInfo
                              {
                                  ProductId = productRows.ProductId,
                                  ProductName = productRows.ProductName,
                                  Total = (int)productRows.TOTAL,
                                  RowNumber = (int)productRows.RowNumber
                              };

            List<ProductInfo> products = productsVar.ToList();

            return products;
            */
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByEdition");
            List<ProductInfo> productList = new List<ProductInfo>();

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    productList.Add(this.getFromDataReaderProductInfo(dataReader));
                }

            }
            
            return productList;


        }

        public List<ProductInfo> getProducts(int editionId,int parentId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var productsVar = from productRows in db.roc_spGetProducts(editionId, numberPage, page)
                              select new ProductInfo
                              {
                                  ProductId = productRows.ProductId,
                                  ProductName = productRows.ProductName,
                                  Total = (int)productRows.TOTAL,
                                  RowNumber = (int)productRows.RowNumber
                              };

            List<ProductInfo> products = productsVar.ToList();

            return products;
            */
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByEdition");
            List<ProductInfo> productList = new List<ProductInfo>();

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    productList.Add(this.getFromDataReaderProductInfo(dataReader));
                }

            }

            return productList;


        }

        public List<ProductInfo> getProductsByLetter(int editionId, string letter)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var productsVar = from productRows in db.roc_spGetProductsByLetter(editionId, numberPage, page, letter)
                              select new ProductInfo
                              {
                                  ProductId = productRows.ProductId,
                                  ProductName = productRows.ProductName,
                                  Total = (int)productRows.TOTAL,
                                  RowNumber = (int)productRows.RowNumber
                              };

            List<ProductInfo> products = productsVar.ToList();

            return products;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByLetter");
            List<ProductInfo> productList = new List<ProductInfo>();

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@letter", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, letter);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    productList.Add(this.getFromDataReaderProductInfo(dataReader));
                }

            }
           
            return productList;

        }

        public List<ProductInfo> getProductsByEditionByText(int editionId, string text)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var productsVar = from productRows in db.roc_spGetProductsByEditionByText(editionId, numberPage, page, text)
                              select new ProductInfo
                              {
                                  ProductId = productRows.ProductId,
                                  ProductName = productRows.ProductName,
                                  Total = (int)productRows.TOTAL,
                                  RowNumber = (int)productRows.RowNumber
                              };

            List<ProductInfo> products = productsVar.ToList();

            return products;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByEditionByText");
            List<ProductInfo> productList = new List<ProductInfo>();

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    productList.Add(this.getFromDataReaderProductInfo(dataReader));
                }

            }
            
            return productList;

        }

        public List<ClientByBrandInfo> getClientsByBrand(int editionId, int brandId, int? top)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsByBrand(editionId, brandId, top)
                             select new ClientByBrandInfo
                             {
                                 ClientId = clientRows.ClientId,
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 ClientTypeId = clientRows.ClientTypeId
                             };

            List<ClientByBrandInfo> clients = clientsVar.ToList();

            return clients;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsByBrand");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@brandId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, brandId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@top", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, top);
            List<ClientByBrandInfo> clients = new List<ClientByBrandInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ClientByBrandInfo record = new ClientByBrandInfo();

                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.CompanyName= dataReader["CompanyName"].ToString();
                    record.ShortName = dataReader["ShortName"].ToString();
                    record.ClientTypeId= Convert.ToByte(dataReader["ClientTypeId"]);
                    clients.Add(record);
                }
                
            }

            return clients;

        }

        public List<SubProductByProductInfo> getSubProductsByProduct(int editionId, int productId)
        {
         /*   GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var subProductsVar = from subProductRows in db.roc_spGetSubProductByProduct(editionId, productId, numberByPage, page)
                                 select new SubProductByProductInfo
                                 {
                                     SubProductId = subProductRows.SubProductId,
                                     Description = subProductRows.Description,
                                     ClientId = subProductRows.ClientId,
                                     ClientTypeId = subProductRows.ClientTypeId,
                                     CompanyName = subProductRows.CompanyName,
                                     Total = (int)subProductRows.TOTAL,
                                     RowNumber = (int)subProductRows.RowNumber
                                 };

            List<SubProductByProductInfo> subProducts = subProductsVar.ToList();

            return subProducts;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSubProductByProduct");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            
            List<SubProductByProductInfo> subProds = new List<SubProductByProductInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    SubProductByProductInfo record = new SubProductByProductInfo();

                    record.SubProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Description = dataReader["ProductName"].ToString();
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.ClientTypeId = Convert.ToByte(dataReader["ClientTypeId"]);
                    record.CompanyName = dataReader["CompanyName"].ToString();
                    
                    
                    subProds.Add(record);
                }

            }
           
            return subProds;


        }

        public List<ClientByProductInfo> getClientsWithoutSubProduct(int editionId, int productId)
        {
          /*  GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientsRows in db.roc_spGetClientsWithoutSubProduct(editionId, productId, numberByPage, page)
                             select new ClientByProductInfo
                             {
                                 ClientId = clientsRows.ClientId,
                                 CompanyName = clientsRows.CompanyName,
                                 ClientTypeId = clientsRows.ClientTypeId,
                                 Total = (int)clientsRows.TOTAL,
                                 RowNumber = (int)clientsRows.RowNumber
                             };

            List<ClientByProductInfo> clients = clientsVar.ToList();

            return clients;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsWithoutSubProduct");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            
            List<ClientByProductInfo> clients = new List<ClientByProductInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ClientByProductInfo record = new ClientByProductInfo();

                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.ClientTypeId = Convert.ToByte(dataReader["ClientTypeId"]);
                    record.CompanyName = dataReader["CompanyName"].ToString();
                    clients.Add(record);
                }

            }
          
            return clients;


        }

        public SpecialityInfo getSpecialityById(int specialityId)
        {
         /*   GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var speVar = from speRows in db.roc_spGetSpeciality(specialityId)
                         select new SpecialityInfo
                         {
                             SpecialityId = speRows.SpecialityId,
                             Description = speRows.Description,
                             Active = speRows.Active
                         };

            SpecialityInfo speciality = speVar.ToList()[0];

            return speciality;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSpecialityById");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    SpecialityInfo record = new SpecialityInfo();

                    record.SpecialityId = Convert.ToInt32(dataReader["SpecialityId"]);
                    record.Description = dataReader["Description"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    return record;
                }
                else
                {
                    return null;   
                }

            }


        }

        public ProductInfo getProductById(int editionId, int productId)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var productVar = from productRows in db.roc_spGetProduct(editionId, productId, numberPage, page)
                             select new ProductInfo
                             {
                                 ProductId = productRows.ProductId,
                                 ProductName = productRows.ProductName,
                                 RowNumber = (int)productRows.RowNumber,
                                 Total = (int)productRows.TOTAL
                             };

            ProductInfo product = productVar.ToList()[0];

            return product;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductById");
            ProductInfo product = new ProductInfo() ;

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if(dataReader.Read())
                {
                    product=this.getFromDataReaderProductInfo(dataReader);
                   
                }

            }
            
            return product;


        }

        public BrandsByEditionInfo getBrandById(int editionId, int brandId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var brandVar = from brandRows in db.roc_spGetBrand(editionId, brandId, numberPage, page)
                           select new BrandsByEditionInfo
                           {
                               BrandId = brandRows.BrandId,
                               BrandName = brandRows.BrandName,
                               Logo = brandRows.Logo,
                               BaseUrl = brandRows.BaseUrl,
                               RowNumber = (int)brandRows.RowNumber,
                               Total = (int)brandRows.TOTAL
                           };

            BrandsByEditionInfo brand = brandVar.ToList()[0];

            return brand;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBrandById");
            BrandsByEditionInfo brand = new BrandsByEditionInfo();

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@brandId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, brandId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    brand.BrandId = Convert.ToInt32(dataReader["BrandId"]);
                    brand.BrandName = dataReader["BrandName"].ToString();
                    brand.BaseUrl = dataReader["BaseUrl"].ToString();
                    brand.Logo = dataReader["Logo"].ToString();
                    return brand;
                }
                else
                {
                    return null;  
                }

            }

        }

        public List<AdvertisementInfo> getAdvertByClient(int clientId,int editionId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var advVar = from advRows in db.roc_spGetAdvertisementsByClient(clientId)
                         orderby advRows.Orden
                         select new AdvertisementInfo
                         {
                             AdvertId = advRows.AdvertId,
                             ClientId = advRows.ClientId,
                             AdvertName = advRows.AdvertName,
                             BaseUrl = advRows.BaseUrl,
                             Orden = advRows.Orden,
                             Active = advRows.Active
                         };

            List<AdvertisementInfo> advertisements = advVar.ToList();

            return advertisements;
            */
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAdvertisementsByClient");
            AdvertisementInfo advert = new AdvertisementInfo();
            List<AdvertisementInfo> advertisements= new List<AdvertisementInfo>();
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    advert.AdvertId = Convert.ToInt32(dataReader["AdvertId"]);
                    advert.AdvertName = dataReader["AdvertName"].ToString();
                    advert.BaseUrl = dataReader["BaseUrl"].ToString();
                    advert.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    advert.Orden = Convert.ToInt32(dataReader["Order"]);
                    advert.Active = Convert.ToBoolean(dataReader["Active"]);
                    advertisements.Add(advert);
                }
            }
            return advertisements;

        }

        public List<ClientsByEditionInfo> getClientsByFullText(int editionId, byte clientTypeId, string text)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsByFullText(editionId, clientTypeId, text, numberPage, page)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode.ToString(),
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 StateName = clientRows.StateName,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };


            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsByFullText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientTypeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientTypeId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);


            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }

            return clientList;

        }

        public List<ClientSpecialityInfo> getClientsAndSpecialityByText(int editionId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsAndSpecialityByText(editionId, numberByPage, page, text)
                             select new ClientSpecialityInfo
                             {
                                 ClientId = clientRows.ClientId,
                                 CompanyName = clientRows.CompanyName,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 SpecialityId = clientRows.SpecialityId,
                                 SpecialityDescription= clientRows.SpecialityDescription,
                                 RowNumber = (int)clientRows.RowNumber,
                                 Total = (int)clientRows.TOTAL
                             };

            List<ClientSpecialityInfo> companies = clientsVar.ToList();

            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsByFullText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);


            List<ClientSpecialityInfo> companies = new List<ClientSpecialityInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    companies.Add(this.getFromDataReaderClientSpecialityInfo(dataReader));
                }

            }

            return companies;

        }

        public List<ClientSpecialityInfo> getClientsAndSpecialityByFullText(int editionId, string fullText)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsAndSpecialityByFullText(editionId, numberByPage, page, fullText)
                             select new ClientSpecialityInfo
                             {
                                 ClientId = clientRows.ClientId,
                                 CompanyName = clientRows.CompanyName,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 SpecialityId = clientRows.SpecialityId,
                                 SpecialityDescription = clientRows.SpecialityDescription,
                                 RowNumber = (int)clientRows.RowNumber,
                                 Total = (int)clientRows.TOTAL
                             };

            List<ClientSpecialityInfo> companies = clientsVar.ToList();

            return companies;
            */
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsAndSpecialityByFullText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@fullText", DbType.String,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, fullText);


            List<ClientSpecialityInfo> companies = new List<ClientSpecialityInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    companies.Add(this.getFromDataReaderClientSpecialityInfo(dataReader));
                }

            }

            return companies;

        }

        public List<ProductsByEditionInfo> getProductsByFullText(int editionId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var productsVar = from productsRows in db.roc_spGetProductsByFullText(editionId, text, numberPage, page)
                              select new ProductsByEditionInfo
                              {
                                  ProductId = (int)productsRows.ProductId,
                                  ProductName = productsRows.ProductName,
                                  SubProductId = productsRows.SubProductId,
                                  SubProductName = productsRows.Description,
                                  Total = (int)productsRows.TOTAL,
                                  RowNumber = (int)productsRows.RowNumber
                              };

            List<ProductsByEditionInfo> products = productsVar.ToList();

            return products;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByFullText");
            List<ProductsByEditionInfo> productList = new List<ProductsByEditionInfo>();
            ProductsByEditionInfo record;
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new ProductsByEditionInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ProductName = dataReader["ProductName"].ToString();
                    record.SubProductId = Convert.ToInt32(dataReader["SubProductId"]);
                    record.SubProductName = dataReader["SubProductName"].ToString();
                    productList.Add(record);
                }

            }

            return productList;

        }

        public List<BrandsByEditionInfo> getBrandsByFullText(int editionId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var brandsVar = from brandsRows in db.roc_spGetBrandsByFullText(editionId, text, numberPage, page)
                            select new BrandsByEditionInfo
                            {
                                BrandId = brandsRows.BrandId,
                                BrandName = brandsRows.BrandName,
                                Logo = brandsRows.Logo,
                                BaseUrl = brandsRows.BaseUrl,
                                RowNumber = (int)brandsRows.RowNumber,
                                Total = (int)brandsRows.TOTAL
                            };

            List<BrandsByEditionInfo> brands = brandsVar.ToList();

            return brands;*/


            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBrandsByFullText");
            List<BrandsByEditionInfo> brandList = new List<BrandsByEditionInfo>();

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    brandList.Add(this.getFromDataReaderBrandsByEditionInfo(dataReader));
                }

            }

            return brandList;

        }

        public List<ClientsByEditionInfo> getClientsBySpecByStateByFullText(int editionId, int specialityId, int stateId, string text)
        {
           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsBySpecByStateByFullText(editionId, specialityId, stateId, numberPage, page, text)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = clientRows.ClientIdParent,
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode.ToString(),
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = clientRows.StateId,
                                 StateName = string.Empty,
                                 CountryId = clientRows.CountryId,
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };

            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsBySpecByStateByFullText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
          ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }

            return clientList;

        }

        public List<ClientsByEditionInfo> getClientsBySpecByFullText(int editionId, int specialityId, string text)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var clientsVar = from clientRows in db.roc_spGetClientsBySpecByFullText(editionId, specialityId, numberPage, page, text)
                             select new ClientsByEditionInfo
                             {
                                 Total = (int)clientRows.TOTAL,
                                 RowNumber = (int)clientRows.RowNumber,
                                 ClientId = clientRows.ClientId,
                                 ClientIdParent = int.Parse(clientRows.ClientIdParent.ToString()),
                                 ClientTypeId = clientRows.ClientTypeId,
                                 ClientCode = clientRows.ClientCode.ToString(),
                                 CompanyName = clientRows.CompanyName,
                                 ShortName = clientRows.ShortName,
                                 Address = clientRows.Address,
                                 Suburb = clientRows.Suburb,
                                 PostalCode = clientRows.PostalCode,
                                 City = clientRows.City,
                                 StateId = int.Parse(clientRows.StateId.ToString()),
                                 StateName = string.Empty,
                                 CountryId = byte.Parse(clientRows.CountryId.ToString()),
                                 Email = clientRows.Email,
                                 Web = clientRows.Web,
                                 Products = clientRows.Products,
                                 Image = clientRows.Image,
                                 Page = clientRows.Page,
                                 EditionId = clientRows.EditionId,
                                 Active = clientRows.Active
                             };

            List<ClientsByEditionInfo> companies = clientsVar.ToList();

            return companies;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientsBySpecByFullText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);


            List<ClientsByEditionInfo> clientList = new List<ClientsByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    clientList.Add(this.getFromDataReaderClientByEditionInfo(dataReader));
                }

            }

            return clientList;



        }

        #endregion

        #region International

        public List<IntClientByEditionInfo> getInternational(int editionId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var client = from clientRows in db.roc_spGetInternational(editionId, numberByPage, page)
                         select new IntClientByEditionInfo
                         {
                             IntClientId = clientRows.IntClientId,
                             CompanyName = clientRows.CompanyName,
                             RowNumber = (int)clientRows.RowNumber,
                             Total = (int)clientRows.TOTAL
                         };

            List<IntClientByEditionInfo> intclients = client.ToList();

            return intclients;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetInternational");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            
            List<IntClientByEditionInfo> Collection = new List<IntClientByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    Collection.Add(this.getFromDataReaderIntClientByEditionInfo(dataReader));
                }
            }
            return Collection;
        }

        public List<IntClientByEditionInfo> getInternationalByLetter(int editionId, string letter)
        {

           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var client = from clientRows in db.roc_spGetInternationalByLetter(editionId, numberByPage, page, letter)
                         select new IntClientByEditionInfo
                         {
                             IntClientId = clientRows.IntClientId,
                             CompanyName = clientRows.CompanyName,
                             RowNumber = (int)clientRows.RowNumber,
                             Total = (int)clientRows.TOTAL
                         };

            List<IntClientByEditionInfo> intclients = client.ToList();

            return intclients;*/
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetInternationalByLetter");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@letter", DbType.String,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, letter);
            List<IntClientByEditionInfo> Collection = new List<IntClientByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    Collection.Add(this.getFromDataReaderIntClientByEditionInfo(dataReader));
                }
            }
            return Collection;
        }

        public List<IntClientByEditionInfo> getInternationalByText(int editionId, string text)
        {

            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var client = from clientRows in db.roc_spGetInternationalByText(editionId, numberByPage, page, text)
                         select new IntClientByEditionInfo
                         {
                             IntClientId = clientRows.IntClientId,
                             CompanyName = clientRows.CompanyName,
                             RowNumber = (int)clientRows.RowNumber,
                             Total = (int)clientRows.TOTAL
                         };


            List<IntClientByEditionInfo> intclients = client.ToList();

            return intclients;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetInternationalByText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            List<IntClientByEditionInfo> Collection = new List<IntClientByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    Collection.Add(this.getFromDataReaderIntClientByEditionInfo(dataReader));
                }
            }
            return Collection;

        }

        public List<IntClientByEditionInfo> getInternationalByFullText(int editionId,  string text)
        {

            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var client = from clientRows in db.roc_spGetInternationalByFullText(editionId, numberByPage, page, text)
                         select new IntClientByEditionInfo
                         {
                             IntClientId = clientRows.IntClientId,
                             CompanyName = clientRows.CompanyName,
                             RowNumber = (int)clientRows.RowNumber,
                             Total = (int)clientRows.TOTAL
                         };

            List<IntClientByEditionInfo> intclients = client.ToList();

            return intclients;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetInternationalByFullText");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            List<IntClientByEditionInfo> Collection = new List<IntClientByEditionInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    Collection.Add(this.getFromDataReaderIntClientByEditionInfo(dataReader));
                }
            }
            return Collection;

        }


        public IntAddressByClientInfo getAddressByInternationalId(int clientId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var address = from addressRow in db.roc_spGetAddressByInternationalId(clientId)
                          select new IntAddressByClientInfo
                          {
                              IntAddressId = addressRow.IntAddressesId,
                              Address = addressRow.Address,
                              ZipCode = addressRow.ZipCode,
                              City = addressRow.City,
                              State = addressRow.State,
                              CountryId = addressRow.CountryId,
                              Email = addressRow.Email,
                              Web = addressRow.Web,
                              Active = addressRow.Active,
                              CountryName = addressRow.CountryName
                          };

            IntAddressByClientInfo intaddress = address.ToList()[0];

            return intaddress;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAddressByInternationalClient");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    IntAddressByClientInfo record = new IntAddressByClientInfo();
                    record.IntAddressId = Convert.ToInt32(dataReader["AddressId"]);
                    record.Address=dataReader["Address"].ToString();
                    record.ZipCode = dataReader["ZipCode"].ToString();
                    record.City = dataReader["City"].ToString();
                    record.State = dataReader["StateName"].ToString();
                    record.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                    record.CountryName = dataReader["CountryName"].ToString();
                    record.Email = dataReader["Email"].ToString();
                    record.Web = dataReader["Web"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    return record;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<IntPhonesByClientInfo> getPhonesByInternationalId(int clientId)
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var phone = from phoneRow in db.roc_spGetPhonesByInternationalId(clientId)
                        select new IntPhonesByClientInfo
                        {
                            IntClientPhoneId = phoneRow.IntClientPhoneId,
                            PhoneTypeId = phoneRow.PhoneTypeId,
                            IntClientId = phoneRow.IntClientId,
                            Number = phoneRow.Number,
                            Lada = phoneRow.Lada,
                            Active = phoneRow.Active,
                            Description = phoneRow.Description
                        };

            List<IntPhonesByClientInfo> intphones = phone.ToList();

            return intphones;*/
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPhonesByInternationalClient");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            IntPhonesByClientInfo record;
            List<IntPhonesByClientInfo> collection = new List<IntPhonesByClientInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while(dataReader.Read())
                {
                    record= new IntPhonesByClientInfo();
                    record.IntClientPhoneId=Convert.ToInt32(dataReader["ClientPhoneId"]);
                    record.IntClientId=Convert.ToInt32(dataReader["ClientId"]);
                    record.PhoneTypeId=Convert.ToByte(dataReader["PhoneTypeId"]);
                    record.Number=dataReader["Number"].ToString();
                    record.Lada=dataReader["Lada"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    collection.Add(record);
                    
                }
            }
            return collection;
        }

        public List<CategoryInfo> getInternationalCategories()
        {
            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var category = from categoryRow in db.roc_spGetInternationalCategories()
                           select new CategoryInfo
                           {
                               CategoryId = categoryRow.CategoryId,
                               ParentId = (byte?)categoryRow.ParentId,
                               Description = categoryRow.Description,
                               Active = categoryRow.Active,
                           };

            List<CategoryInfo> intcategory = category.ToList();

            return intcategory;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCategories");
            List<CategoryInfo> collect = new List<CategoryInfo>(); 
               
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    collect.Add(this.getFromDataReaderCategoryInfo(dataReader));
                }
            }
            return collect;
        }

        public List<CategoryInfo> getInternationalCategoriesByParentId(int parentId)
        {

            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var category = from categoryRow in db.roc_spGetInternationalCategoriesByParentId(parentId)
                           select new CategoryInfo
                           {
                               CategoryId = categoryRow.CategoryId,
                               ParentId = (byte)categoryRow.ParentId,
                               Description = categoryRow.Description,
                               Active = categoryRow.Active,
                           };

            List<CategoryInfo> intcategory = category.ToList();

            return intcategory;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCategories");
            List<CategoryInfo> collect = new List<CategoryInfo>();
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    collect.Add(this.getFromDataReaderCategoryInfo(dataReader));
                }
            }
            return collect;
        }

        public CategoryInfo getInternationalCategoriesByClientId(int clientId)
        {

           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var category = from categoryRow in db.roc_spGetInternationalCategoriesByClientId(clientId)
                           select new CategoryInfo
                           {
                               CategoryId = categoryRow.CategoryId,
                               ParentId = (byte?)categoryRow.ParentId,
                               Description = categoryRow.Description,
                               Active = categoryRow.Active,
                           };

            CategoryInfo intcategory = category.ToList()[0];

            return intcategory;*/
            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCategoriesByClient");
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@parents", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, 0);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    return this.getFromDataReaderCategoryInfo(dataReader);
                }
                else
                {
                    return null;
                }
            }
            
        }

        public List<IntProductsByClientInfo> getInternationalProductsByClientAndCategory(int clientId, int categoryId)
        {
            /*
            GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var products = from productsRow in db.roc_spGetInternationalProductsByClientAndCategory(clientId, categoryId)
                           select new IntProductsByClientInfo
                           {
                               ParentId = (byte?)productsRow.ParentId,
                               IntProductId = productsRow.IntProductId,
                               Description = productsRow.Description,
                               CategoryId = productsRow.CategoryId,

                           };

            List<IntProductsByClientInfo> intproducts = products.ToList();

            return intproducts;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByClientByCategory");
            List<IntProductsByClientInfo> collect = new List<IntProductsByClientInfo>();
            IntProductsByClientInfo record;
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, null);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new IntProductsByClientInfo();
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.Description = dataReader["ProductName"].ToString();
                    record.IntProductId = Convert.ToInt32(dataReader["ProductId"]);
                    
                    if (dataReader["ParentId"] != DBNull.Value)
                    record.ParentId = (byte)dataReader["ParentId"];

                    collect.Add(record);
                }

            }
            return collect;


        }

        public List<CategoryInfo> getInternationalSubcategoriesByClientId(int clientId)
        {

           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var subcat = from subcatRows in db.roc_spGetInternationalSubcategoriesByClientId(clientId)
                         select new CategoryInfo
                         {
                             CategoryId = subcatRows.CategoryId,
                             ParentId = (byte)subcatRows.ParentId,
                             Description = subcatRows.Description,
                             Active = subcatRows.Active
                             
                         };

            List<CategoryInfo> intsubcat = subcat.ToList();

            return intsubcat;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCategoriesByClient");
            List<CategoryInfo> collect = new List<CategoryInfo>();
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@parents", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    collect.Add(this.getFromDataReaderCategoryInfo(dataReader));
                }
                
            }
            return collect;
                       
        }

        public List<IntProductsByClientInfo> getInternationalProductsByParentId(int clientId, int categoryId, int productParentId)
        {

           /* GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var products = from prodRows in db.roc_spGetInternationalProductsByParentId(clientId, categoryId, productParentId)
                           select new IntProductsByClientInfo
                           {
                               ParentId = (byte)prodRows.ParentId,
                               IntProductId = prodRows.IntProductId,
                               Description = prodRows.Description,
                               CategoryId = prodRows.CategoryId
                           };

            List<IntProductsByClientInfo> intproducts = products.ToList();

            return intproducts;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByClientByCategory");
            List<IntProductsByClientInfo> collect = new List<IntProductsByClientInfo>();
            IntProductsByClientInfo record;
            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productParentId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new IntProductsByClientInfo();
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.Description = dataReader["ProductName"].ToString();
                    record.IntProductId = Convert.ToInt32(dataReader["ProductId"]);

                    if (dataReader["ParentId"] != DBNull.Value)
                    record.ParentId = (byte)dataReader["ParentId"];

                    collect.Add(record);
                }

            }
            return collect;

        }

        public List<CategoryInfo> getCategoryById(int categoryId)
        {

            GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var category = from categoryRow in db.roc_spGetCategoryById(categoryId)
                           select new CategoryInfo
                           {
                               CategoryId = categoryRow.CategoryId,
                               ParentId = (byte?)categoryRow.ParentId,
                               Description = categoryRow.Description,
                               Active = categoryRow.Active
                           };

            List<CategoryInfo> intcategory = category.ToList();

            return intcategory;
        }

        public IntClientInfo getIntClientById(int clientId)
        {

            /*GuiaDataClassesDataContext db = new GuiaDataClassesDataContext();

            var client = from intClientRow in db.roc_spGetIntClient(clientId)
                            select new IntClientInfo
                           {
                               IntClientId = intClientRow.IntClientId,
                               CompanyName = intClientRow.CompanyName,
                               ShortName = intClientRow.ShortName,
                               Active = intClientRow.Active
                           };

            IntClientInfo intClient = client.ToList()[0];

            return intClient;*/

            DbCommand dbCmd = Queries.GuiaInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientById");

            // Add the parameters:
            Queries.GuiaInstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = Queries.GuiaInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read()){
                    IntClientInfo intClient = new IntClientInfo();
                    intClient.IntClientId = Convert.ToInt32(dataReader["ClientId"]);
                    intClient.ShortName = dataReader["ShortName"].ToString();
                    intClient.CompanyName = dataReader["CompanyName"].ToString();
                    intClient.Active = Convert.ToBoolean(dataReader["Active"]);
                    return intClient;
                }
                else
                    return null;
            }
        }

        #endregion


        #region Protected methods

        protected  ClientInfo getFromDataReader(IDataReader current)
        {
            ClientInfo record = new ClientInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            
            if (current["ClientIdParent"] != DBNull.Value)
            record.ClientIdParent = Convert.ToInt32(current["ClientIdParent"]);

            record.ClientTypeId = Convert.ToByte(current["ClientTypeId"]);
            
            if (current["ClientCode"] != DBNull.Value)
            record.ClientCode = current["ClientCode"].ToString();

            record.CompanyName = current["CompanyName"].ToString();

            if (current["ShortName"] != DBNull.Value)
            record.ShortName = current["ShortName"].ToString();

            record.Address = current["Address"].ToString();

            if (current["Suburb"] != DBNull.Value)
            record.Suburb = current["Suburb"].ToString();

            if (current["ZipCode"] != DBNull.Value)
            record.PostalCode = current["ZipCode"].ToString();

            if (current["City"] != DBNull.Value)
            record.City = current["City"].ToString();

            if (current["StateId"] != DBNull.Value)
            record.StateId = Convert.ToInt32(current["StateId"]);

            if (current["CountryId"]!= DBNull.Value)
            record.CountryId = (byte?)(current["CountryId"]);

            if (current["Email"] != DBNull.Value)
            record.Email = current["Email"].ToString();

            if (current["Web"] != DBNull.Value)
            record.Web = current["Web"].ToString();

            if (current["Image"]!=DBNull.Value)
            record.Image = current["Image"].ToString();

            if (current["Page"] != DBNull.Value)
            record.Page = current["Page"].ToString();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            return record;
        }

        protected  ClientsByEditionInfo getFromDataReaderClientByEditionInfo(IDataReader current)
        {
            ClientsByEditionInfo record = new ClientsByEditionInfo();
            record.ClientId = Convert.ToInt32(current["ClientId"]);

            if (current["ClientIdParent"] != DBNull.Value)
                record.ClientIdParent = Convert.ToInt32(current["ClientIdParent"]);

            record.ClientTypeId = Convert.ToByte(current["ClientTypeId"]);
            record.ClientCode = current["ClientCode"].ToString();
            record.CompanyName = current["CompanyName"].ToString();

            if (current["ShortName"] != DBNull.Value)
                record.ShortName = current["ShortName"].ToString();

            if (current["Address"] != DBNull.Value)
            record.Address = current["Address"].ToString();

            if (current["Suburb"] != DBNull.Value)
                record.Suburb = current["Suburb"].ToString();

            if (current["ZipCode"] != DBNull.Value)
                record.PostalCode = current["ZipCode"].ToString();

            if (current["City"] != DBNull.Value)
                record.City = current["City"].ToString();

            if (current["StateId"] != DBNull.Value)
                record.StateId = Convert.ToInt32(current["StateId"]);

            if (current["CountryId"] != DBNull.Value)
            record.CountryId = (byte?)(current["CountryId"]);

            if (current["Email"] != DBNull.Value)
                record.Email = current["Email"].ToString();

            if (current["Web"] != DBNull.Value)
                record.Web = current["Web"].ToString();

            if (current["Image"] != DBNull.Value)
                record.Image = current["Image"].ToString();

            if (current["Page"] != DBNull.Value)
                record.Page = current["Page"].ToString();

            if (current["StateName"] != DBNull.Value)
                record.StateName = current["StateName"].ToString();


            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            return record;
        }

        protected BrandsByEditionInfo getFromDataReaderBrandsByEditionInfo(IDataReader current)
        {
            BrandsByEditionInfo record = new BrandsByEditionInfo();
            record.BrandId = Convert.ToInt32(current["BrandId"]);
            record.BrandName = current["BrandName"].ToString();
            record.Logo = current["Logo"].ToString();
            record.BaseUrl = current["BaseUrl"].ToString();
            return record;
        }

        protected ProductInfo getFromDataReaderProductInfo(IDataReader current)
        {
            ProductInfo record = new ProductInfo();
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ProductName= current["ProductName"].ToString();
            
            if (current["ParentId"]!=DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            return record;
        }

        protected CategoryInfo getFromDataReaderCategoryInfo(IDataReader current)
        {
            CategoryInfo record = new CategoryInfo();
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.Description= current["Description"].ToString();
            
            if (current["ParentId"] != DBNull.Value)
            record.ParentId= (byte)(current["ParentId"]);

            record.Active= Convert.ToBoolean(current["Active"]);
            return record;
        }
        
        protected IntClientByEditionInfo getFromDataReaderIntClientByEditionInfo(IDataReader current)
        {
            IntClientByEditionInfo record = new IntClientByEditionInfo();
            record.IntClientId = Convert.ToInt32(current["ClientId"]);
            record.CompanyName= current["CompanyName"].ToString();
            
            return record;
        }

        protected ClientSpecialityInfo getFromDataReaderClientSpecialityInfo(IDataReader current)
        {
            ClientSpecialityInfo record = new ClientSpecialityInfo();
            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.ClientTypeId= Convert.ToInt32(current["ClientTypeId"]);
            record.CompanyName=current["CompanyName"].ToString();
            record.SpecialityDescription = current["SpecialityDescription"].ToString();
            record.SpecialityId = Convert.ToInt32(current["SpecialityId"]);
            return record;
        }


        #endregion

        public static readonly Queries Instance = new Queries();
    }

}
