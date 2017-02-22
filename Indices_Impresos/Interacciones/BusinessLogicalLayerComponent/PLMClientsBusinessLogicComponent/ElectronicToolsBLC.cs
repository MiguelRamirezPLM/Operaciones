using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ElectronicToolsBLC
    {
        #region Constructors

        private ElectronicToolsBLC() { }

        #endregion

        #region Public Methods

        public List<ToolsByTargetInfo> getToolsByCode(string codeString, string country, Catalogs.TargetOutputs targetId, Catalogs.ElectronicToolTypes toolTypeId)
        {
            ValidCodeInfo validCode = new ValidCodeInfo();
            validCode = CodesBLC.Instance.validCode(codeString);

            //Valid user code
            if (validCode.CodeStatusId == Catalogs.CodeStatus.ACTIVO)
            {
                //Get the prefixId
                int prefixId = CodesBLC.Instance.getCode(codeString).PrefixId;

                //Get the country
                CountryInfo BECountry = CatalogsBLC.Instance.getCountry(country);
                if (BECountry == null)
                    throw new ArgumentException("País no válido");

                List<ToolsByTargetInfo> tools = PLMClientsDataAccessComponent.ElectronicToolsDALC.Instance.getByTargetByPrefix(BECountry.CountryId, (byte)targetId, (byte)toolTypeId, prefixId);

                if (tools != null)

                    //Set the root url to note and the banners
                    foreach (ToolsByTargetInfo tool in tools)
                    {
                        if (!string.IsNullOrEmpty(tool.FileName))
                            tool.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + BECountry.CountryName + "/" + tool.BaseUrl;
                        
                        if(!string.IsNullOrEmpty(tool.BannerUrl))
                            tool.BannerUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + BECountry.CountryName + "/" + tool.BannerUrl;
                    }
                return tools;
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        #endregion

        public static readonly ElectronicToolsBLC Instance = new ElectronicToolsBLC();

    }
}
