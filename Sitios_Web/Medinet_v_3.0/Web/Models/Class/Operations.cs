using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;
using Web.Models.Class;

namespace Web.Models.Class
{
    public class Operations
    {
        Medinet db = new Medinet();
        CRUD CRUD = new CRUD();

        public String SaveAddATCEphMRA(int TherapeuticId, int ProductId, int PharmaFormId, string Operation)
        {

            if (Operation == "Insert")
            {
                List<ProductTherapeutics> PT = db.Database.SqlQuery<ProductTherapeutics>("plm_spCRUDProductTherapeutics @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @therapeuticId=" + TherapeuticId + "").ToList();

                if (PT.LongCount() == 0)
                {

                    var _result = db.Database.ExecuteSqlCommand("plm_spCRUDProductTherapeutics @CRUDType=" + CRUD.Create + ", @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @therapeuticId=" + TherapeuticId + "");

                    return "Ok";
                }
                else
                {
                    List<Therapeutics> LT = db.Therapeutics.Where(x => x.TherapeuticId == TherapeuticId).ToList();

                    String Therapeutic = LT[0].TherapeuticKey + " - " + LT[0].SpanishDescription;

                    return Therapeutic;
                }
            }
            else
            {
                List<ProductTherapeutics> PT = db.Database.SqlQuery<ProductTherapeutics>("plm_spCRUDProductTherapeutics @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @therapeuticId=" + TherapeuticId + "").ToList();

                if (PT.LongCount() > 0)
                {

                    var _result = db.Database.ExecuteSqlCommand("plm_spCRUDProductTherapeutics @CRUDType=" + CRUD.Delete + ", @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @therapeuticId=" + TherapeuticId + "");

                    return "Ok";
                }
                else
                {
                    List<Therapeutics> LT = db.Therapeutics.Where(x => x.TherapeuticId == TherapeuticId).ToList();

                    String Therapeutic = LT[0].TherapeuticKey + " - " + LT[0].SpanishDescription;

                    return Therapeutic;
                }
            }
        }

