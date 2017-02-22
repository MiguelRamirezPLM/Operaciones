using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class DEAQP
    {
        //static void Main(string[] args)
        //{
        //    string valid = "";
        //    do
        //    {
        //        Console.WriteLine("Ingresa la ruta del archivo a revisar:");

        //        string path = Console.ReadLine();

        //        //string archivo = "Sustancia,Producto,Sustancias,Descripcion,Laboratorio" + Environment.NewLine;  ////Sustancias
        //        //string archivo = "Uso,Producto,Laboratorio" + Environment.NewLine;   ////Usos
        //        string archivo = "Cultivo,Producto,Descripcion,Laboratorio" + Environment.NewLine;   ////Cultivos
   

        //        GetHTML filecontent = new GetHTML();

        //        string allcontent = filecontent.getHtmlContent(path);

        //        string subs,prods;

        //        int ini, fin,next,med1,med2;

        //        next = 0;

        //        while (next < allcontent.Length)
        //        {
        //            //ini = allcontent.IndexOf("<p class=\"sustanc\">", next);   ////Sustancias
        //            //med1 = ini + "<p class=\"sustanc\">".Length;  ////Sustancias
        //            //ini = allcontent.IndexOf("<p class=\"titulo-1\">", next);   ////Usos
        //            //med1 = ini + "<p class=\"titulo-1\">".Length;    ////Usos
        //            ini = allcontent.IndexOf("<p class=\"sustanc\">", next);   ////Cultivos
        //            med1 = ini + "<p class=\"sustanc\">".Length;   ////Cultivos
        //            med2 = allcontent.IndexOf("</", med1);
        //            //fin = allcontent.IndexOf("<p class=\"sustanc\">", (ini + "<p class=\"sustanc\">".Length));   ////Sustancias
        //            //fin = allcontent.IndexOf("<p class=\"titulo-1\">", (ini + "<p class=\"titulo-1\">".Length));   ////Usos
        //            fin = allcontent.IndexOf("<p class=\"sustanc\">", (ini + "<p class=\"sustanc\">".Length));   ////Cultivos

        //            if (fin > -1)
        //            {
        //                prods = allcontent.Substring(ini, (fin - ini));
        //                next = fin - 1;
        //            }
        //            else
        //            {
        //                prods = allcontent.Substring(ini);
        //                next = allcontent.Length;
        //            }

        //            subs = allcontent.Substring(med1, (med2 - med1));

        //            //subs = filecontent.quitAccentsFileName(subs);   ////Sustancias

        //            int ini2, fin2;

        //            for (int x = 0; x < prods.Length; x++)
        //            {
        //                //ini2 = prods.IndexOf("<p class=\"body-text\">", x);   ////Sustancias
        //                //ini2 = prods.IndexOf("<p class=\"x4-body-text\">", x);   ////Usos
        //                ini2 = prods.IndexOf("<p class=\"x4-body-text\">", x);   ////Cultivos

        //                if (ini2 > -1)
        //                {
        //                    fin2 = prods.IndexOf("</p>", ini2);

        //                    //string lin = prods.Substring(ini2 + "<p class=\"body-text\">".Length, (fin2 - (ini2 + "<p class=\"body-text\">".Length)));   ////Sustancias
        //                    //string lin = prods.Substring(ini2 + "<p class=\"x4-body-text\">".Length, (fin2 - (ini2 + "<p class=\"x4-body-text\">".Length)));   ////Usos
        //                    string lin = prods.Substring(ini2 + "<p class=\"x4-body-text\">".Length, (fin2 - (ini2 + "<p class=\"x4-body-text\">".Length)));   ////Cultivos

        //                    lin = lin.Replace(". ", "_");
        //                    lin = lin.Replace(", ", "-");
        //                    lin = filecontent.quitAccentsFileName(lin);


        //                    string[] dat = lin.Split('_');


        //                    //Sustancias
        //                    //if (dat.Length == 4)
        //                    //{
        //                    //    archivo = archivo + subs + "," + dat[0] + "," + dat[1] + "," + dat[2] + "," + dat[3] + Environment.NewLine;
                                

        //                    //}
        //                    //else if (dat.Length == 3)
        //                    //{
        //                        //archivo = archivo + subs + "," + dat[0] + "," + " ," + dat[1] + "," + dat[2] + Environment.NewLine;

        //                    //}
        //                    //else if (dat.Length == 2)
        //                    //{
        //                    //    archivo = archivo + subs + "," + dat[0] + "," + " ," + " ," + dat[1] + Environment.NewLine;
        //                    //}
        //                    //else if(dat[0].StartsWith("Combinado") || dat[0].StartsWith("Solo"))
        //                    //{
        //                    //    archivo = archivo + subs + "," + dat[0] + "," + " ," + " , " + Environment.NewLine;
        //                    //}
        //                    //else
        //                    //{
        //                    //    archivo = archivo + subs + "," + dat[0] + "," + " ," + " , " + Environment.NewLine;
        //                    //}

        //                    //Usos
        //                    //if (dat.Length == 2)
        //                    //{
        //                    //    archivo = archivo + subs + "," + dat[0] + "," + dat[1] + Environment.NewLine;
        //                    //}
        //                    //else
        //                    //    archivo = archivo + subs + "," + dat[0] + ", " + Environment.NewLine;

        //                    //Cultivos
        //                    if (dat.Length == 3)
        //                    {
        //                        archivo = archivo + subs + "," + dat[0] + "," + dat[1] + "," + dat[2] + Environment.NewLine;
        //                    }
        //                    else
        //                    {
        //                        //archivo = archivo + subs + "," + dat[0] + "," + dat[1] + "," + dat[2] + Environment.NewLine;
        //                    }

        //                    x = fin2;

        //                }
        //                else
        //                    x = prods.Length;
        //             }

                    
        //        }

        //        //filecontent.saveHtmlFile("C:/DEAQ PERU/Indices/", "Ind_Sust.txt", archivo);   ////Sustancias
        //        //filecontent.saveHtmlFile("C:/DEAQ PERU/Indices/", "Ind_Usos.csv", archivo);   ////Usos
        //        filecontent.saveHtmlFile("C:/DEAQ PERU/Indices/", "Ind_Cultivos.csv", archivo);   ////Cultivos
        //        Console.WriteLine("El archivo ha sido segmentado.");

        //        Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

        //        valid = Console.ReadLine();

        //    } while (valid.ToLower().Equals("s"));   


        //}

        static void Main(string[] args)
        {
            string valid = "";
            do
            {
                Console.WriteLine("Ingresa la ruta del archivo a revisar:");
                
                string path = Console.ReadLine();
                string archivo = "Producto,Archivo" + Environment.NewLine;

                GetHTML filecontent = new GetHTML();
                string temp = "../../IndesignTemplate.htm";
                string template = filecontent.getHtmlContent((temp));
                string allcontent = filecontent.getHtmlContent(path);

                string maq,name,name2;

                int ini, fin,next,med,i;

                next=0;

                while (next < allcontent.Length)
                {
                    ini = allcontent.IndexOf("<p class=\"x0-nombre\">", next);
                    med = allcontent.IndexOf("</p>", ini);
                    name2= name = allcontent.Substring((ini + "<p class=\"x0-nombre\">".Length), (med - (ini + "<p class=\"x0-nombre\">".Length)));

                    name2 = filecontent.quitAccentsFileName(name2);

                    fin = allcontent.IndexOf("<p class=\"x0-nombre\">", (ini + "<p class=\"x0-nombre\">".Length));

                    System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

                    if (fin > -1)
                    {
                        maq = allcontent.Substring(ini, (fin - (ini + 1)));
                        next = fin - 1;

                        maq = filecontent.quitAccents(maq);
                        filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim()));
                        sb.Replace("@@@Contenido@@@", maq);

                        if (File.Exists("C:/DEAQ PERU/Semillas/" +
                                    name2.Trim().ToLower() + ".htm"))
                        {
                            i = next;

                            filecontent.saveHtmlFile("C:/DEAQ PERU/Semillas/",
                               name2.Trim().ToLower() + "_" + i.ToString() + ".htm", sb.ToString());

                            archivo = archivo + name + "," + name2.Trim().ToLower() + "_" + i.ToString() + ".htm" + Environment.NewLine;
                        }
                        else
                        {

                            filecontent.saveHtmlFile("C:/DEAQ PERU/Semillas/",
                            name2.Trim().ToLower() + ".htm", sb.ToString());

                            archivo = archivo + name + "," + name2.Trim().ToLower() + ".htm" + Environment.NewLine;
                        }

                    }
                    else
                    {
                        maq = allcontent.Substring(ini);
                        next = allcontent.Length;

                        maq = filecontent.quitAccents(maq);
                        filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim()));
                        sb.Replace("@@@Contenido@@@", maq);

                        if (File.Exists("C:/DEAQ PERU/Semillas/" +
                                    name2.Trim().ToLower() + ".htm"))
                        {
                            i = next;

                            filecontent.saveHtmlFile("C:/DEAQ PERU/Semillas/",
                               name2.Trim().ToLower() + "_" + i.ToString() + ".htm", sb.ToString());

                            archivo = archivo + name + "," + name2.Trim().ToLower() + "_" + i.ToString() + ".htm" + Environment.NewLine;
                        }
                        else
                        {

                            filecontent.saveHtmlFile("C:/DEAQ PERU/Semillas/",
                            name2.Trim().ToLower() + ".htm", sb.ToString());

                            archivo = archivo + name + "," + name2.Trim().ToLower() + ".htm" + Environment.NewLine;
                        }

                    }

                }
                filecontent.saveHtmlFile("C:/DEAQ PERU/Semillas/", "Archivos_Semillas.csv", archivo);

                Console.WriteLine("El archivo ha sido segmentado.");

                Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

                valid = Console.ReadLine();

            } while (valid.ToLower().Equals("s")); 

        }

    }
}
