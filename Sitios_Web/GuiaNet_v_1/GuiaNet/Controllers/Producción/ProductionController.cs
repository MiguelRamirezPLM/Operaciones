using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Globalization;
using System.Text;
using HtmlAgilityPack;

namespace GuiaNet.Controllers.Producción
{
    public class ProductionController : Controller
    {
        Guia db = new Guia();
        PLMUsers PLMUsers = new PLMUsers();
        private Guia dbguia = new Guia();
        ParticipantProducts ParticipantProducts = new ParticipantProducts();
        segmentcontent segmentcontent = new segmentcontent();
        EditionProductShotSizes EditionProductShotSizes = new EditionProductShotSizes();
        EditionClientProductShots EditionClientProductShots = new EditionClientProductShots();
        public List<String> listattr = new List<String>();
        public List<String> listattrATG = new List<String>();
        ProductContents ProductContents = new ProductContents();
        EditionAttributes EditionAttributes = new EditionAttributes();
        classreplace classreplace = new classreplace();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            if (ClientId != null)
            {
                int count = 0;
                int _clienid = int.Parse(ClientId);
                int _editionid = int.Parse(EditionId);
                int _countryid = int.Parse(CountryId);
                int _book = int.Parse(BookId);
                int? productid = null;

                var pp = (from partprod in db.ParticipantProducts
                          where partprod.ClientId == _clienid
                          && partprod.EditionId == _editionid
                          join p in db.Products
                          on partprod.ProductId equals p.ProductId
                          orderby p.ProductName ascending
                          select p).ToList();
                foreach (Products p in pp)
                {
                    count = count + 1;
                }
                if (count > 0)
                {
                    ViewData["Count"] = count;
                }
                else
                {
                    ViewData["Count"] = null;
                }
                sessionproduction session = new sessionproduction(_countryid, _clienid, _editionid, _book, productid);
                Session["sessionproduction"] = session;
                return View(pp);
            }
            if (ind != null)
            {
                int _clienid = ind.ClId;
                int _editionid = ind.EId;
                int _countryid = ind.CId;
                int _book = ind.BId;

                var pp = (from partprod in db.ParticipantProducts
                          where partprod.ClientId == _clienid
                          && partprod.EditionId == _editionid
                          join p in db.Products
                          on partprod.ProductId equals p.ProductId
                          orderby p.ProductName ascending
                          select p).ToList();
                ViewData["Count"] = 0;
                return View(pp);
            }
            else
            {
                var pp = (from partprod in db.ParticipantProducts
                          where partprod.ClientId == 0
                          && partprod.EditionId == 0
                          join p in db.Products
                          on partprod.ProductId equals p.ProductId
                          select p).ToList();
                ViewData["Count"] = 0;
                return View(pp);
            }
        }

        public JsonResult books(string country)
        {
            Books book = new Books();
            List<Books> lbook = new List<Books>();

            var _book = from b in db.Books
                        select b;
            foreach (Books _b in _book)
            {
                book = new Books();
                book.Active = _b.Active;
                book.BookId = _b.BookId;
                book.BookName = _b.BookName;
                lbook.Add(book);
            }
            return Json(lbook, JsonRequestBehavior.AllowGet);
        }

