using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Guianet.Models
{
    public class SegmentContent
    {
        GuiaEntities db = new GuiaEntities();
        PLMUsers PLMUsers = new PLMUsers();
        ParticipantProducts ParticipantProducts = new ParticipantProducts();

        public bool inserthtml(int _clienid, int _editionid, int _productid, String content)
        {
            var ppfile = (from part in db.ParticipantProducts
                          where part.ClientId == _clienid
                          && part.EditionId == _editionid
                          && part.ProductId == _productid
                          select part).ToList();

            if (ppfile.LongCount() > 0)
            {
                foreach (ParticipantProducts pp in ppfile)
                {
                    ParticipantProducts = new ParticipantProducts();

                    pp.HTMLContent = content;

                    db.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public string getxml(String concatfiledir, String filename, String desc, string productnme, string prop, string attr, string labname)
        {
            String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";
            String rootbeg = "<Root>";
            String rootend = "</Root>";

            String productname = "";
            String propag = "";
            String laboratory = "";
            String attribute = "";

            try
            {
                productname = getProduct(concatfiledir, productnme);

                productname = cleanfile(productname);
            }
            catch (Exception e)
            {
                return "errorProductName";
            }

            try
            {
                propag = getPropaganda(concatfiledir, prop);

                propag = cleanfile(propag);
            }
            catch (Exception e)
            {
                return "errorPropag";
            }

            try
            {
                laboratory = getLaboratory(concatfiledir, labname);

                laboratory = cleanfile(laboratory);
            }
            catch (Exception e)
            {
                return "errorLaboratory";
            }

            try
            {
                attribute = getAttribute(concatfiledir, attr);

                attribute = cleanfile(attribute);
            }
            catch (Exception e)
            {
                return "errorAttributes";
            }


            if ((string.IsNullOrEmpty(productname)) && (string.IsNullOrEmpty(propag)) && (string.IsNullOrEmpty(laboratory)) && (string.IsNullOrEmpty(attribute)))
            {
                return "Error_File";
            }

            StreamWriter SW;
            productname = productname.Trim();
            productname = productname.Replace("+", "");
            productname = productname.Replace("*", "");

            filename = filename.Replace(".htm", "");

            SW = File.CreateText(desc + @"\" + filename + ".xml");

            SW.Write(xml);
            SW.Write(rootbeg);
            SW.Write("<Producto>");

            if (productname != "")
            {
                SW.Write(productname);
            }

            if (propag != "")
            {
                SW.Write(propag);
            }
            if (attribute != "")
            {
                SW.Write(attribute);
            }
            if (laboratory != "")
            {
                SW.Write(laboratory);
            }

            SW.Write("</Producto>");
            SW.Write(rootend);
            SW.Close();
            SW.Dispose();
            return "Ok";
            #region ReturnXML
        }

        public static string cleanmail(String concatfiledir)
        {
            String productn = String.Empty;
            String ContentL = String.Empty;
            StreamReader SR;
            String content;
            String tag = "<a href";
            String endtag = ">";
            int beg, end;

            SR = File.OpenText(concatfiledir);
            content = SR.ReadToEnd();

            try
            {
                while (content != null)
                {
                    beg = content.IndexOf(tag);

                    if (beg >= 0)
                    {
                        end = content.IndexOf(">", beg);

                        productn = content.Substring(beg, (end + endtag.Length) - beg);

                        content = content.Replace(productn, "");
                        content = content.Replace("</a>", "");
                    }
                    else
                    {
                        ContentL = content;
                        content = null;
                    }
                }

                SR.Close();
                SR.Dispose();
            }
            catch (Exception e)
            {
                string msg = e.Message;
                SR.Close();
                SR.Dispose();
            }



            return ContentL;
        }

        public static String getProduct(String concatfiledir, string productnme)
        {
            String productname = String.Empty;
            String productn = String.Empty;
            String imagename = String.Empty;
            String image = String.Empty;
            String endimage = " />";
            String tag = productnme;
            StreamReader SR;
            String content;
            int beg, end, tagend, begimg, endimg;

            SR = File.OpenText(concatfiledir);
            content = SR.ReadToEnd();

            //content = cleanmail(concatfiledir);

            beg = content.IndexOf(tag);
            try
            {
                if (beg >= 0)
                {
                    end = content.IndexOf("</p>", beg);
                    tagend = content.IndexOf(">", beg);
                    productn = content.Substring(tagend + 1, end - (tagend + 1));
                    productname = productn;

                    if (productname != null)
                    {
                        begimg = productname.IndexOf("<img");

                        if (begimg >= 0)
                        {
                            endimg = productname.IndexOf("/>", begimg);
                            imagename = productname.Substring(begimg, (endimg + endimage.Length) - begimg);
                            productname = productname.Replace(imagename, "");
                            productname = productname.Replace("<span class=\"cr_logo_derecha\">/span>", "");
                            image += imagename;
                            image = image.Replace("/><", "/>");
                        }
                    }
                }
                else
                {
                    content = null;
                }



                if (productname != "")
                {
                    productname = "<Nombre>" + productname + "</Nombre>";

                    if (image != null)
                    {
                        productname += image;
                    }
                    productname = cleanfile(productname);
                }

                SR.Close();
                SR.Dispose();
            }
            catch (Exception e)
            {
                string msg = e.Message;

                SR.Close();
                SR.Dispose();
            }



            return productname;
        }

        public static String getPropaganda(String concatfiledir, string prop)
        {

            String propag = "";
            String basep = "";
            String imagename = String.Empty;
            String image = String.Empty;
            String endimage = " />";
            //String tag = "<p class=\"pf_base_propaganda\">";
            String tag = prop;

            StreamReader SR;
            String content;
            int beg, end, tagend, begimg, endimg;

            SR = File.OpenText(concatfiledir);
            content = SR.ReadToEnd();

            //content = cleanmail(concatfiledir);

            beg = content.IndexOf(tag);

            try
            {
                if (beg >= 0)
                {
                    end = content.IndexOf("</p>", beg + tag.Length);
                    propag = content.Substring(beg + tag.Length, end - (beg + tag.Length));
                    basep = propag;
                    basep = basep.Replace("<br />", "");

                    //if (propag != null)
                    basep = basep.Replace("<span class=\"cr_superindice\">", "");
                    basep = basep.Replace("</span>", "");
                    //{
                    //    begimg = propag.IndexOf("<img");
                    //    if (begimg >= 0)
                    //    {
                    //        endimg = propag.IndexOf(" />", begimg);
                    //        imagename = propag.Substring(begimg, (endimg + endimage.Length) - begimg);
                    //        propag = propag.Replace(imagename, "");
                    //        propag = propag.Replace("<br/>", "");

                    //        image += imagename;
                    //    }
                    //    else
                    //    {
                    //        propag = null;
                    //    }
                    //}
                }

                if (basep != "")
                {
                    basep = "<BasePropaganda>" + basep + "</BasePropaganda>";
                    if (image != null)
                    {
                        basep += image;

                        basep = cleanfile(basep);
                    }
                }

                SR.Close();
                SR.Dispose();
            }
            catch (Exception e)
            {
                string msg = e.Message;

                SR.Close();
                SR.Dispose();
            }



            return basep;
        }

        public static String getLaboratory(String concatfiledir, string labnam)
        {
            String laboratory = String.Empty;
            String labname = String.Empty;
            String imagename = String.Empty;
            String image = String.Empty;
            String endimage = " />";
            //String tag = "<p class=\"pf_pie_empresa";
            String tag = labnam;
            StreamReader SR;
            String content;
            int beg, end, tagend, begimg, endimg;

            SR = File.OpenText(concatfiledir);
            content = SR.ReadToEnd();

            //content = cleanmail(concatfiledir);

            beg = content.IndexOf(tag);

            try
            {
                if (beg >= 0)
                {
                    end = content.IndexOf("</p>", beg);
                    tagend = content.IndexOf(">", beg);
                    laboratory = content.Substring(tagend + 1, end - (tagend + 1));
                    labname = laboratory;

                    if (labname != null)
                    {
                        begimg = labname.IndexOf("<img");
                        if (begimg >= 0)
                        {
                            endimg = labname.IndexOf(" />", begimg);
                            image = labname.Substring(begimg, (endimg + endimage.Length) - begimg);
                            labname = labname.Replace(image, "");
                            image += imagename;
                        }
                    }
                }

                if (labname != "")
                {
                    labname = "<Laboratorio>" + labname + "</Laboratorio>";

                    if (image != null)
                    {
                        labname += image;

                        labname = cleanfile(labname);
                    }
                }

                SR.Close();
                SR.Dispose();
            }
            catch (Exception e)
            {
                string msg = e.Message;

                SR.Close();
                SR.Dispose();
            }


            return labname;
        }

        public static String getAttribute(String concatfiledir, string attrib)
        {
            String attribute = String.Empty;
            String attr = String.Empty;
            //String tag = "<p class=\"pf_normal\"><span class=\"cr_rubro\"";
            String tag = attrib;
            String endtag = "</span>";
            String attrname = "";
            String attrcontent = null;
            String Info = "";
            StreamReader SR;
            String content;
            String tagfooter = "<p class=\"pie";
            int beg = 0, end = 0, tagend = 0, infaux = 0, footer = 0; ;

            SR = File.OpenText(concatfiledir);
            content = SR.ReadToEnd();
            content = content.Replace("\t", "");

            content = cleanmail(concatfiledir);

            String _string = "";
            for (int i = tag.Length - 1; i > -1; i--)
            {
                _string += tag[i];
            }

            var _str = _string.Substring(0, 1);
            _string = _string.Replace(_str, "");

            for (int i = _str.Length - 1; i > -1; i--)
            {
                tag += _str[i];
            }

            tag = tag.Replace(">>", "");
            //if (beg >= 0)
            //{
            try
            {
                while (content != null)
                {

                    beg = content.IndexOf(tag);

                    end = content.IndexOf(endtag, beg);
                    tagend = content.IndexOf(">", beg + tag.Length);
                    Info = content.Substring(tagend + 1, end - (tagend + 1));
                    attrname = Info;

                    infaux = content.IndexOf(tag, end);

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
                    if (attrname != null)
                    {
                        attribute += "<Rubro>";
                        attribute += "<Titulo>" + attrname + "</Titulo>";
                        attribute += "<Contenido>" + attrcontent + "</Contenido></Rubro>";
                    }
                }
                // }
                if (attribute != "")
                {
                    attribute = getTableByAttribute(attribute);
                    attribute = cleanfile(attribute);
                }

                SR.Close();
                SR.Dispose();
            }
            catch (Exception e)
            {
                string msg = e.Message;

                SR.Close();
                SR.Dispose();
            }

            return attribute;
        }

        public static String getTableByAttribute(String rubro)
        {
            String tablaFin = "</table>";
            String tabla = "";
            int tableIni;
            int tableFin;
            String tablaXML = "";
            String contenidoColumna = rubro;
            int i = 0;

            while (rubro != "")
            {
                tableIni = rubro.IndexOf("<table");

                if (tableIni >= 0)
                {
                    tableFin = rubro.IndexOf("</table>", tableIni);
                    tabla = rubro.Substring(tableIni, (tableFin - tableIni) + tablaFin.Length);
                    tablaXML = createTableXML(tabla);


                    rubro = rubro.Replace(tabla, tablaXML);
                    contenidoColumna = rubro;
                }
                else rubro = "";
                i++;
            }

            return contenidoColumna;

        }

        public static String createTableXML(String S)
        {

            getRows(S);
            int colmax = 0;

            for (int i = 0; i <= numrenglones; i++)
            {
                if (i == 0)
                    colmax = rows[i];

                if (rows[i] > colmax)
                    colmax = rows[i];
            }

            tabla = "<Tabla xmlns:aid=" + "\"http://ns.adobe.com/AdobeInDesign/4.0/" + "\" aid:table=" + "\"table" + "\" aid:trows=" + "\"" + numrenglones + "\" aid:tcols=" + "\"" + colmax.ToString() + "\">";
            int celdas = numcolumnas * numrenglones;
            for (int i = 0; i < j; i++)
            {
                tabla += "<Celda aid:table=" + "\"cell" + "\" aid:crows=" + "\"" + rowS[i] + "" + "\" aid:ccols=" + "\"" + colS[i] + "" + "\">" + contenidoColumna[i] + "</Celda>";
            }
            j = 0;
            tabla += "</Tabla>";
            rowS = new String[8000];
            colS = new String[8000];
            contenidoColumna = new String[8000];
            rows = new int[800];

            return tabla;


        }

        public static void getRows(String S)
        {

            int i = 0;
            int renglonIni, renglonFin;
            String renglon = "";
            int columnas = 0;

            while (S != null)
            {
                renglonIni = S.IndexOf("<tr");

                if (renglonIni > 0)
                {
                    renglonFin = S.IndexOf("</tr>", renglonIni);
                    renglon = S.Substring(renglonIni, renglonFin - renglonIni + 5);
                    columnas = getColumns(renglon);
                    rows[i] = columnas;
                    S = S.Substring(renglonFin + 5, S.Length - (renglonFin + 5));
                    i++;
                }
                else S = null;
            }

            numrenglones = i;
        }

        public static int getColumns(String renglon)
        {

            int i = 0;
            int columnaIni, columnaFin;
            String columna = "", contenido = "";
            String rowS = "rowspan";
            String colS = "colspan";
            String colFin = "</td>";
            int rSini, rSFin, cSIni, cSFin, rSpan = 0, cSpan = 0;
            String valueRowS = "1", valueColS = "1";

            while (renglon != null)
            {
                columnaIni = renglon.IndexOf("<td");

                if (columnaIni > 0)
                {
                    columnaFin = renglon.IndexOf(colFin, columnaIni);
                    columna = renglon.Substring(columnaIni, (columnaFin - columnaIni) + colFin.Length);
                    valueRowS = "1";
                    valueColS = "1";
                    rSpan = columna.IndexOf("rowspan");
                    cSpan = columna.IndexOf("colspan");


                    if (rSpan > 0)
                    {
                        rSini = columna.IndexOf("\"", rSpan);

                        if (rSini > 0)
                        {
                            rSFin = columna.IndexOf("\"", rSini + 1);
                            valueRowS = columna.Substring(rSini + 1, rSFin - (rSini + 1));
                        }
                    }

                    if (cSpan > 0)
                    {
                        cSIni = columna.IndexOf("\"", cSpan);

                        if (cSIni > 0)
                        {
                            cSFin = columna.IndexOf("\"", cSIni + 1);
                            valueColS = columna.Substring(cSIni + 1, cSFin - (cSIni + 1));
                        }
                    }

                    renglon = renglon.Substring(columnaFin + colFin.Length, renglon.Length - (columnaFin + colFin.Length));
                    contenido = getContentColumn(columna, j, valueRowS, valueColS);

                    i++;
                    j++;

                }
                else renglon = null;
                numcolumnas = i;

            }
            return i;
        }

        public static String getContentColumn(String columna, int cont, String rowSpan, String colSpan)
        {
            int i = 0, f, faux, fini;
            String contenido = "";
            String marcadorFin = "</p>";
            //columna = columna.Replace("<p class=" + "\"tabla" + "\">", "");
            //columna = columna.Replace("</p>", " ");
            String contenido1 = "";

            while (columna != null)
            {
                f = columna.IndexOf("<p");

                if (f > 0)
                {
                    fini = columna.IndexOf(">", f);
                    faux = columna.IndexOf(marcadorFin, f);
                    contenido = columna.Substring(fini + 1, faux - (fini + 1));
                    contenido1 = contenido1 + " " + contenido;
                    contenidoColumna[cont] = contenido1;
                    rowS[cont] = rowSpan;
                    colS[cont] = colSpan;
                    columna = columna.Substring(faux + marcadorFin.Length, columna.Length - (faux + marcadorFin.Length));

                }
                else
                {
                    columna = null;
                }
            }
            if (contenido1.Equals(""))
            {
                contenidoColumna[cont] = "";
                rowS[cont] = "1";
                colS[cont] = "1";
            }

            return contenido1;
        }

        public static string cleanfile(string _string)
        {

            _string = clearfilefromtargets(_string);

            _string = cleaffilefromalltargets(_string);

            _string = _string.Replace("<span class=\"Altas\">", "");
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
            _string = _string.Replace("<p class=\"pf_pie_lab\">", "");
            _string = _string.Replace("</div>", "");
            _string = _string.Replace("</html>", "");
            _string = _string.Replace("</body>", "");
            _string = _string.Replace("</span>", "");
            _string = _string.Replace("&nbsp", "");
            _string = _string.Replace("</p>", " ");
            _string = _string.Replace("&ntilde;", "ñ");
            _string = _string.Replace("<p class=\"logo\">", "");
            _string = _string.Replace("&Ntilde;", "Ñ");
            _string = _string.Replace("&uml;", "ü");
            _string = _string.Replace("&Uml;", "Ü");
            _string = _string.Replace("&uuml;", "ü");
            _string = _string.Replace("&Uuml;", "Ü");
            _string = _string.Replace("<strong>", "");
            _string = _string.Replace("</strong>", "");
            _string = _string.Replace("<br>", " ");
            _string = _string.Replace("<td>", "");
            _string = _string.Replace("</td>", "");
            _string = _string.Replace("<tr>", "");
            _string = _string.Replace("</tr>", "");
            _string = _string.Replace("</tbody>", "");
            _string = _string.Replace("</table>", "");
            _string = _string.Replace("<span>", "");
            _string = _string.Replace("<div>", "");
            _string = _string.Replace("<span>", "");
            _string = _string.Replace("<html xmlns=\"http://www.w3.org/1999/xhtml\">", "");
            _string = _string.Replace("<!DOCTYPE html>", "");
            _string = _string.Replace("<head>", "");
            _string = _string.Replace("</head>", "");
            _string = _string.Replace("<span lang=\"en-US\">", "");
            _string = _string.Replace("<p class=\"pf_titulo_producto\">", "");
            _string = _string.Replace("<div id=\"_idContainer007\" class=\"Marco-de-texto-b-sico\">", "");
            _string = _string.Replace("<body id=\"x1_recoleccion_de_sangre_con_tubo_paxgene\">", "");
            _string = _string.Replace("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">", "");
            _string = _string.Replace("<p class=\"Normal\">", "");
            _string = _string.Replace("<p class=\"forma-farmaceutica\">", "");
            _string = _string.Replace("<p class=\"TITULO\">", "");
            _string = _string.Replace("<p class=\"Base-de-propaganda\">", "");
            _string = _string.Replace("<span class=\"logo\">", "");
            _string = _string.Replace("<p class=\"Normal\"><span class=\"negrita\">", "");
            _string = _string.Replace("=\"negrita\">", "");
            _string = _string.Replace("<head>", "");
            _string = _string.Replace("<p class=\"pf_normal\">", "");
            _string = _string.Replace("<p class=\"pf_foto_centrada\">", "");
            _string = _string.Replace("<p class=\"pf_referencias\">", "");
            _string = _string.Replace("</span>", "");
            _string = _string.Replace("<span class=\"cr_superindice\">", "");
            _string = _string.Replace("<p class=\"pf_bullets-1\">", "");
            _string = _string.Replace("<a target='_blank' href='http://www.bd.com/mx'>", "");
            _string = _string.Replace("<br />", "");
            _string = _string.Replace("</p>", "");
            _string = _string.Replace("</a>", "");
            _string = _string.Replace("<span class=\"cr_negritas\">", "");
            _string = _string.Replace("<p class=\"pf_registro\">", "");
            _string = _string.Replace("<p class=\"pf_pie_datos\">", "");
            _string = _string.Replace("<p class=\"pf_pie_empresa\">", "");
            _string = _string.Replace("<span class", "");
            _string = _string.Replace("Entregar al</Titulo><Contenido>", "</Titulo><Contenido>");
            _string = _string.Replace("Para mayor información sobre asesoría</Titulo><Contenido>", "</Titulo><Contenido>");
            _string = _string.Replace("</div>", "");
            _string = _string.Replace("</body>", "");
            _string = _string.Replace("</html>", "");
            _string = _string.Replace("<p class=\"tb_titulo\">", "");
            _string = _string.Replace("&oacute;", "ó");
            _string = _string.Replace("<p class=\"NUMEROS\">", "");
            _string = _string.Replace("<p class=\"FORMA_FARMACE\">", "");
            _string = _string.Replace("<p class=\"NORMAL\">", "");
            _string = _string.Replace("<p class=\"PIE\">", "");
            _string = _string.Replace("<p class=\"PIE_2\">", "");
            _string = _string.Replace("<p class=\"NORMAL_Menor\">", "");
            _string = _string.Replace("<p class=\"BANDOS_BANDOS_CON_BULLET_1\">=\"BOLD_Condensed\">", "");
            _string = _string.Replace("<div id=\"_idContainer002\" class=\"Marco-de-texto-b-sico\">", "");
            _string = _string.Replace("<p class=\"BANDOS_BANDOS_CON_BULLET_1\">=\"BOLD_Condensed\">", "");
            _string = _string.Replace("<p class=\"Reg--SARH\">=\"bold-entrada\">", "");

            _string = _string.Replace(" =\"logo_lab\">=\"superindice\">/span>", "");
            _string = _string.Replace("=\"logo_lab\">=\"superindice\">/span>", "");
            _string = _string.Replace("<a id=\"_idTextAnchor000\"> ", "");
            _string = _string.Replace("=\"superindice\">", "");
            _string = _string.Replace("<span lang=\"ar-SA\">", "");
            _string = _string.Replace("\r", "");
            _string = _string.Replace("\n", "");
            _string = _string.Replace("\t", "");
            _string = _string.Replace(" /span>", "");
            _string = _string.Replace("/span>", "");
            _string = _string.Replace("=\"bold-entrada\">", "");
            _string = _string.Replace("<p>", "");
            _string = _string.Replace("=\"logo_lab\">", "");
            _string = _string.Replace("<span class=\"cursiva\">", "");
            _string = _string.Replace("=\"cursiva\">", "");
            _string = _string.Replace("=\"cr_cursiva_normal\">", "");
            _string = _string.Replace("<span                    class=\"cr_superindice\">", "");


            _string = _string.Replace("<p class=\"Pie1\">", "");
            _string = _string.Replace("<p class=\"Pie\">", "");
            _string = _string.Replace("<span lang=\"en-GB\">", "");

            _string = _string.Replace("<p class=\"Ingrediente\">", "");
            _string = _string.Replace("<p class=\"Forma-farmac-\">", "");
            _string = _string.Replace("<p class=\"T-tulo-copia-3\">", "");
            _string = _string.Replace("=\"Altas\" lang=\"en-GB\">", "");
            _string = _string.Replace("=\"marca-registrada-t-tulo\" lang=\"en-GB\">", "");
            _string = _string.Replace("<p class=\"T-tulo-copia-4-\">", "");
            _string = _string.Replace("=\"cr_rubro\">", "");

            _string = _string.Replace("&#171;", "«");
            _string = _string.Replace("&#187;", "»");
            _string = _string.Replace("&#160;", " ");
            _string = _string.Replace("&#8595;", "↓");
            _string = _string.Replace("&#8593;", "↑");
            _string = _string.Replace("&#8596;", "↔");
            _string = _string.Replace("&#8224;", "†");
            _string = _string.Replace("&#8592;", "←");
            _string = _string.Replace("&#8594;", "→");

            _string = _string.Replace("&#8805;", "≥");
            _string = _string.Replace("&#8804;", "≤");
            _string = _string.Replace("&#8734;", "∞");
            _string = _string.Replace("&#8764;", "~");
            _string = _string.Replace("&#954;", "κ");
            _string = _string.Replace("&#949;", "ε");
            _string = _string.Replace("&#955;", "λ");
            _string = _string.Replace("&#916;", "Δ");
            _string = _string.Replace("&#962;", "ς");
            _string = _string.Replace("&#149;", "•");


            return _string;
        }

        public static string clearfilefromtargets(string _string)
        {
            CountriesUsers p = (CountriesUsers)HttpContext.Current.Session["CountriesUsers"];
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

            PLMUsers PLMUsers = new Models.PLMUsers();

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

            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"];

            //var root = HttpContext.Current.Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

            var root = Path.Combine(PathP, "Templates", "XMLFILES", File);

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

                        _string = _string.Replace(pname, "");
                    }

                    if (xtr.Name == "TargetDescriptionPropag")
                    {
                        prop = xtr.ReadInnerXml();
                        prop = prop.Replace("</p>", "");

                        _string = _string.Replace(prop, "");
                    }

                    if (xtr.Name == "TargetDescriptionAttribute")
                    {
                        attribute = xtr.ReadInnerXml();
                        attribute = attribute.Replace("</p>", "");
                        attribute = attribute.Replace("</span>", "");

                        _string = _string.Replace(attribute, "");
                    }

                    if (xtr.Name == "TargetDescriptionLaboratory")
                    {
                        labname = xtr.ReadInnerXml();
                        labname = labname.Replace("</p>", "");

                        _string = _string.Replace(labname, "");
                    }

                    if (xtr.Name == "TargetDescriptionParagraph")
                    {
                        paragraph = xtr.ReadInnerXml();
                        paragraph = paragraph.Replace("</span>", "");
                        paragraph = paragraph.Replace("</p>", "");

                        _string = _string.Replace(paragraph, "");
                    }

                    if (xtr.Name == "TargetDescriptionImage")
                    {
                        Image = xtr.ReadInnerXml();
                        Image = Image.Replace("</p>", "");

                        _string = _string.Replace(Image, "");
                    }

                    if (xtr.Name == "TargetDescriptionTable")
                    {
                        Table = xtr.ReadInnerXml();
                        Table = Table.Replace("</table>", "");

                        _string = _string.Replace(Table, "");
                    }

                    if (xtr.Name == "TargetDescriptionRow")
                    {
                        Row = xtr.ReadInnerXml();
                        Row = Row.Replace("</tr>", "");

                        _string = _string.Replace(Row, "");
                    }

                    if (xtr.Name == "TargetDescriptionColumn")
                    {
                        Column = xtr.ReadInnerXml();
                        Column = Column.Replace("</td>", "");

                        _string = _string.Replace(Column, "");
                    }

                    if (xtr.Name == "TargetDescriptionRegister")
                    {
                        Register = xtr.ReadInnerXml();
                        Register = Register.Replace("</p>", "");

                        _string = _string.Replace(Register, "");

                    }

                    if (xtr.Name == "TargetDescriptionBullet")
                    {
                        Bullets = xtr.ReadInnerXml();
                        Bullets = Bullets.Replace("</p>", "");

                        _string = _string.Replace(Bullets, "");
                    }

                    if (xtr.Name == "TargetDescriptionBullet1")
                    {
                        Bullets1 = xtr.ReadInnerXml();
                        Bullets1 = Bullets1.Replace("</p>", "");

                        _string = _string.Replace(Bullets1, "");
                    }

                    if (xtr.Name == "TargetDescriptionBullet2")
                    {
                        Bullets2 = xtr.ReadInnerXml();
                        //Bullets2 = Bullets2.Replace("</p>", "");

                        int len = Bullets2.IndexOf("</");
                        Bullets2 = Bullets2.Substring(0, len);

                        _string = _string.Replace(Bullets2, "");

                    }

                    if (xtr.Name == "TargetDescriptionBullet3")
                    {
                        Bullets3 = xtr.ReadInnerXml();
                        Bullets3 = Bullets3.Replace("</p>", "");

                        _string = _string.Replace(Bullets3, "");
                    }

                    if (xtr.Name == "TargetDescriptionLog")
                    {
                        Logo = xtr.ReadInnerXml();
                        Logo = Logo.Replace("</span>", "");

                        _string = _string.Replace(Logo, "");
                    }

                    if (xtr.Name == "TargetDescriptionBold")
                    {
                        Bold = xtr.ReadInnerXml();
                        Bold = Bold.Replace("</span></p>", "");
                        Bold = Bold.Replace("</p>", "");

                        _string = _string.Replace(Bold, "");
                    }

                    if (xtr.Name == "TargetDescriptionReference")
                    {
                        Reference = xtr.ReadInnerXml();
                        Reference = Reference.Replace("</p>", "");

                        _string = _string.Replace(Reference, "");
                    }

                    if (xtr.Name == "TargetDescriptionSuperindice")
                    {
                        SupInd = xtr.ReadInnerXml();
                        SupInd = SupInd.Replace("</span>", "");

                        _string = _string.Replace(SupInd, "");
                    }

                    if (xtr.Name == "TargetDescriptionData")
                    {
                        Data = xtr.ReadInnerXml();
                        Data = Data.Replace("</p>", "");

                        _string = _string.Replace(Data, "");
                    }

                    if (xtr.Name == "TargetDescriptionContainer")
                    {
                        Container = xtr.ReadInnerXml();
                        Container = Container.Replace("</div>", "");

                        _string = _string.Replace(Container, "");
                    }

                    if (xtr.Name == "TargetDescriptionBody")
                    {
                        Body = xtr.ReadInnerXml();
                        Body = Body.Replace("</body>", "");

                        _string = _string.Replace(Body, "");
                    }
                }
                xtr.Close();
            }
            return _string;
        }

        public static string cleaffilefromalltargets(string _string)
        {
            CountriesUsers p = (CountriesUsers)HttpContext.Current.Session["CountriesUsers"];

            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            PLMUsers PLMUsers = new Models.PLMUsers();

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
                    File = "targets_" + _ctr.ID + ".xml";
                }
            }

            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"];

            //var root = HttpContext.Current.Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

            var root = Path.Combine(PathP, "Templates", "XMLFILES", File);

            if (System.IO.File.Exists(root))
            {
                String TargetName = "";

                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(root);

                XmlNode node;
                node = myXmlDocument.DocumentElement;

                XmlReader xtr = XmlReader.Create(root);
                while (xtr.Read())
                {
                    if (xtr.Name == "TargetDescription")
                    {
                        if (_string.Contains("Anoscopio"))
                        {

                        }

                        TargetName = xtr.ReadInnerXml();
                        TargetName = TargetName.Replace("> </p>", "");
                        //TargetName = TargetName.Replace("</p>", "");
                        TargetName = TargetName.Replace("&gt;", ">");
                        TargetName = TargetName.Replace("\n", "");
                        TargetName = TargetName.Replace("\t", "");

                        TargetName = TargetName.Substring(0, TargetName.IndexOf("</"));

                        _string = _string.Replace(TargetName, "");

                    }
                }

                xtr.Close();
            }
            return _string;
        }

        public static String[] contenidoColumna = new String[8000];
        public static String[] rowS = new String[8000];
        public static String[] colS = new String[8000];
        public static int numcolumnas;
        public static int numrenglones;
        public static int[] rows = new int[800];
        public static String tabla;
        public static int j = 0;
            #endregion


        public bool insertxml(int _clienid, int _editionid, int _productid, String _content)
        {

            var ppfile = (from part in db.ParticipantProducts
                          where part.ClientId == _clienid
                          && part.EditionId == _editionid
                          && part.ProductId == _productid
                          select part).ToList();

            if (ppfile.LongCount() > 0)
            {
                foreach (ParticipantProducts pp in ppfile)
                {
                    ParticipantProducts = new ParticipantProducts();

                    pp.XMLContent = _content;

                    db.SaveChanges();

                    return true;
                }
            }
            return false;
        }
    }
}