using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public class PresentationsDALC : AgroDataAccessAdapter<PresentationsInfo>
    {

        #region Construtors

        private PresentationsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PresentationsInfo businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPresentations");

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@presentationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);

    

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            PresentationsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public int insertImage(PresentationImages businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDImagesByPresentation");

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.productShot);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageSize);
            
            PresentationsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public int deleteImage(PresentationImages businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDImagesByPresentation");


            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.productShot);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
          
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageSize);
           
            PresentationsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
            return 0;
        }
        public int updateImage(PresentationImages businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDImagesByPresentation");


            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.productShot);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
         
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pseImageSize", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ImageSize);

            PresentationsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
            return 0;
        }

        public override void update(PresentationsInfo businessEntity)
        {
            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPresentations");

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@presentationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PresentationId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);

            if (businessEntity.QtyExternalPack != null)
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@qtyExternalPack", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.QtyExternalPack);

            if (businessEntity.ExternalPackId != null)
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@externalPackId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ExternalPackId);

            if (businessEntity.QtyInternalPack != null)
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@qtyInternalPack", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.QtyInternalPack);

            if (businessEntity.InternalPackId != null)
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@internalPackId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InternalPackId);

            if (businessEntity.QtyContentUnit != null)
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@qtyContentUnit", DbType.String,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.QtyContentUnit);

            if (businessEntity.ContentUnitId != null)
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@contentUnitId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ContentUnitId);

            if (businessEntity.QtyWeightUnit != null)
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@qtyWeightUnit", DbType.String,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.QtyWeightUnit);

            if (businessEntity.WeightUnitId != null)
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@weightUnitId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.WeightUnitId);

            if (!string.IsNullOrEmpty(businessEntity.Presentation))
                PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@presentation", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Presentation);

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            PresentationsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override PresentationsInfo getOne(int pk)
        {
            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPresentations");

            // Add the parameters:
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@presentationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = PresentationsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<PresentationDetailInfo> getPresentationsByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            List<PresentationDetailInfo> BECollection = new List<PresentationDetailInfo>();

            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPresentationsByProduct");

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            using (IDataReader dataReader = PresentationsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                //PresentationDetailInfo record;

                while (dataReader.Read())
                {
                    PresentationDetailInfo record = new PresentationDetailInfo();

                    record.PresentationId = Convert.ToInt32(dataReader["PresentationId"]);

                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionName = dataReader["DivisionName"].ToString();
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();

                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();

                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();

                    if (dataReader["ExternalPackId"] != System.DBNull.Value)
                        record.ExternalPackId = Convert.ToInt32(dataReader["ExternalPackId"]);
                    record.ExternalPackName = dataReader["ExternalPackName"].ToString();
                    if (dataReader["QtyExternalPack"] != System.DBNull.Value)
                        record.QtyExternalPack = Convert.ToInt32(dataReader["QtyExternalPack"]);

                    if (dataReader["InternalPackId"] != System.DBNull.Value)
                        record.InternalPackId = Convert.ToInt32(dataReader["InternalPackId"]);
                    record.InternalPackName = dataReader["InternalPackName"].ToString();
                    if (dataReader["QtyInternalPack"] != System.DBNull.Value)
                        record.QtyInternalPack = Convert.ToInt32(dataReader["QtyInternalPack"]);

                    if (dataReader["ContentUnitId"] != System.DBNull.Value)
                        record.ContentUnitId = Convert.ToInt32(dataReader["ContentUnitId"]);
                    record.ContentUnitName = dataReader["ContentUnitName"].ToString();
                    record.QtyContentUnit = dataReader["QtyContentUnit"].ToString();

                    if (dataReader["WeightUnitId"] != System.DBNull.Value)
                        record.WeightUnitId = Convert.ToInt32(dataReader["WeightUnitId"]);
                    record.WeightUnitName = dataReader["WeightUnitName"].ToString();
                    record.QtyWeightUnit = dataReader["QtyWeightUnit"].ToString();

                    record.Presentation = dataReader["Presentation"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["PresentationActive"]);
                    record.Editions = dataReader["Editions"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<PresentationDetailInfo> getPresentationList(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            List<PresentationDetailInfo> BECollection = new List<PresentationDetailInfo>();

            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPresentationList");

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            using (IDataReader dataReader = PresentationsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                //PresentationDetailInfo record;

                while (dataReader.Read())
                {
                    PresentationDetailInfo record = new PresentationDetailInfo();

                    record.PresentationId = Convert.ToInt32(dataReader["PresentationId"]);

                    
                    record.Presentation = dataReader["Presentation"].ToString();
                   
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }


        public List<PresentationDetailInfo> getPresentationsByImage(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            List<PresentationDetailInfo> BECollection = new List<PresentationDetailInfo>();

            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetImagesByPresentation");

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            using (IDataReader dataReader = PresentationsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                //PresentationDetailInfo record;

                while (dataReader.Read())
                {
                    PresentationDetailInfo record = new PresentationDetailInfo();

                    record.PresentationId = Convert.ToInt32(dataReader["PresentationId"]);

                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionName = dataReader["DivisionName"].ToString();
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();

                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();

                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();

                    record.Presentation = dataReader["Presentation"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["PresentationActive"]);
                    
                    record.Imagen = dataReader["Images"].ToString();
                    record.ImageId = Convert.ToInt32(dataReader["ImageId"]);
                    record.ImageSizes = dataReader["ImageSizes"].ToString();
                    record.Sizes = dataReader["Sizes"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }
        public string getSizes(string output)
        {
            string returnValue = "";
            //DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSizes");
            //PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@output", DbType.String,
            //    ParameterDirection.Input, string.Empty, DataRowVersion.Current, output);
           
            //IDataReader dataReader = EncyclopediasDALC.AgroInstanceDatabase.ExecuteReader(dbCmd);
            //while (dataReader.Read())
            //{
            //    returnValue = dataReader["Size"].ToString();
            //}
            return returnValue;
        }

        public List<PresentationDetailInfo> getPresentationsByEditionByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            List<PresentationDetailInfo> BECollection = new List<PresentationDetailInfo>();

            DbCommand dbCmd = PresentationsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPresentationsByEditionByProduct");

            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PresentationsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            using (IDataReader dataReader = PresentationsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                //PresentationDetailInfo record;

                while (dataReader.Read())
                {
                    PresentationDetailInfo record = new PresentationDetailInfo();

                    record.PresentationId = Convert.ToInt32(dataReader["PresentationId"]);

                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionName = dataReader["DivisionName"].ToString();
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();

                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();

                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();

                    if (dataReader["ExternalPackId"] != System.DBNull.Value)
                        record.ExternalPackId = Convert.ToInt32(dataReader["ExternalPackId"]);
                    record.ExternalPackName = dataReader["ExternalPackName"].ToString();
                    if (dataReader["QtyExternalPack"] != System.DBNull.Value)
                        record.QtyExternalPack = Convert.ToInt32(dataReader["QtyExternalPack"]);

                    if (dataReader["InternalPackId"] != System.DBNull.Value)
                        record.InternalPackId = Convert.ToInt32(dataReader["InternalPackId"]);
                    record.InternalPackName = dataReader["InternalPackName"].ToString();
                    if (dataReader["QtyInternalPack"] != System.DBNull.Value)
                        record.QtyInternalPack = Convert.ToInt32(dataReader["QtyInternalPack"]);

                    if (dataReader["ContentUnitId"] != System.DBNull.Value)
                        record.ContentUnitId = Convert.ToInt32(dataReader["ContentUnitId"]);
                    record.ContentUnitName = dataReader["ContentUnitName"].ToString();
                    record.QtyContentUnit = dataReader["QtyContentUnit"].ToString();

                    if (dataReader["WeightUnitId"] != System.DBNull.Value)
                        record.WeightUnitId = Convert.ToInt32(dataReader["WeightUnitId"]);
                    record.WeightUnitName = dataReader["WeightUnitName"].ToString();
                    record.QtyWeightUnit = dataReader["QtyWeightUnit"].ToString();

                    record.Presentation = dataReader["Presentation"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["PresentationActive"]);
                    record.Editions = dataReader["Editions"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override PresentationsInfo getFromDataReader(IDataReader current)
        {
            PresentationsInfo record = new PresentationsInfo();

            record.PresentationId = Convert.ToInt32(current["PresentationId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);

            if (current["QtyExternalPack"] != System.DBNull.Value)
                record.QtyExternalPack = Convert.ToInt32(current["QtyExternalPack"]);

            if (current["ExternalPackId"] != System.DBNull.Value)
                record.ExternalPackId = Convert.ToInt32(current["ExternalPackId"]);

            if (current["QtyInternalPack"] != System.DBNull.Value)
                record.QtyInternalPack = Convert.ToInt32(current["QtyInternalPack"]);

            if (current["InternalPackId"] != System.DBNull.Value)
                record.InternalPackId = Convert.ToInt32(current["InternalPackId"]);

            if (current["QtyContentUnit"] != System.DBNull.Value)
                record.QtyContentUnit = current["QtyContentUnit"].ToString();

            if (current["ContentUnitId"] != System.DBNull.Value)
                record.ContentUnitId = Convert.ToInt32(current["ContentUnitId"]);

            if (current["QtyWeightUnit"] != System.DBNull.Value)
                record.QtyWeightUnit = current["QtyWeightUnit"].ToString();

            if (current["WeightUnitId"] != System.DBNull.Value)
                record.WeightUnitId = Convert.ToInt32(current["WeightUnitId"]);

            if (current["Presentation"] != System.DBNull.Value)
                record.Presentation = current["Presentation"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly PresentationsDALC Instance = new PresentationsDALC();

    }
}
