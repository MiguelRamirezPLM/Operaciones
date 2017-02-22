using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using GuiaNet.Models;

namespace GuiaNet.Controllers.Help
{
    public class HelpController : Controller
    {

        /*                          VENTAS                      */

        public ActionResult Index(string module)
        {
            List<string> LS = new List<string>();

            LS.Add(module);

            return View(LS);
        }


        public JsonResult getproductmenu(string Value, string module)
        {
            HelpMenu HelpMenu = new HelpMenu();
            List<HelpMenu> LH = new List<Models.HelpMenu>();

            var path = Server.MapPath("~/App_Data/Help/" + "SM" + ".xml");

            if (System.IO.File.Exists(path))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(path);

                XmlNode node;
                node = myXmlDocument.DocumentElement;

                XmlReader xtr = XmlReader.Create(path);

                while (xtr.Read())
                {
                    if (xtr.Name == "id")
                    {
                        String Id = xtr.ReadInnerXml();

                        HelpMenu.Id = Convert.ToInt32(Id);
                    }

                    if (xtr.Name == "value")
                    {
                        HelpMenu.Text = xtr.ReadInnerXml();
                    }

                    if ((!string.IsNullOrEmpty(HelpMenu.Text)) && (HelpMenu.Id != null))
                    {
                        LH.Add(HelpMenu);

                        HelpMenu = new HelpMenu();
                    }
                }
                xtr.Close();
            }
            return Json(LH, JsonRequestBehavior.AllowGet);
        }


        public ActionResult search(string term, string file)
        {
            List<ClassSearchPred> LS = new List<ClassSearchPred>();
            ClassSearchPred ClassSearchPred = new ClassSearchPred();

            var path = Server.MapPath("~/App_Data/Help/" + file + ".xml");

            if (System.IO.File.Exists(path))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(path);

                XmlNode node;
                node = myXmlDocument.DocumentElement;

                XmlReader xtr = XmlReader.Create(path);

                while (xtr.Read())
                {
                    if (xtr.Name == "id")
                    {
                        String Id = xtr.ReadInnerXml();

                        ClassSearchPred.Id = Convert.ToInt32(Id);
                    }

                    if (xtr.Name == "value")
                    {
                        ClassSearchPred.Text = xtr.ReadInnerXml();

                        if (ClassSearchPred.Id <= 7)
                        {
                            ClassSearchPred.content = "<span class='links otps' onclick='getcontentbyid(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 7) && (ClassSearchPred.Id <= 14))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentHPproducts(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 14) && (ClassSearchPred.Id <= 18))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentcategoriesHom(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 18) && (ClassSearchPred.Id <= 25))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentespecialities(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 25) && (ClassSearchPred.Id <= 28))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentbrands(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 28) && (ClassSearchPred.Id <= 34))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentbranch(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                    }

                    if ((!string.IsNullOrEmpty(ClassSearchPred.Text)) && (ClassSearchPred.Id != null))
                    {
                        LS.Add(ClassSearchPred);

                        ClassSearchPred = new ClassSearchPred();
                    }
                }
                xtr.Close();
            }


            var _result = LS.Where(x => x.Text.ToUpper().Trim().Contains(term.ToUpper().Trim())).OrderBy(x => x.Text).ToList();

            return Json(_result, JsonRequestBehavior.AllowGet);
        }



        /*                          PRODUCCIÓN                      */

        public ActionResult UserManualProd(string module)
        {
            List<string> LS = new List<string>();

            LS.Add(module);

            return View(LS);
        }


        public ActionResult searchPROD(string term, string file)
        {
            List<ClassSearchPred> LS = new List<ClassSearchPred>();
            ClassSearchPred ClassSearchPred = new ClassSearchPred();

            var path = Server.MapPath("~/App_Data/Help/" + file + ".xml");

            if (System.IO.File.Exists(path))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(path);

                XmlNode node;
                node = myXmlDocument.DocumentElement;

                XmlReader xtr = XmlReader.Create(path);

                while (xtr.Read())
                {
                    if (xtr.Name == "id")
                    {
                        String Id = xtr.ReadInnerXml();

                        ClassSearchPred.Id = Convert.ToInt32(Id);
                    }

                    if (xtr.Name == "value")
                    {
                        ClassSearchPred.Text = xtr.ReadInnerXml();

                        if (ClassSearchPred.Id <= 5)
                        {
                            ClassSearchPred.content = "<span class='links otps' onclick='getcontentproductsPROD(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 5) && (ClassSearchPred.Id <= 8))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentspecialitiesPROD(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 8) && (ClassSearchPred.Id <= 13))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentbrandsPROD(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 13) && (ClassSearchPred.Id <= 15))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentcategoriesPROD(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 15) && (ClassSearchPred.Id <= 20))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentbranchPROD(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                    }

                    if ((!string.IsNullOrEmpty(ClassSearchPred.Text)) && (ClassSearchPred.Id != null))
                    {
                        LS.Add(ClassSearchPred);

                        ClassSearchPred = new ClassSearchPred();
                    }
                }
                xtr.Close();
            }


            var _result = LS.Where(x => x.Text.ToUpper().Trim().Contains(term.ToUpper().Trim())).OrderBy(x => x.Text).ToList();

            return Json(_result, JsonRequestBehavior.AllowGet);
        }



        /*                  LABORATORIO DE INFORMACIÓN              */

        public ActionResult UserManualLI(string module)
        {
            List<string> LS = new List<string>();

            LS.Add(module);

            return View(LS);
        }


        public ActionResult searchLI(string term, string file)
        {
            List<ClassSearchPred> LS = new List<ClassSearchPred>();
            ClassSearchPred ClassSearchPred = new ClassSearchPred();

            var path = Server.MapPath("~/App_Data/Help/" + file + ".xml");

            if (System.IO.File.Exists(path))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(path);

                XmlNode node;
                node = myXmlDocument.DocumentElement;

                XmlReader xtr = XmlReader.Create(path);

                while (xtr.Read())
                {
                    if (xtr.Name == "id")
                    {
                        String Id = xtr.ReadInnerXml();

                        ClassSearchPred.Id = Convert.ToInt32(Id);
                    }

                    if (xtr.Name == "value")
                    {
                        ClassSearchPred.Text = xtr.ReadInnerXml();

                        if (ClassSearchPred.Id <= 3)
                        {
                            ClassSearchPred.content = "<span class='links otps' onclick='getcontentproductsLI(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 3) && (ClassSearchPred.Id <= 6))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentclasificationLI(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                        else if ((ClassSearchPred.Id > 6) && (ClassSearchPred.Id <= 10))
                        {
                            ClassSearchPred.content = "<span class='links' onclick='getcontentrecategorizationLI(" + ClassSearchPred.Id + ");'>" + ClassSearchPred.Text + "</span>";
                        }
                    }

                    if ((!string.IsNullOrEmpty(ClassSearchPred.Text)) && (ClassSearchPred.Id != null))
                    {
                        LS.Add(ClassSearchPred);

                        ClassSearchPred = new ClassSearchPred();
                    }
                }
                xtr.Close();
            }


            var _result = LS.Where(x => x.Text.ToUpper().Trim().Contains(term.ToUpper().Trim())).OrderBy(x => x.Text).ToList();

            return Json(_result, JsonRequestBehavior.AllowGet);
        }
    }
}