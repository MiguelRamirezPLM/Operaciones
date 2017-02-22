using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class ParticipantProds
    {
        private DEAQ db = new DEAQ();
        public DEAQ DEAQ = new DEAQ();
        ParticipantProducts PProducts = new ParticipantProducts();
        MentionatedProducts MProds = new MentionatedProducts();
        ProductCategories ProductCategories = new ProductCategories();
        ProductPharmaForms ProductPharmaForms = new ProductPharmaForms();
        DivisionCategories DivisionCategories = new DivisionCategories();
        EditionDivisionProducts EditionDivisionProducts = new EditionDivisionProducts();
        ActivityLogs ActivityLogs = new ActivityLogs();
        ActivityLog ActivityLog = new ActivityLog();

        public void ParticipantProducts(string ProductId, string DivisionId, string Category,
            string EditionId, string PharmaForm, int ApplicationId, int UsersId)
        {
            var ProdId = int.Parse(ProductId);
            var Edition = int.Parse(EditionId);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);

            var PharmaF = from PForm in db.PharmaForms
                          where PForm.PharmaFormId == PharmaFormId
                          select PForm;
            foreach (PharmaForms PF in PharmaF)
            {
                if (PF.PharmaFormId == PharmaFormId)
                {
                    int Division = int.Parse(DivisionId);

                    var Catg = from Categ in db.Categories
                               where Categ.CategoryId == CategoryId
                               select Categ;

                    foreach (Categories Categories in Catg)
                    {
                        if (Categories.CategoryId == CategoryId)
                        {

                            var MM = (from MMProds in db.MentionatedProducts
                                      where MMProds.CategoryId == CategoryId
                                      && MMProds.DivisionId == Division
                                      && MMProds.EditionId == Edition
                                      && MMProds.PharmaFormId == PharmaFormId
                                      && MMProds.ProductId == ProdId
                                      select MMProds);
                            if (MM.LongCount() > 0)
                            {
                                goto deleteMP;
                            }
                            else
                            {
                                goto InsertPP;
                            }
                        deleteMP: foreach (MentionatedProducts MP in MM)
                            {
                            try
                            {
                                var DELETE = DEAQ.MentionatedProducts.SingleOrDefault(x => x.ProductId == MP.ProductId && x.EditionId == MP.EditionId && x.PharmaFormId == MP.PharmaFormId && x.CategoryId == MP.CategoryId && x.DivisionId == MP.DivisionId);
                                DEAQ.MentionatedProducts.Remove(DELETE);
                                DEAQ.SaveChanges();

                                ActivityLog.DeleteMentionatedProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
                            }
                            catch(Exception e)
                            {
                                string msj = e.InnerException.Message;
                            }

                            }

                        InsertPP: var EDP = from EditionDP in db.EditionDivisionProducts
                                            where EditionDP.CategoryId == CategoryId
                                            && EditionDP.DivisionId == Division
                                            && EditionDP.EditionId == Edition
                                            && EditionDP.PharmaFormId == PharmaFormId
                                            && EditionDP.ProductId == ProdId
                                            select EditionDP;
                            if (EDP.LongCount() > 0)
                            {
                                goto ParticipantP;
                            }
                            else
                            {
                                goto EditionDP;
                            }
                        EditionDP: EditionDivisionProducts.EditionId = Edition;
                        EditionDivisionProducts.ProductId = ProdId;
                            EditionDivisionProducts.PharmaFormId = PharmaFormId;
                            EditionDivisionProducts.DivisionId = Division;
                            EditionDivisionProducts.CategoryId = CategoryId;

                            DEAQ.EditionDivisionProducts.Add(EditionDivisionProducts);
                            DEAQ.SaveChanges();

                            ActivityLog.EditionDivisionProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);

                        ParticipantP: var PP = from PProds in db.ParticipantProducts
                                               where PProds.CategoryId == CategoryId
                                               && PProds.DivisionId == Division
                                               && PProds.EditionId == Edition
                                               && PProds.PharmaFormId == PharmaFormId
                                               && PProds.ProductId == ProdId
                                               select PProds;
                            if (PP.LongCount() > 0)
                            {

                            }
                            else
                            {
                                PProducts.CategoryId = CategoryId;
                                PProducts.DivisionId = Division;
                                PProducts.EditionId = Edition;
                                PProducts.PharmaFormId = PharmaFormId;
                                PProducts.ProductId = ProdId;

                                DEAQ.ParticipantProducts.Add(PProducts);
                                DEAQ.SaveChanges();

                                ActivityLog.ParticipanProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
                            }
                        }
                    }

                }
            }
        }
    }
}