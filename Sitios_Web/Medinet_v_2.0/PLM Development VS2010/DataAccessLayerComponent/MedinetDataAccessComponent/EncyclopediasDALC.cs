using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class EncyclopediasDALC : MedinetDataAccessAdapter<EncyclopediasInfo>
    {
        #region Constructors

        private EncyclopediasDALC() { }

        #endregion

        public string getSizes(string output)
        {
            string returnValue="";
            DbCommand dbCmd = EncyclopediasDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSizes");
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.String,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current,"");
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@output", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, output);
            IDataReader dataReader = EncyclopediasDALC.MedInstanceDatabase.ExecuteReader(dbCmd);
            while (dataReader.Read())
            {
                returnValue= dataReader["Size"].ToString();
            }
            return returnValue;
        }
        public List<EncyclopediasInfo> getAllEncyclopediasWithOutProduct(int productId,int pharmaFormId,string encyclopedia) {
            DbCommand dbCmd = EncyclopediasDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopedias");
            List<EncyclopediasInfo> encyclopedias = new List<EncyclopediasInfo>();
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopedia", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopedia);

            using (IDataReader dataReader = EncyclopediasDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    encyclopedias.Add(this.getFromDataReader(dataReader));
                }
            }
            return encyclopedias;

        }

        public List<EncyclopediasInfo> getEncyclopediasByProductByPharmaForm(int productId,int PharmaFormId)
        {
            DbCommand dbCmd = EncyclopediasDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaFormEncyclopedias");
            List<EncyclopediasInfo> encyclopedias = new List<EncyclopediasInfo>();

            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, EncyclopediasDALC.CRUD.Read);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, PharmaFormId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, 0);


            using (IDataReader dataReader = EncyclopediasDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    encyclopedias.Add(this.getFromDataReader(dataReader));
                }
            }
            return encyclopedias;

        }
        public List<EncyclopediasInfo> getEncyclopediasByCountry(int countryId,int bookId,int editionId)
        {
            DbCommand dbCmd = EncyclopediasDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediasByCountry");
            List<EncyclopediasInfo> encyclopedias = new List<EncyclopediasInfo>();
           EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
           


            using (IDataReader dataReader = EncyclopediasDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                EncyclopediasInfo record;
                while (dataReader.Read())
                {
                    record = new EncyclopediasInfo();
                    record.EncyclopediaId = Convert.ToInt32(dataReader["EncyclopediaId"]);
                    record.EncyclopediaName = dataReader["EncyclopediaName"].ToString();
                    record.EncyclopediaTypeId = Convert.ToInt32(dataReader["EncyclopediaTypeId"]);
                    record.ICD = dataReader["ICD"].ToString();
                    record.EncyclopediaType = dataReader["EncyclopediaType"].ToString();
                    encyclopedias.Add(record);
                }
            }
            return encyclopedias;

        }

        public List<EncyclopediasInfo> getImagesByEncyclopedias(int encyclopediaId)
        {
            DbCommand dbCmd = EncyclopediasDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediaImagesByCountry");
            List<EncyclopediasInfo> encyclopedias = new List<EncyclopediasInfo>();


            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaId);



            using (IDataReader dataReader = EncyclopediasDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                EncyclopediasInfo record;
                while (dataReader.Read())
                {
                    record = new EncyclopediasInfo();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.EncyclopediaId = Convert.ToInt32(dataReader["EncyclopediaId"]);
                    record.EncyclopediaName = dataReader["EncyclopediaName"].ToString();
                    record.EncyclopediaTypeId = Convert.ToInt32(dataReader["EncyclopediaTypeId"]);
                    record.ICD = dataReader["ICD"].ToString();
                    record.Imagen = dataReader["Images"].ToString();
                    record.ImageSizes = dataReader["ImageSizes"].ToString();
                    record.Sizes = dataReader["Sizes"].ToString();
                    record.ImageId = Convert.ToInt32(dataReader["ImageId"]);
                    record.EncyclopediaType = dataReader["EncyclopediaType"].ToString();
                    encyclopedias.Add(record);
                }
            }
            return encyclopedias;

        }

        public int insertImage(EncyclopediaImageInfo businessEntity)
        {
            DbCommand dbCmd = EncyclopediasDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDImagesByEncyclopedia");

            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);

            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.encyclopediaImage);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseEncyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.encyclopediaId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageSize);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@previousImage", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PreviousImage);
            EncyclopediasDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public int deleteImage(EncyclopediaImageInfo businessEntity)
        {
            DbCommand dbCmd = EncyclopediasDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDImagesByEncyclopedia");

            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);

            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.encyclopediaImage);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseEncyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.encyclopediaId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageId);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageSize);
            EncyclopediasDALC.MedInstanceDatabase.AddParameter(dbCmd, "@previousImage", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PreviousImage);
            EncyclopediasDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }
        public int updateImage(PresentationImages businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDImagesByPresentation");


            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);

            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.productShot);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@psePresentationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.presentationId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pseImageSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageSize);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@previousImage", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PreviousImage);
            PresentationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return 0;
        }


        #region Protected methods

        protected override EncyclopediasInfo getFromDataReader(IDataReader current)
        {
            EncyclopediasInfo record = new EncyclopediasInfo();

            record.Active = Convert.ToBoolean(current["Active"]);
            record.EncyclopediaId = Convert.ToInt32(current["EncyclopediaId"]);
            record.EncyclopediaName = current["EncyclopediaName"].ToString();
            record.EncyclopediaTypeId = Convert.ToInt32(current["EncyclopediaTypeId"]);
            
            
            return record;
        }

        #endregion

        public static readonly EncyclopediasDALC Instance = new EncyclopediasDALC();
    }
}
