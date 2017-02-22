using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;
using System.Windows.Forms;
using System.Data;

namespace AgroNet.Models
{
    public class MentionatedProds
    {
        private DEAQ db = new DEAQ();
        public DEAQ DEAQ = new DEAQ();
        DEAQ dbs = new DEAQ();
        ParticipantProducts PProducts = new ParticipantProducts();
        MentionatedProducts MProducts = new MentionatedProducts();
        ProductCategories ProductCategories = new ProductCategories();
        ProductPharmaForms ProductPharmaForms = new ProductPharmaForms();
        DivisionCategories DivisionCategories = new DivisionCategories();
        EditionDivisionProducts EditionDivisionProducts = new EditionDivisionProducts();
        ActivityLogs ActivityLogs = new ActivityLogs();
        ActivityLog ActivityLog = new ActivityLog();

        public void MentionatedProducts(string ProductId, string DivisionId, string Category,string EditionId, 
                                        string PharmaForm, int ApplicationId, int UsersId)
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

                            var pc = (from _pc in db.ProductContents
                                      where _pc.DivisionId == Division
                                      && _pc.EditionId == Edition
                                      && _pc.CategoryId == CategoryId
                                      && _pc.PharmaFormId == PharmaFormId
                                      && _pc.ProductId == ProdId
                                      select _pc).ToList();
                            if (pc.LongCount() > 0)
                            {
                                foreach (ProductContents _pc in pc)
                                {
                                    try
                                    {
                                        var delete = dbs.ProductContents.SingleOrDefault(x => x.ProductId == _pc.ProductId && x.PharmaFormId == _pc.PharmaFormId && x.EditionId == _pc.EditionId && x.DivisionId == _pc.DivisionId && x.CategoryId == _pc.CategoryId && x.AttributeId == _pc.AttributeId);
                                        dbs.ProductContents.Remove(delete);
                                        dbs.SaveChanges();

                                        ActivityLog._deleteproductcontents(_pc.ProductId, _pc.PharmaFormId, _pc.EditionId, _pc.DivisionId, _pc.CategoryId, _pc.AttributeId, _pc.HTMLContent, _pc.PlainContent, _pc.Content, ApplicationId, UsersId);
                                    }
                                    catch (Exception e)
                                    {
                                        string msg = e.Message;
                                    }
                                }
                            }

                            var PP = (from PPProds in db.ParticipantProducts
                                      where PPProds.CategoryId == CategoryId
                                      && PPProds.DivisionId == Division
                                      && PPProds.EditionId == Edition
                                      && PPProds.PharmaFormId == PharmaFormId
                                      && PPProds.ProductId == ProdId
                                      select PPProds);
                            if (PP.LongCount() > 0)
                            {
                                goto deletePP;
                            }
                            else
                            {
                                goto InsertMP;
                            }
                        deletePP: foreach (ParticipantProducts PProds in PP)
                            {
                                var Del = DEAQ.ParticipantProducts.SingleOrDefault(x => x.ProductId == PProds.ProductId && x.EditionId == PProds.EditionId && x.PharmaFormId == PProds.PharmaFormId && x.CategoryId == PProds.CategoryId && x.DivisionId == PProds.DivisionId);
                                DEAQ.ParticipantProducts.Remove(Del);
                                DEAQ.SaveChanges();

                                ActivityLog.DeleteParticipanProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
                            }

                        InsertMP: var EDP = from EditionDP in db.EditionDivisionProducts
                                            where EditionDP.CategoryId == CategoryId
                                            && EditionDP.DivisionId == Division
                                            && EditionDP.EditionId == Edition
                                            && EditionDP.PharmaFormId == PharmaFormId
                                            && EditionDP.ProductId == ProdId
                                            select EditionDP;
                            if (EDP.LongCount() > 0)
                            {
                                goto MentionatedP;
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

                        MentionatedP: var MPP = from MProds in db.MentionatedProducts
                                                where MProds.CategoryId == CategoryId
                                                && MProds.DivisionId == Division
                                                && MProds.EditionId == Edition
                                                && MProds.PharmaFormId == PharmaFormId
                                                && MProds.ProductId == ProdId
                                                select MProds;
                            if (MPP.LongCount() > 0)
                            {

                            }
                            else
                            {
                                MProducts.CategoryId = CategoryId;
                                MProducts.DivisionId = Division;
                                MProducts.EditionId = Edition;
                                MProducts.PharmaFormId = PharmaFormId;
                                MProducts.ProductId = ProdId;

                                DEAQ.MentionatedProducts.Add(MProducts);
                                DEAQ.SaveChanges();

                                ActivityLog.MentionatedProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
                            }
                        }
                    }
                }

            }
        }
    }
}