        public String SaveAddCIE10(int ICDId, int ProductId, int PharmaFormId, string Operation)
        {
            if (Operation == "Insert")
            {
                List<ProductICD> LS = db.Database.SqlQuery<ProductICD>("plm_spCRUDProductICD @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ", @icdId=" + ICDId + ", @pharmaformID=" + PharmaFormId + "").ToList();

                if (LS.LongCount() == 0)
                {
                    var response = db.Database.SqlQuery<int>("plm_spCRUDProductICD @CRUDType=" + CRUD.Create + ", @productId=" + ProductId + ", @icdId=" + ICDId + ", @pharmaformID=" + PharmaFormId + "").ToList();

                    if (response[0] == 0)
                    {
                        return "Ok";
                    }
                    else if (response[0] == 1)
                    {
                        return "AsocCNT";
                    }
                    else
                    {
                        return "error";
                    }
                }
                else
                {
                    List<ICD> LICD = db.ICD.Where(x => x.ICDId == ICDId).ToList();

                    string ICD = LICD[0].ICDKey + " - " + LICD[0].SpanishDescription;

                    return ICD;
                }
            }
            else
            {
                List<ProductICD> LS = db.Database.SqlQuery<ProductICD>("plm_spCRUDProductICD @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ", @icdId=" + ICDId + ", @pharmaformID=" + PharmaFormId + "").ToList();

                if (LS.LongCount() > 0)
                {
                    var response = db.Database.ExecuteSqlCommand("plm_spCRUDProductICD @CRUDType=" + CRUD.Delete + ", @productId=" + ProductId + ", @icdId=" + ICDId + ", @pharmaformID=" + PharmaFormId + "");

                    return "Ok";
                }
                else
                {
                    List<ICD> LICD = db.ICD.Where(x => x.ICDId == ICDId).ToList();

                    string ICD = LICD[0].ICDKey + " - " + LICD[0].SpanishDescription;

                    return ICD;
                }
            }
        }

        public String SaveAddATCOMS(int TherapeuticOMSId, int ProductId, int PharmaFormId, string Operation)
        {

            if (Operation == "Insert")
            {
                List<ProductTherapeuticOMS> PT = db.Database.SqlQuery<ProductTherapeuticOMS>("plm_spCRUDProductOMSTherapeutics @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @therapeuticOMSId=" + TherapeuticOMSId + "").ToList();

                if (PT.LongCount() == 0)
                {

                    var _result = db.Database.ExecuteSqlCommand("plm_spCRUDProductOMSTherapeutics @CRUDType=" + CRUD.Create + ", @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @therapeuticOMSId=" + TherapeuticOMSId + "");

                    return "Ok";
                }
                else
                {
                    List<TherapeuticOMS> LT = db.TherapeuticOMS.Where(x => x.TherapeuticOMSId == TherapeuticOMSId).ToList();

                    String Therapeutic = LT[0].TherapeuticKey + " - " + LT[0].SpanishDescription;

                    return Therapeutic;
                }
            }
            else
            {
                List<ProductTherapeuticOMS> PT = db.Database.SqlQuery<ProductTherapeuticOMS>("plm_spCRUDProductOMSTherapeutics @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @therapeuticOMSId=" + TherapeuticOMSId + "").ToList();

                if (PT.LongCount() > 0)
                {

                    var _result = db.Database.ExecuteSqlCommand("plm_spCRUDProductOMSTherapeutics @CRUDType=" + CRUD.Delete + ", @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @therapeuticOMSId=" + TherapeuticOMSId + "");

                    return "Ok";
                }
                else
                {
                    List<TherapeuticOMS> LT = db.TherapeuticOMS.Where(x => x.TherapeuticOMSId == TherapeuticOMSId).ToList();

                    String Therapeutic = LT[0].TherapeuticKey + " - " + LT[0].SpanishDescription;

                    return Therapeutic;
                }
            }
        }

        public String SaveProductSubstanceInteractions(int SubstanceInteractId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ActiveSubstanceId, string Operation)
        {
            if (Operation == "Insert")
            {
                List<ProductSubstanceInteractions> LS = db.Database.SqlQuery<ProductSubstanceInteractions>("plm_spCRUDProductSubstanceInteractions @substanceInteractId=" + SubstanceInteractId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Read + "").ToList();

                if (LS.LongCount() == 0)
                {
                    var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductSubstanceInteractions @substanceInteractId=" + SubstanceInteractId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");

                    return "Ok";
                }
                else
                {
                    var AS = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == SubstanceInteractId).Select(x => x.Description).ToList();

                    string ActiveSubstance = AS[0];

                    return ActiveSubstance;
                }
            }
            else
            {
                List<ProductSubstanceInteractions> LS = db.Database.SqlQuery<ProductSubstanceInteractions>("plm_spCRUDProductSubstanceInteractions @substanceInteractId=" + SubstanceInteractId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Read + "").ToList();

                if (LS.LongCount() > 0)
                {
                    var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductSubstanceInteractions @substanceInteractId=" + SubstanceInteractId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Delete + "");
                }
                return "Ok";
            }
        }

