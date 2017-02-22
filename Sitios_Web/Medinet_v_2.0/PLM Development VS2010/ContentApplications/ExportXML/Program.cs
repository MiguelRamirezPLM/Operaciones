using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExportXML
{
    class Program
    {
        public static String tabla;
        public static int numrenglones;
        public static int j = 0;
        public static int numcolumnas;
        public static String[] contenidoColumna = new String[8000];
        public static String[] rowS = new String[8000];
        public static String[] colS = new String[8000];
        public static int[] rows = new int[800];
        
        static void Main(string[] args)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo("\\\\195.192.2.17\\Proyectos\\Obras PLM\\Mexico\\DEF 58\\Formación\\Actualizaciones de productos\\");
            String nameFile = "";
            foreach (FileInfo f in dir.GetFiles("*.htm"))
            {
                nameFile = f.Name;
                nameFile = nameFile.Replace(".htm", "");
                setXMLFile(f.DirectoryName+"\\"+f.Name, nameFile);
                
                //Console.WriteLine(f.DirectoryName + "\\" + f.Name);                
                //Console.ReadLine();
            }     
        }

        public static void setXMLFile(String file, String nameFile)
        {
            String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";
            String rootIni = "<Root>";
            String rootFin = "</Root>";
            String f = getProduct(file);
            StreamWriter SW;
            String forma = getPharmaForm(file);
            String subs = getSubstance(file);
            String rubro = getAttribute(file);
            String info = getTypeInformation(file);
            String prop = getPropaganda(file);
            String lab = getLaboratory(file);
            
            f = f.Trim();
            forma = forma.Trim();
            f = f.Replace("+","");
            
            f = f.Replace("*", "");
            forma = forma.Replace("+", "");
            forma = forma.Replace("*", "");
            //cleanAttribute(rubro);            

            SW = File.CreateText("\\\\195.192.2.17\\Proyectos\\Obras PLM\\Mexico\\DEF 58\\Formación\\Actualizaciones de productos\\" + nameFile + ".xml");
            SW.Write(xml);
            SW.Write(rootIni);
            SW.Write("<Producto>");

            if (info != "")
            {
                SW.Write(info);
            }
            if (f != "")
            {
               
                SW.Write(f);
            }
            if (forma != "")
            {
                SW.Write(forma);
            }
            if (subs != "")
            {
                SW.Write(subs);
            }
            if (prop != "")
            {
                SW.Write(prop);
            }
            if (rubro != "")
            {
                SW.Write(rubro);
            }
            if (lab != "")
                SW.Write(lab);

            SW.Write("</Producto>");
            SW.Write(rootFin);            
            SW.Close();
                   

        }             

        public static String getAttribute(String F)
        {
            StreamReader SR;
            String S;
            String rubro="";
            String info = "";
            String marcador = "<p class=\"normal\"><span class=\"rubros-azules\""; 
            //String marcador = "<p class=\"normal\"><span class=\"subt";
            //String marcador = "<p class=\"normal\"><span class=\"negro-normal\"";                 
            //String marcador = "<p class=\"normal-normal\"><span class=\"normal-normal-color\"";
            //String marcador = "<p><span class=\"rubros-azules\"";                              
            //String marcador = "<p class=\"normal\">
            //String marcador = "<p class=\"normal\"><span class=\"letra-cyan\"";
            //String marcador = "<p class=\"normal\"><span class=\"letra-cyan-2\"";                   
            //String marcador = "<p class=\"normal-normal\"><span class=\"normal-normal-color\"";
            //String marcador = "<p class=\"normal\"><span class=\"subtitulo\"";
            //String marcador = "<p class=\"normal\"><span class=\"bold-entrada\"";
                               
            String marcadorF ="</span>";                     
            String rubroNombre=" ";
            String rubroContenido = null; ;            
            String rubroC="";
            int i=0, f=0, iaux=0, faux=0;
            SR = File.OpenText(F);
            S = SR.ReadToEnd();                      
            S = S.Replace("\t", "");
            int pie = 0;
            String marcadorPie = "<p class=\"pie";
            
            while (S != null)
            {
                i = S.IndexOf(marcador);
                if (i >= 0){
                    f = S.IndexOf(marcadorF, i);
                    faux = S.IndexOf(">", i + marcador.Length);
                    info = S.Substring(faux + 1, f - (faux + 1));
                    rubroNombre = info;//S.Substring(i + marcador.Length, f - (i + marcador.Length));

                    iaux = S.IndexOf(marcador, f);

                    if (iaux >= 0)
                    {
                        rubroContenido = S.Substring(f + marcadorF.Length, iaux - (f + marcadorF.Length));
                        S = S.Substring(iaux, S.Length - iaux);
                    }
                    else
                    {
                        pie = S.IndexOf(marcadorPie, f);
                        if(pie >= 0)
                            rubroContenido = S.Substring(f, pie-f);
                        else
                            rubroContenido = S.Substring(f, S.Length-f);
                        S = null;
                    }
                    if (rubroNombre != " ")
                    {
                        rubroC += "<Rubro>";
                        rubroC += "<Titulo>" + rubroNombre + "</Titulo>";
                        rubroC += "<Contenido>" + rubroContenido + "</Contenido></Rubro>";
                    }
                }
                else S = null;
                           
            }
            if (rubroC != "")
            {
                rubroC = getTableByAttribute(rubroC);                
                rubroC = cleanFileHTML(rubroC);
            }

            SR.Close();

            return rubroC;
        }

        public static String getLaboratory(String file)
        {
            StreamReader SR;

            int i, f;
            int faux;

            String S;
            String imagen = "";
            String imaFin = " />";
            String lab = "";
            int iIma, fIma;
            String imag = "";
            String marcadorLab = "<p class=\"pie\"";
                                                                   
            String laboratorio = "";

            SR = File.OpenText(file);
            S = SR.ReadToEnd();

            while (S != null)
            {
                i = S.IndexOf(marcadorLab);
                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i);
                    faux = S.IndexOf(">", i);
                    //lab = S.Substring(faux + 1, f - (faux + 1));
                    lab = S.Substring(faux + 1, S.Length - (faux + 1));
                    laboratorio = lab;
                    while (lab != null)
                    {
                        iIma = lab.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = lab.IndexOf(" />", iIma);
                            imagen = lab.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            lab = lab.Replace(imagen, "");
                            laboratorio = lab;
                            imag += imagen;

                        }
                        else lab = null;

                    }
                    S = null;
                }
                else S = null;
            }
            SR.Close();
            if (laboratorio != "")
            {
                laboratorio = "<Laboratorio>" + laboratorio + "</Laboratorio>";
                
                if (imag != "")
                {
                    laboratorio += imag;
                }
                laboratorio = cleanFileHTML(laboratorio);
            }

            return laboratorio;

        }

        public static String getProduct(String file)
        {
            StreamReader SR;
            
            int i, f;
            int faux; 

            String S;
            String imagen="";
            String imaFin = " />";
            String nombre ="";            
            int iIma, fIma;
            String imag = "";                       
            String marcadorName = "<p class=\"t-tulo";
            //String marcadorName = "<p class=\"titulo\"><span class=\"bold\"";                                                         
            String nombreProducto = "";

            SR = File.OpenText(file);
            S = SR.ReadToEnd();            

            while (S != null)
            {
                i = S.IndexOf(marcadorName);
                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i);
                    faux = S.IndexOf(">", i);
                    nombre = S.Substring(faux+1, f - (faux+1));
                    nombreProducto = nombre;
                    while (nombre != null)
                    {
                        iIma = nombre.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = nombre.IndexOf(" />", iIma);
                            imagen = nombre.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            nombre = nombre.Replace(imagen, "");
                            nombreProducto = nombre;                            
                            imag += imagen;
                            
                        }
                        else nombre = null;

                    }
                    S = null;
                }
                else S = null;  
            }
            SR.Close();
            if (nombreProducto != "")
            {
                nombreProducto = "<Nombre>" + nombreProducto + "</Nombre>";                
                if (imag != "")
                {
                    nombreProducto += imag;
                }
                nombreProducto = cleanFileHTML(nombreProducto);
            }

            return nombreProducto;            
        }

        public static String getPharmaForm(String file)
        {
            StreamReader SR;
            
            String S;           
            String imagen = "";
            String imaFin = " />";
            String pharma ="";
            String marcadorPharma = "<p class=" + "\"forma-farmac-" + "\">";
            //String marcadorPharma = "<p class=\"forma-farmaceutica\"><span class=\"boldcursiva\">";                                                                                                 
            String imag = "";
            String pharmaForm = "";

            int i, f, iIma, fIma;
            
            SR = File.OpenText(file);
            S = SR.ReadToEnd();            
            S = S.Replace("<span class="+"\"cursivo-normal"+"\">","");
            S = S.Replace("</span>", "");


            while (S != null)
            {
                i = S.IndexOf(marcadorPharma);

                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i + marcadorPharma.Length);
                    pharma = S.Substring(i + marcadorPharma.Length, f - (i + marcadorPharma.Length));
                    pharmaForm = pharma;
                    while (pharma != null)
                    {
                        iIma = pharma.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = pharma.IndexOf(" />", iIma);
                            imagen = pharma.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            pharma = pharma.Replace(imagen, "");
                            pharmaForm = pharma;
                            imag += imagen;
                        }
                        else pharma = null;

                    }
                    S = null;
                }

                else S = null;
            }
            SR.Close();

            if (pharmaForm != "")
            {
                pharmaForm = "<FormaFarmaceutica>" + pharmaForm + "</FormaFarmaceutica>";
                if (imag != "")
                {
                    pharmaForm += imag;
                }
                pharmaForm = cleanFileHTML(pharmaForm);
            }          

            return pharmaForm;
        }

        public static String getSubstance(String file)
        {
            StreamReader SR;
            
            String S;            
            String imagen = "";
            String imaFin = " />";            
            String subs="";
            //String marcadorSubs = "<p class="+"\"ingrediente"+"\">";
            String marcadorSubs = "<p class=\"ingrediente\">";                                  
            String substance = "";
            String imag = "";

            int i, f, iIma, fIma;
            
            SR = File.OpenText(file);
            S = SR.ReadToEnd();           

            while (S != null)
            {                
                i = S.IndexOf(marcadorSubs);
                
                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i + marcadorSubs.Length);
                    subs = S.Substring(i + marcadorSubs.Length, f - (i + marcadorSubs.Length));
                    substance = subs;
                    while (subs != null)
                    {
                        iIma = subs.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = subs.IndexOf(" />", iIma);
                            imagen = subs.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            subs = subs.Replace(imagen, "");
                            substance = subs;
                            imag += imagen;
                        }
                        else subs = null;

                    }
                    S = null;
                }
                else S = null;
            }

            SR.Close();
            
            if (substance != "")
            {
                substance = "<SustanciaActiva>" + substance + "</SustanciaActiva>";
                if (imag != "")
                {
                    substance += imag;
                }
                substance = cleanFileHTML(substance);
            }     

            return substance;
        }


        public static String getTypeInformation(String file)
        {
       
            StreamReader SR;
            
            String S;            
            String info="";
            //String marcador = "<p class=" + "\"medio-de-d-";
            String marcador = "<p class=\"antetitulo\"";
            String imagen = "";
            String imaFin = " />";            
            String tipoInfo = "";
            String imag = "";

            int i, f, iIma, fIma, faux;
            
            SR = File.OpenText(file);
            S = SR.ReadToEnd();           

            while (S != null)
            {
                i = S.IndexOf(marcador);
                if (i >= 0)
                {
                    f = S.IndexOf("<", i + marcador.Length);
                    faux = S.IndexOf(">", i + marcador.Length);
                    info = S.Substring(faux+1, f- (faux + 1));
                    tipoInfo = info;
                    while (info != null)
                    {
                        iIma = info.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = info.IndexOf(" />", iIma);
                            imagen = info.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            info = info.Replace(imagen, "");
                            tipoInfo = info;
                            imag += imagen;
                        }
                        else info = null;

                    }
                    S = null;
                }
                else S = null;
            }
            SR.Close();

            if (tipoInfo != "")
            {
                tipoInfo = "<TipoInformacion>" + tipoInfo + "</TipoInformacion>";
                if (imag != "")
                {
                    tipoInfo += imag;
                }
                tipoInfo = cleanFileHTML(tipoInfo);
            }

            return tipoInfo;
        }

        public static String getPropaganda(String file)
        {
            StreamReader SR;
            
            String S;            
            String propaganda="";
            String imagen;
            //String marcadorProp = "<p class="+"\"base-de-prop-"+"\">";
            String marcadorProp = "<p class=\"base-de-prop-\">";     
            //String marcadorProp = "<p class=\"base-de-propaganda\"><span class=\"cursiva\"";                 
            String imaIni = "<img";
            String imaFin = " />";
            String baseprop = "";
            String imag = "";

            int i, f, iIma, fIma;

            SR = File.OpenText(file);
            S = SR.ReadToEnd();           

            while (S != null)
            {

                i = S.IndexOf(marcadorProp);
                
                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i + marcadorProp.Length);
                    propaganda = S.Substring(i + marcadorProp.Length, f-(i + marcadorProp.Length));
                    baseprop = propaganda;
                    while (propaganda != null)
                    {
                        iIma = propaganda.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = propaganda.IndexOf(" />", iIma);
                            imagen = propaganda.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            propaganda = propaganda.Replace(imagen, "");
                            baseprop = propaganda;
                            imag += imagen;
                        }
                        else propaganda = null;

                    }
                    S = null;
                }
                else S = null;
            }
            SR.Close();

            if (baseprop != "")
            {
                baseprop = "<BasePropaganda>" + baseprop + "</BasePropaganda>";
                if (imag != "")
                {
                    baseprop += imag;
                }
                baseprop = cleanFileHTML(baseprop);
            }

            return baseprop;                 

        }

        public static String cleanFileHTML(String cadena)
        {
            cadena = cadena.Replace("&aacute;", "á");
            cadena = cadena.Replace("&eacute;", "é");
            cadena = cadena.Replace("&iacute;", "í");
            cadena = cadena.Replace("&oacute;", "ó");
            cadena = cadena.Replace("&uacute;", "ú");

            cadena = cadena.Replace("&Aacute;", "Á");
            cadena = cadena.Replace("&Eacute;", "É");
            cadena = cadena.Replace("&Iacute;", "Í");
            cadena = cadena.Replace("&Oacute;", "Ó");
            cadena = cadena.Replace("&Uacute;", "Ú");

            cadena = cadena.Replace("&#174;", "® ");
            cadena = cadena.Replace("<p class=" + "\"medio-de-d-prod-nuevo" + "\">", "\n");
            cadena = cadena.Replace("&lt;", "");
            cadena = cadena.Replace("<p class=" + "\"medio-de-d-" + "\">", "");
            cadena = cadena.Replace("&gt;", "");
            cadena = cadena.Replace("<br />", " ");
            cadena = cadena.Replace("<span class="+"\"tabla"+"\">", "");
            cadena = cadena.Replace("<div class=" + "\"story" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"corniza-derecha" + "\">", "");
            cadena = cadena.Replace("<p class="+"\"corniza-izquierda"+"\">", "");
            cadena = cadena.Replace("<div class="+"\"group"+"\">", "");
            cadena = cadena.Replace("<p>","");
            cadena = cadena.Replace("<span class="+"\"rubros-azules"+"\">","");
            cadena = cadena.Replace("<span class="+"\"cursivas-normal"+"\">", "");
            cadena = cadena.Replace("<span class="+"\"resaltado"+"\">", "");
            cadena = cadena.Replace("-<br />", " ");
            cadena = cadena.Replace(" <br />", " ");

            cadena = cadena.Replace("</div>", "");
            cadena = cadena.Replace("</html>", "");
            cadena = cadena.Replace("</body>", "");
            cadena = cadena.Replace("<span class=" + "\"cursivo-normal" + "\">", " ");
            cadena = cadena.Replace("<span class=" + "\"symbolprop-bt" + "\">", "");
            cadena = cadena.Replace("</span>", "");
            cadena = cadena.Replace("&nbsp", "");

            cadena = cadena.Replace("</p>", " ");
            cadena = cadena.Replace("<p class=" + "\"texto-de-leyendas-de-prot-" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"pie1" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"pie" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"bandos" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"biblio-refe" + "\">", "");
            cadena = cadena.Replace("<span class=" + "\"symbol" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"pie-de-columna" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"sin-estilo-normal" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"bandos1" + "\">", "");
            

            cadena = cadena.Replace("<p class=" + "\"tabla" + "\">", "\n");
            cadena = cadena.Replace("<p class=" + "\"pie-de-tabla" + "\">", "\n");
            cadena = cadena.Replace("<p class=" + "\"pie-de-tabla-2" + "\">", "\n");

            cadena = cadena.Replace("<p class="+"\"normal"+"\">","");
            cadena = cadena.Replace("<span class="+"\"rubros-az","");

            cadena = cadena.Replace("<img src=" + "\"", "<Imagen href=" + "\"file://");
            cadena = cadena.Replace("<span class=\"superindice-normal\">", "");
            cadena = cadena.Replace("<span class=\"subindice-normal\">", "");
            cadena = cadena.Replace("<span class=\"superindice-titulo\">","");
            cadena = cadena.Replace("<span class=\"letra-cyan\">", "");
            cadena = cadena.Replace("&ntilde;", "ñ");
            cadena = cadena.Replace("<p class=\"logo-pie\">", "");
            cadena = cadena.Replace("<span class=\"subrayado\">", "");
            cadena = cadena.Replace("<span class=\"alfabeto-griego\">", "");
            cadena = cadena.Replace("<span class=\"mg-titulo\">", "");
            cadena = cadena.Replace("<span class=\"subtirulito\">", "");
            cadena = cadena.Replace("<span class=\"subtitulo-3\">", "");
            cadena = cadena.Replace("<span class=\"ref-de-comentario\">", "");
            cadena = cadena.Replace("<span class=\"texto-en-negrita\">", "");
            cadena = cadena.Replace("<span class=\"apple-style-span\">", "");
            cadena = cadena.Replace("<span class=\"hiperv-nculo\">", "");
            cadena = cadena.Replace("<span class=\"numero-titulo\">", "");
            cadena = cadena.Replace("<span class=\"hyperlink\">", "");
            cadena = cadena.Replace("<span class=\"prddispsmbk1\">", "");
            cadena = cadena.Replace("<span class=\"subtitulos1\">", "");
            cadena = cadena.Replace("<p class=\"tabla-titulo\">", "");
            cadena = cadena.Replace("<p class=\"imagen\">", "");
            cadena = cadena.Replace("<p class=\"logo\">", "");
            cadena = cadena.Replace("<p class=\"tabla-celda\">", "");                                    
            cadena = cadena.Replace("<p class=\"zletra\">", "");
            cadena = cadena.Replace("&Ntilde;", "Ñ");

            cadena = cadena.Replace("<span class=\"superindice\">", " ");
            cadena = cadena.Replace("<span class=\"negro-normal\">", " ");
            cadena = cadena.Replace("<span class=\"puntos-formulaci-n\">", " ");
            cadena = cadena.Replace("<span class=\"subindice\">", " ");
            cadena = cadena.Replace("<span class=\"tabla-negro\">", "");
            cadena = cadena.Replace("<span class=\"aa\">", " ");

            cadena = cadena.Replace("&uml;", "ü");
            cadena = cadena.Replace("&Uml;", "Ü"); 
            cadena = cadena.Replace("&uuml;", "ü");
            cadena = cadena.Replace("&Uuml;", "Ü");
            cadena = cadena.Replace("<strong>", "");
            cadena = cadena.Replace("</strong>", "");
            //cadena = cadena.Replace("<span class="letra-cyan-copia">");
            cadena = cadena.Replace("<p class=\"logo-pie\" align=\"center\">", "");
            cadena = cadena.Replace("<span class=\"logo-pie\">", "");
            cadena = cadena.Replace("<p class=\"logo-pie\" align=\"center\">;", "");
            cadena = cadena.Replace("<span class=\"logo\">", "");
            cadena = cadena.Replace("<p class=\"tabla-encabezado-columna-\">", "");
            cadena = cadena.Replace("<span class=\"strong\">", "");
            cadena = cadena.Replace("<p class=\"tabla-pie\">", "");            
            cadena = cadena.Replace("<span class=\"s93\">", "");
            cadena = cadena.Replace("<span class=\"s161\">", "");
            cadena = cadena.Replace("<p class=\"tabla-2\">", "");
            cadena = cadena.Replace("<span class=\"sub-indice-titulo\">", " ");
            cadena = cadena.Replace("<span class=\"superindice-titulo-nuevo\">", " ");
            cadena = cadena.Replace("<span class=\"page-number\">", "");
            cadena = cadena.Replace("<span class=\"ref-de-nota-al-final\">", " ");
            cadena = cadena.Replace("<span class=\"alfabeto-griego-2\">", "");
            cadena = cadena.Replace("<span class=\"strong-car-car\">", "");
            cadena = cadena.Replace("<span class=\"ref-de-nota-al-final\">", "");
            cadena = cadena.Replace("<span class=\"emphasis\">", "");
            cadena = cadena.Replace("<span class=\"a0\">", "");
            cadena = cadena.Replace("<span class=\"short-text\">", "");
            cadena = cadena.Replace("<p class=\"logo\" align=\"center\">", "");
            cadena = cadena.Replace("<p class=\"logo\" align=\"right\">", "");
            cadena = cadena.Replace("<span class=\"tit-nuevo-distribuidores\">", "");
            cadena = cadena.Replace("<span class=\"forma-farma-distribuidores\">", "");
            cadena = cadena.Replace("<span class=\"azul-titulo-distribuidores\">", "");
            cadena = cadena.Replace("<span class=\"normal-vitamina\">", "");
            cadena = cadena.Replace("<p class=\"fin-de-producto\">", "");
            cadena = cadena.Replace("<p class=\"tabla-titulos\">", "");
            cadena = cadena.Replace("<p class=\"bandos-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-3\">", "");
            cadena = cadena.Replace("<span class=\"titulo-distribuidores\">", "");
            cadena = cadena.Replace("<span class=\"endnote-reference\">", "");
            cadena = cadena.Replace("<span class=\"bandos\">", "");
            cadena = cadena.Replace("<p class=\"bandos2\">", "");

            cadena = cadena.Replace("<span class=\"titulo-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-negrita\">", "");
            cadena = cadena.Replace("<p class=\"normal-normal\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-sin-vi-eta\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-normal-superindice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-negrita-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"titulo-numeros\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-1\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-para-numeros\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-negrita\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-normal-subindice\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-1-sin-vi-eta\">", "");           
            cadena = cadena.Replace("<span class=\"tablas-tabla-normal-superindice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-negra-sub-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-symbol\">", "");
            cadena = cadena.Replace("<p class=\"tablas-titulo-tabla\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-bold\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-color-sin-bold-\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-color\">", "");
            cadena = cadena.Replace("<span class=\"pie-1-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"titulo-producto-nuevo-numeros\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-sub-ndice\">", "");
            cadena = cadena.Replace("<div class=\"image\">", "");
            cadena = cadena.Replace("<p class=\"normal-normal-graficos-centrado\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-bold-superindice\">", " ");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-normal\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-normal-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-bold-superindice\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-normal-superindice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-negra-super-ndice\">", " ");
            cadena = cadena.Replace("<span class=\"normal-normal-color-sin-bold-superindice\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-bold-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"tablas-tabla-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"normal-titulo-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-color-sin-bold-superindice\">", " ");
            cadena = cadena.Replace("<p class=\"normal-normal-tabla-izq\">", "");
            cadena = cadena.Replace("<span class=\"pie-1-negrita-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-bold-cursiva\">", "");
            cadena = cadena.Replace("<p class=\"tablas-tabla-izquierda-con-tabulado\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbol\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-normal-supbindice\">", " ");
            cadena = cadena.Replace("<span class=\"bandos-bandos-symbol\">", " ");
            cadena = cadena.Replace("<span class=\"base-de-producto-sub-indice\">", "");
            cadena = cadena.Replace("<span class=\"ingrediente-sub-ndice\">", "");
            cadena = cadena.Replace("<p class=\"tablas-pie-tabla\">", "");
            cadena = cadena.Replace("<span class=\"base-de-producto-subindice\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbolpro\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-normal-subindice\">", "");
            cadena = cadena.Replace("<span class=\"titulo-sub-ndice\">", "");
            cadena = cadena.Replace("<span class=\"titulo-producto-nuevo-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-underline\">", "");
            cadena = cadena.Replace("<span class=\"normal-symbol-winding\">", "");
            cadena = cadena.Replace("<p class=\"tablas-tabla-centrado\">", "");
            cadena = cadena.Replace("<p class=\"tablas-tabla-izquierda\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-windings-3\">", "");
            cadena = cadena.Replace("<span class=\"tablas-symbol\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-bold-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-symbol-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"normal-symbol-sub-ndice\">", " ");
            cadena = cadena.Replace("<span class=\"normal-symbol-pro-\">", "");
            cadena = cadena.Replace("<p class=\"normal-normal-tabla-cen\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-bold-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"tablas-tabla-windings-3-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"normal-symbol-winding-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbol-subindice\">", "");
            cadena = cadena.Replace("<br>", " ");
            cadena = cadena.Replace("<p class=\"cuadro\">", "");
            cadena = cadena.Replace("<p class=\"antetitulo\">", "");
            cadena = cadena.Replace("<p class=\"bandos-copy\">", "");
            cadena = cadena.Replace("<span class=\"superindice-tit\">", "");
            cadena = cadena.Replace("<span class=\"negro-superindice\">", "");
            cadena = cadena.Replace("<span class=\"negro-cursivo\">", "");
            cadena = cadena.Replace("<span class=\"negro-subindice\">", "");
            cadena = cadena.Replace("<span class=\"symbolprop-bt-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"symbolprop-bt-sub-ndice\">", "");
            cadena = cadena.Replace("<span class=\"symbol-subindice\">", "");
            cadena = cadena.Replace("<span class=\"azul-titulo-reg\">", "");
            cadena = cadena.Replace("<p class=\"pie-2\">", "");
            cadena = cadena.Replace("<span class=\"normal-reg\">", "");
            cadena = cadena.Replace("<span class=\"tit-nuevo-reg-\">", "");
            cadena = cadena.Replace("<span class=\"tit-nuevo-distribuidores-copia\">", "");
            cadena = cadena.Replace("<p class=\"pie-3\">", "");
            cadena = cadena.Replace("<p class=\"pie-4-logo\">", "");
            cadena = cadena.Replace("<p class=\"pie-laboratorio\">", "");
            cadena = cadena.Replace("<p class=\"antetitulo-2\">", "");
            cadena = cadena.Replace("<p class=\"pie-4-logo\">", "");
            cadena = cadena.Replace("<p class=\"antetitulo-sin-separacion\">", "");
            cadena = cadena.Replace("<p class=\"tabla-pie-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-1\">", "");
            cadena = cadena.Replace("<span class=\"bandos-negrita-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbolpro-subindice\">", "");
            cadena = cadena.Replace("<span class=\"titulo-producto-nuevo-numeros-2\">", "");

            //DEAQ
            cadena = cadena.Replace("<span class=\"boldcursiva\">", " ");
            cadena = cadena.Replace("<span class=\"cursiva\">", " ");
            cadena = cadena.Replace("<p class=\"cuadro-arriba\">", " ");
            cadena = cadena.Replace("<p class=\"cuadro-fin\">", " ");
            cadena = cadena.Replace("<p class=\"final-pagina\">", " ");
            cadena = cadena.Replace("<p class=\"final-pagina1\">", " ");
            cadena = cadena.Replace("<p class=\"texto-centro1\">", " ");
            cadena = cadena.Replace("<span class=\"min-titulo\">", " ");
            cadena = cadena.Replace("<span class=\"superindicetit\">", " ");
            cadena = cadena.Replace("<span class=\"boldpantalla\">", " ");
            cadena = cadena.Replace("<p class=\"final-p-gina-1\">", " ");
            cadena = cadena.Replace("<p class=\"texto-centro\">", " ");
            cadena = cadena.Replace("<p class=\"final-p-gina-1\">", " ");
            cadena = cadena.Replace("<p class=\"cuadro1\">", "");
            cadena = cadena.Replace("<p class=\"composi\">", " ");
            cadena = cadena.Replace("<p class=\"forma-farmac-\">", "");
            cadena = cadena.Replace("<p class=\"reg-sarh\">", "");
            cadena = cadena.Replace("<p class=\"cuadro2\">", "");
            cadena = cadena.Replace("<p class=\"t-tulosub\">", "");
            cadena = cadena.Replace("<p class=\"composici-n\">", "");
            cadena = cadena.Replace("<p class=\"cuadro3\">", "");
            cadena = cadena.Replace("<span class=\"bold\">", "");
            cadena = cadena.Replace("<p class=\"pie-1\">", "");
            cadena = cadena.Replace("<p class=\"t-tulo-prod-nuevo\">", "");

            cadena = cadena.Replace("<p class=\"tabla-prod\">", " ");
            cadena = cadena.Replace("<span class=\"negro\">", " ");
            cadena = cadena.Replace("<span class=\"symbolprop-bt-superindice\">", " ");
            cadena = cadena.Replace("<td class=\"normal-tabla-prod\">", "");
            cadena = cadena.Replace("<p class=\"productos\">", "");
            cadena = cadena.Replace("class=\"normal-tabla-prod\"", " ");
            cadena = cadena.Replace("<p class=\"x-ning-n-estilo-de-p-rrafo-\">", " ");
            cadena = cadena.Replace("<p class=\"style-group-1-bandos\">", "");

            
            return cadena;
        }   


        //Tabla
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

    }
}
