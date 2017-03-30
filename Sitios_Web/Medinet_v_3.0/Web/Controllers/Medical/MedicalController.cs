using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Sessions;
using Web.Models;
using Newtonsoft.Json;
using Web.Models.Class;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Web.Controllers.Medical
{
    public class MedicalController : Controller
    {

        Medinet db = new Medinet();
        Web.Models.Class.CRUD CRUD = new Web.Models.Class.CRUD();
        Web.Models.Class.Operations Operations = new Web.Models.Class.Operations();
        getData getData = new getData();
        PLMUsers dbusers = new PLMUsers();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            return View();
        }

        public ActionResult Content(int? CId, int? BId, int? EId, int? DId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            List<plm_spGetProductsbyDivisionbyCountry> LS = db.Database.SqlQuery<plm_spGetProductsbyDivisionbyCountry>("plm_spGetProductsbyDivisionbyCountry @CountryId=" + CId + ", @DivisionId=" + DId + "").Take(50).ToList();


            SessionLI SessionLI = new SessionLI(Convert.ToInt32(CId), Convert.ToInt32(BId), 1, Convert.ToInt32(EId), Convert.ToInt32(DId), null, null, null);
            Session["SessionLI"] = SessionLI;

            return View(LS);
        }

        //AL
        public JsonResult UpdateProductType(string Product, string ProductType)
        {
            int ProductId = int.Parse(Product);
            byte ProductTypeId = Convert.ToByte(ProductType);

            List<Products> LP = db.Products.Where(x => x.ProductId == ProductId).ToList();

            if (LP.LongCount() > 0)
            {
                foreach (Products _p in LP)
                {
                    _p.ProductTypeId = ProductTypeId;

                    db.SaveChanges();

                    ActivityLog.UpdateProductType(ProductId, ProductTypeId, 2);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLaboratories(string id)
        {
            int CountryId = int.Parse(id);

            List<Divisions> LS = db.Database.SqlQuery<Divisions>("plm_spGetDivisionsByCountry @countryId=" + CountryId + "").ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        #region Substances

        public ActionResult Clasification(int? ProductId, int? PharmaFormId, int? CategoryId, int? EditionId, string DescriptionAs, string Description)
        {
            SessionLI SessionLI = (SessionLI)Session["SessionLI"];

            if ((!Request.IsAuthenticated) || (ProductId == null) || (PharmaFormId == null) || (CategoryId == null) || (SessionLI == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            GetActiveSubstancesCls GetActiveSubstancesCls = new Models.Class.GetActiveSubstancesCls();

            GetActiveSubstancesCls.ActiveSubstances = db.Database.SqlQuery<Web.Models.Class.ActiveSubstances>("plm_spGetActiveSubstanceByProduct @productId = " + ProductId + ", @substance='" + DescriptionAs + "'").ToList();
            GetActiveSubstancesCls.GetActiveSubstancesWithoutInteractions = db.Database.SqlQuery<Web.Models.Class.ActiveSubstances>("plm_spGetActiveSubstancesWithoutProduct @productId=" + ProductId + ", @substance='" + Description + "'").ToList();

            SessionLI.ProductId = ProductId;
            SessionLI.PharmaFormId = PharmaFormId;
            SessionLI.CategoryId = CategoryId;
            SessionLI.EditionId = Convert.ToInt32(EditionId);

            return View(GetActiveSubstancesCls);
        }

        //AL
        public ActionResult AddProductSubstances(int? ProductId, int? ActiveSubstanceId, int? PharmaFormId, int? CategoryId, int? EditionId)
        {
            try
            {
                var response = db.Database.ExecuteSqlCommand("plm_spCRUDProductSubstance @CRUDType=" + CRUD.Create + ", @productId=" + ProductId + ", @activeSubstanceId=" + ActiveSubstanceId + "");

                ActivityLog.CRUDProductSubstances(Convert.ToInt32(ProductId), Convert.ToInt32(ActiveSubstanceId), 1);
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Clasification", new { ProductId = ProductId, PharmaFormId = PharmaFormId, CategoryId = CategoryId, EditionId = EditionId });
        }

        //AL
        public ActionResult DeleteProductSubstances(int? ProductId, int? ActiveSubstanceId, int? PharmaFormId, int? CategoryId, int? EditionId)
        {
            try
            {
                var response = db.Database.ExecuteSqlCommand("plm_spCRUDProductSubstance @CRUDType=" + CRUD.Delete + ", @productId=" + ProductId + ", @activeSubstanceId=" + ActiveSubstanceId + "");

                ActivityLog.CRUDProductSubstances(Convert.ToInt32(ProductId), Convert.ToInt32(ActiveSubstanceId), 4);
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Clasification", new { ProductId = ProductId, PharmaFormId = PharmaFormId, CategoryId = CategoryId, EditionId = EditionId });
        }

        #endregion

        #region ATCEphMRA

        public ActionResult ATCEphMRA(int? ProductId, int? PharmaFormId, int? CategoryId, int? EditionId)
        {
            SessionLI SessionLI = (SessionLI)Session["SessionLI"];

            if ((!Request.IsAuthenticated) || (ProductId == null) || (PharmaFormId == null) || (CategoryId == null) || (SessionLI == null))
            {
                return RedirectToAction("Logout", "Login");
            }


            List<Web.Models.Class.TherapeuticsEphMRA> LA = db.Database.SqlQuery<Web.Models.Class.TherapeuticsEphMRA>("plm_spGetTherapeuticByProduct @productId = " + ProductId + ", @pharmaFormId=" + PharmaFormId + "").ToList();

            SessionLI.ProductId = ProductId;
            SessionLI.PharmaFormId = PharmaFormId;
            SessionLI.CategoryId = CategoryId;
            SessionLI.EditionId = Convert.ToInt32(EditionId);

            return View(LA);
        }

        public JsonResult GetLevelTwoATCEphMRA(string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);

            List<String> LT = new List<String>();

            var LS1 = db.Therapeutics.Where(x => x.ParentId == TherapeuticId && x.Active == true && x.Level == 2).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (Therapeutics item in LS1)
            {
                String _return = String.Empty;

                List<Therapeutics> LS = db.Therapeutics.Where(x => x.ParentId == item.TherapeuticId && x.Active == true && x.Level == 3).OrderBy(x => x.TherapeuticKey).ToList();

                if (LS.LongCount() > 0)
                {
                    _return = "<span class='glyphicon glyphicon-plus' id='Expand_" + item.TherapeuticId + "' onclick='getlevel3SM(" + item.TherapeuticId + ")' style='cursor:pointer'></span>" +
                                "<span class='glyphicon glyphicon-minus' style='display:none;cursor:pointer' id='Collapse_" + item.TherapeuticId + "' onclick='collapselevel3(" + item.TherapeuticId + ")'></span>" +
                                "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                "<ul id='ListL3_" + item.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>";
                }
                else
                {
                    _return = "<input type='checkbox' value='" + item.TherapeuticId + "' onclick=\"AddATCEphMRA($(this)) \" />" +
                                "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                "<ul id='ListL4_" + item.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>";
                }

                LT.Add(_return);
            }

            return Json(LT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLevelThreeATCEphMRA(string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);

            List<String> LT = new List<String>();

            var LS1 = db.Therapeutics.Where(x => x.ParentId == TherapeuticId && x.Active == true && x.Level == 3).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (Therapeutics item in LS1)
            {
                String _return = String.Empty;

                List<Therapeutics> LS = db.Therapeutics.Where(x => x.ParentId == item.TherapeuticId && x.Active == true && x.Level == 4).OrderBy(x => x.TherapeuticKey).ToList();

                if (LS.LongCount() > 0)
                {
                    _return = "<span class='glyphicon glyphicon-plus' id='Expand_" + item.TherapeuticId + "' onclick='getlevel4SM(" + item.TherapeuticId + ")' style='cursor:pointer'></span>" +
                                "<span class='glyphicon glyphicon-minus' style='display:none;cursor:pointer' id='Collapse_" + item.TherapeuticId + "' onclick='collapselevel4(" + item.TherapeuticId + ")'></span>" +
                                "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                "<ul id='ListL4_" + item.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>";
                }
                else
                {
                    _return = "<input type='checkbox' value='" + item.TherapeuticId + "' onclick=\"AddATCEphMRA($(this)) \" />" +
                                "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                "<ul id='ListL4_" + item.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>";
                }

                LT.Add(_return);
            }

            return Json(LT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLevelFourATCEphMRA(string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);

            List<String> LT = new List<String>();

            var LS1 = db.Therapeutics.Where(x => x.ParentId == TherapeuticId && x.Active == true && x.Level == 4).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (Therapeutics item in LS1)
            {
                String _return = String.Empty;

                List<Therapeutics> LS = db.Therapeutics.Where(x => x.ParentId == item.TherapeuticId && x.Active == true && x.Level == 5).OrderBy(x => x.TherapeuticKey).ToList();

                if (LS.LongCount() > 0)
                {
                    _return = "<span class='glyphicon glyphicon-plus' id='Expand_" + item.TherapeuticId + "' onclick='getlevel5SM(" + item.TherapeuticId + ")' style='cursor:pointer'></span>" +
                                "<span class='glyphicon glyphicon-minus' style='display:none;cursor:pointer' id='Collapse_" + item.TherapeuticId + "' onclick='collapselevel5(" + item.TherapeuticId + ")'></span>" +
                                "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                "<ul id='ListL5_" + item.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>";
                }
                else
                {
                    _return = "<input type='checkbox' value='" + item.TherapeuticId + "'  onclick=\"AddATCEphMRA($(this)) \"/>" +
                                "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                "<ul id='ListL4_" + item.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>";
                }

                LT.Add(_return);
            }

            return Json(LT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLevelFiveATCEphMRA(string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);

            List<String> LS = new List<String>();

            var LS1 = db.Therapeutics.Where(x => x.ParentId == TherapeuticId && x.Active == true && x.Level == 5).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (Therapeutics item in LS1)
            {
                String _return = String.Empty;

                _return = "<input type='checkbox' value='" + item.TherapeuticId + "'  onclick=\"AddATCEphMRA($(this)) \"/>" +
                            "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                            "<ul id='ListL4_" + item.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>";

                LS.Add(_return);
            }

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        //AL
        public JsonResult SaveAddATCEphMRA(String List, string size)
        {
            int SizeId = int.Parse(size);

            dynamic item = JsonConvert.DeserializeObject(List);

            List<String> LS = new List<String>();

            for (var i = 0; i <= SizeId - 1; i++)
            {
                int TherapeuticId = Convert.ToInt32(item[i]["TId"]);
                int ProductId = Convert.ToInt32(item[i]["PId"]);
                int PharmaFormId = Convert.ToInt32(item[i]["PFId"]);

                String response = Operations.SaveAddATCEphMRA(TherapeuticId, ProductId, PharmaFormId, "Insert");

                if (response != "Ok")
                {
                    LS.Add(response.ToUpper());
                }
            }

            if (LS.LongCount() > 0)
            {
                LS = LS.OrderBy(x => x).ToList();

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        //AL
        public JsonResult RemoveATCEphMRA(string Product, string PharmaForm, string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);

            String response = Operations.SaveAddATCEphMRA(TherapeuticId, ProductId, PharmaFormId, "Delete");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Contraindications

        public ActionResult Contraindications(int? ProductId,
            int? PharmaFormId,
            int? CategoryId,
            int? EditionId,
            int? DivisionId,
            string PhysDescription,
            string PharmaDescription,
            string GroupDescription,
            string HypersensibilityDescription,
            string ActiveSubstanceDescription,
            string OtherElementsDescription)
        {
            SessionLI SessionLI = (SessionLI)Session["SessionLI"];

            if ((!Request.IsAuthenticated) || (ProductId == null) || (PharmaFormId == null) || (CategoryId == null) || (SessionLI == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            if (SessionLI != null)
            {
                ProductId = SessionLI.ProductId;
                PharmaFormId = SessionLI.PharmaFormId;
                CategoryId = SessionLI.CategoryId;
                EditionId = SessionLI.EditionId;
            }

            GetContraindications GetContraindications = new Models.Class.GetContraindications();

            SessionLI.ProductId = ProductId;
            SessionLI.PharmaFormId = PharmaFormId;
            SessionLI.CategoryId = CategoryId;
            SessionLI.EditionId = Convert.ToInt32(EditionId);

            GetContraindications.GetActiveSubstancesWithoutInteractions = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            /*          MEDICAL CONTRAINDICATIONS         */
            GetContraindications.GetICDByProductPharmaform = db.Database.SqlQuery<GetICDByProductPharmaform>("plm_spCRUDProductContraindicationsByICD @CRUDType=" + CRUD.Read + ", @ProductId = " + ProductId + ", @PharmaFormId = " + PharmaFormId + ", @divisionId=" + DivisionId + ", @categoryId=" + CategoryId + "").ToList();

            //Catálogo
            GetContraindications.GetICDs = db.Database.SqlQuery<GetICDs>("plm_spGetICDs @ParentId=NULL").ToList();

            /*          PHYSIOLOGICAL CONTRAINDICATIONS         */
            GetContraindications.GetProductPhysContraindications = db.Database.SqlQuery<GetProductPhysContraindications>("plm_spCRUDProductPhysContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + "").ToList();

            //Catálogo
            GetContraindications.GetPhysiologicalContraindications = db.Database.SqlQuery<GetPhysiologicalContraindications>("plm_spGetPhysContraindicationsCatalog @description='" + PhysDescription + "'").ToList();

            /*          PHARMACOLOGICAL CONTRAINDICATIONS           */
            //Catálogo
            GetContraindications.GetPharmaContraindications = db.Database.SqlQuery<GetPharmaContraindications>("plm_spGetPharmaContraindicationsCatalog @description='" + PharmaDescription + "'").ToList();
            GetContraindications.GetProductPharmaContraindications = db.Database.SqlQuery<ProductPharmaContraindications>("plm_spCRUDProductPharmaContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + "").ToList();

            /*          PHARMACOLOGICALGROUPS CONTRAINDICATIONS         */
            //Catálogo
            GetContraindications.GetPharmacologicalGroupsWithoutInteraction = db.Database.SqlQuery<GetPharmacologicalGroupsWithoutInteraction>("plm_spGetPharmacologicalGroupsWithoutInteraction @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + ", @groupName='" + GroupDescription + "'").ToList();
            GetContraindications.GetProductPharmaGroupInteractions = db.Database.SqlQuery<GetProductPharmaGroupInteractions>("plm_spCRUDProductPharmaGroupContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + "").ToList();

            /*          HYPERSENSIBILITIES CONTRAINDICATIONS            */
            //Catálogo
            GetContraindications.GetHypersensibilities = db.Database.SqlQuery<GetHypersensibilities>("plm_spGetHypersensibilitiesCatalog @description='" + HypersensibilityDescription + "'").ToList();
            GetContraindications.GetProductHypersensibilities = db.Database.SqlQuery<GetProductHypersensibilities>("plm_spCRUDProductHypersensibilities @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + "").ToList();

            /*          ACTIVE SUBSTANCES CONTRAINDICATIONS         */
            //Catálogo
            GetContraindications.GetActiveSubstances = db.Database.SqlQuery<GetActiveSubstances>("plm_spGetActiveSubstanceByName @description='" + ActiveSubstanceDescription + "'").ToList();
            GetContraindications.GetProductSubstanceContraindications = db.Database.SqlQuery<GetProductSubstanceContraindications>("plm_spGetProductSubstanceContraindications @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + "").ToList();

            /*          OTHER ELEMENTS CONTRAINDICATIONS            */
            //Catálogo
            GetContraindications.GetOtherElemensWithoutInteractions = db.Database.SqlQuery<GetOtherElemensWithoutInteractions>("plm_spGetOtherElemensWithoutInteractions @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @elementName='" + OtherElementsDescription + "'").ToList();
            GetContraindications.GetProductOtherInteractions = db.Database.SqlQuery<GetProductotherInteractions>("plm_spCRUDProductOtherContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + "").ToList();

            /*          COMMENTS CONTRAINDICATIONS*/
            GetContraindications.GetProductCommentContraindications = db.Database.SqlQuery<GetProductCommentContraindications>("plm_spCRUDProductCommentContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + "").ToList();

            var HTML = db.Database.SqlQuery<String>("plm_spGetHtmlContentContraindications @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + "").ToList();

            if (!string.IsNullOrEmpty(HTML[0]))
            {
                GetContraindications.HTMLContraindications = HTML[0].Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("normal", "Normal").Replace("NORMAL", "Normal").Replace("rubros-azules", "Rubros-azules");
            }
            else
            {
                GetContraindications.HTMLContraindications = "<p class=\"Normal\"><span class=\"Rubros-azules\">CONTRAINDICACIONES:</span> No Clasificado</p>";
            }
            return View(GetContraindications);
        }

        #region Medicas

        public JsonResult GetLevelTwoCICD(string ICD)
        {
            int ICDId = int.Parse(ICD);

            List<ICD> LS = db.ICD.Where(x => x.ParentId == ICDId && x.Active == true && x.Level == 2).OrderBy(x => x.ICDKey).ToList();

            List<String> LF = new List<String>();

            String Field = String.Empty;

            foreach (ICD item in LS)
            {
                var L2 = db.ICD.Where(x => x.ParentId == item.ICDId && x.Active == true && x.Level == 3).ToList();

                if (L2.LongCount() > 0)
                {
                    Field = "<span class=\"glyphicon glyphicon-plus\" id=\"Expand_" + item.ICDId + "\" onclick=\"GetLevelThreeCICD(" + item.ICDId + ")\" style=\"cursor:pointer\"></span>" +
                            "<span class=\"glyphicon glyphicon-minus\" style=\"display:none;cursor:pointer\" id=\"Collapse_" + item.ICDId + "\" onclick=\"collapselevel3(" + item.ICDId + ")\"></span>" +
                            "&nbsp;&nbsp; <input type='checkbox' value='" + item.ICDId + "' Id=\"" + item.SpanishDescription + "\" onclick=\"CheckCIE10Contraindications($(this)) \" />" +
                            "&nbsp;&nbsp; <label style=\"font-weight: bold; color: #065977;margin-bottom:5px\"><b>" + item.ICDKey + "</b></label> - <span style=\"font-weight:100\">" + item.SpanishDescription + "</span>" +
                            "<ul id=\"ListL3_" + item.ICDId + "\" style=\"list-style: none;margin-left:50px\"></ul>";
                }
                else
                {
                    Field = "<input type='checkbox' value='" + item.ICDId + "' Id=\"" + item.SpanishDescription + "\" onclick=\"CheckCIE10Contraindications($(this)) \" />" +
                        //"<span class=\"glyphicon glyphicon-plus\" id=\"Expand_" + item.ICDId + "\" onclick=\"GetLevelTwoICD(" + item.ICDId + ")\" style=\"cursor:pointer\"></span>" +
                            "&nbsp;&nbsp; <label style=\"font-weight: bold; color: #065977;margin-bottom:5px\"><b>" + item.ICDKey + "</b></label> - <span style=\"font-weight:100\">" + item.SpanishDescription + "</span>";
                }

                LF.Add(Field);
            }

            return Json(LF, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLevelThreeCICD(string ICD)
        {
            int ICDId = int.Parse(ICD);

            List<ICD> LS = db.ICD.Where(x => x.ParentId == ICDId && x.Active == true && x.Level == 3).OrderBy(x => x.ICDKey).ToList();

            List<String> LF = new List<String>();

            String Field = String.Empty;

            foreach (ICD item in LS)
            {
                var L2 = db.ICD.Where(x => x.ParentId == item.ICDId && x.Active == true && x.Level == 4).ToList();

                if (L2.LongCount() > 0)
                {
                    Field = "<span class=\"glyphicon glyphicon-plus\" id=\"Expand_" + item.ICDId + "\" onclick=\"GetLevelTwoCICD(" + item.ICDId + ")\" style=\"cursor:pointer\"></span>" +
                            "<span class=\"glyphicon glyphicon-minus\" style=\"display:none;cursor:pointer\" id=\"Collapse_" + item.ICDId + "\" onclick=\"collapselevel4(" + item.ICDId + ")\"></span>" +
                            "&nbsp;&nbsp; <input type='checkbox' value='" + item.ICDId + "' Id=\"" + item.SpanishDescription + "\" onclick=\"CheckCIE10Contraindications($(this)) \" />" +
                            "&nbsp;&nbsp; <label style=\"font-weight: bold; color: #065977;margin-bottom:5px\"><b>" + item.ICDKey + "</b></label> - <span style=\"font-weight:100\">" + item.SpanishDescription + "</span>" +
                            "<ul id=\"ListL3_" + item.ICDId + "\" style=\"list-style: none;margin-left:30px\"></ul>";
                }
                else
                {
                    Field = "<input type='checkbox' value='" + item.ICDId + "' Id=\"" + item.SpanishDescription + "\" onclick=\"CheckCIE10Contraindications($(this)) \" />" +
                        //"<span class=\"glyphicon glyphicon-plus\" id=\"Expand_" + item.ICDId + "\" onclick=\"GetLevelTwoICD(" + item.ICDId + ")\" style=\"cursor:pointer\"></span>" +
                            "&nbsp;&nbsp; <label style=\"font-weight: bold; color: #065977;margin-bottom:5px\"><b>" + item.ICDKey + "</b></label> - <span style=\"font-weight:100\">" + item.SpanishDescription + "</span>";
                }

                LF.Add(Field);
            }

            return Json(LF, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPhysiologicalContraindications(string Division, string Category, string PharmaForm, string Product, string ActiveSubstance)
        {
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);
            int PharmaFormId = int.Parse(PharmaForm);
            int ProductId = int.Parse(Product);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPhysiologicalContraindicationsAsoc(string Division, string Category, string PharmaForm, string Product, string ActiveSubstance, string Edition)
        {
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);
            int PharmaFormId = int.Parse(PharmaForm);
            int ProductId = int.Parse(Product);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int EditionId = int.Parse(Edition);

            List<PhysiologicalContraindications> LS = getData.GetPhysiologicalContraindicationsAsoc(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId, EditionId);

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public static string GetContraindicationText(String Interaction)
        {
            getData getData = new Models.getData();

            Interaction = System.Text.RegularExpressions.Regex.Replace(Interaction, "<.*?>", String.Empty).Replace("INTERACCIONES MEDICAMENTOSAS Y DE OTRO G&Eacute;NERO: ", "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNode textNode = doc.CreateElement("title");
            textNode.InnerHtml = Interaction;
            Interaction = textNode.InnerText;

            Interaction = getData.ReplaceHTMLCodes(Interaction);

            return Interaction;
        }

        public JsonResult CheckSubstancesContraIndications(string Product, string Category, string Division, string PharmaForm, string PharmacologicalGroup, string ICD)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int ICDId = int.Parse(PharmacologicalGroup);

            List<ICD> LICDS = (from lls in db.ICD
                               join pic in db.ProductICDContraindications
                                   on lls.ICDId equals pic.ICDId
                               where pic.ProductId == ProductId &&
                                     pic.PharmaFormId == PharmaFormId &&
                                     pic.CategoryId == CategoryId &&
                                     pic.DivisionId == DivisionId &&
                                     lls.ParentId == ICDId
                               select lls).ToList();

            if (LICDS.LongCount() > 0)
            {
                var ret = new { Data = "AsocNode" };

                return Json(ret, JsonRequestBehavior.AllowGet);
            }

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<ProductContraindicationsByICD> LS1 = db.Database.SqlQuery<ProductContraindicationsByICD>("plm_spCRUDProductContraindicationsByICD @CRUDType=" + CRUD.Read + ",@categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @medicalICDContraindicationId=" + ICDId + "").ToList();

                if (LS1.LongCount() > 0)
                {
                    foreach (ProductContraindicationsByICD item in LS1)
                    {
                        int ActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == ActiveSubstanceId);

                        LS.Remove(res);
                    }
                }

                if (LS.LongCount() == 0)
                {
                    var result = new { Data = "Empty", Items = LS };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Data = true, Items = LS };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //AL
        public JsonResult SaveCIE10Contraindications(String List, string size, string Division, string Category)
        {
            int SizeId = int.Parse(size);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            string ICDKey = string.Empty;
            string SpanishDescription = string.Empty;
            string ICDKeyParent = string.Empty;
            bool FlagParent = false;

            dynamic item = JsonConvert.DeserializeObject(List);

            for (var i = 0; i <= (SizeId - 1); i++)
            {
                int ProductId = Convert.ToInt32(item[i]["PId"]);
                int PharmaFormId = Convert.ToInt32(item[i]["PFId"]);
                int ActiveSubstanceId = Convert.ToInt32(item[i]["ASId"]);
                int ICDId = Convert.ToInt32(item[i]["ICDId"]);

                List<ICD> LICD = db.ICD.Where(x => x.ICDId == ICDId).ToList();

                if (LICD.LongCount() > 0)
                {
                    ICDKey = LICD[0].ICDKey;
                }

                List<ICD> LICDS = (from ic in db.ICD
                                   join pic in db.ProductICDContraindications
                                       on ic.ParentId equals pic.ICDId
                                   where ic.ICDId == ICDId &&
                                            pic.ProductId == ProductId &&
                                            pic.PharmaFormId == PharmaFormId &&
                                            pic.CategoryId == CategoryId &&
                                            pic.DivisionId == DivisionId &&
                                            pic.ActiveSubstanceId == ActiveSubstanceId
                                   select ic).ToList();

                if (LICDS.LongCount() > 0)
                {
                    int ParentId = Convert.ToInt32(LICDS[0].ParentId);

                    List<ICD> LICDP = db.ICD.Where(x => x.ICDId == ParentId).ToList();

                    SpanishDescription = LICDP[0].SpanishDescription;
                    ICDKeyParent = LICDP[0].ICDKey;

                    var _result = db.Database.ExecuteSqlCommand("plm_spCRUDProductContraindicationsByICD @CRUDType=" + CRUD.Delete + ",@categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @medicalICDContraindicationId=" + ParentId + ", @activeSubstanceId=" + ActiveSubstanceId + "");

                    ActivityLog.CRUDProductContraindicationsByICD(ProductId, PharmaFormId, DivisionId, CategoryId, ParentId, ActiveSubstanceId, 4);

                    FlagParent = true;
                }

                Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                if ((ActiveSubstanceId != 0) && (CategoryId != 0) && (DivisionId != 0) && (PharmaFormId != 0) && (ProductId != 0) && (ICDId != 0))
                {
                    try
                    {
                        var check = db.Database.SqlQuery<ProductICD>("plm_spCRUDProductICD @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ",@pharmaformID=" + PharmaFormId + ", @icdId=" + ICDId + "").ToList();

                        if (check.LongCount() == 1)
                        {
                            var ret = new { Data = "_errorInd", Node = (LICD[0].ICDKey + " - " + LICD[0].SpanishDescription) };

                            return Json(ret, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            int ParentId = Convert.ToInt32(LICD[0].ParentId);

                            List<ICD> LS1 = db.ICD.Where(x => x.ICDId == ParentId).ToList();

                            int ICDIdParent = LS1[0].ICDId;

                            var check1 = db.Database.SqlQuery<ProductICD>("plm_spCRUDProductICD @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ",@pharmaformID=" + PharmaFormId + ", @icdId=" + ICDIdParent + "").ToList();

                            if (check1.LongCount() == 1)
                            {
                                var ret = new { Data = "_errorIndParent", Element = (LS1[0].ICDKey + " - " + LS1[0].SpanishDescription), Node = (LICD[0].ICDKey + " - " + LICD[0].SpanishDescription) };

                                return Json(ret, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var _result = db.Database.SqlQuery<int>("plm_spCRUDProductContraindicationsByICD @CRUDType=" + CRUD.Create + ",@categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @medicalICDContraindicationId=" + ICDId + ", @activeSubstanceId=" + ActiveSubstanceId + "").ToList();

                                if (_result[0] == 1)
                                {
                                    ActivityLog.CRUDProductContraindicationsByICD(ProductId, PharmaFormId, DivisionId, CategoryId, ICDId, ActiveSubstanceId, 1);
                                }
                            }
                        }
                        if (FlagParent == true)
                        {
                            var res = new { Data = "ReplaceParentByNode", Parent = SpanishDescription, Node = LICDS[0].SpanishDescription, ICDKeyParent = ICDKeyParent, ICDKeyNode = LICDS[0].ICDKey };

                            return Json(res, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;

                        var res = new { Data = "Error", Message = msg };

                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            var rets = new { Data = true, Message = "" };

            return Json(rets, JsonRequestBehavior.AllowGet);
        }

        //AL
        public JsonResult DeleteICDContraindications(string Product, string Category, string Division, string PharmaForm, string ActiveSubstance, string ICD)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int ICDId = int.Parse(ICD);

            var _result = db.Database.ExecuteSqlCommand("plm_spCRUDProductContraindicationsByICD @CRUDType=" + CRUD.Delete + ",@categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @medicalICDContraindicationId=" + ICDId + ", @activeSubstanceId=" + ActiveSubstanceId + "");

            ActivityLog.CRUDProductContraindicationsByICD(ProductId, PharmaFormId, DivisionId, CategoryId, ICDId, ActiveSubstanceId, 4);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //AL
        public JsonResult NoContraindications(string Product, string Category, string Division, string PharmaForm)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int StatusId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NotInteractions"]);
            int DeleteCM = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DeleteCM"]);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                foreach (GetActiveSubstancesWithoutInteractions item in LS)
                {
                    var res = db.Database.SqlQuery<int>("plm_spCRUDIppaProductContraindications @CRUDType=" + CRUD.Create + ", @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + ", @substanceId=" + item.ActiveSubstanceId + ", @statusCM=" + StatusId + ",@deleteCM =" + DeleteCM + "").ToList();

                    ActivityLog.CRUDProductSubstanceContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, Convert.ToInt32(item.ActiveSubstanceId), 4);
                    ActivityLog.CRUDProductICDContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, Convert.ToInt32(item.ActiveSubstanceId), 4);
                    ActivityLog.CRUDProductPharmaGroupContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, Convert.ToInt32(item.ActiveSubstanceId), 4);
                    ActivityLog.CRUDProductPhysiologicalContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, Convert.ToInt32(item.ActiveSubstanceId), 4);
                    ActivityLog.CRUDProductPharmacologicalContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, Convert.ToInt32(item.ActiveSubstanceId), 4);
                    ActivityLog.CRUDProductHypersensibilityContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, Convert.ToInt32(item.ActiveSubstanceId), 4);
                    ActivityLog.CRUDProductOtherContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, Convert.ToInt32(item.ActiveSubstanceId), 4);
                    ActivityLog.CRUDProductCommentContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, Convert.ToInt32(item.ActiveSubstanceId), "", 4);

                    if (res[0] == 1)
                    {
                        ActivityLog.CRUDIPPAProductContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, StatusId, Convert.ToInt32(item.ActiveSubstanceId), 1);
                    }

                    if (res[0] == 2)
                    {
                        ActivityLog.CRUDIPPAProductContraindications(ProductId, PharmaFormId, DivisionId, CategoryId, StatusId, Convert.ToInt32(item.ActiveSubstanceId), 2);
                    }


                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Fisiologicas

        public JsonResult CheckPhysiologicalContraindications(string Product, string Category, string Division, string PharmaForm, string PharmacologicalGroup)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            //int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int PhysContraindicationId = int.Parse(PharmacologicalGroup);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductPhysContraindications> LSS = db.Database.SqlQuery<GetProductPhysContraindications>("plm_spCRUDProductPhysContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @physContraindicationId=" + PhysContraindicationId + "").ToList();

                if (LSS.LongCount() > 0)
                {
                    foreach (GetProductPhysContraindications item in LSS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);

                    }
                }

                if (LS.LongCount() == 1)
                {
                    int ActiveSubstanceId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                    String response = Operations.SavePhysContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PhysContraindicationId, ActiveSubstanceId, CRUD.Create);

                    var results = new { Data = "Ok" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                if (LS.LongCount() == 0)
                {
                    var results = new { Data = "_notdata" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //AL
        public JsonResult SavePhysiologicalContraindications(String List, string size)
        {
            int SizeId = int.Parse(size);

            dynamic item = JsonConvert.DeserializeObject(List);

            List<String> LS = new List<String>();

            for (var i = 0; i <= SizeId - 1; i++)
            {
                int DivisionId = Convert.ToInt32(item[i]["Division"]);
                int CategoryId = Convert.ToInt32(item[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(item[i]["PharmaForm"]);

                int ProductId = Convert.ToInt32(item[i]["Product"]);
                int ActiveSubstanceId = Convert.ToInt32(item[i]["SubstanceInteract"]);
                int PhysContraindicationId = Convert.ToInt32(item[i]["ActiveSubstance"]);


                Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                String response = Operations.SavePhysContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PhysContraindicationId, ActiveSubstanceId, CRUD.Create);

                if (response != "Ok")
                {
                    LS.Add(response.ToUpper());
                }
            }

            if (LS.LongCount() > 0)
            {
                LS = LS.OrderBy(x => x).ToList();

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        //AL
        public JsonResult DeletePhysiologicalContraindications(string Product, string Category, string Division, string PharmaForm, string ActiveSubstance, string PhysContraindication)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int PhysContraindicationId = int.Parse(PhysContraindication);

            String response = Operations.SavePhysContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PhysContraindicationId, ActiveSubstanceId, CRUD.Delete);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Farmacologicas

        public JsonResult CheckPharmacologicalContraindications(string Product, string Category, string Division, string PharmaForm, string Pharmacological)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int PharmacologicalContraindicationId = int.Parse(Pharmacological);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductPharmaContraindications> LSS = db.Database.SqlQuery<GetProductPharmaContraindications>("plm_spCRUDProductPharmaContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @pharmaContraindicationId=" + PharmacologicalContraindicationId + "").ToList();

                if (LSS.LongCount() > 0)
                {
                    foreach (GetProductPharmaContraindications item in LSS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);

                    }
                }

                if (LS.LongCount() == 1)
                {
                    int ActiveSubstanceId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                    String response = Operations.SavePharmaContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PharmacologicalContraindicationId, ActiveSubstanceId, CRUD.Create);

                    var results = new { Data = "Ok" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                if (LS.LongCount() == 0)
                {
                    var results = new { Data = "_notdata" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //AL
        public JsonResult SavePharmacologicalContraindications(String List, string size)
        {
            int SizeId = int.Parse(size);

            dynamic item = JsonConvert.DeserializeObject(List);

            List<String> LS = new List<String>();

            for (var i = 0; i <= SizeId - 1; i++)
            {
                int DivisionId = Convert.ToInt32(item[i]["Division"]);
                int CategoryId = Convert.ToInt32(item[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(item[i]["PharmaForm"]);

                int ProductId = Convert.ToInt32(item[i]["Product"]);
                int ActiveSubstanceId = Convert.ToInt32(item[i]["SubstanceInteract"]);
                int PhysContraindicationId = Convert.ToInt32(item[i]["ActiveSubstance"]);


                Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                String response = Operations.SavePharmaContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PhysContraindicationId, ActiveSubstanceId, CRUD.Create);

                if (response != "Ok")
                {
                    LS.Add(response.ToUpper());
                }
            }

            if (LS.LongCount() > 0)
            {
                LS = LS.OrderBy(x => x).ToList();

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        //AL
        public JsonResult DeletePharmacologicalContraindications(string Product, string Category, string Division, string PharmaForm, string ActiveSubstance, string PhysContraindication)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int PharmaContraindicationId = int.Parse(PhysContraindication);

            String response = Operations.SavePharmaContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PharmaContraindicationId, ActiveSubstanceId, CRUD.Delete);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Hipersensibilidad

        public JsonResult CheckHyperContraindications(string Product, string Category, string Division, string PharmaForm, string Pharmacological)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int PharmacologicalContraindicationId = int.Parse(Pharmacological);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductHypersensibilities> LSS = db.Database.SqlQuery<GetProductHypersensibilities>("plm_spCRUDProductHypersensibilities @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @hypersensibilityId=" + PharmacologicalContraindicationId + "").ToList();

                if (LSS.LongCount() > 0)
                {
                    foreach (GetProductHypersensibilities item in LSS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);

                    }
                }

                if (LS.LongCount() == 1)
                {
                    int ActiveSubstanceId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                    String response = Operations.SaveHyperContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PharmacologicalContraindicationId, ActiveSubstanceId, CRUD.Create);

                    var results = new { Data = "Ok" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                if (LS.LongCount() == 0)
                {
                    var results = new { Data = "_notdata" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveHyperContraindications(String List, string size)
        {
            int SizeId = int.Parse(size);

            dynamic item = JsonConvert.DeserializeObject(List);

            List<String> LS = new List<String>();

            for (var i = 0; i <= SizeId - 1; i++)
            {
                int DivisionId = Convert.ToInt32(item[i]["Division"]);
                int CategoryId = Convert.ToInt32(item[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(item[i]["PharmaForm"]);

                int ProductId = Convert.ToInt32(item[i]["Product"]);
                int ActiveSubstanceId = Convert.ToInt32(item[i]["SubstanceInteract"]);
                int PhysContraindicationId = Convert.ToInt32(item[i]["ActiveSubstance"]);


                Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                String response = Operations.SaveHyperContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PhysContraindicationId, ActiveSubstanceId, CRUD.Create);

                if (response != "Ok")
                {
                    LS.Add(response.ToUpper());
                }
            }

            if (LS.LongCount() > 0)
            {
                LS = LS.OrderBy(x => x).ToList();

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteHyperContraindications(string Product, string Category, string Division, string PharmaForm, string ActiveSubstance, string PhysContraindication)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int PharmaContraindicationId = int.Parse(PhysContraindication);

            String response = Operations.SaveHyperContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PharmaContraindicationId, ActiveSubstanceId, CRUD.Delete);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Grupos Farmacologicos

        public JsonResult CheckPharmacologicalGroupsContraindications(string Product, string Category, string Division, string PharmaForm, string Pharmacological)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int PharmacologicalContraindicationId = int.Parse(Pharmacological);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductHypersensibilities> LSS = db.Database.SqlQuery<GetProductHypersensibilities>("plm_spCRUDProductPharmaGroupContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @pharmaGroupId=" + PharmacologicalContraindicationId + "").ToList();

                if (LSS.LongCount() > 0)
                {
                    foreach (GetProductHypersensibilities item in LSS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);
                    }
                }

                if (LS.LongCount() == 1)
                {
                    int ActiveSubstanceId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                    String response = Operations.SaveProductPharmaGroupContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PharmacologicalContraindicationId, ActiveSubstanceId, CRUD.Create);

                    var results = new { Data = "Ok" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                if (LS.LongCount() == 0)
                {
                    var results = new { Data = "_notdata" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddProductPharmacologicalGroupsContraindications(string Size, String SubstanceInteractions)
        {
            List<ReturnAddedInteractions> LR = new List<ReturnAddedInteractions>();
            ReturnAddedInteractions r = new ReturnAddedInteractions();

            int SizeId = int.Parse(Size);

            dynamic Array = JsonConvert.DeserializeObject(SubstanceInteractions);

            for (var i = 0; i <= (SizeId - 1); i++)
            {
                int SubstanceInteractId = Convert.ToInt32(Array[i]["SubstanceInteract"]);
                int DivisionId = Convert.ToInt32(Array[i]["Division"]);
                int CategoryId = Convert.ToInt32(Array[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(Array[i]["PharmaForm"]);
                int ProductId = Convert.ToInt32(Array[i]["Product"]);
                int PharmaGroupId = Convert.ToInt32(Array[i]["ActiveSubstance"]);

                Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId);

                Operations.IPPAProductInteractions(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                string result = Operations.SaveProductPharmaGroupContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PharmaGroupId, SubstanceInteractId, CRUD.Create);

                if (result != "Ok")
                {
                    r = new ReturnAddedInteractions();

                    var ActiveSubstance = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == SubstanceInteractId).ToList();

                    string Substance = ActiveSubstance[0].Description;

                    r.ActiveSubstance = Substance;
                    r.Element = result;

                    LR.Add(r);
                }
            }

            if (LR.LongCount() == SizeId)
            {
                return Json(LR, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteProductPharmacologicalGroupsContraindications(string ActiveSubstance, string SubstanceInteraction, string Product, string Category, string Division, string PharmaForm)
        {
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int PharmaGroupId = int.Parse(SubstanceInteraction);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);

            Operations.SaveProductPharmaGroupContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PharmaGroupId, ActiveSubstanceId, CRUD.Delete);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Sustancias Activas

        public JsonResult CheckActiveSubstancesContraindications(string Product, string Category, string Division, string PharmaForm, string Pharmacological)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int SubstanceInteractId = int.Parse(Pharmacological);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductSubstanceContraindications> LSS = db.Database.SqlQuery<GetProductSubstanceContraindications>("plm_spGetProductSubstanceContraindications @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + ", @susbtanceId=" + SubstanceInteractId + ",@TypeSelect=0").ToList();

                if (LSS.LongCount() > 0)
                {
                    foreach (GetProductSubstanceContraindications item in LSS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);
                    }
                }

                if (LS.LongCount() == 1)
                {
                    int ActiveSubstanceId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                    String response = Operations.SaveProductSubstanceContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, ActiveSubstanceId, CRUD.Create);

                    var results = new { Data = "Ok" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                if (LS.LongCount() == 0)
                {
                    var results = new { Data = "_notdata" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveActiveSubstancesContraindications(String List, string Size)
        {
            List<ReturnAddedInteractions> LR = new List<ReturnAddedInteractions>();
            ReturnAddedInteractions r = new ReturnAddedInteractions();

            int SizeId = int.Parse(Size);

            dynamic Array = JsonConvert.DeserializeObject(List);

            for (var i = 0; i <= (SizeId - 1); i++)
            {
                int SubstanceInteractId = Convert.ToInt32(Array[i]["SubstanceInteract"]);
                int DivisionId = Convert.ToInt32(Array[i]["Division"]);
                int CategoryId = Convert.ToInt32(Array[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(Array[i]["PharmaForm"]);
                int ProductId = Convert.ToInt32(Array[i]["Product"]);
                int ActiveSubstanceId = Convert.ToInt32(Array[i]["ActiveSubstance"]);

                Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId);

                Operations.IPPAProductContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                string result = Operations.SaveProductSubstanceContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId, SubstanceInteractId, CRUD.Create);

                if (result != "Ok")
                {
                    r = new ReturnAddedInteractions();

                    var ActiveSubstance = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == SubstanceInteractId).ToList();

                    string Substance = ActiveSubstance[0].Description;

                    r.ActiveSubstance = Substance;
                    r.Element = result;

                    LR.Add(r);
                }
            }

            if (LR.LongCount() == SizeId)
            {
                return Json(LR, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteProductActiveSubstancesContraindications(string ActiveSubstance, string SubstanceInteraction, string Product, string Category, string Division, string PharmaForm)
        {
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int PharmaGroupId = int.Parse(SubstanceInteraction);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);

            Operations.SaveProductSubstanceContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, PharmaGroupId, ActiveSubstanceId, CRUD.Delete);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Otros Elementos

        public JsonResult CheckOtherElementsContraindications(string Product, string Category, string Division, string PharmaForm, string Element)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int ElementId = int.Parse(Element);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductotherInteractions> LSS = db.Database.SqlQuery<GetProductotherInteractions>("plm_spCRUDProductOtherContraindications @CRUDType=" + CRUD.Read + ", @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + ", @elementId=" + ElementId + "").ToList();

                if (LSS.LongCount() > 0)
                {
                    foreach (GetProductotherInteractions item in LSS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);
                    }
                }

                if (LS.LongCount() == 1)
                {
                    int ActiveSubstanceId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                    String response = Operations.SaveProductOtherContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, ElementId, ActiveSubstanceId, CRUD.Create);

                    var results = new { Data = "Ok" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                if (LS.LongCount() == 0)
                {
                    var results = new { Data = "_notdata" };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveOtherElementsContraindications(String List, string Size)
        {
            List<ReturnAddedInteractions> LR = new List<ReturnAddedInteractions>();
            ReturnAddedInteractions r = new ReturnAddedInteractions();

            int SizeId = int.Parse(Size);

            dynamic Array = JsonConvert.DeserializeObject(List);

            for (var i = 0; i <= (SizeId - 1); i++)
            {
                int SubstanceInteractId = Convert.ToInt32(Array[i]["SubstanceInteract"]);
                int DivisionId = Convert.ToInt32(Array[i]["Division"]);
                int CategoryId = Convert.ToInt32(Array[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(Array[i]["PharmaForm"]);
                int ProductId = Convert.ToInt32(Array[i]["Product"]);
                int ActiveSubstanceId = Convert.ToInt32(Array[i]["ActiveSubstance"]);

                Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId);

                Operations.IPPAProductContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                string result = Operations.SaveProductOtherContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId, SubstanceInteractId, CRUD.Create);

                if (result != "Ok")
                {
                    r = new ReturnAddedInteractions();

                    var ActiveSubstance = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == SubstanceInteractId).ToList();

                    string Substance = ActiveSubstance[0].Description;

                    r.ActiveSubstance = Substance;
                    r.Element = result;

                    LR.Add(r);
                }
            }

            if (LR.LongCount() == SizeId)
            {
                return Json(LR, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteOtherElementsContraindications(string ActiveSubstance, string Element, string Product, string Category, string Division, string PharmaForm)
        {
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int ElementId = int.Parse(Element);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);

            Operations.SaveProductOtherContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, ElementId, ActiveSubstanceId, CRUD.Delete);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Comentarios

        public JsonResult CheckCommentsContraindications(string Product)
        {
            int ProductId = int.Parse(Product);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //AL
        public JsonResult SaveCommentsContraindications(String List, string Size)
        {
            List<ReturnAddedInteractions> LR = new List<ReturnAddedInteractions>();
            ReturnAddedInteractions r = new ReturnAddedInteractions();

            int SizeId = int.Parse(Size);

            dynamic Array = JsonConvert.DeserializeObject(List);

            for (var i = 0; i <= (SizeId - 1); i++)
            {
                string Comments = Array[i]["Comment"].ToString();
                int DivisionId = Convert.ToInt32(Array[i]["Division"]);
                int CategoryId = Convert.ToInt32(Array[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(Array[i]["PharmaForm"]);
                int ProductId = Convert.ToInt32(Array[i]["Product"]);
                int ActiveSubstanceId = Convert.ToInt32(Array[i]["ActiveSubstance"]);

                Operations.CheckProductContraindication(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId);

                string result = Operations.SaveProductCommentContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, Comments, ActiveSubstanceId, CRUD.Create);
            }

            if (LR.LongCount() == SizeId)
            {
                return Json(LR, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        //AL
        public JsonResult DeleteCommentsContraindications(string ActiveSubstance, string ProductComment, string Product, string Category, string Division, string PharmaForm)
        {
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int ProductCommentId = int.Parse(ProductComment);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);

            Operations.SaveProductCommentContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, ProductComment, ActiveSubstanceId, CRUD.Create);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //AL
        public JsonResult DeleteAllCommentsContraindications(string Product, string Category, string Division, string PharmaForm)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);

            Operations.DeleteAllCommentsContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, CRUD.Delete);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult PrintContraindications(int? ProductId, int? DivisionId, int? CategoryId, int? PharmaFormId, int? EditionId, int? CountryId)
        {
            var list = db.Database.SqlQuery<String>("plm_spGetHtmlContentContraindications @productid=" + ProductId + ",@divisionid=" + DivisionId + ",@categoryid=" + CategoryId + ",@pharmaformid=" + PharmaFormId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetContraindicationsByKey.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetReportInteractions> cm = new List<GetReportInteractions>();

            GetReportInteractions cmd = new GetReportInteractions();

            if (list.LongCount() > 0)
            {
                List<Categories> LC = db.Categories.Where(x => x.CategoryId == CategoryId).ToList();
                List<PharmaceuticalForms> LPF = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();
                List<Products> LP = db.Products.Where(x => x.ProductId == ProductId).ToList();
                List<Divisions> LD = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();
                List<Editions> LE = db.Editions.Where(x => x.EditionId == EditionId).ToList();
                List<Countries> L = db.Countries.Where(x => x.CountryId == CountryId).ToList();

                string Interaction = string.Empty;

                if (!string.IsNullOrEmpty(list[0]))
                {
                    Interaction = GetContraindicationsText(list[0]);
                }
                else
                {
                    Interaction = "\n\n No Clasificado";
                }

                cmd.HTMLContent = Interaction;
                cmd.CategoryName = LC[0].Description;
                cmd.PharmaForm = LPF[0].Description;
                cmd.Brand = LP[0].Brand;
                cmd.DivisioName = LD[0].Description;
                cmd.NumberEdition = LE[0].NumberEdition;
                cmd.CountryName = L[0].CountryName;

                cm.Add(cmd);
            }

            ReportDataSource rd = new ReportDataSource("GetInteractions", cm);
            lr.DataSources.Add(rd);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }

        public static string GetContraindicationsText(String Contraindicacion)
        {
            getData getData = new Models.getData();

            Contraindicacion = System.Text.RegularExpressions.Regex.Replace(Contraindicacion, "<.*?>", String.Empty).Replace("CONTRAINDICACIONES: ", "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNode textNode = doc.CreateElement("title");
            textNode.InnerHtml = Contraindicacion;
            Contraindicacion = textNode.InnerText;

            Contraindicacion = getData.ReplaceHTMLCodes(Contraindicacion);

            return Contraindicacion;
        }

        public JsonResult GetIndicationsByContraindications(string Product, string PharmaForm, string Edition)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int EditionId = 1;

            List<GetICDByProductPharmaform> LS = db.Database.SqlQuery<GetICDByProductPharmaform>("plm_spGetICDByProductPharmaform @ProductId = " + ProductId + ", @PharmaFormId = " + PharmaFormId + ", @EditionId=" + EditionId + "").OrderBy(x => x.ICDKey).ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CIE10

        public ActionResult CIE(int? ProductId, int? PharmaFormId, int? CategoryId, int? EditionId)
        {
            SessionLI SessionLI = (SessionLI)Session["SessionLI"];

            if ((!Request.IsAuthenticated) || (ProductId == null) || (PharmaFormId == null) || (CategoryId == null) || (SessionLI == null) || (EditionId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionLI.ProductId = ProductId;
            SessionLI.PharmaFormId = PharmaFormId;
            SessionLI.CategoryId = CategoryId;
            SessionLI.EditionId = Convert.ToInt32(EditionId);

            List<GetICDByProductPharmaform> LS = db.Database.SqlQuery<GetICDByProductPharmaform>("plm_spGetICDByProductPharmaform @ProductId = " + ProductId + ", @PharmaFormId = " + PharmaFormId + ", @EditionId=" + EditionId + "").OrderBy(x => x.ICDKey).ToList();

            return View(LS);
        }

        public JsonResult GetLevelTwoICD(string ICD)
        {
            int ICDId = int.Parse(ICD);

            List<ICD> LS = db.ICD.Where(x => x.ParentId == ICDId && x.Active == true && x.Level == 2).OrderBy(x => x.ICDKey).ToList();

            List<String> LF = new List<String>();

            String Field = String.Empty;

            foreach (ICD item in LS)
            {
                var L2 = db.ICD.Where(x => x.ParentId == item.ICDId && x.Active == true && x.Level == 3).ToList();

                if (L2.LongCount() > 0)
                {
                    Field = "<span class=\"glyphicon glyphicon-plus\" id=\"Expand_" + item.ICDId + "\" onclick=\"GetLevelThreeICD(" + item.ICDId + ")\" style=\"cursor:pointer\"></span>" +
                            "<span class=\"glyphicon glyphicon-minus\" style=\"display:none;cursor:pointer\" id=\"Collapse_" + item.ICDId + "\" onclick=\"collapselevel3(" + item.ICDId + ")\"></span>" +
                            "&nbsp;&nbsp; <input type='checkbox' value='" + item.ICDId + "' onclick=\"AddCIE10($(this)) \" />" +
                            "&nbsp;&nbsp; <label style=\"font-weight: bold; color: #065977;margin-bottom:5px\"><b>" + item.ICDKey + "</b></label> - <span style=\"font-weight:100\">" + item.SpanishDescription + "</span>" +
                            "<ul id=\"ListL3_" + item.ICDId + "\" style=\"list-style: none;margin-left:50px\"></ul>";
                }
                else
                {
                    Field = "<input type='checkbox' value='" + item.ICDId + "' onclick=\"AddCIE10($(this)) \" />" +
                        //"<span class=\"glyphicon glyphicon-plus\" id=\"Expand_" + item.ICDId + "\" onclick=\"GetLevelTwoICD(" + item.ICDId + ")\" style=\"cursor:pointer\"></span>" +
                            "&nbsp;&nbsp; <label style=\"font-weight: bold; color: #065977;margin-bottom:5px\"><b>" + item.ICDKey + "</b></label> - <span style=\"font-weight:100\">" + item.SpanishDescription + "</span>";
                }

                LF.Add(Field);
            }

            return Json(LF, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLevelThreeICD(string ICD)
        {
            int ICDId = int.Parse(ICD);

            List<ICD> LS = db.ICD.Where(x => x.ParentId == ICDId && x.Active == true && x.Level == 3).OrderBy(x => x.ICDKey).ToList();

            List<String> LF = new List<String>();

            String Field = String.Empty;

            foreach (ICD item in LS)
            {
                var L2 = db.ICD.Where(x => x.ParentId == item.ICDId && x.Active == true && x.Level == 4).ToList();

                if (L2.LongCount() > 0)
                {
                    Field = "<span class=\"glyphicon glyphicon-plus\" id=\"Expand_" + item.ICDId + "\" onclick=\"GetLevelTwoICD(" + item.ICDId + ")\" style=\"cursor:pointer\"></span>" +
                            "<span class=\"glyphicon glyphicon-minus\" style=\"display:none;cursor:pointer\" id=\"Collapse_" + item.ICDId + "\" onclick=\"collapselevel4(" + item.ICDId + ")\"></span>" +
                            "&nbsp;&nbsp; <input type='checkbox' value='" + item.ICDId + "' onclick=\"AddCIE10($(this)) \" />" +
                            "&nbsp;&nbsp; <label style=\"font-weight: bold; color: #065977;margin-bottom:5px\"><b>" + item.ICDKey + "</b></label> - <span style=\"font-weight:100\">" + item.SpanishDescription + "</span>" +
                            "<ul id=\"ListL3_" + item.ICDId + "\" style=\"list-style: none;margin-left:30px\"></ul>";
                }
                else
                {
                    Field = "<input type='checkbox' value='" + item.ICDId + "' onclick=\"AddCIE10($(this)) \" />" +
                        //"<span class=\"glyphicon glyphicon-plus\" id=\"Expand_" + item.ICDId + "\" onclick=\"GetLevelTwoICD(" + item.ICDId + ")\" style=\"cursor:pointer\"></span>" +
                            "&nbsp;&nbsp; <label style=\"font-weight: bold; color: #065977;margin-bottom:5px\"><b>" + item.ICDKey + "</b></label> - <span style=\"font-weight:100\">" + item.SpanishDescription + "</span>";
                }

                LF.Add(Field);
            }

            return Json(LF, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCIE10(string PharmaForm, string Product, string ICD)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int ICDId = int.Parse(ICD);

            List<ICD> LICDS = (from lls in db.ICD
                               join pic in db.ProductICD
                                   on lls.ICDId equals pic.ICDId
                               where pic.ProductId == ProductId &&
                                     pic.PharmaFormId == PharmaFormId &&
                                     lls.ParentId == ICDId
                               select lls).ToList();

            if (LICDS.LongCount() > 0)
            {
                var ret = new { Data = "AsocNode" };

                return Json(ret, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveCIE10(String List, string size, string Category, string Division)
        {
            int SizeId = int.Parse(size);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            bool FlagParent = false;
            string SpanishDescription = string.Empty;
            string ICDKeyParent = string.Empty;

            dynamic item = JsonConvert.DeserializeObject(List);


            List<String> LS = new List<String>();

            for (var i = 0; i <= SizeId - 1; i++)
            {
                int ICDId = Convert.ToInt32(item[i]["TId"]);
                int ProductId = Convert.ToInt32(item[i]["PId"]);
                int PharmaFormId = Convert.ToInt32(item[i]["PFId"]);

                List<ICD> LICDS = (from ic in db.ICD
                                   join pic in db.ProductICD
                                       on ic.ParentId equals pic.ICDId
                                   where ic.ICDId == ICDId &&
                                            pic.ProductId == ProductId &&
                                            pic.PharmaFormId == PharmaFormId
                                   select ic).ToList();

                if (LICDS.LongCount() > 0)
                {
                    int ParentId = Convert.ToInt32(LICDS[0].ParentId);

                    List<ICD> LICDP = db.ICD.Where(x => x.ICDId == ParentId).ToList();

                    SpanishDescription = LICDP[0].SpanishDescription;
                    ICDKeyParent = LICDP[0].ICDKey;

                    String responses = Operations.SaveAddCIE10(ParentId, ProductId, PharmaFormId, "Delete");

                    FlagParent = true;
                }

                List<ICD> LICD = db.ICD.Where(x => x.ICDId == ICDId).ToList();

                List<ProductContraindicationsByICD> LS1 = db.Database.SqlQuery<ProductContraindicationsByICD>("plm_spCRUDProductContraindicationsByICD @CRUDType=" + CRUD.Read + ",@categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @medicalICDContraindicationId=" + ICDId + "").ToList();

                String response = Operations.SaveAddCIE10(ICDId, ProductId, PharmaFormId, "Insert");

                if ((response != "Ok") && (response != "AsocCNT"))
                {
                    LS.Add(response.ToUpper());
                }

                if (response == "AsocCNT")
                {
                    var res = new { Data = response, Node = LICD[0].SpanishDescription, ICDKey = LICD[0].ICDKey };

                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                if (FlagParent == true)
                {
                    var res = new { Data = "ReplaceParentByNode", Parent = SpanishDescription, Node = LICDS[0].SpanishDescription, ICDKeyParent = ICDKeyParent, ICDKeyNode = LICDS[0].ICDKey };

                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }

            if (LS.LongCount() > 0)
            {
                LS = LS.OrderBy(x => x).ToList();

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RemoveCIE10(string Product, string PharmaForm, string ICD)
        {
            int ICDId = int.Parse(ICD);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);

            String response = Operations.SaveAddCIE10(ICDId, ProductId, PharmaFormId, "Delete");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContraindicationsFromIndications(string Product, string PharmaForm, string Division, string Category)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);

            var LS = db.Database.SqlQuery<GetICDByProductPharmaform>("plm_spCRUDProductContraindicationsByICD @CRUDType=" + CRUD.Read + ", @ProductId = " + ProductId + ", @PharmaFormId = " + PharmaFormId + ", @divisionId=" + DivisionId + ", @categoryId=" + CategoryId + "")
                                                .OrderBy(x => x.ICDKey)
                                                .Select(x => new { x.ICDId, x.ICDKey, x.ParentICDKey, x.SpanishDescription }).Distinct().ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ATCOMS

        public ActionResult ATCOMS(int? ProductId, int? PharmaFormId, int? CategoryId, int? EditionId)
        {
            SessionLI SessionLI = (SessionLI)Session["SessionLI"];

            if ((!Request.IsAuthenticated) || (ProductId == null) || (PharmaFormId == null) || (CategoryId == null) || (SessionLI == null) || (EditionId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionLI.ProductId = ProductId;
            SessionLI.PharmaFormId = PharmaFormId;
            SessionLI.CategoryId = CategoryId;
            SessionLI.EditionId = Convert.ToInt32(EditionId);

            List<GetTherapeuticOMSByProduct> LS = db.Database.SqlQuery<GetTherapeuticOMSByProduct>("plm_spGetTherapeuticOMSByProduct @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + "").OrderBy(x => x.TherapeuticKey).ToList();

            return View(LS);
        }

        public JsonResult GetLevelTwoATCOMS(string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);

            List<String> LT = new List<String>();

            var LS1 = db.TherapeuticOMS.Where(x => x.ParentId == TherapeuticId && x.Active == true && x.Level == 2).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (TherapeuticOMS item in LS1)
            {
                String _return = String.Empty;

                List<TherapeuticOMS> LS = db.TherapeuticOMS.Where(x => x.ParentId == item.TherapeuticOMSId && x.Active == true && x.Level == 3).OrderBy(x => x.TherapeuticKey).ToList();

                if (LS.LongCount() > 0)
                {
                    _return = "<span class='glyphicon glyphicon-plus' id='Expand_" + item.TherapeuticOMSId + "' onclick='getlevel3OMS(" + item.TherapeuticOMSId + ")' style='cursor:pointer'></span>" +
                                    "<span class='glyphicon glyphicon-minus' style='display:none;cursor:pointer' id='Collapse_" + item.TherapeuticOMSId + "' onclick='collapselevel3OMS(" + item.TherapeuticOMSId + ")'></span>" +
                                    "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                    "<ul id='ListL3_" + item.TherapeuticOMSId + "' style='list-style: none;margin-left:30px'></ul>";
                }
                else
                {
                    _return = "<input type='checkbox' value='" + item.TherapeuticOMSId + "' onclick=\"AddATCOMS($(this)) \" />" +
                                    "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                    "<ul id='ListL4_" + item.TherapeuticOMSId + "' style='list-style: none;margin-left:30px'></ul>";
                }

                LT.Add(_return);
            }

            return Json(LT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLevelThreeATCOMS(string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);

            List<String> LT = new List<String>();

            var LS1 = db.TherapeuticOMS.Where(x => x.ParentId == TherapeuticId && x.Active == true && x.Level == 3).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (TherapeuticOMS item in LS1)
            {
                String _return = String.Empty;

                List<TherapeuticOMS> LS = db.TherapeuticOMS.Where(x => x.ParentId == item.TherapeuticOMSId && x.Active == true && x.Level == 4).OrderBy(x => x.TherapeuticKey).ToList();

                if (LS.LongCount() > 0)
                {
                    _return = "<span class='glyphicon glyphicon-plus' id='Expand_" + item.TherapeuticOMSId + "' onclick='getlevel4OMS(" + item.TherapeuticOMSId + ")' style='cursor:pointer'></span>" +
                                    "<span class='glyphicon glyphicon-minus' style='display:none;cursor:pointer' id='Collapse_" + item.TherapeuticOMSId + "' onclick='collapselevel4OMS(" + item.TherapeuticOMSId + ")'></span>" +
                                    "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                    "<ul id='ListL4_" + item.TherapeuticOMSId + "' style='list-style: none;margin-left:30px'></ul>";
                }
                else
                {
                    _return = "<input type='checkbox' value='" + item.TherapeuticOMSId + "' onclick=\"AddATCOMS($(this)) \" />" +
                                    "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                    "<ul id='ListL4_" + item.TherapeuticOMSId + "' style='list-style: none;margin-left:30px'></ul>";
                }

                LT.Add(_return);
            }

            return Json(LT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLevelFourATCOMS(string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);

            List<String> LT = new List<String>();

            var LS1 = db.TherapeuticOMS.Where(x => x.ParentId == TherapeuticId && x.Active == true && x.Level == 4).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (TherapeuticOMS item in LS1)
            {
                String _return = String.Empty;

                List<TherapeuticOMS> LS = db.TherapeuticOMS.Where(x => x.ParentId == item.TherapeuticOMSId && x.Active == true && x.Level == 5).OrderBy(x => x.TherapeuticKey).ToList();

                if (LS.LongCount() > 0)
                {
                    _return = "<span class='glyphicon glyphicon-plus' id='Expand_" + item.TherapeuticOMSId + "' onclick='getlevel5OMS(" + item.TherapeuticOMSId + ")' style='cursor:pointer'></span>" +
                                    "<span class='glyphicon glyphicon-minus' style='display:none;cursor:pointer' id='Collapse_" + item.TherapeuticOMSId + "' onclick='collapselevel5OMS(" + item.TherapeuticOMSId + ")'></span>" +
                                    "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                    "<ul id='ListL5_" + item.TherapeuticOMSId + "' style='list-style: none;margin-left:30px'></ul>";
                }
                else
                {
                    _return = "<input type='checkbox' value='" + item.TherapeuticOMSId + "'  onclick=\"AddATCOMS($(this)) \"/>" +
                                    "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                    "<ul id='ListL4_" + item.TherapeuticOMSId + "' style='list-style: none;margin-left:30px'></ul>";
                }

                LT.Add(_return);
            }

            return Json(LT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLevelFiveATCOMS(string Therapeutic)
        {
            int TherapeuticId = int.Parse(Therapeutic);

            List<String> LS = new List<String>();

            var LS1 = db.TherapeuticOMS.Where(x => x.ParentId == TherapeuticId && x.Active == true && x.Level == 5).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (TherapeuticOMS item in LS1)
            {
                String _return = String.Empty;

                _return = "<input type='checkbox' value='" + item.TherapeuticOMSId + "'  onclick=\"AddATCOMS($(this)) \"/>" +
                                                 "&nbsp;&nbsp;<label style='font-weight:bold;color:#065977; margin-bottom: 5px'><b>" + item.TherapeuticKey + "</b></label> - <span style='font-weight:100'>" + item.SpanishDescription.ToUpper() + "</span>" +
                                                 "<ul id='ListL4_" + item.TherapeuticOMSId + "' style='list-style: none;margin-left:30px'></ul>";

                LS.Add(_return);
            }

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAddATCOMS(String List, string size)
        {
            int SizeId = int.Parse(size);

            dynamic item = JsonConvert.DeserializeObject(List);

            List<String> LS = new List<String>();

            for (var i = 0; i <= SizeId - 1; i++)
            {
                int TherapeuticOMSId = Convert.ToInt32(item[i]["TId"]);
                int ProductId = Convert.ToInt32(item[i]["PId"]);
                int PharmaFormId = Convert.ToInt32(item[i]["PFId"]);

                String response = Operations.SaveAddATCOMS(TherapeuticOMSId, ProductId, PharmaFormId, "Insert");

                if (response != "Ok")
                {
                    LS.Add(response.ToUpper());
                }
            }

            if (LS.LongCount() > 0)
            {
                LS = LS.OrderBy(x => x).ToList();

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RemoveATCOMS(string Product, string PharmaForm, string Therapeutic)
        {
            int TherapeuticOMSId = int.Parse(Therapeutic);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);

            String response = Operations.SaveAddATCOMS(TherapeuticOMSId, ProductId, PharmaFormId, "Delete");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Interactions

        public ActionResult Interactions(int? ProductId, int? PharmaFormId, int? CategoryId, int? EditionId, int? DivisionId, string Description, string GroupName, string ElementName)
        {
            SessionLI SessionLI = (SessionLI)Session["SessionLI"];

            if ((!Request.IsAuthenticated) || (ProductId == null) || (PharmaFormId == null) || (CategoryId == null) || (SessionLI == null) || (EditionId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionLI.ProductId = ProductId;
            SessionLI.PharmaFormId = PharmaFormId;
            SessionLI.CategoryId = CategoryId;
            SessionLI.EditionId = Convert.ToInt32(EditionId);

            GetInteractions GetInteractions = new Models.Class.GetInteractions();

            GetInteractions.ProductSubstanceInteractions = db.Database.SqlQuery<GetProductSubstanceInteractions>("plm_spGetProductSubstanceInteractions @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + "").ToList();
            GetInteractions.ProductPharmaGroupInteractions = db.Database.SqlQuery<GetProductPharmaGroupInteractions>("plm_spGetProductPharmaGroupInteractions  @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + "").ToList();
            GetInteractions.ProductotherInteractions = db.Database.SqlQuery<GetProductotherInteractions>("plm_spGetProductotherInteractions  @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + "").ToList();
            GetInteractions.ActiveSubstancesWithoutInteractions = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstancesWithoutInteractions  @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaForm=" + PharmaFormId + ",@productId=" + ProductId + ",@substance='" + Description + "'").ToList();
            GetInteractions.PharmacologicalGroupsWithoutInteraction = db.Database.SqlQuery<GetPharmacologicalGroupsWithoutInteraction>("plm_spGetPharmacologicalGroupsWithoutInteraction @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ",@groupName='" + GroupName + "'").ToList();
            GetInteractions.OtherElemensWithoutInteractions = db.Database.SqlQuery<GetOtherElemensWithoutInteractions>("plm_spGetOtherElemensWithoutInteractions @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @elementName='" + ElementName + "'").ToList();
            GetInteractions.ActiveSubstancesByProduct = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();
            var HTMLContent = db.Database.SqlQuery<String>("plm_spGetHtmlContentInteractions @productid=" + ProductId + ",@divisionid=" + DivisionId + ",@categoryid=" + CategoryId + ",@pharmaformid=" + PharmaFormId + "").ToList();
            if (!string.IsNullOrEmpty(HTMLContent[0]))
            {
                GetInteractions.HTMLContent = HTMLContent[0].Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("normal", "Normal").Replace("NORMAL", "Normal").Replace("rubros-azules", "Rubros-azules");
            }
            else
            {
                GetInteractions.HTMLContent = "<p class=\"Normal\"><span class=\"Rubros-azules\">INTERACCIONES MEDICAMENTOSAS Y DE OTRO G&Eacute;NERO:</span> No Clasificado</p>";
            }
            return View(GetInteractions);
        }

        public JsonResult Pager(string Page, string PagePG, string PageOE)
        {
            int PageId = int.Parse(Page);
            int PagePGId = int.Parse(PagePG);
            int PageOEId = int.Parse(PageOE);

            if ((PageId == 0))
            {
                PageId = 10;
            }

            if ((PagePGId == 0))
            {
                PagePGId = 10;
            }

            if ((PageOEId == 0))
            {
                PageOEId = 10;
            }

            Web.Models.Sessions.Pager SPager = new Pager(PageId, PagePGId, PageOEId);
            Session["Pager"] = SPager;


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProductSubstanceInteractions(string Size, String SubstanceInteractions)
        {
            List<ReturnAddedInteractions> LR = new List<ReturnAddedInteractions>();
            ReturnAddedInteractions r = new ReturnAddedInteractions();

            int SizeId = int.Parse(Size);

            dynamic Array = JsonConvert.DeserializeObject(SubstanceInteractions);

            for (var i = 0; i <= (SizeId - 1); i++)
            {
                int SubstanceInteractId = Convert.ToInt32(Array[i]["SubstanceInteract"]);
                int DivisionId = Convert.ToInt32(Array[i]["Division"]);
                int CategoryId = Convert.ToInt32(Array[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(Array[i]["PharmaForm"]);
                int ProductId = Convert.ToInt32(Array[i]["Product"]);
                int ActiveSubstanceId = Convert.ToInt32(Array[i]["ActiveSubstance"]);

                Operations.CheckProductInteraction(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId);

                Operations.IPPAProductInteractions(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                string result = Operations.SaveProductSubstanceInteractions(ActiveSubstanceId, DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                if (result != "Ok")
                {
                    r = new ReturnAddedInteractions();

                    var ActiveSubstance = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == SubstanceInteractId).ToList();

                    string Substance = ActiveSubstance[0].Description;

                    r.ActiveSubstance = Substance;
                    r.Element = result;

                    LR.Add(r);
                }
            }

            if (LR.LongCount() == SizeId)
            {
                return Json(LR, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteProductSubstanceInteractions(string ActiveSubstance, string SubstanceInteraction, string Product, string Category, string Division, string PharmaForm)
        {
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int SubstanceInteractionId = int.Parse(SubstanceInteraction);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);

            Operations.SaveProductSubstanceInteractions(SubstanceInteractionId, DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId, "Delete");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckActiveSubstancesByProduct(string Product, string Category, string Division, string PharmaForm, string ActiveSubstance)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductSubstanceInteractions> LPS = db.Database.SqlQuery<GetProductSubstanceInteractions>("plm_spGetProductSubstanceInteractions @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @susbtanceId=" + ActiveSubstanceId + ", @TypeSelect=0").ToList();

                if (LPS.LongCount() > 0)
                {
                    foreach (GetProductSubstanceInteractions item in LPS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);
                    }
                }

                if (LS.LongCount() == 1)
                {
                    int SubstanceInteractId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    string json = "[{\"Division\":" + DivisionId + ",\"Category\":" + CategoryId + ",\"PharmaForm\":" + PharmaFormId + ",\"Product\":" + ProductId + ",\"SubstanceInteract\":" + SubstanceInteractId + ",\"ActiveSubstance\":" + ActiveSubstanceId + "}]";

                    AddProductSubstanceInteractions("1", json);

                    var rest = new { Data = "Ok" };

                    return Json(rest, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddProductPharmacologicalGroupsInteractions(string Size, String SubstanceInteractions)
        {
            List<ReturnAddedInteractions> LR = new List<ReturnAddedInteractions>();
            ReturnAddedInteractions r = new ReturnAddedInteractions();

            int SizeId = int.Parse(Size);

            dynamic Array = JsonConvert.DeserializeObject(SubstanceInteractions);

            for (var i = 0; i <= (SizeId - 1); i++)
            {
                int SubstanceInteractId = Convert.ToInt32(Array[i]["SubstanceInteract"]);
                int DivisionId = Convert.ToInt32(Array[i]["Division"]);
                int CategoryId = Convert.ToInt32(Array[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(Array[i]["PharmaForm"]);
                int ProductId = Convert.ToInt32(Array[i]["Product"]);
                int ActiveSubstanceId = Convert.ToInt32(Array[i]["ActiveSubstance"]);

                Operations.CheckProductInteraction(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId);

                Operations.IPPAProductInteractions(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                string result = Operations.SaveProductPharmacologicalGroupInteractions(ActiveSubstanceId, DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                if (result != "Ok")
                {
                    r = new ReturnAddedInteractions();

                    var ActiveSubstance = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == SubstanceInteractId).ToList();

                    string Substance = ActiveSubstance[0].Description;

                    r.ActiveSubstance = Substance;
                    r.Element = result;

                    LR.Add(r);
                }
            }

            if (LR.LongCount() == SizeId)
            {
                return Json(LR, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckPharmacologicalGroupsByProduct(string Product, string Category, string Division, string PharmaForm, string PharmacologicalGroup)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int PharmacologicalGroupId = int.Parse(PharmacologicalGroup);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductPharmaGroupInteractions> LPS = db.Database.SqlQuery<GetProductPharmaGroupInteractions>("plm_spGetProductPharmaGroupInteractions  @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ",@pharmaGroupId=" + PharmacologicalGroupId + ", @susbtanceId=0").ToList();

                if (LPS.LongCount() > 0)
                {
                    foreach (GetProductPharmaGroupInteractions item in LPS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);
                    }
                }

                if (LS.LongCount() == 1)
                {
                    int ActiveSubstanceId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    string json = "[{\"Division\":" + DivisionId + ",\"Category\":" + CategoryId + ",\"PharmaForm\":" + PharmaFormId + ",\"Product\":" + ProductId + ",\"SubstanceInteract\":" + ActiveSubstanceId + ",\"ActiveSubstance\":" + PharmacologicalGroupId + "}]";

                    AddProductPharmacologicalGroupsInteractions("1", json);

                    var rest = new { Data = "Ok" };

                    return Json(rest, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeletePharmacologicalInteracions(string ActiveSubstance, string PharmacologicalInteraction, string Product, string Category, string Division, string PharmaForm)
        {

            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int PharmacologicalInteractionId = int.Parse(PharmacologicalInteraction);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);

            Operations.SaveProductPharmacologicalGroupInteractions(PharmacologicalInteractionId, DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId, "Delete");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckOtherElementsByProduct(string Product, string Category, string Division, string PharmaForm, string OtherElement)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int OtherElementId = int.Parse(OtherElement);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                List<GetProductotherInteractions> LPS = db.Database.SqlQuery<GetProductotherInteractions>("plm_spGetProductotherInteractions @categoryId=" + CategoryId + ",@divisionId=" + DivisionId + ",@pharmaFormId=" + PharmaFormId + ",@productId=" + ProductId + ", @substanceId=0 , @elementId=" + OtherElementId + "").ToList();

                if (LPS.LongCount() > 0)
                {
                    foreach (GetProductotherInteractions item in LPS)
                    {
                        int remActiveSubstanceId = Convert.ToInt32(item.ActiveSubstanceId);

                        var res = LS.SingleOrDefault(x => x.ActiveSubstanceId == remActiveSubstanceId);

                        LS.Remove(res);
                    }
                }

                if (LS.LongCount() == 1)
                {
                    int ActiveSubstanceId = Convert.ToInt32(LS[0].ActiveSubstanceId);

                    string json = "[{\"Division\":" + DivisionId + ",\"Category\":" + CategoryId + ",\"PharmaForm\":" + PharmaFormId + ",\"Product\":" + ProductId + ",\"SubstanceInteract\":" + ActiveSubstanceId + ",\"ActiveSubstance\":" + OtherElementId + "}]";

                    AddProductOtherElementInteractions("1", json);

                    var rest = new { Data = "Ok" };

                    return Json(rest, JsonRequestBehavior.AllowGet);
                }

                var result = new { Data = true, Items = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Data = false };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddProductOtherElementInteractions(string Size, String OtherElementInteractions)
        {
            List<ReturnAddedInteractions> LR = new List<ReturnAddedInteractions>();
            ReturnAddedInteractions r = new ReturnAddedInteractions();

            int SizeId = int.Parse(Size);

            dynamic Array = JsonConvert.DeserializeObject(OtherElementInteractions);

            for (var i = 0; i <= (SizeId - 1); i++)
            {
                int SubstanceInteractId = Convert.ToInt32(Array[i]["SubstanceInteract"]);
                int DivisionId = Convert.ToInt32(Array[i]["Division"]);
                int CategoryId = Convert.ToInt32(Array[i]["Category"]);
                int PharmaFormId = Convert.ToInt32(Array[i]["PharmaForm"]);
                int ProductId = Convert.ToInt32(Array[i]["Product"]);
                int OtherElementId = Convert.ToInt32(Array[i]["ActiveSubstance"]);

                Operations.CheckProductInteraction(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId);

                Operations.IPPAProductInteractions(DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                string result = Operations.SaveProductOtherElementInteractions(OtherElementId, DivisionId, CategoryId, PharmaFormId, ProductId, SubstanceInteractId, "Insert");

                if (result != "Ok")
                {
                    r = new ReturnAddedInteractions();

                    var ActiveSubstance = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == SubstanceInteractId).ToList();

                    string Substance = ActiveSubstance[0].Description;

                    r.ActiveSubstance = Substance;
                    r.Element = result;

                    LR.Add(r);
                }
            }

            if (LR.LongCount() == SizeId)
            {
                return Json(LR, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteOtherElementInteracions(string ActiveSubstance, string OtherElement, string Product, string Category, string Division, string PharmaForm)
        {
            int ActiveSubstanceId = int.Parse(ActiveSubstance);
            int OtherElementId = int.Parse(OtherElement);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);

            Operations.SaveProductOtherElementInteractions(OtherElementId, DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId, "Delete");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintInteractions(int? ProductId, int? DivisionId, int? CategoryId, int? PharmaFormId, int? EditionId, int? CountryId)
        {
            var list = db.Database.SqlQuery<String>("plm_spGetHtmlContentInteractions @productid=" + ProductId + ",@divisionid=" + DivisionId + ",@categoryid=" + CategoryId + ",@pharmaformid=" + PharmaFormId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetInteractionsByKey.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetReportInteractions> cm = new List<GetReportInteractions>();

            GetReportInteractions cmd = new GetReportInteractions();

            if (list.LongCount() > 0)
            {
                List<Categories> LC = db.Categories.Where(x => x.CategoryId == CategoryId).ToList();
                List<PharmaceuticalForms> LPF = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();
                List<Products> LP = db.Products.Where(x => x.ProductId == ProductId).ToList();
                List<Divisions> LD = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();
                List<Editions> LE = db.Editions.Where(x => x.EditionId == EditionId).ToList();
                List<Countries> L = db.Countries.Where(x => x.CountryId == CountryId).ToList();

                string Interaction = string.Empty;

                if (!string.IsNullOrEmpty(list[0]))
                {
                    Interaction = GetInteractionText(list[0]);
                }
                else
                {
                    Interaction = "\n\n No Clasificado";
                }

                cmd.HTMLContent = Interaction;
                cmd.CategoryName = LC[0].Description;
                cmd.PharmaForm = LPF[0].Description;
                cmd.Brand = LP[0].Brand;
                cmd.DivisioName = LD[0].Description;
                cmd.NumberEdition = LE[0].NumberEdition;
                cmd.CountryName = L[0].CountryName;

                cm.Add(cmd);
            }

            ReportDataSource rd = new ReportDataSource("GetInteractions", cm);
            lr.DataSources.Add(rd);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }

        public static string GetInteractionText(String Interaction)
        {
            getData getData = new Models.getData();

            Interaction = System.Text.RegularExpressions.Regex.Replace(Interaction, "<.*?>", String.Empty).Replace("INTERACCIONES MEDICAMENTOSAS Y DE OTRO G&Eacute;NERO: ", "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNode textNode = doc.CreateElement("title");
            textNode.InnerHtml = Interaction;
            Interaction = textNode.InnerText;

            Interaction = getData.ReplaceHTMLCodes(Interaction);

            return Interaction;
        }

        public JsonResult NoInteractions(string Product, string Category, string Division, string PharmaForm)
        {
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int PharmaFormId = int.Parse(PharmaForm);
            int StatusId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NotInteractions"]);

            List<GetActiveSubstancesWithoutInteractions> LS = db.Database.SqlQuery<GetActiveSubstancesWithoutInteractions>("plm_spGetActiveSubstanceByProduct @productId=" + ProductId + "").ToList();

            if (LS.LongCount() > 0)
            {
                foreach (GetActiveSubstancesWithoutInteractions item in LS)
                {
                    var res = db.Database.ExecuteSqlCommand("plm_spCRUDIppaProductInteractions @CRUDType=" + CRUD.Create + ", @categoryId=" + CategoryId + ", @divisionId=" + DivisionId + ", @pharmaFormId=" + PharmaFormId + ", @productId=" + ProductId + ", @substanceId=" + item.ActiveSubstanceId + ", @statusIM=" + StatusId + "");
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Indications

        public ActionResult Indications(int? ProductId, int? PharmaFormId, int? CategoryId, int? EditionId, int? DivisionId, string Description)
        {
            SessionLI SessionLI = (SessionLI)Session["SessionLI"];

            if ((!Request.IsAuthenticated) || (ProductId == null) || (PharmaFormId == null) || (CategoryId == null) || (SessionLI == null) || (EditionId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionLI.ProductId = ProductId;
            SessionLI.PharmaFormId = PharmaFormId;
            SessionLI.CategoryId = CategoryId;
            SessionLI.EditionId = Convert.ToInt32(EditionId);

            GetIndications GetIndications = new Models.Class.GetIndications();

            GetIndications.GetIndicationsByProduct = db.Database.SqlQuery<GetIndicationsByProduct>("plm_spGetIndicationsByProduct @productId=" + ProductId + "").OrderBy(x => x.Description).ToList();
            GetIndications.GetIndicationssWithoutProduct = db.Database.SqlQuery<GetIndicationsByProduct>("plm_spGetIndicationsWithoutProduct @productId=" + ProductId + ", @indication='" + Description + "'").OrderBy(x => x.Description).ToList();

            return View(GetIndications);
        }

        public JsonResult AddProductIndications(string Product, string Indication)
        {
            int ProductId = int.Parse(Product);
            int IndicationId = int.Parse(Indication);

            List<ProductIndications> LS = db.Database.SqlQuery<ProductIndications>("plm_spCRUDProductIndications @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ", @indicationId=" + IndicationId + "").ToList();

            if (LS.LongCount() == 0)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductIndications @CRUDType=" + CRUD.Create + ", @productId=" + ProductId + ", @indicationId=" + IndicationId + "");

                ActivityLog.CRUDProductIndications(ProductId, IndicationId, 1);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveProductIndications(string Product, string Indication)
        {
            int ProductId = int.Parse(Product);
            int IndicationId = int.Parse(Indication);

            List<ProductIndications> LS = db.Database.SqlQuery<ProductIndications>("plm_spCRUDProductIndications @CRUDType=" + CRUD.Read + ", @productId=" + ProductId + ", @indicationId=" + IndicationId + "").ToList();

            if (LS.LongCount() > 0)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDProductIndications @CRUDType=" + CRUD.Delete + ", @productId=" + ProductId + ", @indicationId=" + IndicationId + "");

                ActivityLog.CRUDProductIndications(ProductId, IndicationId, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PagerInd(string Page)
        {
            int PageId = int.Parse(Page);

            if ((PageId == 0))
            {
                PageId = 10;
            }

            Web.Models.Sessions.PagerIndications PagerIndications = new PagerIndications(PageId);
            Session["PagerIndications"] = PagerIndications;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region AdministrationRoutes

        public ActionResult AdministrationRoutes(int? ProductId, int? PharmaFormId, int? CategoryId, int? EditionId, string Description, string DescriptionAS)
        {
            if ((!Request.IsAuthenticated) || (ProductId == null) || (PharmaFormId == null) || (CategoryId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionLI SessionLI = (SessionLI)Session["SessionLI"];

            GetAdministrationRoutes GetAdministrationRoutes = new Models.Class.GetAdministrationRoutes();

            GetAdministrationRoutes.GetAdministrationRoutesByProductPharmaForm = db.Database.SqlQuery<GetAdministrationRoutesByProductPharmaForm>("plm_spGetAdministrationRoutesByProduct @productId=" + ProductId + ", @pharmaFormId=" + PharmaFormId + ", @Description='" + DescriptionAS + "'").OrderBy(x => x.RouteName).ToList();
            GetAdministrationRoutes.GetAdministrationRoutesClass = db.Database.SqlQuery<GetAdministrationRoutesClass>("plm_spGetAdministrationRoutes @Description='" + Description + "'").OrderBy(x => x.RouteName).ToList();


            SessionLI.ProductId = ProductId;
            SessionLI.PharmaFormId = PharmaFormId;
            SessionLI.CategoryId = CategoryId;
            SessionLI.EditionId = Convert.ToInt32(EditionId);

            return View(GetAdministrationRoutes);
        }

        public JsonResult AddProductAdministrationRoutes(string Route, string Product, string PharmaForm, string Operation)
        {
            int RouteId = int.Parse(Route);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);

            if (Operation == "Insert")
            {
                List<ProductPharmaFormRoutes> LS = db.ProductPharmaFormRoutes.Where(x => x.ProductId == ProductId && x.PharmaFormId == PharmaFormId && x.RouteId == RouteId).ToList();

                if (LS.LongCount() == 0)
                {
                    List<Products> LP = db.Products.Where(x => x.ProductId == ProductId).ToList();
                    string Brand = LP[0].Brand;
                    List<PharmaceuticalForms> LPF = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();
                    string PharmaFormName = LPF[0].Description;
                    List<AdministrationRoutes> LA = db.AdministrationRoutes.Where(x => x.RouteId == RouteId).ToList();
                    string RouteName = LA[0].RouteName;

                    ProductPharmaFormRoutes ProductPharmaFormRoutes = new ProductPharmaFormRoutes();

                    ProductPharmaFormRoutes.PharmaFormId = PharmaFormId;
                    ProductPharmaFormRoutes.ProductId = ProductId;
                    ProductPharmaFormRoutes.RouteId = RouteId;
                    ProductPharmaFormRoutes.JSONFormat = "{\"ProductId\":" + ProductId + ",\"Brand\":\"" + Brand + "\",\"PharmaFormId\":" + PharmaFormId + ",\"PharmaForm\":\"" + PharmaFormName + "\",\"RouteId\":" + RouteId + ",\"RouteName\":\"" + RouteName + "\"}";

                    db.ProductPharmaFormRoutes.Add(ProductPharmaFormRoutes);
                    db.SaveChanges();

                    ActivityLog.CRUDProductPharmaFormRoutes(ProductId, PharmaFormId, RouteId, ProductPharmaFormRoutes.JSONFormat, 1);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                List<ProductPharmaFormRoutes> LS = db.ProductPharmaFormRoutes.Where(x => x.ProductId == ProductId && x.PharmaFormId == PharmaFormId && x.RouteId == RouteId).ToList();

                if (LS.LongCount() > 0)
                {
                    var delete = db.ProductPharmaFormRoutes.SingleOrDefault(x => x.ProductId == ProductId && x.PharmaFormId == PharmaFormId && x.RouteId == RouteId);
                    db.ProductPharmaFormRoutes.Remove(delete);
                    db.SaveChanges();

                    ActivityLog.CRUDProductPharmaFormRoutes(ProductId, PharmaFormId, RouteId, LS[0].JSONFormat, 4);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}