        public String IPPAProductInteractions(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ActiveSubstanceId, string Operation)
        {
            try
            {
                List<IPPAProductInteractions> LS = db.Database.SqlQuery<IPPAProductInteractions>("plm_spCRUDIppaProductInteractions @divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@substanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Read + "").ToList();

                if (LS.LongCount() == 0)
                {
                    var result = db.Database.ExecuteSqlCommand("plm_spCRUDIppaProductInteractions @divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@substanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");
                }
            }
            catch (Exception e)
            {

            }


            return "";
        }

        public String CheckProductInteraction(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ActiveSubstanceId)
        {
            try
            {
                List<CheckProductInteraction> LS = db.Database.SqlQuery<CheckProductInteraction>("plm_spCheckProductInteraction @productId=" + ProductId + ",@substanceId=" + ActiveSubstanceId + ",@divisionId=" + DivisionId + ",@categoryId=" + CategoryId + ",@pharmaFormId=" + PharmaFormId + "").ToList();
            }
            catch (Exception e)
            {

            }

            return "";
        }

        public String SaveProductPharmacologicalGroupInteractions(int PharmaGroupId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ActiveSubstanceId, string Operation)
        {
            if (Operation == "Insert")
            {
                List<GetProductPharmaGroupInteractions> LS = db.Database.SqlQuery<GetProductPharmaGroupInteractions>("plm_spCRUDProductPharmacologicalGroupInteractions @pharmaGroupId=" + PharmaGroupId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Read + "").ToList();

                if (LS.LongCount() == 0)
                {
                    var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductPharmacologicalGroupInteractions @pharmaGroupId=" + PharmaGroupId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");

                    return "Ok";
                }
                else
                {
                    var AS = db.PharmacologicalGroups.Where(x => x.PharmaGroupId == PharmaGroupId).Select(x => x.GroupName).ToList();

                    string ActiveSubstance = AS[0];

                    return ActiveSubstance;
                }
            }
            else
            {
                List<GetProductPharmaGroupInteractions> LS = db.Database.SqlQuery<GetProductPharmaGroupInteractions>("plm_spCRUDProductPharmacologicalGroupInteractions @pharmaGroupId=" + PharmaGroupId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Read + "").ToList();

                if (LS.LongCount() > 0)
                {
                    var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductPharmacologicalGroupInteractions @pharmaGroupId=" + PharmaGroupId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Delete + "");
                }

                return "Ok";
            }
        }

        public String SaveProductOtherElementInteractions(int ElementId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ActiveSubstanceId, string Operation)
        {
            if (Operation == "Insert")
            {
                List<GetProductotherInteractions> LS = db.Database.SqlQuery<GetProductotherInteractions>("plm_spCRUDProductOtherElementInteractions @elementID=" + ElementId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Read + "").ToList();

                if (LS.LongCount() == 0)
                {
                    var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductOtherElementInteractions @elementID=" + ElementId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");

                    return "Ok";
                }
                else
                {
                    var AS = db.OtherElements.Where(x => x.ElementId == ElementId).Select(x => x.ElementName).ToList();

                    string ActiveSubstance = AS[0];

                    return ActiveSubstance;
                }
            }
            else
            {
                List<GetProductotherInteractions> LS = db.Database.SqlQuery<GetProductotherInteractions>("plm_spCRUDProductOtherElementInteractions @elementID=" + ElementId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Read + "").ToList();

                if (LS.LongCount() > 0)
                {
                    var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductOtherElementInteractions @elementID=" + ElementId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Delete + "");
                }

                return "Ok";
            }
        }

        public String CheckProductContraindication(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ActiveSubstanceId)
        {
            try
            {
                List<CheckProductInteraction> LS = db.Database.SqlQuery<CheckProductInteraction>("plm_spCheckProductContraindication @productId=" + ProductId + ",@substanceId=" + ActiveSubstanceId + ",@divisionId=" + DivisionId + ",@categoryId=" + CategoryId + ",@pharmaFormId=" + PharmaFormId + "").ToList();
            }
            catch (Exception e)
            {

            }

            return "";
        }

