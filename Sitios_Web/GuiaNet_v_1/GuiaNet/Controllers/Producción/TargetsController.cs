using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;
using System.IO;
using System.Xml;

namespace GuiaNet.Controllers.Producción
{
    public class TargetsController : Controller
    {

        PLMUsers PLMUsers = new PLMUsers();

        public ActionResult Index()
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];

            if (p != null)
            {
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                String pname = "";
                String prop = "";
                String attribute = "";
                String labname = "";
                String paragraph = "";
                String Image = "";
                String Table = "";
                String Row = "";
                String Column = "";
                String Register = "";
                String Bullets = "";
                String Bullets1 = "";
                String Bullets2 = "";
                String Bullets3 = "";
                String Logo = "";
                String Bold = "";
                String Reference = "";
                String Data = "";
                String Container = "";
                String Body = "";
                String SupInd = "";

                String File = "";

                var ctr = (from c in PLMUsers.Users
                           join count in PLMUsers.CountriesUser
                               on c.CountryId equals count.CountryId
                           where c.UserId == UsersId
                           select count).ToList();
                if (ctr.LongCount() > 0)
                {
                    foreach(CountriesUser _ctr in ctr)
                    {
                        File = "targetsallfile_" + _ctr.ID +".xml";
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
                            pname = xtr.ReadInnerXml();
                            pname = pname.Replace("> </p>", "");
                            pname = pname.Replace("</p>", "");

                            ViewData["pnametargets"] = pname;
                        }

                        if (xtr.Name == "TargetDescriptionPropag")
                        {
                            prop = xtr.ReadInnerXml();
                            prop = prop.Replace("</p>", "");

                            ViewData["Bproptargets"] = prop;
                        }

                        if (xtr.Name == "TargetDescriptionAttribute")
                        {
                            attribute = xtr.ReadInnerXml();
                            attribute = attribute.Replace("</p>", "");
                            attribute = attribute.Replace("</span>", "");

                            ViewData["attrtargets"] = attribute;
                        }

                        if (xtr.Name == "TargetDescriptionLaboratory")
                        {
                            labname = xtr.ReadInnerXml();
                            labname = labname.Replace("</p>", "");

                            ViewData["labnametargets"] = labname;
                        }

                        if (xtr.Name == "TargetDescriptionParagraph")
                        {
                            paragraph = xtr.ReadInnerXml();
                            paragraph = paragraph.Replace("</span>", "");
                            paragraph = paragraph.Replace("</p>", "");

                            ViewData["paragrapgtargets"] = paragraph;
                        }

                        if (xtr.Name == "TargetDescriptionImage")
                        {
                            Image = xtr.ReadInnerXml();
                            Image = Image.Replace("</p>", "");

                            ViewData["Imagetargets"] = Image;
                        }

                        if (xtr.Name == "TargetDescriptionTable")
                        {
                            Table = xtr.ReadInnerXml();
                            Table = Table.Replace("</table>", "");

                            ViewData["Tabletargets"] = Table;
                        }

                        if (xtr.Name == "TargetDescriptionRow")
                        {
                            Row = xtr.ReadInnerXml();

                            Row = Row.Replace("</tr>", "");

                            ViewData["Rowtargets"] = Row;
                        }

                        if (xtr.Name == "TargetDescriptionColumn")
                        {
                            Column = xtr.ReadInnerXml();

                            Column = Column.Replace("</td>", "");

                            ViewData["Columntargets"] = Column;
                        }

                        if (xtr.Name == "TargetDescriptionRegister")
                        {
                            Register = xtr.ReadInnerXml();

                            Register = Register.Replace("</p>", "");

                            ViewData["Registertargets"] = Register;
                        }

                        if (xtr.Name == "TargetDescriptionBullet")
                        {
                            Bullets = xtr.ReadInnerXml();
                            Bullets = Bullets.Replace("</p>", "");

                            ViewData["Bulletstargets"] = Bullets;
                        }

                        if (xtr.Name == "TargetDescriptionBullet1")
                        {
                            Bullets1 = xtr.ReadInnerXml();
                            Bullets1 = Bullets1.Replace("</p>", "");

                            ViewData["Bullets1targets"] = Bullets1;
                        }

                        if (xtr.Name == "TargetDescriptionBullet2")
                        {
                            Bullets2 = xtr.ReadInnerXml();
                            Bullets2 = Bullets2.Replace("</p>", "");

                            ViewData["Bullets2targets"] = Bullets2;
                        }

                        if (xtr.Name == "TargetDescriptionBullet3")
                        {
                            Bullets3 = xtr.ReadInnerXml();
                            Bullets3 = Bullets3.Replace("</p>", "");

                            ViewData["Bullets3targets"] = Bullets3;
                        }

                        if (xtr.Name == "TargetDescriptionLog")
                        {
                            Logo = xtr.ReadInnerXml();
                            Logo = Logo.Replace("</span>", "");

                            ViewData["Logotargets"] = Logo;
                        }

                        if (xtr.Name == "TargetDescriptionBold")
                        {
                            Bold = xtr.ReadInnerXml();
                            Bold = Bold.Replace("</span></p>", "");
                            Bold = Bold.Replace("</p>", "");

                            ViewData["Boldtargets"] = Bold;
                        }

                        if (xtr.Name == "TargetDescriptionReference")
                        {
                            Reference = xtr.ReadInnerXml();
                            Reference = Reference.Replace("</p>", "");

                            ViewData["Referencetargets"] = Reference;

                        }

                        if (xtr.Name == "TargetDescriptionSuperindice")
                        {
                            SupInd = xtr.ReadInnerXml();

                            SupInd = SupInd.Replace("</span>", "");

                            ViewData["SupIndtargets"] = SupInd;
                        }

                        if (xtr.Name == "TargetDescriptionData")
                        {
                            Data = xtr.ReadInnerXml();
                            Data = Data.Replace("</p>", "");

                            ViewData["Datatargets"] = Data;
                        }

                        if (xtr.Name == "TargetDescriptionContainer")
                        {
                            Container = xtr.ReadInnerXml();
                            Container = Container.Replace("</div>", "");

                            ViewData["Containertargets"] = Container;
                        }

                        if (xtr.Name == "TargetDescriptionBody")
                        {
                            Body = xtr.ReadInnerXml();
                            Body = Body.Replace("</body>", "");

                            ViewData["Bodytargets"] = Body;
                        }
                    }
                    xtr.Close();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult gettargets(string pname, string propaganda, string attribute, string laboratory, string paragraph, string image, string table,
            string row, string column, string register, string bullet, string bullet1, string bullet2, string bullet3, string log, string bold, string reference,
            string superindice, string _data, string container, string body)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            
            if(p != null)
            {
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                pname = replacetargets(pname);
                propaganda = replacetargets(propaganda);
                attribute = replacetargets(attribute);
                laboratory = replacetargets(laboratory);
                paragraph = replacetargets(paragraph);
                image = replacetargets(image);
                table = replacetargets(table);
                row = replacetargets(row);
                column = replacetargets(column);
                register = replacetargets(register);
                bullet = replacetargets(bullet);
                bullet1 = replacetargets(bullet1);
                bullet2 = replacetargets(bullet2);
                bullet3 = replacetargets(bullet3);
                log = replacetargets(log);
                bold = replacetargets(bold);
                reference = replacetargets(reference);
                superindice = replacetargets(superindice);
                _data = replacetargets(_data);
                container = replacetargets(container);
                body = replacetargets(body);

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

                    var rr = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

                    String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";

                    SW = System.IO.File.CreateText(rr);
                    SW.Write(xml);
                    SW.Write(rootbeg);

                    if ((pname != null) && (pname != String.Empty))
                    {
                        SW.Write("<TargetName>");
                        SW.Write("Producto");
                        SW.Write("</TargetName>");

                        SW.Write("<TargetDescription>");
                        SW.Write(pname + "</p>");
                        SW.Write("</TargetDescription>");
                    }

                    if ((propaganda != null) && (propaganda != String.Empty))
                    {
                        SW.Write("<TargetPropag>");
                        SW.Write("Base de Propaganda");
                        SW.Write("</TargetPropag>");

                        SW.Write("<TargetDescriptionPropag>");
                        SW.Write(propaganda + "</p>");
                        SW.Write("</TargetDescriptionPropag>");
                    }

                    if ((attribute != null) && (attribute != string.Empty))
                    {
                        SW.Write("<TargetAttr>");
                        SW.Write("Atributo");
                        SW.Write("</TargetAttr>");

                        SW.Write("<TargetDescriptionAttribute>");
                        SW.Write(attribute + "</span></p>");
                        SW.Write("</TargetDescriptionAttribute>");
                    }

                    if ((laboratory != null) && (laboratory != String.Empty))
                    {
                        SW.Write("<TargetLab>");
                        SW.Write("Laboratorio");
                        SW.Write("</TargetLab>");

                        SW.Write("<TargetDescriptionLaboratory>");
                        SW.Write(laboratory + "</p>");
                        SW.Write("</TargetDescriptionLaboratory>");
                    }

                    if ((paragraph != null) && (paragraph != String.Empty))
                    {
                        SW.Write("<TargetParagraph>");
                        SW.Write("Parrafo");
                        SW.Write("</TargetParagraph>");

                        SW.Write("<TargetDescriptionParagraph>");
                        SW.Write(paragraph + "</p>");
                        SW.Write("</TargetDescriptionParagraph>");
                    }

                    if ((image != null) && (image != String.Empty))
                    {
                        SW.Write("<TargetImage>");
                        SW.Write("Imagen");
                        SW.Write("</TargetImage>");

                        SW.Write("<TargetDescriptionImage>");
                        SW.Write(image + "</p>");
                        SW.Write("</TargetDescriptionImage>");
                    }

                    if ((table != null) && (table != String.Empty))
                    {
                        SW.Write("<TargetTable>");
                        SW.Write("Tabla");
                        SW.Write("</TargetTable>");

                        SW.Write("<TargetDescriptionTable>");
                        SW.Write(table + "</table>");
                        SW.Write("</TargetDescriptionTable>");
                    }

                    if ((row != null) && (row != String.Empty))
                    {
                        SW.Write("<TargetRow>");
                        SW.Write("Fila");
                        SW.Write("</TargetRow>");

                        SW.Write("<TargetDescriptionRow>");
                        SW.Write(row + "</tr>");
                        SW.Write("</TargetDescriptionRow>");
                    }

                    if ((column != null) && (column != String.Empty))
                    {
                        SW.Write("<TargetColumn>");
                        SW.Write("Columna");
                        SW.Write("</TargetColumn>");

                        SW.Write("<TargetDescriptionColumn>");
                        SW.Write(column + "</td>");
                        SW.Write("</TargetDescriptionColumn>");
                    }

                    if ((register != null) && (register != String.Empty))
                    {
                        SW.Write("<TargetRegister>");
                        SW.Write("Registro");
                        SW.Write("</TargetRegister>");

                        SW.Write("<TargetDescriptionRegister>");
                        SW.Write(register + "</p>");
                        SW.Write("</TargetDescriptionRegister>");
                    }

                    if ((bullet != null) && (bullet != String.Empty))
                    {
                        SW.Write("<TargetBullet>");
                        SW.Write("Viñeta");
                        SW.Write("</TargetBullet>");

                        SW.Write("<TargetDescriptionBullet>");
                        SW.Write(bullet + "</p>");
                        SW.Write("</TargetDescriptionBullet>");
                    }

                    if ((bullet1 != null) && (bullet1 != String.Empty))
                    {
                        SW.Write("<TargetBullet1>");
                        SW.Write("Viñeta 1");
                        SW.Write("</TargetBullet1>");

                        SW.Write("<TargetDescriptionBullet1>");
                        SW.Write(bullet1 + "</p>");
                        SW.Write("</TargetDescriptionBullet1>");
                    }

                    if ((bullet2 != null) && (bullet2 != String.Empty))
                    {
                        SW.Write("<TargetBullet2>");
                        SW.Write("Viñeta 2");
                        SW.Write("</TargetBullet2>");

                        SW.Write("<TargetDescriptionBullet2>");
                        SW.Write(bullet2 + "</p>");
                        SW.Write("</TargetDescriptionBullet2>");
                    }

                    if ((bullet3 != null) && (bullet3 != String.Empty))
                    {
                        SW.Write("<TargetBullet3>");
                        SW.Write("Viñeta 3");
                        SW.Write("</TargetBullet3>");

                        SW.Write("<TargetDescriptionBullet3>");
                        SW.Write(bullet3 + "</p>");
                        SW.Write("</TargetDescriptionBullet3>");
                    }

                    if ((log != null) && (log != String.Empty))
                    {
                        SW.Write("<TargetLog>");
                        SW.Write("Logo");
                        SW.Write("</TargetLog>");

                        SW.Write("<TargetDescriptionLog>");
                        SW.Write(log + "</span>");
                        SW.Write("</TargetDescriptionLog>");
                    }

                    if ((bold != null) && (bold != String.Empty))
                    {
                        SW.Write("<TargetBold>");
                        SW.Write("Negritas");
                        SW.Write("</TargetBold>");

                        SW.Write("<TargetDescriptionBold>");
                        SW.Write(bold + "</span></p>");
                        SW.Write("</TargetDescriptionBold>");
                    }

                    if ((reference != null) && (reference != String.Empty))
                    {
                        SW.Write("<TargetReference>");
                        SW.Write("Referencia");
                        SW.Write("</TargetReference>");

                        SW.Write("<TargetDescriptionReference>");
                        SW.Write(reference + "</p>");
                        SW.Write("</TargetDescriptionReference>");
                    }

                    if ((superindice != null) && (superindice != String.Empty))
                    {
                        SW.Write("<TargetSuperindice>");
                        SW.Write("Superindice");
                        SW.Write("</TargetSuperindice>");

                        SW.Write("<TargetDescriptionSuperindice>");
                        SW.Write(superindice + "</span>");
                        SW.Write("</TargetDescriptionSuperindice>");
                    }

                    if ((_data != null) && (_data != String.Empty))
                    {
                        SW.Write("<TargetData>");
                        SW.Write("Datos");
                        SW.Write("</TargetData>");

                        SW.Write("<TargetDescriptionData>");
                        SW.Write(_data + "</p>");
                        SW.Write("</TargetDescriptionData>");
                    }

                    if ((container != null) && (container != String.Empty))
                    {
                        SW.Write("<TargetContainer>");
                        SW.Write("Contenedor");
                        SW.Write("</TargetContainer>");

                        SW.Write("<TargetDescriptionContainer>");
                        SW.Write(container + "</div>");
                        SW.Write("</TargetDescriptionContainer>");
                    }

                    if ((body != null) && (body != String.Empty))
                    {
                        SW.Write("<TargetBody>");
                        SW.Write("Contenedor");
                        SW.Write("</TargetBody>");

                        SW.Write("<TargetDescriptionBody>");
                        SW.Write(body + "</body>");
                        SW.Write("</TargetDescriptionBody>");
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
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public static string replacetargets(string _string)
        {
            _string = _string.Replace("60", "<");
            _string = _string.Replace("62", ">");

            return _string;
        }
    }
}