﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Class;


namespace Web.Models
{
    public class Inserts
    {
        Medinet db = new Medinet();
        CRUD CRUD = new CRUD();

        public int inserproduct(int CountryId, int LaboratoryId, int AlphabetId, int ProductTypeId, string ProductName, string Description)
        {
            try
            {
                
                List<CreateProduct_result> LI = new List<CreateProduct_result>();

                if (string.IsNullOrEmpty(Description))
                {
                    Description = null;
                }
                int Active = 1;
                int PproductId = 0;

                // Create
                LI = db.Database.SqlQuery<CreateProduct_result>("plm_spCRUDProducts @productId=" + PproductId + ",@CRUDType=" + CRUD.Create + ", @countryId=" + CountryId + ", @laboratoryId=" + LaboratoryId + ", @alphabetId=" + AlphabetId + ", @ProductTypeId=" + ProductTypeId + ", @brand=" + ProductName + ",@description=" + Description + ", @active=" + Active + "").ToList();

                LI.ToString();

                string _productId = String.Join(",", LI);
                int productId = int.Parse(_productId);

                ////// REVISAR ////////

                ////// REVISAR ////////

                ////// REVISAR ////////
                //var qryprod = db.Products.Where(x => x.CountryId == CountryId && x.AlphabetId == AlphabetId && x.LaboratoryId == LaboratoryId && x.ProductTypeId == ProductTypeId && x.Brand.ToUpper().Trim() == ProductName.ToUpper().Trim()).ToList();

                //ProductId = qryprod[0].ProductId;

                return productId;

                //return ProductId;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return 1;
            }

        }

        public bool insertproductpharmaforms(int ProductId, int PharmaFormId)
        {
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDProductPharmaForms @CRUDType=" + CRUD.Create + ", @pharmaFormId= " + PharmaFormId + " ,@productId = " + ProductId + ",@active= 1");

                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }

        public bool insertproductcategories(int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDProductCategories @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId= " + ProductId + ",@technicalSheet= 0, @CRUDType=" + CRUD.Create + "");

                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }

        }

        public bool insertparticipantproducts(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId)
        {
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Create + "");

                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }

        }

        public bool insertnewproducts(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId)
        {
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDNewProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Create + "");

                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }

        }

        public bool inserteditiondivisionproducts(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId)
        {
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Create + "");

                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }

        public bool insertDivisionCategories(int DivisionId, int CategoryId)
        {
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDDivisionCategories @CRUDType=" + CRUD.Create + ", @divisionId=" + DivisionId + ", @categoryId=" + CategoryId + "");

                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }

        public bool deletenewproducts(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId)
        {
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDNewProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Delete + "");

                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }
    }
}