        public JsonResult editions(string country, string bookid)
        {
            Editions edition = new Editions();
            List<Editions> leditions = new List<Editions>();

            int countryid = int.Parse(country);
            int book = int.Parse(bookid);

            var _edition = from e in db.Editions
                           where e.CountryId == countryid
                           && e.BookId == book
                           select e;
            foreach (Editions _e in _edition)
            {
                edition = new Editions();
                edition.Active = _e.Active;
                edition.BarCode = _e.BarCode;
                edition.BookId = _e.BookId;
                edition.CountryId = _e.CountryId;
                edition.EditionId = _e.EditionId;
                edition.ISBN = _e.ISBN;
                edition.NumberEdition = _e.NumberEdition;
                edition.ParentId = _e.ParentId;
                leditions.Add(edition);
            }
            return Json(leditions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getclients(string country)
        {
            Clients clients = new Clients();
            List<Clients> lclients = new List<Clients>();

            int countryid = int.Parse(country);
            byte clienttypeid = 0;
            var ct = (from clientt in db.ClientTypes
                      where clientt.TypeName == "ANUNCIANTE"
                      select clientt).ToList();
            foreach (ClientTypes _ct in ct)
            {
                clienttypeid = _ct.ClientTypeId;
            }

            var _clients = (from c in db.Clients
                            join ec in db.EditionClients
                            on c.ClientId equals ec.ClientId
                            where ec.ClientTypeId == clienttypeid
                            && c.CountryId == countryid
                            orderby c.CompanyName ascending
                            select c).Distinct().OrderBy(x => x.CompanyName).ToList();

            foreach (Clients _c in _clients)
            {
                clients = new Clients();
                clients.Active = _c.Active;
                clients.ClientId = _c.ClientId;
                clients.ClientIdParent = _c.ClientIdParent;
                clients.ClientCode = _c.ClientCode;
                clients.CompanyName = _c.CompanyName;
                clients.Image = _c.Image;
                clients.ShortName = _c.ShortName;
                clients.Description = _c.Description;
                clients.AlphabetId = _c.AlphabetId;
                clients.ANUNCIANTEID = _c.ANUNCIANTEID;
                lclients.Add(clients);
            }
            return Json(lclients, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchproduct(string ProductName)
        {
            if (ProductName != null)
            {
                if (ProductName == string.Empty)
                {
                    return RedirectToAction("Index", "Production");
                }
                else
                {
                    sessionproduction ind = (sessionproduction)Session["sessionproduction"];
                    int _clientid = ind.ClId;
                    int count = 0;
                    int typeid = 0;
                    var type = (from types in db.ProductTypes
                                where types.Description == "Productos"
                                select types).ToList();
                    foreach (ProductTypes t in type)
                    {
                        typeid = t.TypeId;
                    }
                    var prods = (from product in db.Products
                                 join cp in db.ClientProducts
                                 on product.ProductId equals cp.ProductId
                                 join pp in db.ParticipantProducts
                                 on cp.ProductId equals pp.ProductId
                                 where product.ProductName.StartsWith(ProductName)
                                 && cp.ClientId == pp.ClientId
                                 && cp.ClientId == _clientid
                                 && product.TypeId == typeid
                                 select product).ToList();
                    foreach (Products p in prods)
                    {
                        count = count + 1;
                    }
                    ViewData["Countresults"] = count;
                    return View("Index", prods);
                }
            }
            return RedirectToAction("Index", "InformationLaboratory");
        }

        [HttpPost]
        public ActionResult newpdffile(HttpPostedFileBase file, string ProductId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int _clienid = ind.ClId;
            int _editionid = ind.EId;
            int _productid = int.Parse(ProductId);

            string pdf = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            var CL = from client in db.Clients
                     where client.ClientId == _clienid
                     select client;
            var prod = (from product in db.Products
                        where product.ProductId == _productid
                        select product).ToList();

            foreach (Clients _client in CL)
            {
                if (_client.ShortName == null)
                {
                    pdf = _client.CompanyName;
                }
                if (_client.ShortName == string.Empty)
                {
                    pdf = _client.CompanyName;
                }
                else if ((_client.ShortName != null) && (_client.ShortName != string.Empty))
                {
                    pdf = _client.ShortName;
                }
            }

            foreach (Products pr in prod)
            {
                pdf = pdf + "_" + pr.ProductName;
            }

            pdf = classreplace.replacepdffilename(pdf);

            pdf = pdf + extension;
            var path = Path.Combine(Server.MapPath("~/App_Data/PDF"), pdf);
            file.SaveAs(path);

            var ppfile = (from part in db.ParticipantProducts
                          where part.ClientId == _clienid
                          && part.EditionId == _editionid
                          && part.ProductId == _productid
                          && part.FileName == pdf
                          select part).ToList();
            if (ppfile.LongCount() > 0)
            {
                foreach (ParticipantProducts partp in ppfile)
                {
                    partp.FileName = pdf;
                }
                db.SaveChanges();

                string _message = "El archivo se ha Modificado Exitosamente";
                return Json(_message, JsonRequestBehavior.AllowGet);
            }
            var pp = (from part in db.ParticipantProducts
                      where part.ClientId == _clienid
                      && part.EditionId == _editionid
                      && part.ProductId == _productid
                      select part).ToList();
            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts partp in pp)
                {
                    partp.FileName = pdf;
                }
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult newhtmlfile(HttpPostedFileBase file, string ProductId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            string pname = string.Empty;
            string prop = string.Empty;
            string attribute = string.Empty;
            string labname = string.Empty;

            String File = "";

            var ctr = (from c in PLMUsers.Users
                       join count in PLMUsers.CountriesUser
                           on c.CountryId equals count.CountryId
                       where c.UserId == UsersId
                       select count).ToList();
            if (ctr.LongCount() > 0)
            {
                foreach (CountriesUser _ctr in ctr)
                {
                    File = "targetsallfile_" + _ctr.ID + ".xml";
                }
            }

            var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(root);

            XmlNode node;
            node = myXmlDocument.DocumentElement;

            XmlReader xtr = XmlReader.Create(root);
            while (xtr.Read())
            {
                if (xtr.Name == "TargetDescription")
                {
                    pname = xtr.ReadInnerXml();
                    pname = pname.Replace("> </p>", "");
                    pname = pname.Replace("</p>", "");
                }

                if (xtr.Name == "TargetDescriptionPropag")
                {
                    prop = xtr.ReadInnerXml();
                    prop = prop.Replace("</p>", "");
                }

                if (xtr.Name == "TargetDescriptionAttribute")
                {
                    attribute = xtr.ReadInnerXml();
                    attribute = attribute.Replace("</p>", "");
                    attribute = attribute.Replace("</span>", "");
                }

                if (xtr.Name == "TargetDescriptionLaboratory")
                {
                    labname = xtr.ReadInnerXml();
                    labname = labname.Replace("</p>", "");
                }
            }
            xtr.Close();
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int _clienid = ind.ClId;
            int _editionid = ind.EId;
            int _productid = int.Parse(ProductId);

            ind.PId = _productid;

            string html = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            var CL = from client in db.Clients
                     where client.ClientId == _clienid
                     select client;
            var prod = (from product in db.Products
                        where product.ProductId == _productid
                        select product).ToList();

            foreach (Clients _client in CL)
            {
                if (_client.ShortName == null)
                {
                    html = _client.CompanyName;
                }
                if (_client.ShortName == string.Empty)
                {
                    html = _client.CompanyName;
                }
                else if ((_client.ShortName != null) && (_client.ShortName != string.Empty))
                {
                    html = _client.ShortName;
                }
            }

            foreach (Products pr in prod)
            {
                html = html + "_" + pr.ProductName;
            }

            html = classreplace.replacehtmlfilename(html);

            html = html + "_tmp" + extension;
            var path = Path.Combine(Server.MapPath("~/App_Data/HTML"), html);
            var desc = Path.Combine(Server.MapPath("~/App_Data/XML"));
            try
            {
                file.SaveAs(path);
            }
            catch (Exception)
            {

            }
            if (System.IO.File.Exists(path))
            {
                FileInfo FI = new FileInfo(path);
                String filename = FI.Name;
                String directoryname = FI.DirectoryName;

                String concatfiledir = directoryname + "\\" + filename;

                filename = filename.Replace(".html", "");
                filename = filename.Replace(".htm", "");

                segmentcontent.getxml(concatfiledir, filename, desc, pname, prop, attribute, labname);

                String check = checkXML(desc, filename);
                if (check == "_error")
                {
                    try
                    {
                        string xmlpath = desc + @"\" + filename + ".xml";
                        System.IO.File.Delete(path);
                        System.IO.File.Delete(xmlpath);
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                    }
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string xmlpath = desc + @"\" + filename + ".xml";

                    string destinyPath = desc + @"\" + filename + ".xml";

                    destinyPath = destinyPath.Replace("_tmp", "");

                    System.IO.File.Copy(xmlpath, destinyPath, true);

                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch (Exception)
                    {

                    }
                    System.IO.File.Delete(xmlpath);

                    path = path.Replace("_tmp", "");
                    xmlpath = xmlpath.Replace("_tmp", "");

                    file.SaveAs(path);

                    string path1 = path.Replace(".html", ".htm");

                    if (System.IO.File.Exists(path1))
                    {
                        System.IO.File.Delete(path1);
                    }

                    filename = filename.Replace("_tmp", "");

                    StreamReader SR = System.IO.File.OpenText(path);
                    String content = SR.ReadToEnd();

                    bool _response = segmentcontent.inserthtml(_clienid, _editionid, _productid, content);


                    SR.Close();

                    var source = Path.Combine(Server.MapPath("~/App_Data/XML"), filename + ".xml");
                    StreamReader StreamR;
                    StreamR = System.IO.File.OpenText(source);
                    String _content = StreamR.ReadToEnd();

                    bool response = segmentcontent.insertxml(_clienid, _editionid, _productid, _content);

                    if ((_response == true) && (response == true))
                    {
                        ActivityLog._updatecontentparticipantproducts(_editionid, _clienid, _productid, ApplicationId, UsersId);
                    }

                    StreamR.Close();

                    var pc = (from _pc in db.ProductContents
                              where _pc.ClientId == _clienid
                              && _pc.EditionId == _editionid
                              && _pc.ProductId == _productid
                              select _pc).ToList();
                    if (pc.LongCount() > 0)
                    {
                        foreach (ProductContents _pc in pc)
                        {
                            var delete = db.ProductContents.SingleOrDefault(x => x.ProductId == _pc.ProductId && x.EditionId == _pc.EditionId && x.ClientId == _pc.ClientId && x.AttributeId == _pc.AttributeId);
                            db.ProductContents.Remove(delete);
                            db.SaveChanges();
                        }
                        ActivityLog._deleteproductcontents(_editionid, _clienid, _productid, ApplicationId, UsersId);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public string checkXML(string desc, string filename)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo(desc + @"\");
            FileInfo fl = new FileInfo(desc + "\\" + filename + ".xml");
            try
            {
                XmlDocument cdoc = new XmlDocument();
                cdoc.Load(fl.FullName);


            }
            catch (Exception e)
            {
                String message = e.Message;
                return "_error";
            }
            return "ok";
        }

        public ActionResult getpdf(string ProductId)
        {
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int _clienid = ind.ClId;
            int _productid = int.Parse(ProductId);

            var CL = (from client in db.Clients
                      where client.ClientId == _clienid
                      select client).ToList();

            var prod = (from product in db.Products
                        where product.ProductId == _productid
                        select product).ToList();

            String pdf = String.Empty;

            foreach (Clients _client in CL)
            {
                if (_client.ShortName == null)
                {
                    pdf = _client.CompanyName;
                }
                if (_client.ShortName == string.Empty)
                {
                    pdf = _client.CompanyName;
                }
                else if ((_client.ShortName != null) && (_client.ShortName != string.Empty))
                {
                    pdf = _client.ShortName;
                }
            }

            foreach (Products pr in prod)
            {
                pdf = pdf + "_" + pr.ProductName;
            }

            pdf = classreplace.replacehtmlfilename(pdf);

            pdf = pdf + ".pdf";

            var path = Path.Combine(Server.MapPath("~/App_Data/PDF"), pdf);
            if (System.IO.File.Exists(path))
            {
                return File(path, "application/pdf");
            }
            else
            {
                return PartialView("errorfile");
            }
        }

        public ActionResult segmenthtml(int? ProductId)
        {
            ViewData["validationhtml"] = false;
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];

            if (ind != null)
            {
                CountriesUsers cp = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = cp.ApplicationId;
                int UsersId = cp.userId;

                int _clienid = ind.ClId;
                int _editionid = ind.EId;
                int _bookid = ind.BId;
                int _countryid = ind.CId;

                var product = (from p in db.Products
                               where p.ProductId == ProductId
                               select p).ToList();
                foreach (Products p in product)
                {
                    ViewData["ProductName"] = p.ProductName.ToUpper();
                    ViewData["ProductId"] = ProductId;
                }

                var client = (from c in db.Clients
                              where c.ClientId == _clienid
                              select c).ToList();
                foreach (Clients c in client)
                {
                    ViewData["CompanyName"] = c.CompanyName;
                    ViewData["ClientId"] = c.ClientId;
                }

                var edition = (from e in db.Editions
                               where e.EditionId == _editionid
                               select e).ToList();
                foreach (Editions e in edition)
                {
                    ViewData["NumberEdition"] = e.NumberEdition;
                    ViewData["EditionId"] = e.EditionId;
                }

                var book = (from b in db.Books
                            where b.BookId == _bookid
                            select b).ToList();
                foreach (Books b in book)
                {
                    ViewData["BookName"] = b.BookName;
                }

                var country = (from c in db.Countries
                               where c.CountryId == _countryid
                               select c).ToList();
                foreach (Countries c in country)
                {
                    ViewData["CountryName"] = c.CountryName;
                }

                String File = "";

                var ctr = (from c in PLMUsers.Users
                           join count in PLMUsers.CountriesUser
                               on c.CountryId equals count.CountryId
                           where c.UserId == UsersId
                           select count).ToList();
                if (ctr.LongCount() > 0)
                {
                    foreach (CountriesUser _ctr in ctr)
                    {
                        File = "targetsallfile_" + _ctr.ID + ".xml";
                    }
                }

                var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

                if (System.IO.File.Exists(root))
                {
                    XmlDocument myXmlDocument = new XmlDocument();
                    myXmlDocument.Load(root);

                    XmlNode node;
                    node = myXmlDocument.DocumentElement;

                    XmlReader xtr = XmlReader.Create(root);
                    while (xtr.Read())
                    {
                        if (xtr.Name == "TargetDescription")
                        {
                            String pname = xtr.ReadInnerXml();

                            pname = pname.Replace("> </p>", "");
                            pname = pname.Replace("</p>", "");

                            ViewData["ProductNametarget"] = pname;
                        }

                        if (xtr.Name == "TargetDescriptionPropag")
                        {
                            String bpropag = xtr.ReadInnerXml();

                            bpropag = bpropag.Replace("</p>", "");

                            ViewData["BPropaganda"] = bpropag;
                        }

                        if (xtr.Name == "TargetDescriptionAttribute")
                        {
                            String attribute = xtr.ReadInnerXml();

                            attribute = attribute.Replace("</p>", "");
                            attribute = attribute.Replace("</span>", "");

                            ViewData["Attribute"] = attribute;
                        }

                        if (xtr.Name == "TargetDescriptionLaboratory")
                        {
                            String labname = xtr.ReadInnerXml();

                            labname = labname.Replace("</p>", "");

                            ViewData["Laboratory"] = labname;
                        }
                    }
                    xtr.Close();
                }
                else
                {
                    ViewData["validationhtml"] = true;
                }
                int _productid = Convert.ToInt32(ProductId);

                List<String> ls = getattributes(_productid);

                ls = ls.OrderBy(x => x).ToList();

                listattr = ls;

                return View(ls);
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult getattr(String productname, String propag, String attrb, String laboratoryname)
        {
            bool _response = saveattr(productname, propag, attrb, laboratoryname);
            if (_response == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public bool saveattr(String productname, String propag, String attrb, String laboratoryname)
        {
            CountriesUsers cp = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = cp.ApplicationId;
            int UsersId = cp.userId;

            productname = productname.Replace("60", "<");
            productname = productname.Replace("62", ">");
            productname = productname.Replace("> ", ">");
            propag = propag.Replace("60", "<");
            propag = propag.Replace("62", ">");
            attrb = attrb.Replace("60", "<");
            attrb = attrb.Replace("62", ">");
            attrb = attrb.Replace("> <", "><");
            laboratoryname = laboratoryname.Replace("60", "<");
            laboratoryname = laboratoryname.Replace("62", ">");
            attrproduction attrproduction = new attrproduction(productname, propag, attrb, laboratoryname);
            Session["attrproduction"] = attrproduction;

            if (attrproduction != null)
            {
                try
                {
                    StreamWriter SW;

                    String rootbeg = "<Root>";
                    String rootend = "</Root>";

                    String File = "";

                    var ctr = (from c in PLMUsers.Users
                               join count in PLMUsers.CountriesUser
                                   on c.CountryId equals count.CountryId
                               where c.UserId == UsersId
                               select count).ToList();
                    if (ctr.LongCount() > 0)
                    {
                        foreach (CountriesUser _ctr in ctr)
                        {
                            File = "targetsallfile_" + _ctr.ID + ".xml";
                        }
                    }

                    var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);
                    //if (System.IO.Directory.Exists(root))
                    //{
                    //    map = Path.Combine(root, File);
                    //}
                    //else
                    //{
                    //    DirectoryInfo di = Directory.CreateDirectory(root);
                    //    map = Path.Combine(root, File);
                    //}

                    String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";

                    SW = System.IO.File.CreateText(root);
                    SW.Write(xml);
                    SW.Write(rootbeg);

                    if ((productname != null) || (productname != String.Empty))
                    {
                        SW.Write("<TargetName>");
                        SW.Write("Producto");
                        SW.Write("</TargetName>");

                        SW.Write("<TargetDescription>");
                        SW.Write(productname + "</p>");
                        SW.Write("</TargetDescription>");
                    }

                    if ((propag != null) || (propag != String.Empty))
                    {
                        SW.Write("<TargetPropag>");
                        SW.Write("Base de Propaganda");
                        SW.Write("</TargetPropag>");

                        SW.Write("<TargetDescriptionPropag>");
                        SW.Write(propag + "</p>");
                        SW.Write("</TargetDescriptionPropag>");
                    }

                    if ((attrb != null) || (attrb != String.Empty))
                    {
                        SW.Write("<TargetAttr>");
                        SW.Write("Attributos");
                        SW.Write("</TargetAttr>");

                        SW.Write("<TargetDescriptionAttribute>");
                        SW.Write(attrb + "</span></p>");
                        SW.Write("</TargetDescriptionAttribute>");
                    }

                    if ((laboratoryname != null) || (laboratoryname != String.Empty))
                    {
                        SW.Write("<TargetLab>");
                        SW.Write("Laboratorio");
                        SW.Write("</TargetLab>");

                        SW.Write("<TargetDescriptionLaboratory>");
                        SW.Write(laboratoryname + "</p>");
                        SW.Write("</TargetDescriptionLaboratory>");
                    }
                    SW.Write(rootend);
                    SW.Close();
                }
                catch (Exception e)
                {
                    var ex = e.Message;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public ActionResult newimages(HttpPostedFileBase file, string size, string Product)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            string Size = "";
            string ProductName = "";
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int ClientId = ind.ClId;
            int ImageSizeId = int.Parse(size);
            int ProductId = int.Parse(Product);
            int EditionId = ind.EId;

            string img = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            var prods = (from prod in db.Products
                         where prod.ProductId == ProductId
                         select prod).ToList();
            foreach (Products _prods in prods)
            {
                ProductName = _prods.ProductName;
            }

            var CL = from ClientImg in db.Clients
                     where ClientImg.ClientId == ClientId
                     select ClientImg;
            foreach (Clients Clients in CL)
            {
                if (Clients.ShortName == null)
                {
                    img = Clients.CompanyName;
                    img = img + " " + ProductName;
                }
                if (Clients.ShortName == string.Empty)
                {
                    img = Clients.CompanyName;
                    img = img + " " + ProductName;
                }
                if ((Clients.ShortName != null) && (Clients.ShortName != string.Empty))
                {
                    img = Clients.ShortName;
                    img = img + " " + ProductName;
                }
            }
            img = ReplacesImageNames.replaces(img);

            img = img + extension;

            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), img);
            file.SaveAs(path);

            var images = (from _image in db.EditionClientProductShots
                          where _image.ProductId == ProductId
                          && _image.ClientId == ClientId
                          && _image.EditionId == EditionId
                          select _image).ToList();
            if (images.LongCount() > 0)
            {
                foreach (EditionClientProductShots _image in images)
                {
                    _image.ProductShot = img;

                    db.SaveChanges();
                }
            }
            else
            {
                EditionClientProductShots.Active = true;
                EditionClientProductShots.ClientId = ClientId;
                EditionClientProductShots.EditionId = EditionId;
                EditionClientProductShots.ProductId = ProductId;
                EditionClientProductShots.ProductShot = img;

                db.EditionClientProductShots.Add(EditionClientProductShots);
                db.SaveChanges();
            }


            var ecpss = (from ecp in db.EditionClientProductShots
                         where ecp.ProductId == ProductId
                         && ecp.EditionId == EditionId
                         && ecp.ClientId == ClientId
                         select ecp).ToList();
            if (ecpss.LongCount() > 0)
            {
                foreach (EditionClientProductShots _ecpss in ecpss)
                {
                    var epss = (from eps in db.EditionProductShotSizes
                                where eps.EditionClientProductShotId == _ecpss.EditionClientProductShotId
                                select eps).ToList();
                    if (epss.LongCount() > 0)
                    {
                        var epsss = (from eps in db.EditionProductShotSizes
                                     where eps.EditionClientProductShotId == _ecpss.EditionClientProductShotId
                                     && eps.ImageSizeId == ImageSizeId
                                     select eps).ToList();
                        if (epsss.LongCount() == 0)
                        {
                            EditionProductShotSizes.EditionClientProductShotId = _ecpss.EditionClientProductShotId;
                            EditionProductShotSizes.ImageSizeId = Convert.ToByte(ImageSizeId);

                            db.EditionProductShotSizes.Add(EditionProductShotSizes);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        EditionProductShotSizes.EditionClientProductShotId = _ecpss.EditionClientProductShotId;
                        EditionProductShotSizes.ImageSizeId = Convert.ToByte(ImageSizeId);

                        db.EditionProductShotSizes.Add(EditionProductShotSizes);
                        db.SaveChanges();
                    }
                }
            }

            var ImagesS = (from ImagesSize in db.ImagesSizes
                           where ImagesSize.ImageSizeId == ImageSizeId
                           select ImagesSize).ToList();
            foreach (ImagesSizes IS in ImagesS)
            {
                Size = IS.ImageSize;

                if (IS.ImageSizeId == ImageSizeId)
                {
                    var d = path;
                    string destinyPath = System.Configuration.ConfigurationManager.AppSettings["Path3"];
                    destinyPath = destinyPath + Size;

                    if (System.IO.Directory.Exists(destinyPath))
                    {
                        destinyPath = System.IO.Path.Combine(destinyPath, img);
                        System.IO.File.Copy(d, destinyPath, true);
                        System.IO.File.Delete(path);
                    }
                    else
                    {
                        DirectoryInfo Dir = Directory.CreateDirectory(destinyPath);
                        destinyPath = System.IO.Path.Combine(destinyPath, img);
                        System.IO.File.Copy(d, destinyPath, true);
                        System.IO.File.Delete(path);
                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult showimagesdetails(int image, int? client, int? edition)
        {
            var ims = (from i in db.EditionClientProductShots
                       join eps in db.EditionProductShotSizes
                           on i.EditionClientProductShotId equals eps.EditionClientProductShotId
                       join iss in db.ImagesSizes
                           on eps.ImageSizeId equals iss.ImageSizeId
                       where i.ClientId == client
                       && i.EditionId == edition
                       && i.ProductId == image
                       select iss).ToList();
            if (ims.LongCount() > 0)
            {
                string ProductShot = "";

                var images = (from img in db.EditionClientProductShots
                              where img.ProductId == image
                              select img).ToList();
                if (images.LongCount() > 0)
                {
                    foreach (EditionClientProductShots _images in images)
                    {
                        ProductShot = _images.ProductShot;
                    }
                    foreach (ImagesSizes _images in ims)
                    {
                        var root = System.Configuration.ConfigurationManager.AppSettings["Path3"];
                        root = root + _images.ImageSize;
                        var path = Path.Combine(root, ProductShot);
                        if (System.IO.File.Exists(path))
                        {
                            return File(path, "image/png");
                        }
                        else
                        {
                            string ProductShots = "not_available.png";
                            var rooterr = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShots);
                            return File(rooterr, "image/png");
                        }

                    }

                }
                return View();
            }
            else
            {
                string ProductShot = "not_available.png";
                var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShot);
                return File(root, "image/png");
            }
        }

        public ActionResult images(int? ProductId, int? client, int? edition)
        {
            joinclientproductshots joinclientproductshots = new Models.joinclientproductshots();
            List<joinclientproductshots> ljoin = new List<Models.joinclientproductshots>();
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            if (ind != null)
            {
                int _clienid = ind.ClId;
                int _editionid = ind.EId;

                var prods = (from p in db.Products
                             where p.ProductId == ProductId
                             select p).ToList();
                if (prods.LongCount() > 0)
                {
                    foreach (Products _prods in prods)
                    {
                        ViewData["ProductId"] = _prods.ProductId;
                        ViewData["ProductName"] = _prods.ProductName.ToUpper();
                    }
                }

                var ims = (from i in db.EditionClientProductShots
                           join eps in db.EditionProductShotSizes
                               on i.EditionClientProductShotId equals eps.EditionClientProductShotId
                           join iss in db.ImagesSizes
                               on eps.ImageSizeId equals iss.ImageSizeId
                           where i.ClientId == client
                           && i.EditionId == edition
                           && i.ProductId == ProductId
                           select new joinclientproductshots { EditionClientProductShots = i, EditionProductShotSizes = eps, ImagesSizes = iss }).ToList();

                return View("images", ims);
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult showimages(string image, int? EditionClientProductShotId, int? size)
        {
            var ims = (from i in db.EditionClientProductShots
                       join eps in db.EditionProductShotSizes
                           on i.EditionClientProductShotId equals eps.EditionClientProductShotId
                       join iss in db.ImagesSizes
                           on eps.ImageSizeId equals iss.ImageSizeId
                       where eps.EditionClientProductShotId == EditionClientProductShotId
                       && eps.ImageSizeId == size
                       select iss).ToList();

            foreach (ImagesSizes _images in ims)
            {
                var root = System.Configuration.ConfigurationManager.AppSettings["Path3"];
                root = root + _images.ImageSize;
                var path = Path.Combine(root, image);
                if (System.IO.File.Exists(path))
                {
                    return File(path, "image/png");
                }
                else
                {
                    string ProductShots = "not_available.png";
                    var rooterr = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShots);
                    return File(rooterr, "image/png");
                }
            }
            return View();
        }

        public List<String> getattributes(int _productid)
        {
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int _clienid = ind.ClId;
            int _editionid = ind.EId;

            var pp = (from _partp in db.ParticipantProducts
                      where _partp.EditionId == _editionid
                      && _partp.ClientId == _clienid
                      && _partp.ProductId == _productid
                      select _partp).ToList();

            int i, end;
            String attributeDescription = "";
            String titulo = "<Titulo>";

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    String attribute = _pp.XMLContent;

                    String attr = "";

                    while (attribute != null)
                    {
                        i = attribute.IndexOf("<Titulo>");
                        if (i >= 0)
                        {
                            end = attribute.IndexOf("</Titulo>", i);
                            attributeDescription = attribute.Substring(i + titulo.Length, end - (i + titulo.Length));
                            attribute = attribute.Substring(end);


                            if (attributeDescription != "")
                            {
                                //attr = new String;

                                attr = attributeDescription.Replace(":", "").Trim().Replace(";", "").Trim().Replace(".", "");

                                var ats = (from _at in db.Attributes
                                           //join pc in db.ProductContents
                                           //on _at.AttributeId equals pc.AttributeId
                                           where _at.Description == attr
                                           // && pc.ClientId == _clienid
                                           select _at).ToList();
                                if (ats.LongCount() == 0)
                                {
                                    listattr.Add(attr);
                                }
                                else
                                {
                                    listattr.Add(attr);
                                    listattrATG.Add(attr);
                                }
                            }
                        }
                        else
                        {
                            attribute = null;
                        }
                    }
                    listattrATG = listattrATG.OrderBy(x => x).ToList();
                    XMLclass XMLclass = new XMLclass(listattrATG);
                    Session["listitems"] = XMLclass;
                }
            }
            return listattr;
        }

        public ActionResult getnewattr(String ListItems, string size, string ProductId)
        {
            try
            {
                sessionproduction ind = (sessionproduction)Session["sessionproduction"];
                Attributes Attributes = new Attributes();
                List<String> ls = new List<String>();

                int _clienid = ind.ClId;
                int _editionid = ind.EId;
                int _productid = int.Parse(ProductId);

                int arraysize = int.Parse(size);

                dynamic d = JsonConvert.DeserializeObject(ListItems);

                String AttributeName = "";
                String description = "";
                String Attribute = "";


                for (int i = 0; i <= arraysize - 1; i++)
                {
                    foreach (String _d in d[i])
                    {
                        Attribute = _d.Trim();
                        AttributeName = cleanattribute(_d.Trim());
                        description = getattributedb(_d.Trim(), _editionid);
                    }
                }


            }
            catch (Exception e)
            {
                String msg = e.Message;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult insertproductcontents(String ClientId, String EditionId, String ProductId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int _clienid = int.Parse(ClientId);
            int _editionid = int.Parse(EditionId);
            int _productid = int.Parse(ProductId);

            try
            {
                var _pc = (from _ppc in db.ProductContents
                           where _ppc.ProductId == _productid
                           && _ppc.EditionId == _editionid
                           select _ppc).ToList();
                if (_pc.LongCount() > 0)
                {
                    foreach (ProductContents _ppc in _pc)
                    {
                        var delete = db.ProductContents.SingleOrDefault(x => x.ProductId == _ppc.ProductId && x.ClientId == _ppc.ClientId && x.EditionId == _ppc.EditionId && x.AttributeId == _ppc.AttributeId);

                        db.ProductContents.Remove(delete);
                        db.SaveChanges();
                    }
                    ActivityLog._deleteproductcontents(_editionid, _clienid, _productid, ApplicationId, UsersId);
                }
            }
            catch (Exception e)
            {
                String msj = e.Message;
            }

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ClientId == _clienid
                      && _pp.EditionId == _editionid
                      && _pp.ProductId == _productid
                      select _pp).ToList();
            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    int i, end, iContent, fContent;

                    String attributeId = "";
                    String attribute = _pp.XMLContent;

                    String attr = "";
                    String content = "";
                    String attributeDescription = "";
                    String titulo = "<Titulo>";
                    String contenido = "<Contenido>";
                    String plainContent = "";

                    while (attribute != null)
                    {
                        i = attribute.IndexOf("<Titulo>");
                        if (i >= 0)
                        {
                            end = attribute.IndexOf("</Titulo>", i);
                            attributeDescription = attribute.Substring(i + titulo.Length, end - (i + titulo.Length));
                            attribute = attribute.Substring(end);

                            if (attributeDescription != "")
                            {
                                attr = attributeDescription.Replace(":", "").Trim().Replace(";", "").Trim().Replace(".", "");

                                var attrs = (from atr in db.Attributes
                                             where atr.Description == attr.Trim()
                                             select atr).ToList();
                                if (attrs.LongCount() > 0)
                                {
                                    foreach (Attributes _attrs in attrs)
                                    {
                                        attributeId = _attrs.AttributeId.ToString();
                                    }
                                    attributeDescription = "";
                                }
                            }

                            iContent = attribute.IndexOf("<Contenido>", 0);
                            if (iContent >= 0)
                            {
                                fContent = attribute.IndexOf("</Contenido>", iContent);
                                content = attribute.Substring(iContent + contenido.Length, fContent - (iContent + contenido.Length));
                                content = content.Trim();

                                attribute = attribute.Substring(iContent, attribute.Length - iContent);
                            }
                            else
                            {
                                attribute = null;
                            }
                            if ((attributeId != "") && (content != ""))
                            {
                                plainContent = content;
                                plainContent = cleanImage(plainContent);
                                plainContent = cleanTable(plainContent);

                                int AttributeId = int.Parse(attributeId);

                                var pc = (from pcont in db.ProductContents
                                          where pcont.ClientId == _clienid
                                          && pcont.EditionId == _editionid
                                          && pcont.ProductId == _productid
                                          && pcont.AttributeId == AttributeId
                                          select pcont).ToList();

                                if (pc.LongCount() == 0)
                                {

                                    ProductContents = new ProductContents();

                                    ProductContents.AttributeId = AttributeId;
                                    ProductContents.Content = content;
                                    ProductContents.PlainContent = plainContent;
                                    ProductContents.EditionId = _editionid;
                                    ProductContents.ClientId = _clienid;
                                    ProductContents.ProductId = _productid;
                                    ProductContents.HtmlContent = null;

                                    db.ProductContents.Add(ProductContents);
                                    db.SaveChanges();



                                    attributeId = "";
                                    content = "";
                                    plainContent = "";
                                }
                            }
                        }
                        else
                        {
                            attribute = null;
                        }
                    }
                }
                ActivityLog._insertproductcontents(_editionid, _clienid, _productid, ApplicationId, UsersId);
            }
            insertHTMLContent(_productid, _editionid, _clienid);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public static String cleanImage(String S)
        {
            String imagen;
            int i, f;
            String content = S;

            while (S != null)
            {
                i = S.IndexOf("<Imagen ");
                if (i >= 0)
                {
                    f = S.IndexOf("/>", i);
                    imagen = S.Substring(i, (f + 2) - i);
                    S = S.Replace(imagen, " ");
                    content = S;
                }
                else S = null;
            }
            return content;
        }

        public static String cleanTable(String S)
        {
            String table;
            int i, f;
            String content = S;

            while (S != null)
            {
                i = S.IndexOf("<Tabla ");
                if (i >= 0)
                {
                    f = S.IndexOf("</Tabla>", i);
                    table = S.Substring(i, (f + 8) - i);
                    S = S.Replace(table, " ");
                    content = S;
                }
                else S = null;
            }
            return content;
        }

        public static String cleanattribute(String attribute)
        {

            attribute = attribute.Replace(",", "");
            attribute = attribute.Replace("..", "");
            attribute = attribute.Replace("  ", " ");

            attribute = attribute.Replace("á", "a");
            attribute = attribute.Replace("Á", "A");
            attribute = attribute.Replace("é", "e");
            attribute = attribute.Replace("É", "E");
            attribute = attribute.Replace("í", "i");
            attribute = attribute.Replace("Í", "I");
            attribute = attribute.Replace("ó", "o");
            attribute = attribute.Replace("Ó", "O");
            attribute = attribute.Replace("Ú", "U");
            attribute = attribute.Replace("ú", "u");
            attribute = attribute.Replace("ü", "u");
            attribute = attribute.Replace("Ü", "U");

            //attribute = attribute.Replace("","");
            //attribute = attribute.Replace("", "");
            //attribute = attribute.Replace("", "");
            //attribute = attribute.Replace("", "");
            //attribute = attribute.Replace("", "");
            //attribute = attribute.Replace("", "");
            //attribute = attribute.Replace("", "");
            //attribute = attribute.Replace("", "");
            //attribute = attribute.Replace("", "");
            //attribute = attribute.Replace("", "");


            return attribute;
        }

        public String getattributedb(String AttributeName, int _editionid)
        {
            AttributeName = AttributeName.ToUpper();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            string descriptiondatabase = string.Empty;
            Attributes Attributes = new Models.Attributes();
            List<Attributes> la = new List<Models.Attributes>();
            String Attribute = "";
            int AttributeId = 0;

            String _description = AttributeName;

            _description = cleanattribute(_description);

            var att = (from attr in db.Attributes
                       select attr).OrderBy(x => x.Description).ToList();

            foreach (Attributes _att in att)
            {
                Attributes = new Attributes();

                descriptiondatabase = _att.Description.Trim();
                descriptiondatabase = cleanattribute(descriptiondatabase.Trim().ToUpper());

                Attributes.Description = descriptiondatabase;
                Attributes.AttributeId = _att.AttributeId;

                la.Add(Attributes);
            }

            foreach (Attributes _at in la)
            {
                if (_description.Equals(_at.Description.Trim()))
                {
                    Attribute = _at.Description;
                    AttributeId = _at.AttributeId;
                }
            }

            var res = (from _res in la
                       where _res.Description == Attribute
                       select _res).ToList();

            if (res.LongCount() > 0)
            {
                var at = (from atts in db.Attributes
                          where atts.AttributeId == AttributeId
                          select atts).ToList();
                foreach (Attributes _at in at)
                {
                    var ea = (from edt in db.EditionAttributes
                              where edt.AttributeId == _at.AttributeId
                                      && edt.EditionId == _editionid
                              select edt).ToList();

                    if (ea.LongCount() == 0)
                    {
                        EditionAttributes = new Models.EditionAttributes();

                        EditionAttributes.EditionId = _editionid;
                        EditionAttributes.AttributeId = _at.AttributeId;
                        EditionAttributes.Active = true;

                        db.EditionAttributes.Add(EditionAttributes);
                        db.SaveChanges();

                        ActivityLog._inserteditionattributes(_at.AttributeId, _editionid, ApplicationId, UsersId);
                    }
                }
            }
            else
            {
                Attributes = new Models.Attributes();

                Attributes.Active = true;
                Attributes.Description = AttributeName;

                db.Attributes.Add(Attributes);
                db.SaveChanges();

                var at = (from attts in db.Attributes
                          where attts.Description == AttributeName
                          select attts).ToList();
                if (at.LongCount() > 0)
                {
                    foreach (Attributes _at in at)
                    {

                        ActivityLog._insertattributes(_at.AttributeId, AttributeName, ApplicationId, UsersId);

                        var ea = (from edt in db.EditionAttributes
                                  where edt.AttributeId == _at.AttributeId
                                          && edt.EditionId == _editionid
                                  select edt).ToList();

                        if (ea.LongCount() == 0)
                        {
                            EditionAttributes = new Models.EditionAttributes();

                            EditionAttributes.EditionId = _editionid;
                            EditionAttributes.AttributeId = _at.AttributeId;
                            EditionAttributes.Active = true;

                            db.EditionAttributes.Add(EditionAttributes);
                            db.SaveChanges();

                            ActivityLog._inserteditionattributes(_at.AttributeId, _editionid, ApplicationId, UsersId);
                        }
                    }
                }
            }
            return "";
        }

        public ActionResult asociateattributegroup(String AttributeGoup, String Attribute, String Edition)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            EditionAttributeGroup EditionAttributeGroup = new Models.EditionAttributeGroup();
            EditionAttributes EditionAttributes = new Models.EditionAttributes();

            int AttributeGroupId = int.Parse(AttributeGoup);
            int AttributeId = int.Parse(Attribute);
            int EditionId = int.Parse(Edition);

            var ea = (from _ea in db.EditionAttributes
                      where _ea.AttributeId == AttributeId
                      && _ea.EditionId == EditionId
                      select _ea).ToList();
            if (ea.LongCount() > 0)
            {
                var eag = (from _eag in db.EditionAttributeGroup
                           where _eag.AttributeGroupId == AttributeGroupId
                           && _eag.AttributeId == AttributeId
                           && _eag.EditionId == EditionId
                           select _eag).ToList();

                if (eag.LongCount() == 0)
                {
                    try
                    {

                        var delete = db.EditionAttributeGroup.SingleOrDefault(x => x.EditionId == EditionId && x.AttributeId == AttributeId);
                        if (delete != null)
                        {
                            var eagg = (from _ea in db.EditionAttributeGroup
                                        where _ea.AttributeId == AttributeId
                                        && _ea.EditionId == EditionId
                                        select _ea).ToList();
                            if (eagg.LongCount() > 0)
                            {
                                foreach (EditionAttributeGroup _eagg in eagg)
                                {
                                    db.EditionAttributeGroup.Remove(delete);
                                    db.SaveChanges();

                                    ActivityLog._deleteeditionattributesgroup(_eagg.EditionId, _eagg.AttributeId, _eagg.AttributeGroupId, ApplicationId, UsersId);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    EditionAttributeGroup = new Models.EditionAttributeGroup();

                    EditionAttributeGroup.Active = true;
                    EditionAttributeGroup.AttributeGroupId = AttributeGroupId;
                    EditionAttributeGroup.AttributeId = AttributeId;
                    EditionAttributeGroup.EditionId = EditionId;

                    db.EditionAttributeGroup.Add(EditionAttributeGroup);
                    db.SaveChanges();

                    ActivityLog._inserteditionattributesgroup(EditionId, AttributeId, AttributeGroupId, ApplicationId, UsersId);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                EditionAttributes.AttributeId = AttributeId;
                EditionAttributes.EditionId = EditionId;

                db.EditionAttributes.Add(EditionAttributes);
                db.SaveChanges();

                ActivityLog._inserteditionattributes(AttributeId, EditionId, ApplicationId, UsersId);

                var eas = (from _ea in db.EditionAttributes
                           where _ea.AttributeId == AttributeId
                           && _ea.EditionId == EditionId
                           select _ea).ToList();
                if (eas.LongCount() > 0)
                {
                    var eag = (from _eag in db.EditionAttributeGroup
                               where _eag.AttributeGroupId == AttributeGroupId
                               && _eag.AttributeId == AttributeId
                               && _eag.EditionId == EditionId
                               select _eag).ToList();

                    if (eag.LongCount() == 0)
                    {
                        try
                        {

                            var delete = db.EditionAttributeGroup.SingleOrDefault(x => x.EditionId == EditionId && x.AttributeId == AttributeId);
                            if (delete != null)
                            {
                                var eagg = (from _ea in db.EditionAttributeGroup
                                            where _ea.AttributeId == AttributeId
                                            && _ea.EditionId == EditionId
                                            select _ea).ToList();
                                if (eagg.LongCount() > 0)
                                {
                                    foreach (EditionAttributeGroup _eagg in eagg)
                                    {
                                        db.EditionAttributeGroup.Remove(delete);
                                        db.SaveChanges();

                                        ActivityLog._deleteeditionattributesgroup(_eagg.EditionId, _eagg.AttributeId, _eagg.AttributeGroupId, ApplicationId, UsersId);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }
                        EditionAttributeGroup = new Models.EditionAttributeGroup();

                        EditionAttributeGroup.Active = true;
                        EditionAttributeGroup.AttributeGroupId = AttributeGroupId;
                        EditionAttributeGroup.AttributeId = AttributeId;
                        EditionAttributeGroup.EditionId = EditionId;

                        db.EditionAttributeGroup.Add(EditionAttributeGroup);
                        db.SaveChanges();

                        ActivityLog._inserteditionattributesgroup(EditionId, AttributeId, AttributeGroupId, ApplicationId, UsersId);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                return View();
            }
        }

        public ActionResult getxml(int Id)
        {
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            if (ind != null)
            {
                int ClientId = ind.ClId;

                String FIleName = "";

                var client = (from c in db.Clients
                              where c.ClientId == ClientId
                              select c).ToList();
                foreach (Clients _client in client)
                {
                    if (_client.ShortName == null)
                    {
                        FIleName = _client.CompanyName;
                    }
                    if (_client.ShortName == string.Empty)
                    {
                        FIleName = _client.CompanyName;
                    }
                    else if ((_client.ShortName != null) && (_client.ShortName != string.Empty))
                    {
                        FIleName = _client.ShortName;
                    }
                }

                var product = (from p in db.Products
                               where p.ProductId == Id
                               select p).ToList();
                foreach (Products _product in product)
                {
                    FIleName = FIleName + "_" + _product.ProductName.Trim();
                }

                FIleName = classreplace.replacehtmlfilename(FIleName);

                FIleName = FIleName + ".xml";

                var root = Path.Combine(Server.MapPath("/App_Data/XML"), FIleName);

                if (System.IO.File.Exists(root))
                {
                    return File(root, "text/xml");
                }
                else
                {
                    var path = Path.Combine(Server.MapPath("/App_Data/XML"), "errorfile.html");

                    //return File(path, "text/html");
                    return PartialView("errorfile");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult gethtmlshow(int Id)
        {
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int ClientId = ind.ClId;

            String FIleName = "";

            var client = (from c in db.Clients
                          where c.ClientId == ClientId
                          select c).ToList();
            foreach (Clients _client in client)
            {
                FIleName = FIleName + _client.CompanyName;
            }

            var product = (from p in db.Products
                           where p.ProductId == Id
                           select p).ToList();
            foreach (Products _product in product)
            {
                FIleName = FIleName + "_" + _product.ProductName.Trim();
            }

            FIleName = classreplace.replacehtmlfilename(FIleName);

            FIleName = FIleName + ".htm";

            var root = Path.Combine(Server.MapPath("/App_Data/HTML"), FIleName);

            if (System.IO.File.Exists(root))
            {
                return File(root, "text/html");
            }
            else
            {
                FIleName = FIleName + ".html";

                FIleName = FIleName.Replace(".htm.html", ".html");

                root = Path.Combine(Server.MapPath("/App_Data/HTML"), FIleName);

                if (System.IO.File.Exists(root))
                {
                    return File(root, "text/html");
                }
                else
                {
                    var path = Path.Combine(Server.MapPath("/Images"), "error_image.png");

                    //return File(path, "image/png");

                    return PartialView("errorfile");
                }
            }
        }

        public ActionResult segmentallproducts(string CountryId, string EditionId, string BookId)
        {
            ViewData["validation"] = false;
            ViewData["ResultSearch"] = null;
            if (CountryId != null)
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                int _countryid = int.Parse(CountryId);
                int _editionid = int.Parse(EditionId);
                int _bookid = int.Parse(BookId);

                int CountryIdS = 0;
                String CountryName = "";
                int EditionIdS = 0;
                int NumberEdition = 0;
                int BookIdS = 0;
                String BookName = "";

                var country = (from c in db.Countries
                               where c.CountryId == _countryid
                               select c).ToList();
                foreach (Countries _country in country)
                {
                    CountryIdS = _country.CountryId;
                    CountryName = _country.CountryName;
                }

                var edition = (from e in db.Editions
                               where e.EditionId == _editionid
                               select e).ToList();
                foreach (Editions _edition in edition)
                {
                    NumberEdition = _edition.NumberEdition;
                    EditionIdS = _edition.EditionId;
                }

                var book = (from b in db.Books
                            where b.BookId == _bookid
                            select b).ToList();
                foreach (Books _book in book)
                {
                    BookIdS = _book.BookId;
                    BookName = _book.BookName;
                }

                sessionsegmentallproducts sessionsegmentallproducts = new sessionsegmentallproducts(CountryIdS, CountryName, EditionIdS, NumberEdition, BookIdS, BookName);
                Session["sessionsegmentallproducts"] = sessionsegmentallproducts;

                ViewData["segment"] = true;

                String File = "";

                var ctr = (from c in PLMUsers.Users
                           join count in PLMUsers.CountriesUser
                               on c.CountryId equals count.CountryId
                           where c.UserId == UsersId
                           select count).ToList();
                if (ctr.LongCount() > 0)
                {
                    foreach (CountriesUser _ctr in ctr)
                    {
                        File = "targetsallfile_" + _ctr.ID + ".xml";
                    }
                }


                var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

                if (System.IO.File.Exists(root))
                {
                    XmlDocument myXmlDocument = new XmlDocument();
                    myXmlDocument.Load(root);

                    XmlNode node;
                    node = myXmlDocument.DocumentElement;

                    XmlReader xtr = XmlReader.Create(root);
                    while (xtr.Read())
                    {
                        if (xtr.Name == "TargetDescription")
                        {
                            String pname = xtr.ReadInnerXml();

                            pname = pname.Replace("> </p>", "");
                            pname = pname.Replace("</p>", "");

                            ViewData["ProductNametargetall"] = pname;
                        }

                        if (xtr.Name == "TargetDescriptionPropag")
                        {
                            String bpropag = xtr.ReadInnerXml();

                            bpropag = bpropag.Replace("</p>", "");

                            ViewData["BPropagandaall"] = bpropag;
                        }

                        if (xtr.Name == "TargetDescriptionAttribute")
                        {
                            String attr = xtr.ReadInnerXml();

                            attr = attr.Replace("</p>", "");
                            attr = attr.Replace("</span>", "");

                            ViewData["Attributell"] = attr;
                        }

                        if (xtr.Name == "TargetDescriptionLaboratory")
                        {
                            String Laboratory = xtr.ReadInnerXml();

                            Laboratory = Laboratory.Replace("</p>", "");
                            Laboratory = Laboratory.Replace("</span>", "");

                            ViewData["LaboratoryL"] = Laboratory;
                        }
                    }
                    xtr.Close();
                }
                else
                {
                    ViewData["validation"] = false;
                }
                List<HTMLProductsAttributes> LS = getlistresume();

                return View("segmentallproducts", LS);
            }
            else
            {
                ViewData["validation"] = false;
                sessionsegmentallproducts sessionsegmentallproducts = (sessionsegmentallproducts)Session["sessionsegmentallproducts"];
                if (sessionsegmentallproducts != null)
                {
                    CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
                    int UsersId = cu.userId;

                    String File = "";

                    var ctr = (from c in PLMUsers.Users
                               join count in PLMUsers.CountriesUser
                                   on c.CountryId equals count.CountryId
                               where c.UserId == UsersId
                               select count).ToList();
                    if (ctr.LongCount() > 0)
                    {
                        foreach (CountriesUser _ctr in ctr)
                        {
                            File = "targetsallfile_" + _ctr.ID + ".xml";
                        }
                    }

                    var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

                    if (System.IO.File.Exists(root))
                    {
                        XmlDocument myXmlDocument = new XmlDocument();
                        myXmlDocument.Load(root);

                        XmlNode node;
                        node = myXmlDocument.DocumentElement;

                        XmlReader xtr = XmlReader.Create(root);
                        while (xtr.Read())
                        {
                            if (xtr.Name == "TargetDescription")
                            {
                                String pname = xtr.ReadInnerXml();

                                pname = pname.Replace("> </p>", "");
                                pname = pname.Replace("</p>", "");

                                ViewData["ProductNametargetall"] = pname;
                            }

                            if (xtr.Name == "TargetDescriptionPropag")
                            {
                                String bpropag = xtr.ReadInnerXml();

                                bpropag = bpropag.Replace("</p>", "");

                                ViewData["BPropagandaall"] = bpropag;
                            }

                            if (xtr.Name == "TargetDescriptionAttribute")
                            {
                                String attr = xtr.ReadInnerXml();

                                attr = attr.Replace("</p>", "");
                                attr = attr.Replace("</span>", "");

                                ViewData["Attributell"] = attr;

                            }

                            if (xtr.Name == "TargetDescriptionLaboratory")
                            {
                                String Laboratory = xtr.ReadInnerXml();

                                Laboratory = Laboratory.Replace("</p>", "");
                                Laboratory = Laboratory.Replace("</span>", "");

                                ViewData["LaboratoryL"] = Laboratory;
                            }
                        }
                        xtr.Close();
                    }
                    else
                    {
                        ViewData["validation"] = false;
                    }
                    List<HTMLProductsAttributes> LS = getlistresume();

                    return View("segmentallproducts", LS);
                }
            }

            return View();
        }

        public ActionResult gettargets(String productname, String propag, String Attribute, String Laboratory)
        {
            productname = productname.Replace("60", "<");
            productname = productname.Replace("62", ">");
            productname = productname.Replace("> ", ">");
            propag = propag.Replace("60", "<");
            propag = propag.Replace("62", ">");
            Attribute = Attribute.Replace("60", "<");
            Attribute = Attribute.Replace("62", ">");
            Attribute = Attribute.Replace("> ", ">");
            Laboratory = Laboratory.Replace("60", "<");
            Laboratory = Laboratory.Replace("62", ">");
            Laboratory = Laboratory.Replace("> ", ">");
            try
            {
                StreamWriter SW;

                String rootbeg = "<Root>";
                String rootend = "</Root>";



                CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
                int UsersId = cu.userId;

                String File = "";
                String Folder = "";
                var usr = (from _usr in PLMUsers.Users
                           where _usr.UserId == UsersId
                           select _usr).ToList();
                foreach (Users _usr in usr)
                {
                    File = "targetsallfile_" + _usr.NickName + ".xml";
                    Folder = _usr.NickName;
                }

                var rr = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);

                var map = "";

                if (System.IO.Directory.Exists(rr))
                {
                    map = Path.Combine(rr, File);
                }
                else
                {
                    DirectoryInfo dir = System.IO.Directory.CreateDirectory(rr);
                    map = Path.Combine(rr, File);
                }

                String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";

                SW = System.IO.File.CreateText(map);
                SW.Write(xml);
                SW.Write(rootbeg);

                if ((productname != null) || (productname != String.Empty))
                {
                    SW.Write("<TargetName>");
                    SW.Write("Producto");
                    SW.Write("</TargetName>");

                    SW.Write("<TargetDescription>");
                    SW.Write(productname + "</p>");
                    SW.Write("</TargetDescription>");
                }

                if ((propag != null) || (propag != String.Empty))
                {
                    SW.Write("<TargetPropag>");
                    SW.Write("Base de Propaganda");
                    SW.Write("</TargetPropag>");

                    SW.Write("<TargetDescriptionPropag>");
                    SW.Write(propag + "</p>");
                    SW.Write("</TargetDescriptionPropag>");
                }

                if ((Attribute != null) && (Attribute != String.Empty))
                {
                    SW.Write("<TargetAttribute>");
                    SW.Write("Atributo");
                    SW.Write("</TargetAttribute>");

                    SW.Write("<TargetDescriptionAttribute>");
                    SW.Write(Attribute + "</span></p>");
                    SW.Write("</TargetDescriptionAttribute>");
                }

                if ((Laboratory != null) && (Laboratory != String.Empty))
                {
                    SW.Write("<TargetLaboratory>");
                    SW.Write("Laboratorio");
                    SW.Write("</TargetLaboratory>");

                    SW.Write("<TargetDescriptionLaboratory>");
                    SW.Write(Laboratory + "</p>");
                    SW.Write("</TargetDescriptionLaboratory>");
                }
                SW.Write(rootend);
                SW.Close();
            }
            catch (Exception e)
            {
                var ex = e.Message;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult segmentallcontent(HttpPostedFileBase file)
        {
            string pname = string.Empty;
            string prop = string.Empty;
            string attr = string.Empty;
            string Laboratory = string.Empty;

            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
            int UsersId = cu.userId;

            String File = "";

            var ctr = (from c in PLMUsers.Users
                       join count in PLMUsers.CountriesUser
                           on c.CountryId equals count.CountryId
                       where c.UserId == UsersId
                       select count).ToList();
            if (ctr.LongCount() > 0)
            {
                foreach (CountriesUser _ctr in ctr)
                {
                    File = "targetsallfile_" + _ctr.ID + ".xml";
                }
            }

            var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(root);

            XmlNode node;
            node = myXmlDocument.DocumentElement;

            XmlReader xtr = XmlReader.Create(root);
            while (xtr.Read())
            {
                if (xtr.Name == "TargetDescription")
                {
                    pname = xtr.ReadInnerXml();
                    pname = pname.Replace("></p>", "");
                    pname = pname.Replace("> </p>", "");
                    pname = pname.Replace("</p>", "");

                }

                if (xtr.Name == "TargetDescriptionPropag")
                {
                    prop = xtr.ReadInnerXml();
                    prop = prop.Replace("></p>", "");
                    prop = prop.Replace("</p>", "");

                }

                if (xtr.Name == "TargetDescriptionAttribute")
                {
                    attr = xtr.ReadInnerXml();

                    attr = attr.Replace("</p>", "");
                    attr = attr.Replace("</span>", "");
                }

                if (xtr.Name == "TargetDescriptionLaboratory")
                {
                    Laboratory = xtr.ReadInnerXml();

                    Laboratory = Laboratory.Replace("</p>", "");
                    Laboratory = Laboratory.Replace("</span>", "");
                }

            }
            xtr.Close();

            String FileName = file.FileName;
            String Extention = Path.GetExtension(file.FileName);

            var rootcopy = Path.Combine(Server.MapPath("/App_Data/SegmentProducts/CompleteHtml"), FileName);
            try
            {
                file.SaveAs(rootcopy);
            }
            catch (Exception e)
            {
                String msg = e.Message;
            }

            var rootgetfile = Path.Combine(Server.MapPath("/App_Data/SegmentProducts/CompleteHtml"), FileName);

            if (System.IO.File.Exists(rootgetfile))
            {
                string resp = gethtml(pname, prop, rootgetfile, attr, Laboratory);

                if (resp == "_error")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    FileName = FileName.Replace(Extention, "");
                    string path2 = Server.MapPath("/App_Data/SegmentProducts/SegmentedProducts/" + FileName + "/XML");

                    List<String> check = VERIFYXML(path2);

                    if (check.LongCount() > 0)
                    {
                        String Filen = "resumeproducts.xml";
                        String Foldern = "";
                        var usr = (from _usr in PLMUsers.Users
                                   where _usr.UserId == UsersId
                                   select _usr).ToList();
                        foreach (Users _usr in usr)
                        {
                            Foldern = _usr.NickName;
                        }

                        var map = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Foldern);
                        var rootmap = Path.Combine(map, Filen);

                        System.IO.File.Delete(rootmap);

                        return Json(check, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        getfilehtml(attr);

                        List<HTMLProductsAttributes> LS = getlistresume();

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public List<String> VERIFYXML(string desc)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo(desc + @"\");
            List<String> Litems = new List<String>();
            foreach (FileInfo fln in dir.GetFiles())
            {
                try
                {
                    XmlDocument cdoc = new XmlDocument();
                    cdoc.Load(fln.FullName);
                }
                catch (Exception e)
                {
                    Litems.Add(fln.Name);
                }
            }
            return Litems;
        }

        public string getHtmlContent(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentException("The source path cannot be null or empty.");

            StreamReader sr = new StreamReader(sourcePath, System.Text.Encoding.UTF8);
            string strHtml = sr.ReadToEnd();

            sr.Close();
            sr.Dispose();

            return strHtml;
        }

        public string gethtml(String pname, String prop, String rootgetfile, String attr, String Laboratory)
        {
            //string temp = "../../IndesignTemplate.htm";
            string temp = Path.Combine(Server.MapPath("/App_Data/Templates"), "IndesignTemplate.htm");
            string token3 = "<p class='t-tulo-prod-nuevo'>";
            string s = "";
            string path3 = "";
            int cont = 0;
            int count = 0;
            do
            {
                string path = rootgetfile;
                string pathD = path.Substring(0, path.LastIndexOf("\\") + 1);

                DirectoryInfo directory = new DirectoryInfo(path);

                FileInfo item = new FileInfo(path);

                cont++;
                string filename = item.Name.Substring(0, item.Name.IndexOf("."));

                sessionfilename sessionfilename = new Models.sessionfilename(filename);
                Session["sessionfilename"] = sessionfilename;

                string path2 = Server.MapPath("/App_Data/SegmentProducts/SegmentedProducts");


                string allcontent = getHtmlContent(item.FullName);

                string template = "";
                try
                {
                    template = getHtmlContent((temp));
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }

                string name, pf, inf;

                int ini, ini2, ini3, next;
                int fin, fin2, med, med2;
                int len, len2, len3;



                len = pname.Length;
                len3 = token3.Length;

                next = 0;
                inf = "";
                ini3 = 0;
                if (allcontent.IndexOf(token3, next) != -1)
                {
                    ini3 = allcontent.IndexOf(token3, next);
                    inf = allcontent.Substring(ini3, (allcontent.IndexOf("<", (ini3 + len3)) - ini3));
                }

                for (int i = 0; i < allcontent.Length; i++)
                {

                    if (allcontent.IndexOf(pname, next) != -1)
                    {
                        StreamWriter SW;

                        String rootbeg = "<Root>";
                        String rootend = "</Root>";
                        String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";

                        CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
                        int UsersId = cu.userId;

                        String File = "filename.xml";
                        String Folder = "";
                        var usr = (from _usr in PLMUsers.Users
                                   where _usr.UserId == UsersId
                                   select _usr).ToList();
                        foreach (Users _usr in usr)
                        {
                            Folder = _usr.NickName;
                        }

                        var rr = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);

                        var map = "";

                        if (System.IO.Directory.Exists(rr))
                        {
                            map = Path.Combine(rr, File);
                        }
                        else
                        {
                            DirectoryInfo dir = System.IO.Directory.CreateDirectory(rr);
                            map = Path.Combine(rr, File);
                        }

                        SW = System.IO.File.CreateText(map);
                        SW.Write(xml);
                        SW.Write(rootbeg);

                        SW.Write("<TargetName>");
                        SW.Write("Producto");
                        SW.Write("</TargetName>");

                        SW.Write("<ProductName>");
                        SW.Write(filename);
                        SW.Write("</ProductName>");

                        SW.Write(rootend);
                        SW.Close();


                        count = count + 1;

                        ini = allcontent.IndexOf(pname, next);
                        med = allcontent.IndexOf(">", ini);
                        fin = allcontent.IndexOf("</", med);

                        name = allcontent.Substring(med + 1, (fin - med) - 1);
                        name = classreplace.quitAccentsFileName(name);
                        int xx = allcontent.IndexOf(pname, fin);
                        ini2 = allcontent.IndexOf(prop, next);

                        if (ini2 > 0 && ini2 < allcontent.IndexOf(pname, fin))
                        {
                            med2 = allcontent.IndexOf(">", ini2);
                            fin2 = allcontent.IndexOf("</", med2);
                            pf = allcontent.Substring(med2 + 1, (fin2 - med2) - 1);
                            pf = classreplace.quitAccentsFileName(pf);
                            pf = classreplace.cleanName(pf);
                        }
                        else
                            pf = string.Empty;



                        next = allcontent.IndexOf(pname, fin);

                        System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

                        if (next > 0)
                        {
                            string file = allcontent.Substring(ini, (next - ini));

                            file = classreplace.quitAccents(file);
                            file = classreplace.changeImage(file, filename);
                            file = classreplace.findEmail(file, filename);
                            file = classreplace.findUrl(file, filename);

                            file = file.Replace("xml:lang='es-ES'>", "");
                            file = file.Replace("<p class=\"Antetitulo\">Información nueva</p>", "");

                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");



                            file = file.Replace("class=\"Ningún-estilo-de-tabla\"", "");
                            file = file.Replace("class=\"Ning&uacute;n-estilo-de-tabla\"", "");
                            file = file.Replace("Ning&uacute;n-estilo-de-tabla", "Ningun-estilo-de-tabla");
                            file = file.Replace(" lang=\"en-US\"", "");
                            file = file.Replace("<p class=\"Medio-de-D--prod--nuevo\">INFORMACIÓN NUEVA</p>", "");
                            file = file.Replace("<p class=\"Medio-de-D--prod--nuevo\">INFORMACI&Oacute;N NUEVA</p>", "");
                            file = file.Replace(" lang=\"es-ES\"", "");
                            file = file.Replace("<td class=\"Tabla-b-sica Tabla\" />", "<td class=\"Tabla-b-sica Tabla\"> </td>");
                            file = file.Replace("                                      </p>", "</p>");
                            file = file.Replace("\n\n\n", "");
                            file = file.Replace("</span><span class=\"bold-entrada\">", "");
                            file = file.Replace("<td class=\"tb_transparente cl_celdas_sin_filo cl_celdas_sin_filo\" />", "<td class=\"tb_transparente cl_celdas_sin_filo cl_celdas_sin_filo\"> </td>");
                            file = file.Replace("<p class=\"pf_especial_titulo_prod\">", "<p class=\"pf_normal\">");
                            file = file.Replace("T-tulo-prod--nuevo", "T-tulo");


                            classreplace.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));
                            sb.Replace("@@@Contenido@@@", inf + file);

                            inf = classreplace.getString(file, token3);
                        }
                        else
                        {
                            string file = allcontent.Substring(ini);

                            file = classreplace.quitAccents(file);
                            classreplace.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));//, "@@@Contenido@@@=" + file); 
                            sb.Replace("@@@Contenido@@@", inf + file);

                            i = allcontent.Length;
                        }

                        path3 = path2;

                        path3 = path3 + "\\" + filename + "\\HTML";

                        if (System.IO.Directory.Exists(path3))
                        {
                            classreplace.saveHtmlFile(path3,
                            name.Trim() + "_" + pf.Trim() + ".htm", sb.ToString());

                            String file = name.Trim() + "_" + pf.Trim();
                            String desc = path2 + "\\" + filename + "\\XML";

                            if (System.IO.Directory.Exists(desc))
                            {
                                file = file + ".htm";
                                path3 = Path.Combine(path3, file);
                                try
                                {
                                    segmentcontent.getxml(path3, file, desc, pname, prop, attr, Laboratory);
                                }
                                catch (Exception e)
                                {

                                }
                            }
                            else
                            {
                                System.IO.Directory.CreateDirectory(desc);

                                file = file + ".htm";
                                path3 = Path.Combine(path3, file);
                                segmentcontent.getxml(path3, file, desc, pname, prop, attr, Laboratory);
                            }
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(path2 + "\\" + filename + "\\HTML");

                            var root = path2 + "\\" + filename + "\\HTML";

                            classreplace.saveHtmlFile(root,
                            name.Trim() + "_" + pf.Trim() + ".htm", sb.ToString());

                            String file = name.Trim() + "_" + pf.Trim();
                            String desc = path2 + "\\" + filename + "\\XML";
                            if (System.IO.Directory.Exists(desc))
                            {
                                file = file + ".htm";
                                path3 = Path.Combine(path3, file);
                                segmentcontent.getxml(path3, file, desc, pname, prop, attr, Laboratory);
                            }
                            else
                            {
                                System.IO.Directory.CreateDirectory(desc);

                                file = file + ".htm";
                                path3 = Path.Combine(path3, file);
                                segmentcontent.getxml(path3, file, desc, pname, prop, attr, Laboratory);
                            }

                        }
                        name = "";
                        pf = "";
                    }
                    else
                    {
                        return "_error";
                    }
                }
            }
            while (s.ToLower() == "s");

            if (count == 0)
            {
                return "_error";
            }
            else
            {
                return "";
            }
        }

        public ActionResult getfilehtml(String attr)
        {
            attr = attr.Replace("\t", "");
            String _string = "";
            for (int i = attr.Length - 1; i > -1; i--)
            {
                _string += attr[i];
            }

            var _str = _string.Substring(0, 1);
            _string = _string.Replace(_str, "");

            for (int i = _str.Length - 1; i > -1; i--)
            {
                attr += _str[i];
            }

            attr = attr.Replace(">>", "");

            StreamWriter SW;

            String rootbeg = "<Root>";
            String rootend = "</Root>";
            String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";

            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
            int UsersId = cu.userId;

            String File = "resumeproducts.xml";
            String Folder = "";
            var usr = (from _usr in PLMUsers.Users
                       where _usr.UserId == UsersId
                       select _usr).ToList();
            foreach (Users _usr in usr)
            {
                Folder = _usr.NickName;
            }

            var map = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);
            var rootmap = Path.Combine(map, File);

            string root = Server.MapPath("/App_Data/SegmentProducts/SegmentedProducts");
            sessionfilename sessionfn = (sessionfilename)Session["sessionfilename"];

            List<HTMLProductsAttributes> LS = new List<HTMLProductsAttributes>();
            HTMLProductsAttributes item = new HTMLProductsAttributes();

            if (sessionfn != null)
            {
                root = root + "\\" + sessionfn.filename;

                root = Path.Combine(root, "HTML");

                DirectoryInfo DI = new DirectoryInfo(root);

                FileInfo[] FI = DI.GetFiles();

                if (FI.Length != 0)
                {
                    SW = System.IO.File.CreateText(rootmap);
                    SW.Write(xml);
                    SW.Write(rootbeg);
                    foreach (FileInfo _fi in FI)
                    {
                        if ((_fi.Name != null) || (_fi.Name != String.Empty))
                        {
                            SW.Write("<TargetName>");
                            SW.Write("Producto");
                            SW.Write("</TargetName>");

                            SW.Write("<ProductName>");
                            SW.Write(_fi.Name);
                            SW.Write("</ProductName>");
                        }

                        var completepath = Path.Combine(root, _fi.Name);

                        StreamReader SR = System.IO.File.OpenText(completepath);
                        String content = SR.ReadToEnd();
                        String Info = "";
                        int count = 0;
                        if (!String.IsNullOrEmpty(content))
                        {
                            String target = attr.Trim();
                            String endtag = "</span>";
                            String attrcontent = null;
                            String tagfooter = "<p class=\"pie";
                            int beg, end, tagend, infaux, footer;

                            while (content != null)
                            {
                                beg = content.IndexOf(target);
                                end = content.IndexOf(endtag, beg);
                                tagend = content.IndexOf(">", beg + target.Length);
                                Info = content.Substring(tagend + 1, end - (tagend + 1));

                                infaux = content.IndexOf(target, end);

                                if (!String.IsNullOrEmpty(Info))
                                {
                                    count = count + 1;
                                }

                                if (infaux >= 0)
                                {
                                    attrcontent = content.Substring(end + endtag.Length, infaux - (end + endtag.Length));
                                    content = content.Substring(infaux, content.Length - infaux);
                                }
                                else
                                {
                                    footer = content.IndexOf(tagfooter, end);
                                    if (footer >= 0)
                                    {
                                        attrcontent = content.Substring(end, footer - end);
                                    }
                                    else
                                    {
                                        attrcontent = content.Substring(end, content.Length - end);
                                        content = null;
                                    }
                                }
                            }
                        }
                        SW.Write("<QuantityOfAttributes>");
                        SW.Write(count);
                        SW.Write("</QuantityOfAttributes>");
                        item.QuantityOfAttributes = count;

                        SR.Close();
                    }
                    SW.Write(rootend);
                    SW.Close();
                }
            }
            return View("segmentallproducts", LS);
        }

        public ActionResult imagehead()
        {
            var root = Path.Combine(Server.MapPath("~/App_Data/XML"), "filenotfound.png");

            return File(root, "image/png");
        }

        public ActionResult imagefoot()
        {
            var root = Path.Combine(Server.MapPath("~/App_Data/XML"), "error_image.png");

            return File(root, "image/png");
        }

        public ActionResult asociateContent(string Product, string Client, string FileName, string Edition)
        {
            CountriesUsers pr = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = pr.ApplicationId;
            int UsersId = pr.userId;

            int _clienid = int.Parse(Client);
            int _productid = int.Parse(Product);
            int _editionid = int.Parse(Edition);

            sessionfilename sessionfilename = (sessionfilename)Session["sessionfilename"];
            String Folder = getfilename();

            String FIleName = "";

            var client = (from c in db.Clients
                          where c.ClientId == _clienid
                          select c).ToList();
            foreach (Clients _client in client)
            {
                FIleName = FIleName + _client.CompanyName;
            }

            var product = (from p in db.Products
                           where p.ProductId == _productid
                           select p).ToList();
            foreach (Products _product in product)
            {
                FIleName = FIleName + "_" + _product.ProductName.Trim();
            }

            FIleName = classreplace.replacehtmlfilename(FIleName);

            FIleName = FIleName + ".htm";

            string path = Path.Combine(Server.MapPath("/App_Data/SegmentProducts/SegmentedProducts"), Folder, "HTML");
            path = Path.Combine(path, FileName);

            var pathxml = Path.Combine(Server.MapPath("~/App_Data/SegmentProducts/SegmentedProducts"), Folder, "XML");

            FileName = FileName.Replace(".htm", "");
            FileName = FileName.Replace(".html", "");

            string xmlfile = FileName + ".xml";

            pathxml = Path.Combine(pathxml, xmlfile);

            var deschtml = Path.Combine(Server.MapPath("~/App_Data/HTML"));
            var descxml = Path.Combine(Server.MapPath("~/App_Data/XML"));

            var pathdest = deschtml + "\\" + FIleName;

            string xml = FIleName.Replace(".htm", "").Replace(".html", "");

            xml = xml + ".xml";

            string pathdesc = Path.Combine(descxml, xml);

            if ((System.IO.File.Exists(pathdest)) || (System.IO.File.Exists(pathdesc)))
            {
                try
                {
                    if (System.IO.File.Exists(pathdest))
                    {
                        System.IO.File.Delete(pathdest);
                    }
                    if (System.IO.File.Exists(pathdesc))
                    {
                        System.IO.File.Delete(pathdesc);
                    }
                    if (System.IO.File.Exists(pathdest + "l"))
                    {
                        System.IO.File.Delete(pathdest + "l");
                    }

                }
                catch (Exception e)
                {

                }
            }
            try
            {
                System.IO.File.Copy(path, pathdest);
                System.IO.File.Copy(pathxml, pathdesc);
            }
            catch (Exception e)
            {

            }

            if (System.IO.File.Exists(pathdest))
            {
                FileInfo FI = new FileInfo(pathdest);
                String filename = FI.Name;
                String directoryname = FI.DirectoryName;

                String concatfiledir = directoryname + "\\" + filename;

                filename = filename.Replace(".html", "");
                filename = filename.Replace(".htm", "");


                String check = checkXMLallproducts(pathdesc);

                if (check == "_error")
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    StreamReader SR = System.IO.File.OpenText(pathdest);
                    String content = SR.ReadToEnd();

                    bool _response = segmentcontent.inserthtml(_clienid, _editionid, _productid, content);

                    SR.Close();

                    var source = pathdesc;
                    StreamReader StreamR;
                    StreamR = System.IO.File.OpenText(source);
                    String _content = StreamR.ReadToEnd();

                    bool response = segmentcontent.insertxml(_clienid, _editionid, _productid, _content);

                    if ((_response == true) && (response == true))
                    {
                        ActivityLog._updatecontentparticipantproducts(_editionid, _clienid, _productid, ApplicationId, UsersId);
                    }

                    StreamR.Close();

                    var pc = (from _pc in db.ProductContents
                              where _pc.ClientId == _clienid
                              && _pc.EditionId == _editionid
                              && _pc.ProductId == _productid
                              select _pc).ToList();
                    if (pc.LongCount() > 0)
                    {
                        foreach (ProductContents _pc in pc)
                        {
                            var delete = db.ProductContents.SingleOrDefault(x => x.ProductId == _pc.ProductId && x.EditionId == _pc.EditionId && x.ClientId == _pc.ClientId && x.AttributeId == _pc.AttributeId);
                            db.ProductContents.Remove(delete);
                            db.SaveChanges();
                        }
                        ActivityLog._deleteproductcontents(_editionid, _clienid, _productid, ApplicationId, UsersId);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        public string checkXMLallproducts(string descxml)
        {
            FileInfo fl = new FileInfo(descxml);
            try
            {
                XmlDocument cdoc = new XmlDocument();
                cdoc.Load(fl.FullName);
            }
            catch (Exception e)
            {
                String message = e.Message;
                return "_error";
            }
            return "ok";
        }

        public List<HTMLProductsAttributes> getlistresume()
        {
            List<HTMLProductsAttributes> LS = new List<HTMLProductsAttributes>();
            HTMLProductsAttributes item = new HTMLProductsAttributes();

            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
            int UsersId = cu.userId;

            String File = "resumeproducts.xml";
            String Folder = "";
            var usr = (from _usr in PLMUsers.Users
                       where _usr.UserId == UsersId
                       select _usr).ToList();
            foreach (Users _usr in usr)
            {
                Folder = _usr.NickName;
            }

            var map = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);
            var root = Path.Combine(map, File);

            if (System.IO.File.Exists(root))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(root);

                XmlNode node;
                node = myXmlDocument.DocumentElement;

                XmlReader xtr = XmlReader.Create(root);
                while (xtr.Read())
                {
                    if (xtr.Name == "ProductName")
                    {
                        String pname = xtr.ReadInnerXml();

                        pname = pname.Replace("> </p>", "");
                        pname = pname.Replace("</p>", "");

                        item = new HTMLProductsAttributes();

                        item.HTMLName = pname;

                    }
                    if (xtr.Name == "QuantityOfAttributes")
                    {
                        String Attributes = xtr.ReadInnerXml();
                        int QuantityOfAttributes = int.Parse(Attributes);

                        item.QuantityOfAttributes = QuantityOfAttributes;

                        LS.Add(item);
                    }

                }
                xtr.Close();
            }
            return LS;
        }

        public String getfilename()
        {
            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
            int UsersId = cu.userId;

            String File = "filename.xml";
            String Folder = "";
            var usr = (from _usr in PLMUsers.Users
                       where _usr.UserId == UsersId
                       select _usr).ToList();
            foreach (Users _usr in usr)
            {
                Folder = _usr.NickName;
            }

            var map = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);
            var rootmap = Path.Combine(map, File);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(rootmap);

            XmlNode node;
            node = myXmlDocument.DocumentElement;

            XmlReader xtr = XmlReader.Create(rootmap);

            String pname = "";
            while (xtr.Read())
            {
                if (xtr.Name == "ProductName")
                {
                    pname = xtr.ReadInnerXml();
                    pname = pname.Replace("> </p>", "");
                    pname = pname.Replace("</p>", "");
                }
            }
            xtr.Close();

            return pname;
        }

        public void insertHTMLContent(int _productid, int _editionid, int _clienid)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            String S;
            String target = gettargetattr();

            String targetF = "</span>";
            String attrNombre = "";
            String attrContent = null;
            int i = 0, f = 0, iaux = 0;

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ClientId == _clienid
                      && _pp.ProductId == _productid
                      && _pp.EditionId == _editionid
                      select _pp).ToList();
            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    S = _pp.HTMLContent;

                    int attributeId = 0;

                    int ff = 0;

                    while (S != null)
                    {
                        i = S.IndexOf(target);
                        if (i >= 0)
                        {
                            f = S.IndexOf(targetF, i);
                            ff = S.IndexOf(">", i + target.Length);
                            attrNombre = S.Substring(ff + 1, f - (ff + 1));
                            attrNombre = cleanattr(attrNombre);

                            iaux = S.IndexOf(target, f);

                            if (iaux >= 0)
                            {
                                attrContent = S.Substring(i, iaux - i);
                                S = S.Substring(iaux, S.Length - iaux);
                            }
                            else
                            {
                                attrContent = S.Substring(i, S.Length - i);
                                S = null;
                            }

                            attrContent = attrContent.Replace("</div>", "");
                            attrContent = attrContent.Replace("</body>", "");
                            attrContent = attrContent.Replace("</html>", "");
                            attrContent = attrContent.Replace("'", "\"");

                            attrNombre = attrNombre.Replace(":", "").Trim().Replace(";", "").Trim().Replace(".", "");

                            var attrs = (from atr in db.Attributes
                                         where atr.Description == attrNombre.Trim()
                                         select atr).ToList();
                            if (attrs.LongCount() > 0)
                            {
                                foreach (Attributes _attrs in attrs)
                                {
                                    attributeId = _attrs.AttributeId;
                                }
                                attrNombre = "";
                            }

                            var _pc = (from _ppc in db.ProductContents
                                       where _ppc.ProductId == _productid
                                       && _ppc.EditionId == _editionid
                                       && _ppc.ClientId == _clienid
                                       && _ppc.AttributeId == attributeId
                                       select _ppc).ToList();
                            if (_pc.LongCount() > 0)
                            {
                                foreach (ProductContents _ppc in _pc)
                                {
                                    _ppc.HtmlContent = attrContent.Trim();

                                    db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            S = null;
                        }
                    }
                }
            }
        }

        public static String cleanattr(String attrNombre)
        {
            attrNombre = attrNombre.Replace("&aacute;", "á");
            attrNombre = attrNombre.Replace("&eacute;", "é");
            attrNombre = attrNombre.Replace("&iacute;", "í");
            attrNombre = attrNombre.Replace("&oacute;", "ó");
            attrNombre = attrNombre.Replace("&uacute;", "ú");

            attrNombre = attrNombre.Replace("&Aacute;", "Á");
            attrNombre = attrNombre.Replace("&Eacute;", "É");
            attrNombre = attrNombre.Replace("&Iacute;", "Í");
            attrNombre = attrNombre.Replace("&Oacute;", "Ó");
            attrNombre = attrNombre.Replace("&OACUTE;", "Ó");
            attrNombre = attrNombre.Replace("&Uacute;", "Ú");

            attrNombre = attrNombre.Replace("&Ntilde;", "Ñ");
            attrNombre = attrNombre.Replace("&ntilde;", "ñ");
            attrNombre = attrNombre.Replace("&NTILDE;", "Ñ");

            attrNombre = attrNombre.Replace("&uml;", "ü");
            attrNombre = attrNombre.Replace("&Uml;", "Ü");
            attrNombre = attrNombre.Replace("&uuml;", "ü");
            attrNombre = attrNombre.Replace("&Uuml;", "Ü");

            attrNombre = attrNombre.Replace("<BR />", " ");
            attrNombre = attrNombre.Replace("&nbsp;", " ");
            attrNombre = attrNombre.Replace("&NBSP;", " ");
            attrNombre = attrNombre.Replace("<br />", " ");
            attrNombre = attrNombre.Replace("<br />", " ");
            attrNombre = attrNombre.Replace("&#174;", "®");
            attrNombre = attrNombre.Replace("¡", "");
            attrNombre = attrNombre.Replace("!", "");

            attrNombre = attrNombre.Replace(":", "");
            attrNombre = attrNombre.ToUpper();
            attrNombre = attrNombre.Trim();

            return attrNombre;
        }

        public String gettargetattr()
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            string pname = string.Empty;
            string prop = string.Empty;
            string attribute = string.Empty;
            string labname = string.Empty;

            String File = "";

            var ctr = (from c in PLMUsers.Users
                       join count in PLMUsers.CountriesUser
                           on c.CountryId equals count.CountryId
                       where c.UserId == UsersId
                       select count).ToList();
            if (ctr.LongCount() > 0)
            {
                foreach (CountriesUser _ctr in ctr)
                {
                    File = "targetsallfile_" + _ctr.ID + ".xml";
                }
            }


            var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(root);

            XmlNode node;
            node = myXmlDocument.DocumentElement;

            XmlReader xtr = XmlReader.Create(root);
            while (xtr.Read())
            {
                if (xtr.Name == "TargetDescription")
                {
                    pname = xtr.ReadInnerXml();
                    pname = pname.Replace("> </p>", "");
                    pname = pname.Replace("</p>", "");
                }

                if (xtr.Name == "TargetDescriptionPropag")
                {
                    prop = xtr.ReadInnerXml();
                    prop = prop.Replace("</p>", "");
                }

                if (xtr.Name == "TargetDescriptionAttribute")
                {
                    attribute = xtr.ReadInnerXml();
                    attribute = attribute.Replace("</p>", "");
                    attribute = attribute.Replace("</span>", "");
                }

                if (xtr.Name == "TargetDescriptionLaboratory")
                {
                    labname = xtr.ReadInnerXml();
                    labname = labname.Replace("</p>", "");
                }
            }
            xtr.Close();

            String _string = "";
            for (int i = attribute.Length - 1; i > -1; i--)
            {
                _string += attribute[i];
            }

            var _str = _string.Substring(0, 1);
            _string = _string.Replace(_str, "");

            for (int i = _str.Length - 1; i > -1; i--)
            {
                attribute += _str[i];
            }

            attribute = attribute.Replace(">>", "");

            return attribute;
        }

        public JsonResult getcontent(int? Id)
        {
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int _clienid = ind.ClId;
            int _editionid = ind.EId;
            int _bookid = ind.BId;
            int _countryid = ind.CId;

            Getattributesandcontent Getattributesandcontent = new Models.Getattributesandcontent();
            List<Getattributesandcontent> LPC = new List<Getattributesandcontent>();

            var pc = (from _pc in db.ProductContents
                      where _pc.ClientId == _clienid
                      && _pc.EditionId == _editionid
                      && _pc.ProductId == Id
                      select _pc).ToList();

            if (pc.LongCount() > 0)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                foreach (ProductContents _pc in pc)
                {
                    doc.LoadHtml(_pc.HtmlContent);

                    var d = doc.DocumentNode;

                    String content = d.InnerHtml;

                    content = clearname(content);

                    var attr = (from _at in db.Attributes
                                where _at.AttributeId == _pc.AttributeId
                                select _at).ToList();
                    String AttributeName = "";
                    foreach (Attributes _attr in attr)
                    {
                        AttributeName = _attr.Description.Trim();
                    }

                    int ln = AttributeName.Length;

                    if (d.InnerHtml.ToUpper().Contains("<TABLE"))
                    {
                        content = d.InnerHtml;

                        content = clearname(content);
                    }

                    if (d.InnerHtml.ToUpper().Contains("<IMG"))
                    {
                        content = d.InnerHtml;

                        content = clearname(content);
                    }

                    String Value = String.Empty;
                    String tag = "";
                    String tags = "";
                    String cierre = "";
                    int j, i, k;
                    String _string = content;
                    i = _string.IndexOf(_string, 0);
                    if (i >= 0)
                    {
                        j = _string.IndexOf("</span>", i);
                        Value = _string.Substring(i, j - i);
                        cierre = Value;

                        k = cierre.ToUpper().Trim().IndexOf(AttributeName, i);
                        tag = cierre.Substring(i, k - i);
                        tags = tag;

                        if (cierre != string.Empty)
                        {
                            cierre = cierre.Replace(tags, "");
                            string target = tags + cierre;
                            string close = tags + cierre.ToUpper() + ":";

                            close = close.Replace("::", ":");

                            if (content.ToUpper().Trim().StartsWith(close.ToUpper().Trim()))
                            {
                                content = content.Trim().Replace(target, tags);
                            }
                            else
                            {
                                close = close.Replace(":", "");
                                content = content.Trim().Replace(target, tags);
                            }

                        }
                    }

                    Getattributesandcontent = new Getattributesandcontent();

                    Getattributesandcontent.Content = content;
                    Getattributesandcontent.AttributeName = AttributeName.ToUpper().Trim();

                    LPC.Add(Getattributesandcontent);
                }

                LPC = LPC.OrderBy(x => x.AttributeName).ToList();

                return Json(LPC, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public static string clearname(string _string)
        {
            _string = _string.Replace("&uacute;", "ú");
            _string = _string.Replace("&oacute;", "ó");
            _string = _string.Replace("&iacute;", "í");
            _string = _string.Replace("&eacute;", "é");
            _string = _string.Replace("&aacute;", "á");
            _string = _string.Replace("&Uacute;", "Ú");
            _string = _string.Replace("&Oacute;", "Ó");
            _string = _string.Replace("&Iacute;", "Í");
            _string = _string.Replace("&Eacute;", "É");
            _string = _string.Replace("&Aacute;", "Á");
            _string = _string.Replace("&#174;", "®");
            _string = _string.Replace("\r", "");
            _string = _string.Replace("\n", "");
            _string = _string.Replace("\t", "");
            _string = _string.Replace("&#9;", "");
            _string = _string.Replace("&NTILDE;", "Ñ");
            _string = _string.Replace("&ntilde;", "ñ");

            return _string;
        }

        public ActionResult clientsbyedition(string CountryId, string EditionId, string BookId)
        {
            sessionClientsbyEdition ind = (sessionClientsbyEdition)Session["sessionClientsbyEdition"];

            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                byte clienttypeid = 0;
                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "ANUNCIANTE"
                          select clientt).ToList();
                foreach (ClientTypes _ct in ct)
                {
                    clienttypeid = _ct.ClientTypeId;
                }

                var ec = (from _ec in db.EditionClients
                          join c in db.Clients
                          on _ec.ClientId equals c.ClientId
                          where _ec.EditionId == Edition
                          && _ec.ClientTypeId == clienttypeid
                          orderby c.CompanyName ascending
                          select new JoinClientsEditionClients { EditionClients = _ec, Clients = c }).ToList();

                sessionClientsbyEdition sessionClientsbyEdition = new sessionClientsbyEdition(Country, Edition, Book);
                Session["sessionClientsbyEdition"] = sessionClientsbyEdition;

                return View(ec);
            }
            if (ind != null)
            {
                int Country = ind.CId;
                int Edition = ind.EId;
                int Book = ind.BId;

                byte clienttypeid = 0;
                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "ANUNCIANTE"
                          select clientt).ToList();
                foreach (ClientTypes _ct in ct)
                {
                    clienttypeid = _ct.ClientTypeId;
                }

                var ec = (from _ec in db.EditionClients
                          join c in db.Clients
                          on _ec.ClientId equals c.ClientId
                          where _ec.EditionId == Edition
                          && _ec.ClientTypeId == clienttypeid
                          orderby c.CompanyName ascending
                          select new JoinClientsEditionClients { EditionClients = _ec, Clients = c }).ToList();

                return View(ec);
            }
            else
            {
                var ec = (from _ec in db.EditionClients
                          join c in db.Clients
                          on _ec.ClientId equals c.ClientId
                          where _ec.EditionId == 0
                          select new JoinClientsEditionClients { EditionClients = _ec, Clients = c }).ToList();
                return View(ec);
            }
        }

        public JsonResult getnumberofclients(string Values)
        {
            sessiongetnumberofclients sessiongetnumberofclients = new sessiongetnumberofclients(Values);
            Session["sessiongetnumberofclients"] = sessiongetnumberofclients;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchclient(string CompanyName)
        {
            sessionClientsbyEdition ind = (sessionClientsbyEdition)Session["sessionClientsbyEdition"];

            if (string.IsNullOrEmpty(CompanyName))
            {
                return RedirectToAction("clientsbyedition");
            }
            else
            {
                if (CompanyName.Length == 3)
                {
                    int Country = ind.CId;
                    int Edition = ind.EId;
                    int Book = ind.BId;

                    var ec = (from _ec in db.EditionClients
                              join c in db.Clients
                              on _ec.ClientId equals c.ClientId
                              where _ec.EditionId == Edition
                              && c.CompanyName.ToUpper().Trim().StartsWith(CompanyName.ToUpper().Trim())
                              orderby c.CompanyName ascending
                              select new JoinClientsEditionClients { EditionClients = _ec, Clients = c }).ToList();

                    return View("clientsbyedition", ec);
                }

                if (CompanyName.Length >= 4)
                {
                    int Country = ind.CId;
                    int Edition = ind.EId;
                    int Book = ind.BId;

                    var ec = (from _ec in db.EditionClients
                              join c in db.Clients
                              on _ec.ClientId equals c.ClientId
                              where _ec.EditionId == Edition
                              && c.CompanyName.ToUpper().Trim().Contains(CompanyName.ToUpper().Trim())
                              orderby c.CompanyName ascending
                              select new JoinClientsEditionClients { EditionClients = _ec, Clients = c }).ToList();

                    return View("clientsbyedition", ec);
                }
            }
            return View("clientsbyedition");
        }

        public ActionResult UpdatePages(string Client, string Edition, string Page, string ClientType)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (Client != null)
            {
                if (!string.IsNullOrEmpty(Page))
                {
                    int ClientId = int.Parse(Client);
                    int EditionId = int.Parse(Edition);
                    int ClientTypeId = int.Parse(ClientType);

                    var ec = (from _ec in db.EditionClients
                              where _ec.ClientId == ClientId
                              && _ec.EditionId == EditionId
                              && _ec.ClientTypeId == ClientTypeId
                              && _ec.Page != Page.Trim()
                              select _ec).ToList();
                    if (ec.LongCount() > 0)
                    {
                        foreach (EditionClients _ec in ec)
                        {
                            _ec.Page = Page.Trim();
                        }
                        db.SaveChanges();

                        ActivityLog._updatepagesbyclient(ClientId, EditionId, ClientTypeId, Page, ApplicationId, UsersId);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    int ClientId = int.Parse(Client);
                    int EditionId = int.Parse(Edition);
                    int ClientTypeId = int.Parse(ClientType);

                    var ec = (from _ec in db.EditionClients
                              where _ec.ClientId == ClientId
                              && _ec.EditionId == EditionId
                              && _ec.ClientTypeId == ClientTypeId
                              && _ec.Page != Page.Trim()
                              select _ec).ToList();
                    if (ec.LongCount() > 0)
                    {
                        foreach (EditionClients _ec in ec)
                        {
                            _ec.Page = null;
                        }
                        db.SaveChanges();

                        ActivityLog._updatepagesbyclient(ClientId, EditionId, ClientTypeId, Page, ApplicationId, UsersId);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return View();
        }

        public JsonResult checkcontent(string Product, string Client, string Edition)
        {
            if (Product != null)
            {
                int EditionId = int.Parse(Edition);
                int ProductId = int.Parse(Product);
                int ClientId = int.Parse(Client);

                var pp = (from _pp in db.ParticipantProducts
                          where _pp.ProductId == ProductId
                          && _pp.EditionId == EditionId
                          && _pp.ClientId == ClientId
                          && _pp.HTMLContent != null
                          && _pp.XMLContent != null
                          select _pp).ToList();
                if (pp.LongCount() > 0)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getxmlprueba(string id)
        {
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int ProductId = int.Parse(id);
            int ClientId = ind.ClId;
            int EditionId = ind.EId;

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ClientId == ClientId
                      && _pp.EditionId == EditionId
                      && _pp.ProductId == ProductId
                      select _pp).ToList();

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    string JsonXML = _pp.XMLContent;

                    return Json(JsonXML, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult gethtmlview(string id)
        {
            sessionproduction ind = (sessionproduction)Session["sessionproduction"];
            int ProductId = int.Parse(id);
            int ClientId = ind.ClId;
            int EditionId = ind.EId;

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ClientId == ClientId
                      && _pp.EditionId == EditionId
                      && _pp.ProductId == ProductId
                      select _pp).ToList();

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    string JsonXML = _pp.HTMLContent;

                    return Json(JsonXML, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult clientbranch(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionclientbranchProd ind = (sessionclientbranchProd)Session["sessionclientbranchProd"];
            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Client = int.Parse(ClientId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                sessionclientbranchProd sessionclientbranchProd = new sessionclientbranchProd(Country, Client, Edition, Book);
                Session["sessionclientbranchProd"] = sessionclientbranchProd;

                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "SUCURSAL"
                          select clientt).ToList();
                if (ct.LongCount() > 0)
                {
                    foreach (ClientTypes _ct in ct)
                    {
                        var cl = (from suc in db.Clients
                                  where suc.ClientIdParent == Client
                                  && suc.Active == true
                                  orderby suc.CompanyName ascending
                                  select suc).ToList();

                        return View(cl);
                    }
                }
            }
            if (ind != null)
            {
                int Country = ind.CId;
                int Client = ind.ClId;
                int Edition = ind.EId;
                int Book = ind.BId;

                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "SUCURSAL"
                          select clientt).ToList();
                if (ct.LongCount() > 0)
                {
                    foreach (ClientTypes _ct in ct)
                    {
                        var cl = (from suc in db.Clients
                                  where suc.ClientIdParent == Client
                                  && suc.Active == true
                                  orderby suc.CompanyName ascending
                                  select suc).ToList();

                        return View(cl);
                    }
                }
            }
            else
            {
                var cl = (from suc in db.Clients
                          where suc.ClientIdParent == 0
                          select suc).ToList();

                return View(cl);
            }

            return View();
        }

        public ActionResult informationbyclient(int? Id)
        {
            if (Id != null)
            {
                var ad = (from ca in db.ClientAddresses
                          join c in db.Clients
                              on ca.ClientId equals c.ClientId
                          join a in db.Addresses
                              on ca.AddressId equals a.AddressId
                          join s in db.States
                            on a.StateId equals s.StateId
                          where ca.ClientId == Id
                          select new JoinClientAddressesPhones { ClientAddresses = ca, Addresses = a, Clients = c, States = s }).OrderBy(x => x.Addresses.City).ToList();

                var cl = (from c in db.Clients
                          where c.ClientId == Id
                          && c.ClientIdParent == null
                          select c).ToList();
                if (cl.LongCount() > 0)
                {
                    foreach (Clients _c in cl)
                    {
                        ViewData["ClientIdSP"] = _c.ClientId;
                        ViewData["CompanyNameSP"] = "---";
                    }
                }
                else
                {
                    var cls = (from c in db.Clients
                               where c.ClientId == Id
                               select c).ToList();
                    foreach (Clients _c in cls)
                    {
                        ViewData["ClientIdSP"] = _c.ClientId;
                        ViewData["CompanyNameSP"] = _c.CompanyName;
                    }
                }

                return View(ad);
            }
            return View();
        }

        public ActionResult editclient(string CompanyName, string ShortName, string Country, string Client)
        {

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ClientId = int.Parse(Client);
            int CountryId = int.Parse(Country);

            var c = (from cl in db.Clients
                     where cl.ClientId == ClientId
                     && cl.CountryId == CountryId
                     select cl).ToList();

            if (c.LongCount() > 0)
            {
                foreach (Clients _c in c)
                {
                    _c.CompanyName = CompanyName.Trim();
                    _c.ShortName = ShortName.Trim();

                    db.SaveChanges();

                    byte AlphabetId = Convert.ToByte(_c.AlphabetId);

                    ActivityLog._editclient(ClientId, CountryId, CompanyName, ShortName, ApplicationId, UsersId, AlphabetId);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult editinformation(string Address, string Suburb, string City, string ZipC, string Email, string Web, string Country, string AddressId, string State)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int CountryId = int.Parse(Country);
            int AddressIdd = int.Parse(AddressId);
            int StateId = int.Parse(State);

            var addres = (from ad in db.Addresses
                          where ad.AddressId == AddressIdd
                          select ad).ToList();

            if (addres.LongCount() > 0)
            {
                foreach (Addresses _addres in addres)
                {
                    _addres.Address = Address.Trim();
                    _addres.Suburb = Suburb.Trim();
                    _addres.City = City.Trim();
                    _addres.Email = Email.Trim();
                    _addres.Web = Web.Trim();
                    _addres.ZipCode = ZipC.Trim();
                    _addres.StateId = StateId;

                    db.SaveChanges();

                    ActivityLog._editinformation(AddressIdd, Address, Suburb, City, Email, Web, ZipC, CountryId, ApplicationId, UsersId);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult clientphones(int? AddressId, int? ClientId)
        {
            if (AddressId != null)
            {
                var cp = (from _cp in db.ClientPhones
                          join pt in db.PhoneTypes
                          on _cp.PhoneTypeId equals pt.PhoneTypeId
                          where _cp.ClientId == ClientId
                          && _cp.AddressId == AddressId
                          orderby _cp.Number ascending
                          select new JoinPhoneTypes { PhoneTypes = pt, ClientPhones = _cp }).ToList();

                var c = (from cl in db.Clients
                         where cl.ClientId == ClientId
                         && cl.ClientIdParent == null
                         select cl).ToList();
                if (c.LongCount() > 0)
                {
                    foreach (Clients _c in c)
                    {
                        ViewData["CompanyNamePP"] = "---";
                        ViewData["ClientIdPP"] = _c.ClientId;
                    }
                }
                else
                {
                    var _cls = (from cls in db.Clients
                                where cls.ClientId == ClientId
                                select cls).ToList();
                    foreach (Clients _c in _cls)
                    {
                        ViewData["ClientIdPP"] = _c.ClientId;
                        ViewData["CompanyNamePP"] = _c.CompanyName;
                    }
                }


                var addr = (from a in db.Addresses
                            where a.AddressId == AddressId
                            select a).ToList();
                foreach (Addresses _addr in addr)
                {
                    ViewData["AddressIdPP"] = _addr.AddressId;
                }

                return View(cp);
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult editphones(string Number, string Lada, string ClientPhone, string Country, string Client, string Address, string PhoneType)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ClientPhoneId = int.Parse(ClientPhone);
            int CountryId = int.Parse(Country);
            int ClientId = int.Parse(Client);
            int AddressId = int.Parse(Address);
            byte PhoneTypeId = Convert.ToByte(PhoneType);

            var ca = (from _ca in db.ClientAddresses
                      where _ca.ClientId == ClientId
                      && _ca.AddressId == AddressId
                      select _ca).ToList();

            if (ca.LongCount() > 0)
            {
                var cp = (from _cp in db.ClientPhones
                          where _cp.ClientPhoneId == ClientPhoneId
                          && _cp.ClientId == ClientId
                          && _cp.AddressId == AddressId
                          && _cp.PhoneTypeId == PhoneTypeId
                          select _cp).ToList();

                if (cp.LongCount() > 0)
                {
                    foreach (ClientPhones _cp in cp)
                    {
                        _cp.Number = Number.Trim();
                        if ((Lada == "") || (Lada == string.Empty))
                        {
                            _cp.Lada = null;
                        }
                        else
                        {
                            _cp.Lada = Lada.Trim();
                        }
                        _cp.Active = true;
                    }
                    try
                    {
                        db.SaveChanges();

                        ActivityLog._editphones(ClientPhoneId, ClientId, AddressId, PhoneTypeId, Number, Lada, ApplicationId, UsersId);
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public JsonResult editproducts(string ProductName, string Product)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            byte alphabetid = 0;
            var products = ProductName.Substring(0, 1);

            var alph = (from leters in db.Alphabet
                        where leters.Letter == products
                        select leters).ToList();
            foreach (Alphabet al in alph)
            {
                alphabetid = al.AlphabetId;
            }

            byte typeid = 0;

            var pt = (from ptypes in db.ProductTypes
                      where ptypes.Description == "Productos"
                      select ptypes).ToList();
            foreach (ProductTypes _pt in pt)
            {
                typeid = _pt.TypeId;
            }

            int ProductId = int.Parse(Product);

            var prod = db.Products.Where(x => x.ProductName.Trim() == ProductName.Trim()).ToList();
            if (prod.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var prods = db.Products.Where(x => x.ProductId == ProductId).ToList();

                if (prods.LongCount() > 0)
                {
                    foreach (Products _p in prods)
                    {
                        _p.ProductName = ProductName.Trim();

                        db.SaveChanges();
                    }
                }

                ActivityLog.editproduct(ProductName, ProductId, ApplicationId, UsersId, alphabetid, typeid);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}