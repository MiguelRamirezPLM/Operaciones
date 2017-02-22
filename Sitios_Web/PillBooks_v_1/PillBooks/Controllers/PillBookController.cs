using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PillBooks.Models;
using System.Globalization;
using System.Text;
using System.Collections;
using Newtonsoft.Json;

namespace PillBooks.Controllers
{
    public class PillBookController : Controller
    {
        MedinetPB db = new MedinetPB();
        PillBook PillBook = new PillBook();
        ActivityLog ActivityLog = new ActivityLog();
        ListPillBooks PB = new ListPillBooks();
        List<ListPillBooks> LPB = new List<ListPillBooks>();
        SendEmail sendemail = new SendEmail();

        public ActionResult Index(int? PillBookId)
        {
            CountriesUsers CountriesUsers = (CountriesUsers)Session["CountriesUsers"];
            SessionCatch ind = (SessionCatch)Session["SessionCatch"];
            JoinPillBooks JoinPillBooks = new Models.JoinPillBooks();
            List<ProductPharmaFormsByPillBook> LSPPBP = new List<ProductPharmaFormsByPillBook>();
            ProductPharmaFormsByPillBook ProductPharmaFormsByPillBook = new ProductPharmaFormsByPillBook();

            if (CountriesUsers != null)
            {
                string Country = System.Configuration.ConfigurationManager.AppSettings["CountryId"];

                int CountryId = int.Parse(Country);

                if (PillBookId != null)
                {

                    var pb = db.Database.SqlQuery<INNActiveSubstances>("plm_spGetINNSubstancesByPillBook @pillbookid=" + PillBookId + "").ToList();

                    var pbatc = db.Database.SqlQuery<TherapeuticsByPillBook>("plm_spGetTherapeuticsByPillBook @pillbookid=" + PillBookId + "").ToList();

                    var pbpsss = db.Database.SqlQuery<ProductPharmaFormsByPillBook>("plm_spGetBrandsByPillBook @pillbookid=" + PillBookId + ", @countryid=" + CountryId + "").OrderBy(x => x.Brand).ToList();

                    var pbicd = db.Database.SqlQuery<ICDByPillBook>("plm_spGetICDByPillBook @pillbookid=" + PillBookId + "").ToList();

                    var oms = db.Database.SqlQuery<TherapeuticOMSByPillBook>("plm_spGetTherapeuticsOMSByPillBook @pillbookid=" + PillBookId + "").ToList();

                    JoinPillBooks.TherapeuticOMSByPillBook = oms;
                    JoinPillBooks.INNActiveSubstances = pb;
                    JoinPillBooks.ProductPharmaFormsByPillBook = pbpsss;
                    JoinPillBooks.TherapeuticsByPillBook = pbatc;
                    JoinPillBooks.ICDByPillBook = pbicd;

                    List<JoinPillBooks> ljp = new List<JoinPillBooks>();

                    ljp.Add(JoinPillBooks);

                    SessionCatch SessionCatch = new SessionCatch(PillBookId);
                    Session["SessionCatch"] = SessionCatch;

                    return View(ljp);
                }

                if (ind != null)
                {
                    PillBookId = ind.PillBookId;

                    var pb = db.Database.SqlQuery<INNActiveSubstances>("plm_spGetINNSubstancesByPillBook @pillbookid=" + PillBookId + "").ToList();

                    var pbatc = db.Database.SqlQuery<TherapeuticsByPillBook>("plm_spGetTherapeuticsByPillBook @pillbookid=" + PillBookId + "").ToList();

                    var pbpsss = db.Database.SqlQuery<ProductPharmaFormsByPillBook>("plm_spGetBrandsByPillBook @pillbookid=" + PillBookId + ", @countryid=" + CountryId + "").OrderBy(x => x.Brand).ToList();

                    var pbicd = db.Database.SqlQuery<ICDByPillBook>("plm_spGetICDByPillBook @pillbookid=" + PillBookId + "").ToList();

                    var oms = db.Database.SqlQuery<TherapeuticOMSByPillBook>("plm_spGetTherapeuticsOMSByPillBook @pillbookid=" + PillBookId + "").ToList();

                    JoinPillBooks.TherapeuticOMSByPillBook = oms;
                    JoinPillBooks.INNActiveSubstances = pb;
                    JoinPillBooks.ProductPharmaFormsByPillBook = pbpsss;
                    JoinPillBooks.TherapeuticsByPillBook = pbatc;
                    JoinPillBooks.ICDByPillBook = pbicd;

                    List<JoinPillBooks> ljp = new List<JoinPillBooks>();

                    ljp.Add(JoinPillBooks);

                    SessionCatch SessionCatch = new SessionCatch(PillBookId);
                    Session["SessionCatch"] = SessionCatch;

                    return View(ljp);
                }
                else
                {

                    var pb = db.Database.SqlQuery<INNActiveSubstances>("plm_spGetINNSubstancesByPillBook @pillbookid=" + 0 + "").ToList();

                    var pbatc = db.Database.SqlQuery<TherapeuticsByPillBook>("plm_spGetTherapeuticsByPillBook @pillbookid=" + 0 + "").OrderBy(x => x.SpanishDescriptionNivel4).ToList();

                    var pbpsss = db.Database.SqlQuery<ProductPharmaFormsByPillBook>("plm_spGetBrandsByPillBook @pillbookid=" + 0 + ", @countryid=" + 0 + "").OrderBy(x => x.Brand).ToList();

                    var pbicd = db.Database.SqlQuery<ICDByPillBook>("plm_spGetICDByPillBook @pillbookid=" + 0 + "").ToList();

                    var oms = db.Database.SqlQuery<TherapeuticOMSByPillBook>("plm_spGetTherapeuticsOMSByPillBook @pillbookid=" + 0 + "").ToList();

                    JoinPillBooks.TherapeuticOMSByPillBook = oms;
                    JoinPillBooks.INNActiveSubstances = pb;
                    JoinPillBooks.ProductPharmaFormsByPillBook = pbpsss;
                    JoinPillBooks.TherapeuticsByPillBook = pbatc;
                    JoinPillBooks.ICDByPillBook = pbicd;

                    List<JoinPillBooks> ljp = new List<JoinPillBooks>();

                    ljp.Add(JoinPillBooks);

                    return View(ljp);
                }
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public JsonResult createpillbook(string PBName, string PBCode)
        {
            var pbn = db.PillBook.Where(x => x.PillBookName.ToUpper().Trim() == PBName.ToUpper().Trim());

            if (pbn.LongCount() == 0)
            {
                var pbc = db.PillBook.Where(x => x.PIllBookCode.ToUpper().Trim() == PBCode.ToUpper().Trim());

                if (pbc.LongCount() == 0)
                {
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    int ApplicationId = p.ApplicationId;
                    int UsersId = p.userId;

                    PillBook.Active = true;
                    PillBook.PillBookName = PBName.Trim();
                    PillBook.PIllBookCode = PBCode.Trim();
                    PillBook.AddDate = DateTime.Now;
                    PillBook.ModifyDate = DateTime.Now;
                    PillBook.Finished = false;

                    db.PillBook.Add(PillBook);
                    db.SaveChanges();


                    var pb = db.PillBook.Where(x => x.PillBookName == PBName.Trim() && x.PIllBookCode == PBCode.Trim()).ToList();

                    ActivityLog.createpillbook(pb[0].PillBookId, pb[0].PillBookName, pb[0].PIllBookCode, pb[0].Active, ApplicationId, UsersId);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("errorcode", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("errorpillbook", JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult editpillbook(string PBName, string PBId)
        {
            int PillBookId = int.Parse(PBId);

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var pb = db.PillBook.Where(x => x.PillBookId == PillBookId).ToList();

            if (pb.LongCount() > 0)
            {
                foreach (PillBook _pb in pb)
                {
                    var pbs = db.PillBook.Where(x => x.PillBookName.ToUpper().Trim() == PBName.ToUpper().Trim()).ToList();

                    if (pbs.LongCount() == 0)
                    {
                        _pb.PillBookName = PBName.Trim();

                        db.SaveChanges();

                        ActivityLog.editpillbook(PillBookId, _pb.PillBookName, _pb.PIllBookCode, _pb.Active, ApplicationId, UsersId);
                    }
                    else
                    {
                        return Json("errorpillbook", JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getproducts(string PillBook)
        {
            ProductPharmaFormsPB Products = new Models.ProductPharmaFormsPB();
            List<ProductPharmaFormsPB> LS = new List<Models.ProductPharmaFormsPB>();

            string Country = System.Configuration.ConfigurationManager.AppSettings["CountryId"];

            int CountryId = int.Parse(Country);

            int PillBookId = int.Parse(PillBook);

            var prod = db.Database.SqlQuery<ProductPharmaFormsPB>("plm_spGetProductsByPillBook @pillbookid=" + PillBookId + ", @countryid=" + CountryId + "").OrderBy(x => x.Brand).ToList();

            foreach (ProductPharmaFormsPB _prod in prod)
            {
                Products = new Models.ProductPharmaFormsPB();

                Products.Brand = _prod.Brand;
                Products.ProductId = _prod.ProductId;
                Products.PharmaForm = _prod.PharmaForm;
                Products.PharmaFormId = _prod.PharmaFormId;

                var acs = db.Database.SqlQuery<ActiveSubstance>("plm_spGetActiveSubstancesByBrand @productId=" + _prod.ProductId + "").OrderBy(x => x.Description).ToList();

                if (acs.LongCount() > 0)
                {
                    string activesubstaces = "";

                    foreach (ActiveSubstance _acs in acs)
                    {
                        activesubstaces = activesubstaces + _acs.Description;
                    }

                    Products.Substances = activesubstaces;
                }
                else
                {
                    Products.Substances = "";
                }

                LS.Add(Products);
            }

            LS = LS.OrderBy(x => x.Brand).ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult addproduct(string PillBook, string PharmaForm, string Product)
        {
            PillBookProducts PillBookProducts = new Models.PillBookProducts();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PillBookId = int.Parse(PillBook);
            int PharmaFormId = int.Parse(PharmaForm);
            int ProductId = int.Parse(Product);

            var pbp = db.PillBookProducts.Where(x => x.PharmaFormId == PharmaFormId && x.PillBookId == PillBookId && x.ProductId == ProductId).ToList();

            if (pbp.LongCount() == 0)
            {
                PillBookProducts.PharmaFormId = PharmaFormId;
                PillBookProducts.PillBookId = PillBookId;
                PillBookProducts.ProductId = ProductId;

                db.PillBookProducts.Add(PillBookProducts);
                db.SaveChanges();

                ActivityLog.insertpillbookproducts(PillBookId, ProductId, PharmaFormId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult addpillbookatc(string PBId, string Therapeutic)
        {
            PillBookATC PillBookATC = new Models.PillBookATC();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PillBookId = int.Parse(PBId);
            int TherapeuticId = int.Parse(Therapeutic);

            var pbatc = db.PillBookATC.Where(x => x.PillBookId == PillBookId && x.TherapeuticId == TherapeuticId).ToList();

            if (pbatc.LongCount() == 0)
            {
                PillBookATC = new Models.PillBookATC();

                PillBookATC.PillBookId = PillBookId;
                PillBookATC.TherapeuticId = TherapeuticId;

                db.PillBookATC.Add(PillBookATC);
                db.SaveChanges();

                ActivityLog.insertpillbookatc(PillBookId, TherapeuticId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult activetherapeuticstree(bool flag)
        {
            bool active = Convert.ToBoolean(flag);

            SessionActiveTT SessionActiveTT = new SessionActiveTT(active);
            Session["SessionActiveTT"] = SessionActiveTT;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCIElevel2(int? ICDId)
        {
            List<ICD> cie = new List<ICD>();
            ICD ICD = new Models.ICD();


            var cie10 = db.ICD.Where(x => x.Active == true && x.ParentId == ICDId && x.Level == 2).OrderBy(x => x.ICDKey).ToList();

            foreach (ICD _icd in cie10)
            {
                ICD = new ICD();

                ICD.Active = _icd.Active;
                ICD.EnglishDescription = _icd.EnglishDescription;
                ICD.ICDId = _icd.ICDId;
                ICD.ICDKey = _icd.ICDKey;
                ICD.Level = _icd.Level;
                ICD.ParentId = _icd.ParentId;

                var level3 = db.ICD.Where(x => x.ParentId == _icd.ICDId && x.Level == 3).OrderBy(x => x.ICDKey).ToList();
                if (level3.LongCount() > 0)
                {
                    ICD.SpanishDescription = "<img src='../Images/ui-icons_454545_256x2401.png' onclick='collapselevel2($(this))' id='collapselist_" + _icd.ICDId + "' style='display:none' /><img src='../Images/ui-icons_454545_256x240.png' onclick='getCIElevel2($(this))' id='" + _icd.ICDId + "' />&nbsp;<input type='checkbox' onclick='addpillbookcie($(this).val())' value='" + _icd.ICDId + "' id='" + _icd.ICDId + "' />&nbsp;<label style='color: #065977;'> " + _icd.ICDKey + " - &nbsp;</label>" + _icd.SpanishDescription + "<ul id='ICDl2_" + _icd.ICDId + "' style='list-style: none;'></ul>";
                }
                else
                {
                    ICD.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookcie($(this).val())' value='" + _icd.ICDId + "' id='" + _icd.ICDId + "' />&nbsp;<label style='color: #065977;'> " + _icd.ICDKey + " - &nbsp;</label>" + _icd.SpanishDescription + "";
                }

                cie.Add(ICD);
            }

            return Json(cie, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCIElevel3(int? ICDId)
        {
            List<ICD> cie = new List<ICD>();
            ICD ICD = new Models.ICD();


            var cie10 = db.ICD.Where(x => x.Active == true && x.ParentId == ICDId && x.Level == 3).OrderBy(x => x.ICDKey).ToList();

            foreach (ICD _icd in cie10)
            {
                ICD = new ICD();

                ICD.Active = _icd.Active;
                ICD.EnglishDescription = _icd.EnglishDescription;
                ICD.ICDId = _icd.ICDId;
                ICD.ICDKey = _icd.ICDKey;
                ICD.Level = _icd.Level;
                ICD.ParentId = _icd.ParentId;

                ICD.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookcie($(this).val())' value='" + _icd.ICDId + "' id='" + _icd.ICDId + "' />&nbsp;<label style='color: #065977;'> " + _icd.ICDKey + " - &nbsp;</label>" + _icd.SpanishDescription + "";

                cie.Add(ICD);
            }

            return Json(cie, JsonRequestBehavior.AllowGet);
        }

        public JsonResult addpillbookcie(string PillBook, string ICD)
        {
            PillBookICD PillBookICD = new Models.PillBookICD();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PillBookId = int.Parse(PillBook);
            int ICDId = int.Parse(ICD);

            var pbc = db.PillBookICD.Where(x => x.PillBookId == PillBookId && x.ICDId == ICDId).ToList();

            if (pbc.LongCount() == 0)
            {
                PillBookICD = new PillBookICD();

                PillBookICD.ICDId = ICDId;
                PillBookICD.PillBookId = PillBookId;

                db.PillBookICD.Add(PillBookICD);
                db.SaveChanges();

                ActivityLog.insertpillbookcie(PillBookId, ICDId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getinnasubstances(string term)
        {
            INNActiveSubstances INNActiveSubstances = new Models.INNActiveSubstances();
            List<INNActiveSubstances> LS = new List<Models.INNActiveSubstances>();

            term = cleansubstancename(term.ToUpper().Trim());

            int Index = term.Length;

            if (Index <= 3)
            {
                var inna = db.INNActiveSubstances.Where(x => x.Description.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("Ü", "U")
                                        .ToUpper().Trim().StartsWith(term.ToUpper().Trim())).Distinct().OrderBy(x => x.Description).ToList();

                foreach (INNActiveSubstances _inna in inna)
                {
                    INNActiveSubstances = new INNActiveSubstances();

                    INNActiveSubstances.Active = _inna.Active;
                    INNActiveSubstances.Description = _inna.Description;
                    INNActiveSubstances.INNActiveSubstanceId = _inna.INNActiveSubstanceId;

                    LS.Add(INNActiveSubstances);
                }
            }
            if (Index > 3)
            {
                var inna = db.INNActiveSubstances.Where(x => x.Description.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("Ü", "U")
                                        .ToUpper().Trim().Contains(term.ToUpper().Trim())).Distinct().OrderBy(x => x.Description).ToList();

                foreach (INNActiveSubstances _inna in inna)
                {
                    INNActiveSubstances = new INNActiveSubstances();

                    INNActiveSubstances.Active = _inna.Active;
                    INNActiveSubstances.Description = _inna.Description;
                    INNActiveSubstances.INNActiveSubstanceId = _inna.INNActiveSubstanceId;

                    LS.Add(INNActiveSubstances);
                }
            }
            //else
            //{
            //    var inna = db.INNActiveSubstances.Where(x => x.Description.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("Ü", "U")
            //                            .ToUpper().Trim().StartsWith(term.ToUpper().Trim())).Distinct().OrderBy(x => x.Description).ToList();

            //    foreach (INNActiveSubstances _inna in inna)
            //    {
            //        INNActiveSubstances = new INNActiveSubstances();

            //        INNActiveSubstances.Active = _inna.Active;
            //        INNActiveSubstances.Description = _inna.Description;
            //        INNActiveSubstances.INNActiveSubstanceId = _inna.INNActiveSubstanceId;

            //        LS.Add(INNActiveSubstances);
            //    }
            //}
            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public static string cleansubstancename(string term)
        {
            term = term.Replace("Á", "A");
            term = term.Replace("É", "E");
            term = term.Replace("Í", "I");
            term = term.Replace("Ó", "O");
            term = term.Replace("Ú", "U");
            term = term.Replace("Ü", "U");

            return term;
        }

        public JsonResult addpillbookinnasubstances(string PillBook, string INNAId)
        {
            PillBookINNSubstances PillBookINNSubstances = new PillBookINNSubstances();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PillBookId = int.Parse(PillBook);
            int InnaActiveSubstanceId = int.Parse(INNAId);

            var pbinna = db.PillBookINNSubstances.Where(x => x.PillBookId == PillBookId && x.INNActiveSubstanceId == InnaActiveSubstanceId).ToList();

            if (pbinna.LongCount() == 0)
            {
                PillBookINNSubstances = new PillBookINNSubstances();

                PillBookINNSubstances.INNActiveSubstanceId = InnaActiveSubstanceId;
                PillBookINNSubstances.PillBookId = PillBookId;

                db.PillBookINNSubstances.Add(PillBookINNSubstances);
                db.SaveChanges();

                ActivityLog.insertpillbookinnasubstances(PillBookId, InnaActiveSubstanceId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }

        public void cleanpillbookasociate(int AttributeGroupId, int AttributeId, int EditionId, int PillBookId)
        {
            var pba = db.PillBookAssociated.Where(x => x.AttributeGroupId == AttributeGroupId && x.EditionId == EditionId &&
            x.PillBookId == PillBookId).ToList();

            if (pba.LongCount() > 0)
            {
                foreach (PillBookAssociated _pba in pba)
                {
                    var delete = db.PillBookAssociated.SingleOrDefault(x => x.AttributeGroupId == _pba.AttributeGroupId &&
                                                                      x.AttributeId == _pba.AttributeId &&
                                                                      x.EditionId == _pba.EditionId &&
                                                                      x.PillBookId == _pba.PillBookId &&
                                                                      x.PillBookAssociatedId == _pba.PillBookAssociatedId);

                    db.PillBookAssociated.Remove(delete);
                    db.SaveChanges();
                }
            }

            var eca = db.PillBookEncyclopedias.Where(x => x.AttributeGroupId == AttributeGroupId && x.EditionId == EditionId &&
                                                     x.PillBookId == PillBookId).ToList();

            if (eca.LongCount() > 0)
            {
                foreach (PillBookEncyclopedias _eca in eca)
                {
                    var delete = db.PillBookEncyclopedias.SingleOrDefault(x => x.AttributeGroupId == _eca.AttributeGroupId &&
                                                                      x.AttributeId == _eca.AttributeId &&
                                                                      x.EditionId == _eca.EditionId &&
                                                                      x.PillBookId == _eca.PillBookId &&
                                                                      x.EncyclopediaId == _eca.EncyclopediaId);

                    db.PillBookEncyclopedias.Remove(delete);
                    db.SaveChanges();
                }
            }
        }

        public JsonResult inserteditionpillbookcontent(string Attribute, String Content, string PillBook)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            EditionPillBookAttributes EditionPillBookAttributes = new Models.EditionPillBookAttributes();
            string ISBN = System.Configuration.ConfigurationManager.AppSettings["ISBN"];

            var ed = db.Editions.Where(x => x.ISBN == ISBN).ToList();
            int EditionId = ed[0].EditionId;
            int AttributeGroupId = int.Parse(Attribute);
            int PillBookId = int.Parse(PillBook);

            var attr = db.EditionAttributeGroup.Where(x => x.AttributeGroupId == AttributeGroupId && x.EditionId == EditionId).ToList();
            int AttributeId = attr[0].AttributeId;

            cleanpillbookasociate(AttributeGroupId, AttributeId, EditionId, PillBookId);

            insertpillbookasociate(Content, AttributeGroupId, AttributeId, EditionId, PillBookId);

            insertpillbookencyclopedia(Content, AttributeGroupId, AttributeId, EditionId, PillBookId);

            var epa = db.EditionPillBookAttributes.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId && x.PillBookId == PillBookId && x.PillBookIconId == null).ToList();

            if (epa.LongCount() == 0)
            {
                EditionPillBookAttributes.AttributeGroupId = AttributeGroupId;
                EditionPillBookAttributes.AttributeId = AttributeId;
                EditionPillBookAttributes.EditionId = EditionId;
                EditionPillBookAttributes.PillBookId = PillBookId;
                EditionPillBookAttributes.Content = Content.Trim();

                db.EditionPillBookAttributes.Add(EditionPillBookAttributes);
                db.SaveChanges();

                ActivityLog.inserteditionpillbookattributes(AttributeGroupId, AttributeId, EditionId, PillBookId, Content, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                foreach (EditionPillBookAttributes _epa in epa)
                {
                    if (epa.LongCount() > 0)
                    {
                        string ContentBD = "";

                        ContentBD = _epa.Content;

                        var pba = db.PillBookAssociated.Where(x => x.PillBookId == PillBookId && x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId).ToList();

                        if (pba.LongCount() > 0)
                        {
                            foreach (PillBookAssociated _pba in pba)
                            {
                                var pb = db.PillBook.Where(x => x.PillBookId == _pba.PillBookAssociatedId).ToList();

                                if (pb.LongCount() > 0)
                                {
                                    foreach (PillBook _pb in pb)
                                    {
                                        //Content = Content.Trim().Replace("[" + _pb.PillBookName.ToUpper().Trim() + "(" + _pb.PIllBookCode + ")" + "]", _pb.PillBookName.ToUpper().Trim() + "(" + _pb.PIllBookCode + ")");
                                        //Content = Content.Trim().Replace("[" + _pb.PillBookName.ToUpper().Trim() + "(" + _pb.PIllBookCode + ")" + "]", _pb.PillBookName.ToUpper().Trim() + _pb.PIllBookCode);
                                        Content = Content.Trim().Replace("[" + _pb.PillBookName.Trim() + "(" + _pb.PIllBookCode + ")" + "]", _pb.PillBookName.Trim() + "(" + _pb.PIllBookCode + ")");

                                    }
                                }
                            }

                        }
                        else
                        {
                            EditionPillBookAttributes.Content = Content.Trim();
                        }

                        var eca = db.PillBookEncyclopedias.Where(x => x.PillBookId == PillBookId && x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId).ToList();
                        if (eca.LongCount() > 0)
                        {
                            foreach (PillBookEncyclopedias _eca in eca)
                            {
                                var ec = db.Encyclopedias.Where(x => x.EncyclopediaId == _eca.EncyclopediaId).ToList();

                                if (ec.LongCount() > 0)
                                {
                                    foreach (Encyclopedias _ec in ec)
                                    {
                                        //Content = Content.Trim().Replace("[" + _ec.EncyclopediaName.ToUpper().Trim() + "(" + _ec.PLMCode + ")" + "]", _ec.EncyclopediaName.ToUpper().Trim() + "(" + _ec.PLMCode + ")");
                                        //Content = Content.Trim().Replace("[" + _ec.EncyclopediaName.ToLower().Trim() + "(" + _ec.PLMCode + ")" + "]", _ec.EncyclopediaName.ToLower().Trim() + "(" + _ec.PLMCode + ")");
                                        Content = Content.Trim().Replace("[" + _ec.EncyclopediaName.Trim() + "(" + _ec.PLMCode + ")" + "]", _ec.EncyclopediaName.Trim() + "(" + _ec.PLMCode + ")");
                                    }
                                }
                            }
                        }
                    }
                    _epa.Content = Content.Trim();

                    db.SaveChanges();

                    ActivityLog.editeditionpillbookattributes(AttributeGroupId, AttributeId, EditionId, PillBookId, Content, ApplicationId, UsersId);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getcontent(string Attribute, string PillBook)
        {
            EditionPillBookAttributes EditionPillBookAttributes = new Models.EditionPillBookAttributes();
            List<EditionPillBookAttributes> LS = new List<Models.EditionPillBookAttributes>();

            string ISBN = System.Configuration.ConfigurationManager.AppSettings["ISBN"];

            var ed = db.Editions.Where(x => x.ISBN == ISBN).ToList();
            int EditionId = ed[0].EditionId;
            int AttributeGroupId = int.Parse(Attribute);
            int PillBookId = int.Parse(PillBook);

            var attr = db.EditionAttributeGroup.Where(x => x.AttributeGroupId == AttributeGroupId && x.EditionId == EditionId).ToList();
            int AttributeId = attr[0].AttributeId;

            var epa = db.EditionPillBookAttributes.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId && x.PillBookId == PillBookId).ToList();

            if (epa.LongCount() > 0)
            {
                string ContentBD = "";

                foreach (EditionPillBookAttributes _epa in epa)
                {
                    ContentBD = _epa.Content;

                    var pba = db.PillBookAssociated.Where(x => x.PillBookId == PillBookId && x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId).ToList();

                    if (pba.LongCount() > 0)
                    {
                        foreach (PillBookAssociated _pba in pba)
                        {
                            var pb = db.PillBook.Where(x => x.PillBookId == _pba.PillBookAssociatedId).ToList();

                            if (pb.LongCount() > 0)
                            {
                                foreach (PillBook _pb in pb)
                                {
                                    //ContentBD = ContentBD.Trim().Replace(_pb.PillBookName.ToLower().Trim() + "(" + _pb.PIllBookCode + ")", "[" + _pb.PillBookName.ToLower().Trim() + "(" + _pb.PIllBookCode + ")]");
                                    //ContentBD = ContentBD.Trim().Replace(_pb.PillBookName.ToUpper().Trim() + "(" + _pb.PIllBookCode + ")", "[" + _pb.PillBookName.ToUpper().Trim() + "(" + _pb.PIllBookCode + ")]");
                                    ContentBD = ContentBD.Trim().Replace(_pb.PillBookName.Trim() + "(" + _pb.PIllBookCode + ")", "[" + _pb.PillBookName.Trim() + "(" + _pb.PIllBookCode + ")]");
                                }
                            }
                        }
                    }
                    else
                    {
                        EditionPillBookAttributes.Content = _epa.Content;
                    }

                    var eca = db.PillBookEncyclopedias.Where(x => x.PillBookId == PillBookId && x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId).ToList();
                    if (eca.LongCount() > 0)
                    {
                        foreach (PillBookEncyclopedias _eca in eca)
                        {
                            var ec = db.Encyclopedias.Where(x => x.EncyclopediaId == _eca.EncyclopediaId).ToList();

                            if (ec.LongCount() > 0)
                            {
                                foreach (Encyclopedias _ec in ec)
                                {
                                    //ContentBD = ContentBD.Trim().Replace(_ec.EncyclopediaName.ToUpper().Trim() + "(" + _ec.PLMCode + ")", "[" + _ec.EncyclopediaName.ToUpper().Trim() + "(" + _ec.PLMCode + ")]");
                                    //ContentBD = ContentBD.Trim().Replace(_ec.EncyclopediaName.ToLower().Trim() + "(" + _ec.PLMCode + ")", "[" + _ec.EncyclopediaName.ToLower().Trim() + "(" + _ec.PLMCode + ")]");
                                    ContentBD = ContentBD.Trim().Replace(_ec.EncyclopediaName.Trim() + "(" + _ec.PLMCode + ")", "[" + _ec.EncyclopediaName.Trim() + "(" + _ec.PLMCode + ")]");
                                }
                            }
                        }
                    }

                }
                EditionPillBookAttributes.Content = ContentBD;
                LS.Add(EditionPillBookAttributes);
            }
            else
            {
                EditionPillBookAttributes.Content = "";
                LS.Add(EditionPillBookAttributes);
            }

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public void insertpillbookasociate(String Content, int AttributeGroupId, int AttributeId, int EditionId, int PillBookId)
        {
            EditionPillBookAttributes EditionPillBookAttributes = new Models.EditionPillBookAttributes();
            PillBookAssociated PillBookAssociated = new Models.PillBookAssociated();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var epa = db.EditionPillBookAttributes.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId && x.PillBookId == PillBookId && x.PillBookIconId == null).ToList();

            if (epa.LongCount() == 0)
            {
                EditionPillBookAttributes.AttributeGroupId = AttributeGroupId;
                EditionPillBookAttributes.AttributeId = AttributeId;
                EditionPillBookAttributes.EditionId = EditionId;
                EditionPillBookAttributes.PillBookId = PillBookId;
                EditionPillBookAttributes.Content = Content.Trim();

                db.EditionPillBookAttributes.Add(EditionPillBookAttributes);
                db.SaveChanges();

                ActivityLog.inserteditionpillbookattributes(AttributeGroupId, AttributeId, EditionId, PillBookId, Content, ApplicationId, UsersId);
            }

            int begin, end;
            string key = "";
            string begintag = "(";

            String ContentC = Content;

            while (ContentC != null)
            {
                begin = ContentC.IndexOf("(");

                if (begin >= 0)
                {
                    end = ContentC.IndexOf(")", begin);
                    key = ContentC.Substring(begin + begintag.Length, end - (begin + begintag.Length));
                    ContentC = ContentC.Substring(end);

                    var pb = db.PillBook.Where(x => x.PIllBookCode == key).ToList();

                    if (pb.LongCount() > 0)
                    {
                        int PillBookAssociatedId = pb[0].PillBookId;

                        var pba = db.PillBookAssociated.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId &&
                                                          x.PillBookId == PillBookId && x.PillBookAssociatedId == PillBookAssociatedId).ToList();

                        if (pba.LongCount() == 0)
                        {
                            PillBookAssociated = new Models.PillBookAssociated();

                            PillBookAssociated.AttributeGroupId = AttributeGroupId;
                            PillBookAssociated.AttributeId = AttributeId;
                            PillBookAssociated.EditionId = EditionId;
                            PillBookAssociated.PillBookAssociatedId = PillBookAssociatedId;
                            PillBookAssociated.PillBookId = PillBookId;

                            db.PillBookAssociated.Add(PillBookAssociated);
                            db.SaveChanges();

                            ActivityLog.insertpillbookasociated(AttributeGroupId, AttributeId, EditionId, PillBookId, PillBookAssociatedId, ApplicationId, UsersId);
                        }
                    }
                }
                else
                {
                    ContentC = null;
                }
            }
        }

        public void insertpillbookencyclopedia(String Content, int AttributeGroupId, int AttributeId, int EditionId, int PillBookId)
        {
            EditionPillBookAttributes EditionPillBookAttributes = new Models.EditionPillBookAttributes();
            PillBookEncyclopedias PillBookEncyclopedias = new Models.PillBookEncyclopedias();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var epa = db.EditionPillBookAttributes.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId && x.PillBookId == PillBookId && x.PillBookIconId == null).ToList();

            if (epa.LongCount() == 0)
            {
                EditionPillBookAttributes.AttributeGroupId = AttributeGroupId;
                EditionPillBookAttributes.AttributeId = AttributeId;
                EditionPillBookAttributes.EditionId = EditionId;
                EditionPillBookAttributes.PillBookId = PillBookId;
                EditionPillBookAttributes.Content = Content.Trim();

                db.EditionPillBookAttributes.Add(EditionPillBookAttributes);
                db.SaveChanges();

                ActivityLog.inserteditionpillbookattributes(AttributeGroupId, AttributeId, EditionId, PillBookId, Content, ApplicationId, UsersId);
            }

            int begin, end;
            string key = "";
            string begintag = "(";

            String ContentC = Content;

            while (ContentC != null)
            {
                begin = ContentC.IndexOf("(");

                if (begin >= 0)
                {
                    end = ContentC.IndexOf(")", begin);
                    key = ContentC.Substring(begin + begintag.Length, end - (begin + begintag.Length));
                    ContentC = ContentC.Substring(end);

                    var pb = db.Encyclopedias.Where(x => x.PLMCode == key).ToList();

                    if (pb.LongCount() > 0)
                    {
                        int EncyclopediaId = pb[0].EncyclopediaId;

                        var pba = db.PillBookEncyclopedias.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId &&
                                                          x.PillBookId == PillBookId && x.EncyclopediaId == EncyclopediaId).ToList();
                        if (pba.LongCount() == 0)
                        {
                            PillBookEncyclopedias = new Models.PillBookEncyclopedias();

                            PillBookEncyclopedias.AttributeGroupId = AttributeGroupId;
                            PillBookEncyclopedias.AttributeId = AttributeId;
                            PillBookEncyclopedias.EditionId = EditionId;
                            PillBookEncyclopedias.EncyclopediaId = EncyclopediaId;
                            PillBookEncyclopedias.PillBookId = PillBookId;

                            db.PillBookEncyclopedias.Add(PillBookEncyclopedias);
                            db.SaveChanges();

                            ActivityLog.insertpillbookasociated(AttributeGroupId, AttributeId, EditionId, PillBookId, EncyclopediaId, ApplicationId, UsersId);
                        }
                    }
                }
                else
                {
                    ContentC = null;
                }
            }
        }

        public JsonResult asociatepillbook(string Attribute, string PillBook, string PillBookAsociate)
        {
            SessionListPillBooks ind = (SessionListPillBooks)Session["SessionListPillBooks"];

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            PillBookAssociated PillBookAssociated = new Models.PillBookAssociated();

            string ISBN = System.Configuration.ConfigurationManager.AppSettings["ISBN"];

            var ed = db.Editions.Where(x => x.ISBN == ISBN).ToList();

            int EditionId = ed[0].EditionId;
            int AttributeGroupId = int.Parse(Attribute);
            int PillBookId = int.Parse(PillBook);
            int PillBookAsociateId = int.Parse(PillBookAsociate);

            var attr = db.EditionAttributeGroup.Where(x => x.AttributeGroupId == AttributeGroupId && x.EditionId == EditionId).ToList();
            int AttributeId = attr[0].AttributeId;

            var pba = db.PillBookAssociated.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId &&
                                                  x.EditionId == EditionId && x.PillBookAssociatedId == PillBookAsociateId &&
                                                  x.PillBookId == PillBookId).ToList();

            if (pba.LongCount() == 0)
            {
                //var epa = db.EditionPillBookAttributes.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId && x.PillBookId == PillBookId).ToList();

                //if (epa.LongCount() == 0)
                //{
                //    return Json("errorpbc", JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                var pb = db.PillBook.Where(x => x.PillBookId == PillBookAsociateId).ToList();

                string pbname = pb[0].PillBookName + "(" + pb[0].PIllBookCode + ")";

                if (ind != null)
                {
                    foreach (ListPillBooks element in ind.PillBook)
                    {
                        PB = new ListPillBooks();

                        PB.Id = element.Id;
                        PB.Value = element.Value;

                        LPB.Add(PB);
                    }
                }

                PB = new ListPillBooks();

                PB.Id = pb[0].PillBookId;
                PB.Value = pbname;

                LPB.Add(PB);

                SessionListPillBooks SessionListPillBooks = new Models.SessionListPillBooks(LPB);
                Session["SessionListPillBooks"] = SessionListPillBooks;


                string pbs = "";

                foreach (var item in LPB)
                {
                    pbs = pbs + "[" + item.Value + "]";
                }

                return Json(pbs, JsonRequestBehavior.AllowGet);
                //}
            }
            else
            {
                var pb = db.PillBook.Where(x => x.PillBookId == PillBookAsociateId).ToList();

                string pbname = pb[0].PillBookName + "(" + pb[0].PIllBookCode + ")";

                if (ind != null)
                {
                    foreach (ListPillBooks element in ind.PillBook)
                    {
                        PB = new ListPillBooks();

                        PB.Id = element.Id;
                        PB.Value = element.Value;

                        LPB.Add(PB);
                    }
                }

                PB = new ListPillBooks();

                PB.Id = pb[0].PillBookId;
                PB.Value = pbname;

                LPB.Add(PB);

                SessionListPillBooks SessionListPillBooks = new Models.SessionListPillBooks(LPB);
                Session["SessionListPillBooks"] = SessionListPillBooks;


                string pbs = "";

                foreach (var item in LPB)
                {
                    pbs = pbs + "[" + item.Value + "]";
                }

                return Json(pbs, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult asociateencyclopedia(string Attribute, string PillBook, string EncyclopediAsociate)
        {
            SessionListPillBooks ind = (SessionListPillBooks)Session["SessionListPillBooks"];

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            PillBookEncyclopedias PillBookEncyclopedias = new Models.PillBookEncyclopedias();

            string ISBN = System.Configuration.ConfigurationManager.AppSettings["ISBN"];

            var ed = db.Editions.Where(x => x.ISBN == ISBN).ToList();

            int EditionId = ed[0].EditionId;
            int AttributeGroupId = int.Parse(Attribute);
            int PillBookId = int.Parse(PillBook);
            int EncyclopediaId = int.Parse(EncyclopediAsociate);

            var attr = db.EditionAttributeGroup.Where(x => x.AttributeGroupId == AttributeGroupId && x.EditionId == EditionId).ToList();
            int AttributeId = attr[0].AttributeId;

            var ec = db.PillBookEncyclopedias.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId &&
                                                    x.EditionId == EditionId && x.EncyclopediaId == EncyclopediaId &&
                                                    x.PillBookId == PillBookId).ToList();

            if (ec.LongCount() == 0)
            {
                var enc = db.Encyclopedias.Where(x => x.EncyclopediaId == EncyclopediaId).ToList();

                string pbname = enc[0].EncyclopediaName.Trim() + "(" + enc[0].PLMCode + ")";

                if (ind != null)
                {
                    foreach (ListPillBooks element in ind.PillBook)
                    {
                        PB = new ListPillBooks();

                        PB.Id = enc[0].EncyclopediaId;
                        PB.Value = pbname;

                        LPB.Add(element);
                    }
                }

                PB = new ListPillBooks();

                PB.Id = enc[0].EncyclopediaId;
                PB.Value = pbname;

                LPB.Add(PB);

                SessionListPillBooks SessionListPillBooks = new Models.SessionListPillBooks(LPB);
                Session["SessionListPillBooks"] = SessionListPillBooks;

                string pbs = "";

                foreach (var item in LPB)
                {
                    pbs = pbs + "[" + item.Value + "]";
                }

                return Json(pbs, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var enc = db.Encyclopedias.Where(x => x.EncyclopediaId == EncyclopediaId).ToList();

                string pbname = enc[0].EncyclopediaName.Trim() + "(" + enc[0].PLMCode + ")";

                if (ind != null)
                {
                    foreach (ListPillBooks element in ind.PillBook)
                    {
                        PB = new ListPillBooks();

                        PB.Id = enc[0].EncyclopediaId;
                        PB.Value = pbname;

                        LPB.Add(element);
                    }
                }

                PB = new ListPillBooks();

                PB.Id = enc[0].EncyclopediaId;
                PB.Value = pbname;

                LPB.Add(PB);

                SessionListPillBooks SessionListPillBooks = new Models.SessionListPillBooks(LPB);
                Session["SessionListPillBooks"] = SessionListPillBooks;

                string pbs = "";

                foreach (var item in LPB)
                {
                    pbs = pbs + "[" + item.Value + "]";
                }

                return Json(pbs, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getpillbooks()
        {
            PillBook PillBook = new Models.PillBook();
            List<PillBook> LS = new List<Models.PillBook>();

            var pb = db.PillBook.Where(x => x.Active == true).OrderBy(x => x.PillBookName).ToList();

            foreach (PillBook _pb in pb)
            {
                PillBook = new Models.PillBook();

                PillBook.PillBookId = _pb.PillBookId;
                PillBook.PillBookName = _pb.PillBookName;

                LS.Add(PillBook);
            }


            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getencyclopedias()
        {
            Encyclopedias Encyclopedias = new Encyclopedias();
            List<Encyclopedias> LS = new List<Encyclopedias>();

            var ec = db.Encyclopedias.Where(x => x.Active == true).OrderBy(x => x.EncyclopediaName).ToList();

            foreach (Encyclopedias _ec in ec)
            {
                Encyclopedias = new Models.Encyclopedias();

                Encyclopedias.EncyclopediaId = _ec.EncyclopediaId;
                Encyclopedias.EncyclopediaName = _ec.EncyclopediaName;

                LS.Add(Encyclopedias);
            }

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertpillbookicon(string Attribute, string PillBookIcon, string PillBook)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            EditionPillBookAttributes EditionPillBookAttributes = new Models.EditionPillBookAttributes();
            PillBookIcons PillBookIcons = new Models.PillBookIcons();

            string ISBN = System.Configuration.ConfigurationManager.AppSettings["ISBN"];

            var ed = db.Editions.Where(x => x.ISBN == ISBN).ToList();
            int EditionId = ed[0].EditionId;
            int AttributeGroupId = int.Parse(Attribute);
            int PillBookId = int.Parse(PillBook);

            var attr = db.EditionAttributeGroup.Where(x => x.AttributeGroupId == AttributeGroupId && x.EditionId == EditionId).ToList();
            int AttributeId = attr[0].AttributeId;

            byte PillBookIconId = Convert.ToByte(PillBookIcon);

            var epa = db.EditionPillBookAttributes.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId && x.PillBookId == PillBookId).ToList();

            if (epa.LongCount() > 0)
            {
                foreach (EditionPillBookAttributes _epa in epa)
                {
                    if (string.IsNullOrEmpty(_epa.Content))
                    {
                        if (_epa.PillBookIconId != PillBookIconId)
                        {
                            _epa.PillBookIconId = PillBookIconId;

                            db.SaveChanges();

                            ActivityLog.editpillbookicon(AttributeGroupId, AttributeId, EditionId, PillBookId, PillBookIconId, null, ApplicationId, UsersId);

                            return Json(true, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        _epa.Content = "";
                        _epa.PillBookIconId = PillBookIconId;

                        db.SaveChanges();

                        ActivityLog.editpillbookicon(AttributeGroupId, AttributeId, EditionId, PillBookId, PillBookIconId, "", ApplicationId, UsersId);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                EditionPillBookAttributes.AttributeGroupId = AttributeGroupId;
                EditionPillBookAttributes.AttributeId = AttributeId;
                EditionPillBookAttributes.EditionId = EditionId;
                EditionPillBookAttributes.PillBookId = PillBookId;
                EditionPillBookAttributes.Content = "";
                EditionPillBookAttributes.PillBookIconId = PillBookIconId;

                db.EditionPillBookAttributes.Add(EditionPillBookAttributes);

                db.SaveChanges();

                ActivityLog.insertpillbookicon(AttributeGroupId, AttributeId, EditionId, PillBookId, PillBookIconId, "", ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deletepillbookinnasubstances(string PillBook, string INNAId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            PillBookINNSubstances PillBookINNSubstances = new PillBookINNSubstances();

            int PillBookId = int.Parse(PillBook);
            int InnaActiveSubstanceId = int.Parse(INNAId);

            var pbinna = db.PillBookINNSubstances.Where(x => x.PillBookId == PillBookId && x.INNActiveSubstanceId == InnaActiveSubstanceId).ToList();

            if (pbinna.LongCount() > 0)
            {

                var delete = db.PillBookINNSubstances.SingleOrDefault(x => x.INNActiveSubstanceId == InnaActiveSubstanceId && x.PillBookId == PillBookId);
                db.PillBookINNSubstances.Remove(delete);
                db.SaveChanges();

                ActivityLog.deletepillbookinnSubstances(PillBookId, InnaActiveSubstanceId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult deletepillbookatc(string PillBook, string Therapeutic)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            PillBookINNSubstances PillBookINNSubstances = new PillBookINNSubstances();

            int PillBookId = int.Parse(PillBook);
            int TherapeuticId = int.Parse(Therapeutic);

            var pbatc = db.PillBookATC.Where(x => x.PillBookId == PillBookId && x.TherapeuticId == TherapeuticId).ToList();

            if (pbatc.LongCount() > 0)
            {

                var delete = db.PillBookATC.SingleOrDefault(x => x.TherapeuticId == TherapeuticId && x.PillBookId == PillBookId);
                db.PillBookATC.Remove(delete);
                db.SaveChanges();

                ActivityLog.deletepillbookatc(PillBookId, TherapeuticId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult deletepillbookproducts(string PillBook, string Product, string PharmaForm)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            PillBookINNSubstances PillBookINNSubstances = new PillBookINNSubstances();

            int PillBookId = int.Parse(PillBook);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);

            var pbprod = db.PillBookProducts.Where(x => x.PillBookId == PillBookId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (pbprod.LongCount() > 0)
            {

                var delete = db.PillBookProducts.SingleOrDefault(x => x.PillBookId == PillBookId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
                db.PillBookProducts.Remove(delete);
                db.SaveChanges();

                ActivityLog.deletepillbookproducts(PillBookId, ProductId, PharmaFormId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deletepillbookcie(string PillBook, string ICD)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            PillBookINNSubstances PillBookINNSubstances = new PillBookINNSubstances();

            int PillBookId = int.Parse(PillBook);
            int ICDId = int.Parse(ICD);

            var pbicd = db.PillBookICD.Where(x => x.PillBookId == PillBookId && x.ICDId == ICDId).ToList();

            if (pbicd.LongCount() > 0)
            {

                var delete = db.PillBookICD.SingleOrDefault(x => x.PillBookId == PillBookId && x.ICDId == ICDId);
                db.PillBookICD.Remove(delete);
                db.SaveChanges();

                ActivityLog.deletepillbookcie(PillBookId, ICDId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getcheckoptions(string PillBook, string Attribute)
        {
            EditionPillBookAttributes EditionPillBookAttributes = new Models.EditionPillBookAttributes();
            List<EditionPillBookAttributes> LS = new List<Models.EditionPillBookAttributes>();

            string ISBN = System.Configuration.ConfigurationManager.AppSettings["ISBN"];

            var ed = db.Editions.Where(x => x.ISBN == ISBN).ToList();
            int EditionId = ed[0].EditionId;
            int AttributeGroupId = int.Parse(Attribute);
            int PillBookId = int.Parse(PillBook);

            var attr = db.EditionAttributeGroup.Where(x => x.AttributeGroupId == AttributeGroupId && x.EditionId == EditionId).ToList();
            int AttributeId = attr[0].AttributeId;

            var epa = db.EditionPillBookAttributes.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId && x.PillBookId == PillBookId).ToList();

            if (epa.LongCount() > 0)
            {
                byte? PillBookIconId = epa[0].PillBookIconId;

                if (PillBookIconId != null)
                {
                    var pbi = db.PillBookIcons.Where(x => x.PillBookIconId == PillBookIconId).ToList();

                    if (pbi.LongCount() > 0)
                    {
                        int PillBookIconAnswerId = pbi[0].PillBookIconAnswerId;

                        var pba = db.PillBookIconAnswers.Where(x => x.PillBookIconAnswerId == PillBookIconAnswerId).ToList();

                        if (pba.LongCount() > 0)
                        {
                            string answer = pba[0].PillBookIconAnswer;

                            if (answer.ToUpper() == "SI")
                            {
                                return Json("SI", JsonRequestBehavior.AllowGet);
                            }
                            if (answer.ToUpper() == "NO")
                            {
                                return Json("NO", JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
               
            }
            else
            {
                EditionPillBookAttributes = new EditionPillBookAttributes();

                EditionPillBookAttributes.AttributeGroupId = AttributeGroupId;
                EditionPillBookAttributes.AttributeId = AttributeId;
                EditionPillBookAttributes.Content = "";
                EditionPillBookAttributes.EditionId = EditionId;
                EditionPillBookAttributes.PillBookIconId = null;
                EditionPillBookAttributes.PillBookId = PillBookId;

                db.EditionPillBookAttributes.Add(EditionPillBookAttributes);

                db.SaveChanges();

                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cleanlist()
        {
            Session["SessionListPillBooks"] = null;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult savereference(string ClinicalReference, string URL, string PillBook)
        {
            ClinicalReferences ClinicalReferences = new Models.ClinicalReferences();
            PillBookReferences PillBookReferences = new Models.PillBookReferences();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PillBookId = int.Parse(PillBook);
            int ClinicalReferenceId = 0;

            var cr = db.ClinicalReferences.Where(x => x.ClinicalReference.ToUpper().Trim() == ClinicalReference.ToUpper().Trim() && x.ReferenceSource.ToUpper().Trim() == URL.ToUpper().Trim() && x.Active == true).ToList();

            if (cr.LongCount() == 0)
            {
                ClinicalReferences.Active = true;
                ClinicalReferences.ClinicalReference = ClinicalReference.Trim();
                if (!string.IsNullOrEmpty(URL))
                {
                    ClinicalReferences.ReferenceSource = URL.Trim();
                }
                else
                {
                    ClinicalReferences.ReferenceSource = null;
                }

                db.ClinicalReferences.Add(ClinicalReferences);
                db.SaveChanges();
            }

            if (!string.IsNullOrEmpty(URL))
            {
                var crss = db.ClinicalReferences.Where(x => x.ClinicalReference.ToUpper().Trim() == ClinicalReference.ToUpper().Trim() && x.ReferenceSource.ToUpper().Trim() == URL.ToUpper().Trim() && x.Active == true).ToList();

                ClinicalReferenceId = crss[0].ClinicalReferenceId;

                ActivityLog.clinicalreferences(ClinicalReferenceId, ClinicalReference, URL, 1, ApplicationId, UsersId);
            }
            else
            {
                var crss = db.ClinicalReferences.Where(x => x.ClinicalReference.ToUpper().Trim() == ClinicalReference.ToUpper().Trim() && x.ReferenceSource.ToUpper().Trim() == null && x.Active == true).ToList();

                ClinicalReferenceId = crss[0].ClinicalReferenceId;

                ActivityLog.clinicalreferences(ClinicalReferenceId, ClinicalReference, URL, 1, ApplicationId, UsersId);
            }


            var pbr = db.PillBookReferences.Where(x => x.ClinicalReferenceId == ClinicalReferenceId && x.PillBookId == PillBookId).ToList();

            if (pbr.LongCount() == 0)
            {
                PillBookReferences.ClinicalReferenceId = ClinicalReferenceId;
                PillBookReferences.PillBookId = PillBookId;

                db.PillBookReferences.Add(PillBookReferences);
                db.SaveChanges();

                ActivityLog.insertpillbookreferences(PillBookId, ClinicalReferenceId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getreferences(string ClinicalReference, string URL, string PillBook)
        {
            ClinicalReferences ClinicalReferences = new Models.ClinicalReferences();
            List<ClinicalReferences> LC = new List<Models.ClinicalReferences>();

            int PillBookId = int.Parse(PillBook);

            var refe = (from pr in db.PillBookReferences
                        join cr in db.ClinicalReferences
                            on pr.ClinicalReferenceId equals cr.ClinicalReferenceId
                        where pr.PillBookId == PillBookId
                        && cr.Active == true
                        select cr).OrderBy(x => x.ClinicalReference).ToList();

            foreach (ClinicalReferences _refe in refe)
            {
                ClinicalReferences = new ClinicalReferences();

                ClinicalReferences.Active = _refe.Active;
                ClinicalReferences.ClinicalReference = _refe.ClinicalReference;
                ClinicalReferences.ClinicalReferenceId = _refe.ClinicalReferenceId;
                ClinicalReferences.ReferenceSource = _refe.ReferenceSource;

                LC.Add(ClinicalReferences);
            }
            return Json(LC, JsonRequestBehavior.AllowGet);
        }

        public JsonResult editreferencepb(string ClinicalReference, string ReferenceSource, string ClinicarRId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ClinicalReferenceId = int.Parse(ClinicarRId);

            var cr = db.ClinicalReferences.Where(x => x.ClinicalReferenceId == ClinicalReferenceId && x.Active == true).ToList();

            if (cr.LongCount() > 0)
            {
                foreach (ClinicalReferences _cr in cr)
                {
                    _cr.ClinicalReference = ClinicalReference;
                    if (!string.IsNullOrEmpty(ReferenceSource))
                    {
                        _cr.ReferenceSource = ReferenceSource;

                        db.SaveChanges();

                        ActivityLog.clinicalreferences(ClinicalReferenceId, ClinicalReference, ReferenceSource, 2, ApplicationId, UsersId);
                    }
                    else
                    {
                        _cr.ReferenceSource = null;

                        db.SaveChanges();

                        ActivityLog.clinicalreferences(ClinicalReferenceId, ClinicalReference, null, 2, ApplicationId, UsersId);
                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult deletereferences(string PillBook, string ClinicarRId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ClinicalReferenceId = int.Parse(ClinicarRId);
            int PillBookId = int.Parse(PillBook);

            var cr = db.PillBookReferences.Where(x => x.ClinicalReferenceId == ClinicalReferenceId && x.PillBookId == PillBookId).ToList();

            if (cr.LongCount() > 0)
            {
                var delete = db.PillBookReferences.SingleOrDefault(x => x.ClinicalReferenceId == ClinicalReferenceId && x.PillBookId == PillBookId);

                if (delete != null)
                {
                    db.PillBookReferences.Remove(delete);
                    db.SaveChanges();

                    ActivityLog.deletepillbookreferences(PillBookId, ClinicalReferenceId, ApplicationId, UsersId);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult seachpillbook(String PillBookName)
        {
            List<PillBook> LP = new List<Models.PillBook>();

            LP = db.Database.SqlQuery<PillBook>("plm_spGetPillBooksBySearch @param='" + PillBookName + "'").ToList();

            return Json(LP, JsonRequestBehavior.AllowGet);
        }

        public JsonResult seachencyclopedia(String Encyclopedia)
        {
            List<Encyclopedias> LE = new List<Encyclopedias>();

            LE = db.Database.SqlQuery<Encyclopedias>("plm_spGetEncyclopediasBySearch @param='" + Encyclopedia + "'").ToList();

            return Json(LE, JsonRequestBehavior.AllowGet);
        }

        public JsonResult addactivepbf(string PBId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PillBookId = int.Parse(PBId);

            var pb = db.PillBook.Where(x => x.PillBookId == PillBookId).ToList();

            if (pb.LongCount() > 0)
            {
                foreach (PillBook _pb in pb)
                {
                    _pb.Finished = true;

                    db.SaveChanges();

                    ActivityLog.updatefinishedpillbook(PillBookId, true, ApplicationId, UsersId);

                    string PillBook = _pb.PillBookName + "(" + _pb.PIllBookCode + ")";

                    String Message = getmessageclose(UsersId, PillBook);

                    EmailClosePB(Message);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult openpillbook(string PBId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PillBookId = int.Parse(PBId);

            var pb = db.PillBook.Where(x => x.PillBookId == PillBookId).ToList();

            if (pb.LongCount() > 0)
            {
                foreach (PillBook _pb in pb)
                {
                    _pb.Finished = false;
                    _pb.ModifyDate = DateTime.Now;

                    db.SaveChanges();

                    ActivityLog.updatefinishedpillbook(PillBookId, false, ApplicationId, UsersId);

                    string PillBook = _pb.PillBookName + "(" + _pb.PIllBookCode + ")";

                    String Message = getmessageopen(UsersId, PillBook);

                    EmailOpenPB(Message);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /*                          THERAPEUTIC_OMS                          */


        public JsonResult getOMSlevel2(int? Toms)
        {
            List<TherapeuticOMS> OMS = new List<TherapeuticOMS>();
            TherapeuticOMS TherapeuticOMS = new Models.TherapeuticOMS();


            var level2 = db.TherapeuticOMS.Where(x => x.Active == true && x.ParentId == Toms && x.Level == 2).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (TherapeuticOMS _level2 in level2)
            {
                TherapeuticOMS = new TherapeuticOMS();

                TherapeuticOMS.Active = _level2.Active;
                TherapeuticOMS.TherapeuticOMSId = _level2.TherapeuticOMSId;
                TherapeuticOMS.TherapeuticKey = _level2.TherapeuticKey;
                TherapeuticOMS.Level = _level2.Level;
                TherapeuticOMS.ParentId = _level2.ParentId;

                var level3 = db.TherapeuticOMS.Where(x => x.ParentId == _level2.TherapeuticOMSId && x.Level == 3).OrderBy(x => x.TherapeuticKey).ToList();
                if (level3.LongCount() > 0)
                {
                    TherapeuticOMS.SpanishDescription = "<img src='../Images/ui-icons_454545_256x2401.png' onclick='collapselevel2OMS($(this))' id='collapselistOMS_" + _level2.TherapeuticOMSId + "' style='display:none' /><img src='../Images/ui-icons_454545_256x240.png' onclick='getlevel2TOMS($(this))' id='" + _level2.TherapeuticOMSId + "' />&nbsp;<label style='color: #065977;'> " + _level2.TherapeuticKey + " - &nbsp;</label>" + _level2.SpanishDescription + "<ul id='OMSl2_" + _level2.TherapeuticOMSId + "' style='list-style: none;'></ul>";
                }
                else
                {
                    TherapeuticOMS.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookatcoms($(this).val())' value='" + _level2.TherapeuticOMSId + "' id='" + _level2.TherapeuticOMSId + "' />&nbsp;<label style='color: #065977;'> " + _level2.TherapeuticKey + " - &nbsp;</label>" + _level2.SpanishDescription + "";
                }

                OMS.Add(TherapeuticOMS);
            }

            return Json(OMS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getOMSlevel3(int? Toms)
        {
            List<TherapeuticOMS> OMS = new List<TherapeuticOMS>();
            TherapeuticOMS TherapeuticOMS = new Models.TherapeuticOMS();


            var level3 = db.TherapeuticOMS.Where(x => x.Active == true && x.ParentId == Toms && x.Level == 3).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (TherapeuticOMS _level3 in level3)
            {
                TherapeuticOMS = new TherapeuticOMS();

                TherapeuticOMS.Active = _level3.Active;
                TherapeuticOMS.TherapeuticOMSId = _level3.TherapeuticOMSId;
                TherapeuticOMS.TherapeuticKey = _level3.TherapeuticKey;
                TherapeuticOMS.Level = _level3.Level;
                TherapeuticOMS.ParentId = _level3.ParentId;

                var level4 = db.TherapeuticOMS.Where(x => x.ParentId == _level3.TherapeuticOMSId && x.Level == 4).OrderBy(x => x.TherapeuticKey).ToList();
                if (level4.LongCount() > 0)
                {
                    TherapeuticOMS.SpanishDescription = "<img src='../Images/ui-icons_454545_256x2401.png' onclick='collapselevel3OMS($(this))' id='collapselistOMS_" + _level3.TherapeuticOMSId + "' style='display:none' /><img src='../Images/ui-icons_454545_256x240.png' onclick='getlevel3TOMS($(this))' id='" + _level3.TherapeuticOMSId + "' />&nbsp;<label style='color: #065977;'> " + _level3.TherapeuticKey + " - &nbsp;</label>" + _level3.SpanishDescription + "<ul id='OMSl3_" + _level3.TherapeuticOMSId + "' style='list-style: none;'></ul>";
                }
                else
                {
                    TherapeuticOMS.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookatcoms($(this).val())' value='" + _level3.TherapeuticOMSId + "' id='" + _level3.TherapeuticOMSId + "' />&nbsp;<label style='color: #065977;'> " + _level3.TherapeuticKey + " - &nbsp;</label>" + _level3.SpanishDescription + "";
                }

                OMS.Add(TherapeuticOMS);
            }

            return Json(OMS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getOMSlevel4(int? Toms)
        {
            List<TherapeuticOMS> OMS = new List<TherapeuticOMS>();
            TherapeuticOMS TherapeuticOMS = new Models.TherapeuticOMS();


            var level4 = db.TherapeuticOMS.Where(x => x.Active == true && x.ParentId == Toms && x.Level == 4).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (TherapeuticOMS _level4 in level4)
            {
                TherapeuticOMS = new TherapeuticOMS();

                TherapeuticOMS.Active = _level4.Active;
                TherapeuticOMS.TherapeuticOMSId = _level4.TherapeuticOMSId;
                TherapeuticOMS.TherapeuticKey = _level4.TherapeuticKey;
                TherapeuticOMS.Level = _level4.Level;
                TherapeuticOMS.ParentId = _level4.ParentId;

                var level5 = db.TherapeuticOMS.Where(x => x.ParentId == _level4.TherapeuticOMSId && x.Level == 5).OrderBy(x => x.TherapeuticKey).ToList();
                if (level5.LongCount() > 0)
                {
                    TherapeuticOMS.SpanishDescription = "<img src='../Images/ui-icons_454545_256x2401.png' onclick='collapselevel4OMS($(this))' id='collapselistOMS_" + _level4.TherapeuticOMSId + "' style='display:none' /><img src='../Images/ui-icons_454545_256x240.png' onclick='getlevel4TOMS($(this))' id='" + _level4.TherapeuticOMSId + "' />&nbsp;<label style='color: #065977;'> " + _level4.TherapeuticKey + " - &nbsp;</label>" + _level4.SpanishDescription + "<ul id='OMSl4_" + _level4.TherapeuticOMSId + "' style='list-style: none;'></ul>";
                }
                else
                {
                    TherapeuticOMS.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookatcoms($(this).val())' value='" + _level4.TherapeuticOMSId + "' id='" + _level4.TherapeuticOMSId + "' />&nbsp;<label style='color: #065977;'> " + _level4.TherapeuticKey + " - &nbsp;</label>" + _level4.SpanishDescription + "";
                }

                OMS.Add(TherapeuticOMS);
            }

            return Json(OMS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getOMSlevel5(int? Toms)
        {
            List<TherapeuticOMS> OMS = new List<TherapeuticOMS>();
            TherapeuticOMS TherapeuticOMS = new Models.TherapeuticOMS();


            var level5 = db.TherapeuticOMS.Where(x => x.Active == true && x.ParentId == Toms && x.Level == 5).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (TherapeuticOMS _level5 in level5)
            {
                TherapeuticOMS = new TherapeuticOMS();

                TherapeuticOMS.Active = _level5.Active;
                TherapeuticOMS.TherapeuticOMSId = _level5.TherapeuticOMSId;
                TherapeuticOMS.TherapeuticKey = _level5.TherapeuticKey;
                TherapeuticOMS.Level = _level5.Level;
                TherapeuticOMS.ParentId = _level5.ParentId;

                var level6 = db.TherapeuticOMS.Where(x => x.ParentId == _level5.TherapeuticOMSId && x.Level == 6).OrderBy(x => x.TherapeuticKey).ToList();
                if (level6.LongCount() > 0)
                {
                    TherapeuticOMS.SpanishDescription = "<img src='../Images/ui-icons_454545_256x2401.png' onclick='collapselevel5OMS($(this))' id='collapselistOMS_" + _level5.TherapeuticOMSId + "' style='display:none' /><img src='../Images/ui-icons_454545_256x240.png' onclick='getlevel4TOMS($(this))' id='" + _level5.TherapeuticOMSId + "' />&nbsp;<label style='color: #065977;'> " + _level5.TherapeuticKey + " - &nbsp;</label>" + _level5.SpanishDescription + "<ul id='OMSl5_" + _level5.TherapeuticOMSId + "' style='list-style: none;'></ul>";
                }
                else
                {
                    TherapeuticOMS.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookatcoms($(this).val())' value='" + _level5.TherapeuticOMSId + "' id='" + _level5.TherapeuticOMSId + "' />&nbsp;<label style='color: #065977;'> " + _level5.TherapeuticKey + " - &nbsp;</label>" + _level5.SpanishDescription + "";
                }

                OMS.Add(TherapeuticOMS);
            }

            return Json(OMS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult addpillbookatcoms(string ATCId, string PillBook)
        {
            PillBookATCOMS PillBookATCOMS = new PillBookATCOMS();
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            int TherapeuticOMSId = int.Parse(ATCId);
            int PillBookId = int.Parse(PillBook);

            var pt = db.PillBookATCOMS.Where(x => x.TherapeuticOMSId == TherapeuticOMSId && x.PillBookId == PillBookId).ToList();

            if (pt.LongCount() == 0)
            {
                PillBookATCOMS = new PillBookATCOMS();

                PillBookATCOMS.PillBookId = PillBookId;
                PillBookATCOMS.TherapeuticOMSId = TherapeuticOMSId;

                db.PillBookATCOMS.Add(PillBookATCOMS);
                db.SaveChanges();


                ActivityLog.insertpillbookatcOMS(PillBookId, TherapeuticOMSId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deletepillbookatcoms(string PillBook, string Therapeutic)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PillBookId = int.Parse(PillBook);
            int TherapeuticOMSId = int.Parse(Therapeutic);

            var pbatc = db.PillBookATCOMS.Where(x => x.PillBookId == PillBookId && x.TherapeuticOMSId == TherapeuticOMSId).ToList();

            if (pbatc.LongCount() > 0)
            {

                var delete = db.PillBookATCOMS.SingleOrDefault(x => x.TherapeuticOMSId == TherapeuticOMSId && x.PillBookId == PillBookId);

                db.PillBookATCOMS.Remove(delete);
                db.SaveChanges();

                ActivityLog.deletepillbookatcOMS(PillBookId, TherapeuticOMSId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        /*                              END                                */



        /*                      THERAPEUTICS_ATC                            */

        public JsonResult getATClevel2(int? Atc)
        {
            List<Therapeutics> OMS = new List<Therapeutics>();
            Therapeutics TherapeuticOMS = new Models.Therapeutics();


            var level2 = db.Therapeutics.Where(x => x.Active == true && x.ParentId == Atc).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (Therapeutics _level2 in level2)
            {
                TherapeuticOMS = new Therapeutics();

                TherapeuticOMS.Active = _level2.Active;
                TherapeuticOMS.TherapeuticId = _level2.TherapeuticId;
                TherapeuticOMS.TherapeuticKey = _level2.TherapeuticKey;
                TherapeuticOMS.ParentId = _level2.ParentId;

                var level3 = db.Therapeutics.Where(x => x.ParentId == _level2.TherapeuticId).OrderBy(x => x.TherapeuticKey).ToList();
                if (level3.LongCount() > 0)
                {
                    TherapeuticOMS.SpanishDescription = "<img src='../Images/ui-icons_454545_256x2401.png' onclick='collapselevel2ATC($(this))' id='collapselistATC_" + _level2.TherapeuticId + "' style='display:none' /><img src='../Images/ui-icons_454545_256x240.png' onclick='getlevel3ATC($(this))' id='" + _level2.TherapeuticId + "' />&nbsp;<label style='color: #065977;'> " + _level2.TherapeuticKey + " - &nbsp;</label>" + _level2.SpanishDescription + "<ul id='ATCl2_" + _level2.TherapeuticId + "' style='list-style: none;'></ul>";
                }
                else
                {
                    TherapeuticOMS.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookatcephmra($(this).val())' value='" + _level2.TherapeuticId + "' id='" + _level2.TherapeuticId + "' />&nbsp;<label style='color: #065977;'> " + _level2.TherapeuticKey + " - &nbsp;</label>" + _level2.SpanishDescription + "";
                }

                OMS.Add(TherapeuticOMS);
            }

            return Json(OMS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getATClevel3(int? Atc)
        {
            List<Therapeutics> OMS = new List<Therapeutics>();
            Therapeutics TherapeuticOMS = new Models.Therapeutics();


            var level3 = db.Therapeutics.Where(x => x.Active == true && x.ParentId == Atc).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (Therapeutics _level3 in level3)
            {
                TherapeuticOMS = new Therapeutics();

                TherapeuticOMS.Active = _level3.Active;
                TherapeuticOMS.TherapeuticId = _level3.TherapeuticId;
                TherapeuticOMS.TherapeuticKey = _level3.TherapeuticKey;
                TherapeuticOMS.ParentId = _level3.ParentId;

                var level4 = db.Therapeutics.Where(x => x.ParentId == _level3.TherapeuticId).OrderBy(x => x.TherapeuticKey).ToList();
                if (level4.LongCount() > 0)
                {
                    TherapeuticOMS.SpanishDescription = "<img src='../Images/ui-icons_454545_256x2401.png' onclick='collapselevel3ATC($(this))' id='collapselistATC_" + _level3.TherapeuticId + "' style='display:none' /><img src='../Images/ui-icons_454545_256x240.png' onclick='getlevel4ATC($(this))' id='" + _level3.TherapeuticId + "' />&nbsp;<label style='color: #065977;'> " + _level3.TherapeuticKey + " - &nbsp;</label>" + _level3.SpanishDescription + "<ul id='ATCl3_" + _level3.TherapeuticId + "' style='list-style: none;'></ul>";
                }
                else
                {
                    TherapeuticOMS.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookatcephmra($(this).val())' value='" + _level3.TherapeuticId + "' id='" + _level3.TherapeuticId + "' />&nbsp;<label style='color: #065977;'> " + _level3.TherapeuticKey + " - &nbsp;</label>" + _level3.SpanishDescription + "";
                }

                OMS.Add(TherapeuticOMS);
            }

            return Json(OMS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getATClevel4(int? Atc)
        {
            List<Therapeutics> OMS = new List<Therapeutics>();
            Therapeutics TherapeuticOMS = new Models.Therapeutics();

            var level4 = db.Therapeutics.Where(x => x.Active == true && x.ParentId == Atc).OrderBy(x => x.TherapeuticKey).ToList();

            foreach (Therapeutics _level4 in level4)
            {
                TherapeuticOMS = new Therapeutics();

                TherapeuticOMS.Active = _level4.Active;
                TherapeuticOMS.TherapeuticId = _level4.TherapeuticId;
                TherapeuticOMS.TherapeuticKey = _level4.TherapeuticKey;
                TherapeuticOMS.ParentId = _level4.ParentId;

                var level5 = db.Therapeutics.Where(x => x.ParentId == _level4.TherapeuticId).OrderBy(x => x.TherapeuticKey).ToList();
                if (level5.LongCount() > 0)
                {
                    TherapeuticOMS.SpanishDescription = "<img src='../Images/ui-icons_454545_256x2401.png' onclick='collapselevel4ATC($(this))' id='collapselistATC_" + _level4.TherapeuticId + "' style='display:none' /><img src='../Images/ui-icons_454545_256x240.png' onclick='getlevel2TOMS($(this))' id='" + _level4.TherapeuticId + "' />&nbsp;<label style='color: #065977;'> " + _level4.TherapeuticKey + " - &nbsp;</label>" + _level4.SpanishDescription + "<ul id='ATCl4_" + _level4.TherapeuticId + "' style='list-style: none;'></ul>";
                }
                else
                {
                    TherapeuticOMS.SpanishDescription = "&nbsp;<input type='checkbox' onclick='addpillbookatcephmra($(this).val())' value='" + _level4.TherapeuticId + "' id='" + _level4.TherapeuticId + "' />&nbsp;<label style='color: #065977;'> " + _level4.TherapeuticKey + " - &nbsp;</label>" + _level4.SpanishDescription + "";
                }

                OMS.Add(TherapeuticOMS);
            }

            return Json(OMS, JsonRequestBehavior.AllowGet);
        }



        /*          SEND EMAIL CLOSE PILLBOOK             */

        static void EmailClosePB(object Message)
        {

            //Se crea un hilo que iniciará llamando al método TareaLenta
            System.Threading.Thread tarea =
                new System.Threading.Thread(
                    new System.Threading.ParameterizedThreadStart(ClosePB));
            //Se inicia la otra tarea

            tarea.Start(Message);

        }

        static void ClosePB(object Message)
        {

            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            /*-------------------------CORREO RECEPTOR-------------------------*/

            mmsg.To.Add("beatriz.vazquez@plmlatina.com");
            mmsg.To.Add("erick.morales@plmlatina.com");

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            /*-------------------------ASUNTO-------------------------*/


            mmsg.Subject = "Cierre de PillBook";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;


            /*-------------------------CORREO A ENVÍAR COPIA-------------------------*/

            //mmsg.Bcc.Add("beatriz.vazquez@plmlatina.com"); //Opcional

            mmsg.CC.Add("miguel.ramirez@plmlatina.com");


            /*-------------------------CUERPO DEL CORREO-------------------------*/

            mmsg.Body = Message.ToString();

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Enviar Correo como HTML


            /*-------------------------CORREO EMISOR-------------------------*/


            ////mmsg.From = new System.Net.Mail.MailAddress(mail);

            mmsg.From = new System.Net.Mail.MailAddress("PillBook_System@plmlatina.com");


            /*-------------------------OBJETO TIPO CLIENTE DE CORREO-------------------------*/

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


            /*-------------------------CREDENCIALES DEL CORREO EMISOR-------------------------*/

            cliente.Credentials =
                new System.Net.NetworkCredential("PillBook_System@plmlatina.com", "PillBook_System");

            cliente.Host = "plmlatina.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                string msg = ex.InnerException.Message;
            }
        }

        public static string getmessageclose(int UsersId, string PillBook)
        {
            PLMUsers plm = new PLMUsers();

            var usr = plm.Users.Where(x => x.UserId == UsersId).ToList();

            string mail = string.Empty;

            foreach (Users _usr in usr)
            {
                mail = _usr.Email;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("Hola !!!");
            sb.Append("<br>");
            sb.Append("<br>");
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.Append("&bull; El Usuario: <b>" + usr[0].Name.ToUpper().Trim() + " " + usr[0].LastName.ToUpper().Trim() + " (" + usr[0].NickName + ")" + "</b> " + " ha cerrado el PillBook: <label style='color: #065977'><b><i> " + PillBook + "</i></b></label>");

            String Message = sb.ToString();

            return Message;
        }



        /*          SEND EMAIL OPEN PILLBOOK             */

        static void EmailOpenPB(object Message)
        {

            //Se crea un hilo que iniciará llamando al método TareaLenta
            System.Threading.Thread tarea =
                new System.Threading.Thread(
                    new System.Threading.ParameterizedThreadStart(OpenPB));

            tarea.Start(Message);

        }

        public static string getmessageopen(int UsersId, string PillBook)
        {
            PLMUsers plm = new PLMUsers();

            var usr = plm.Users.Where(x => x.UserId == UsersId).ToList();

            string mail = string.Empty;

            foreach (Users _usr in usr)
            {
                mail = _usr.Email;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("Hola !!!");
            sb.Append("<br>");
            sb.Append("<br>");
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.Append("&bull; El Usuario: <b>" + usr[0].Name.ToUpper().Trim() + " " + usr[0].LastName.ToUpper().Trim() + " (" + usr[0].NickName + ")" + "</b> " + " ha abierto el PillBook: <label style='color: #065977'><b><i> " + PillBook + "</i></b></label>");

            String Message = sb.ToString();

            return Message;
        }

        static void OpenPB(object Message)
        {
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();


            /*-------------------------CORREO RECEPTOR-------------------------*/

            mmsg.To.Add("beatriz.vazquez@plmlatina.com");
            mmsg.To.Add("erick.morales@plmlatina.com");

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            /*-------------------------ASUNTO-------------------------*/


            mmsg.Subject = "Apertura de PillBook";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;


            /*-------------------------CORREO A ENVÍAR COPIA-------------------------*/

            //mmsg.Bcc.Add("beatriz.vazquez@plmlatina.com"); //Opcional

            mmsg.CC.Add("miguel.ramirez@plmlatina.com");


            /*-------------------------CUERPO DEL CORREO-------------------------*/

            mmsg.Body = Message.ToString();

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Enviar Correo como HTML


            /*-------------------------CORREO EMISOR-------------------------*/


            ////mmsg.From = new System.Net.Mail.MailAddress(mail);

            mmsg.From = new System.Net.Mail.MailAddress("PillBook_System@plmlatina.com");


            /*-------------------------OBJETO TIPO CLIENTE DE CORREO-------------------------*/

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


            /*-------------------------CREDENCIALES DEL CORREO EMISOR-------------------------*/

            cliente.Credentials =
                new System.Net.NetworkCredential("PillBook_System@plmlatina.com", "PillBook_System");

            cliente.Host = "plmlatina.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                string msg = ex.InnerException.Message;
            }
        }
    }
}