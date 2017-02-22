using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class DivisionsDALC : MedinetDataAccessAdapter<DivisionsInfo>
    {

        #region Constructors

        private DivisionsDALC() { }

        #endregion

        #region Public methods
        public int insertLogo(LogoInfo businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLogoDivisions");
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);

            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoName", DbType.String,
                      ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.divisionShot);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseDivisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LogoId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LogoSize);
            PresentationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return 0;
        }

        public int deleteLogo(LogoInfo businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLogoDivisions");


            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);

            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoName", DbType.String,
                      ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.divisionShot);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseDivisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LogoId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LogoSize);
            PresentationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return 0;
        }
        public int updateLogo(LogoInfo businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLogoDivisions");


            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);


            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoName", DbType.String,
                      ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.divisionShot);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseDivisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LogoId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseLogoSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LogoSize);
            PresentationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return 0;
        }
        public DivisionsInfo getOneByProduct(int productId)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionByProduct");

            // Add the parameters:
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }      
        }

        public override DivisionsInfo getOne(int pk)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisions");

            // Add the parameters:
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }       
        }

        public List<DivisionsInfo> getAllByEdition(int editionId)
        {
            List<DivisionsInfo> BECollection = new List<DivisionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(
                DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionsByEdition", editionId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
           
            return BECollection;      
        }
        public List<DivisionsInfo> getLogosByDivision(int countryId, int editionId)
        {
            List<DivisionsInfo> BECollection = new List<DivisionsInfo>();

            DbCommand dbCmd = PresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLogosByDivision");

       
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            

            using (IDataReader dataReader = PresentationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                //PresentationDetailInfo record;

                while (dataReader.Read())
                {
                    DivisionsInfo record = new DivisionsInfo();
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                   // record.LaboratoryId = Convert.ToInt32(dataReader["LaboratoryId"]);
                    record.Description = dataReader["Description"].ToString();
                    record.ShortName = dataReader["ShortName"].ToString();
                    record.Logo = dataReader["Logo"].ToString();
                      BECollection.Add(record);
                }
            }
            return BECollection;
        }
        public string getSizes(string output)
        {
            string returnValue = "";
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSizes");
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@output", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, output);
            IDataReader dataReader = EncyclopediasDALC.MedInstanceDatabase.ExecuteReader(dbCmd);
            while (dataReader.Read())
            {
                returnValue = dataReader["Size"].ToString();
            }
            return returnValue;
        }
        public List<DivisionsInfo> getDivisionLogos(int editionId, int divisionId, int countryId, int bookId)
        {
            List<DivisionsInfo> BECollection = new List<DivisionsInfo>();

            DbCommand dbCmd = PresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionLogos");

            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);

            using (IDataReader dataReader = PresentationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                //PresentationDetailInfo record;

                while (dataReader.Read())
                {
                    DivisionsInfo record = new DivisionsInfo();
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.LogoId = Convert.ToInt32(dataReader["LogoId"]);
                    record.LogoSizes = dataReader["LogoSizes"].ToString();
                    record.Description = dataReader["Description"].ToString();
                    record.ShortName = dataReader["ShortName"].ToString();
                    record.Logo = dataReader["Logo"].ToString();
                    record.Sizes = dataReader["Sizes"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<DivisionsInfo> getAllByCountryLab(int countryId, int laboratoryId)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionsByCountryLab");
            
            //Add Parameters:
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@laboratoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, laboratoryId);

            List<DivisionsInfo> BECollection = new List<DivisionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<DivisionsInfo> getAllByCountry(int countryId)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionsByCountry");

            //Add Parameters:
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            List<DivisionsInfo> BECollection = new List<DivisionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public override void update(DivisionsInfo businessEntity)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisions");

            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@laboratoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LaboratoryId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@shortName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ShortName);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            DivisionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        #endregion

        #region Protected methods

        protected override DivisionsInfo getFromDataReader(System.Data.IDataReader current)
        {
            DivisionsInfo record = new DivisionsInfo();

            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.LaboratoryId = Convert.ToInt32(current["LaboratoryId"]);
            record.CountryId = Convert.ToInt32(current["CountryId"]);
            record.Description = current["Description"].ToString();
            record.ShortName = current["ShortName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly DivisionsDALC Instance = new DivisionsDALC();
    }
}