        public String SavePhysContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int PhysContraindicationId, int ActiveSubstanceId, int OperationId)
        {
            if (OperationId == CRUD.Create)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductPhysContraindications @CRUDType=" + CRUD.Create + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @physContraindicationId=" + PhysContraindicationId + ", @activeSubstanceId=" + ActiveSubstanceId + "");
            }
            else
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductPhysContraindications @CRUDType=" + CRUD.Delete + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @physContraindicationId=" + PhysContraindicationId + ", @activeSubstanceId=" + ActiveSubstanceId + "");
            }

            return "Ok";
        }

        public String SavePharmaContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int PhysContraindicationId, int ActiveSubstanceId, int OperationId)
        {
            if (OperationId == CRUD.Create)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductPharmaContraindications @CRUDType=" + CRUD.Create + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @pharmaContraindicationId=" + PhysContraindicationId + ", @activeSubstanceId=" + ActiveSubstanceId + "");
            }
            else
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductPharmaContraindications @CRUDType=" + CRUD.Delete + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @pharmaContraindicationId=" + PhysContraindicationId + ", @activeSubstanceId=" + ActiveSubstanceId + "");
            }

            return "Ok";
        }

        public String SaveHyperContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int PhysContraindicationId, int ActiveSubstanceId, int OperationId)
        {
            if (OperationId == CRUD.Create)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductHypersensibilities @CRUDType=" + CRUD.Create + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @hypersensibilityId=" + PhysContraindicationId + ", @activeSubstanceId=" + ActiveSubstanceId + "");
            }
            else
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductHypersensibilities @CRUDType=" + CRUD.Delete + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @hypersensibilityId=" + PhysContraindicationId + ", @activeSubstanceId=" + ActiveSubstanceId + "");
            }

            return "Ok";
        }

        public String SaveProductPharmaGroupContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int PharmaGroupId, int ActiveSubstanceId, int Operation)
        {
            if (Operation == CRUD.Create)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductPharmaGroupContraindications @pharmaGroupId=" + PharmaGroupId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");
            }
            else
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductPharmaGroupContraindications @pharmaGroupId=" + PharmaGroupId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Delete + "");
            }
            return "Ok";
        }

        public String SaveProductSubstanceContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int SubstanceInteractId, int ActiveSubstanceId, int Operation)
        {
            if (Operation == CRUD.Create)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductSubstanceContraindications @subsContraindicationId=" + SubstanceInteractId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");
            }
            else
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductSubstanceContraindications @subsContraindicationId=" + SubstanceInteractId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Delete + "");
            }
            return "Ok";
        }

        public String IPPAProductContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ActiveSubstanceId, string Operation)
        {
            try
            {
                List<IPPAProductContraindications> LS = db.Database.SqlQuery<IPPAProductContraindications>("plm_spCRUDIppaProductContraindications @divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@substanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Read + "").ToList();

                if (LS.LongCount() == 0)
                {
                    var result = db.Database.ExecuteSqlCommand("plm_spCRUDIppaProductContraindications @divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@substanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");
                }
            }
            catch (Exception e)
            {

            }


            return "";
        }

        public String SaveProductOtherContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ElementId, int ActiveSubstanceId, int Operation)
        {
            if (Operation == CRUD.Create)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductOtherContraindications @elementId=" + ElementId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");
            }
            else
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductOtherContraindications @elementId=" + ElementId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Delete + "");
            }
            return "Ok";
        }

        public String SaveProductCommentContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, string Comments, int ActiveSubstanceId, int Operation)
        {
            if (Operation == CRUD.Create)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductCommentContraindications @comments='" + Comments + "',@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Create + "");
            }
            else
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductCommentContraindications @comments='" + Comments + "',@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Delete + "");
            }
            return "Ok";
        }

        public String DeleteProductCommentContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ProductCommentId, int ActiveSubstanceId, int Operation)
        {
            try
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductCommentContraindications @productCommentId=" + ProductCommentId + ",@divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@activeSubstanceId = " + ActiveSubstanceId + ",@CRUDType =" + CRUD.Delete + "");
            }
            catch (Exception e)
            {

            }

            return "Ok";
        }

        public String DeleteAllCommentsContraindications(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int Operation)
        {
            try
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductCommentContraindications @divisionId =" + DivisionId + ",@categoryId = " + CategoryId + ",@pharmaFormId = " + PharmaFormId + ",@productId = " + ProductId + ",@CRUDType =" + CRUD.Delete + "");
            }
            catch (Exception e)
            {

            }

            return "Ok";
        }
    }
}