using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Data;
using System.Reflection;

namespace PLMExportDataComponent
{
    public class ExportDataComponent
    {
        #region Constructor
        public ExportDataComponent() { }
        #endregion

        public string getDocumentExcel(DataTable dataExport,bool includeHeaders,bool includeTitles, List<string> headerTitles, string path, string fileName,int indexColorHeader)
        {
            
            string filePath = "";
            
            Excel.Application appExcel  ;
            appExcel = new Excel.Application();
            Excel._Workbook bookExcel ;
            bookExcel= (Excel._Workbook)appExcel.Workbooks.Add(Missing.Value);
            Excel._Worksheet sheetBook = (Excel._Worksheet)bookExcel.ActiveSheet;
            Excel.Range rangeF;
            Excel.Range rangeAux1;
            Excel.Range rangeAux2;
            int fontSize = 16,column = 0;
            int countTitles = 1;
            int rowInfo = 1;
            
            if (includeTitles)
            {
                countTitles = headerTitles.Count;
                    for (int i = 1; i <= countTitles; i++)
                    {
                        sheetBook.Cells[i, 2] = headerTitles[i - 1];
                        rangeAux1 = sheetBook.Cells[i, 2];
                        rangeAux2 = sheetBook.Cells[i, 2];
                        rangeF = (Excel.Range)sheetBook.get_Range(rangeAux1, rangeAux2);
                        rangeF.Font.Size = fontSize;
                        rangeF.Font.Bold = true;
                        rangeAux2 = sheetBook.Cells[i, dataExport.Columns.Count];
                        rangeF = (Excel.Range)sheetBook.get_Range(rangeAux1, rangeAux2);
                        rangeF.Merge(true);
                        fontSize = 12;
                    }
                    countTitles = headerTitles.Count + 2;
            }
            
            if (includeHeaders)
            {
                    foreach (DataColumn dataCol in dataExport.Columns)
                    {
                        column++;
                        sheetBook.Cells[countTitles, column] = dataCol.ColumnName;
                    }

                    rangeAux1 = sheetBook.Cells[countTitles , 1];
                    rangeAux2 = sheetBook.Cells[countTitles , column];
                    rangeF = sheetBook.get_Range(rangeAux1, rangeAux2);
                    rangeF.Font.Bold = true;
                    rangeF.Font.ColorIndex = 2;
                    rangeF.Font.Size = 12;
                    rangeF.Interior.ColorIndex = indexColorHeader;
                    rowInfo = 3;
            }
            if (includeHeaders==true||includeTitles==true)
            {
                rowInfo += countTitles - 2;    
            }

            for (int i = 0; i < dataExport.Rows.Count; i++)
            {
                for (int j = 0; j < dataExport.Columns.Count; j++)
                {
                    sheetBook.Cells[rowInfo, j + 1] = dataExport.Rows[i][j].ToString();
                }
                rowInfo++;
            }

            rangeAux1 = sheetBook.Cells[1, 1];
            rangeAux2 = sheetBook.Cells[(rowInfo-1), dataExport.Columns.Count];
            rangeF = sheetBook.get_Range(rangeAux1, rangeAux2);
            rangeF.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            for (int i = 1; i <= dataExport.Columns.Count+3; i++)
            {
                rangeAux1 = sheetBook.Cells[1, i];
                sheetBook.get_Range(rangeAux1, rangeAux1).EntireColumn.AutoFit();
            }

            filePath = path + @"\" + fileName + ".xls";

            bookExcel.SaveAs(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            bookExcel.Close(Excel.XlSaveAction.xlDoNotSaveChanges, Type.Missing, Type.Missing);
            
                        try
                        {
                            foreach (System.Diagnostics.Process proceso in System.Diagnostics.Process.GetProcesses())
                            {
                                if (proceso.ProcessName.Contains("EXCEL"))
                                {
                                    proceso.Kill();
                                }
                            }
                        }
                        catch (Exception)
                        {}
            
            return filePath;
            
        }
        public string getDocumentPdf(DataTable dataExport,bool includeHeaders,bool includeTitles, List<string> headerTitles, string path, string fileName){
            string filePath = "";
            object miss = System.Reflection.Missing.Value;
            object end = "\\endofdoc";
            Word.Application appWord = new Word.Application();
            Word.Document docWord;
            docWord=appWord.Documents.Add(miss, miss, miss, miss);
            Word.Paragraph paragWord;
            paragWord = docWord.Content.Paragraphs.Add(miss);
            int fontSize = 16, column = 0, rowInfo = 1,head=0 ;
            if (includeTitles)
            {
                for (int i = 0; i < headerTitles.Count; i++)
                {
                    paragWord.Range.Text = headerTitles[i];
                    paragWord.Range.Font.Bold = 1;
                    paragWord.Range.Font.Size = fontSize;
                    paragWord.Format.SpaceAfter = 6;
                    paragWord.Range.InsertParagraphAfter();
                    fontSize = 12;
                }
            }

            if (includeHeaders)
            {
                head = 1;
            }
            object oRng = docWord.Bookmarks.get_Item(ref end).Range;
            Word.Table tableWord;
            Word.Range rangeWord = docWord.Bookmarks.get_Item(ref end).Range;
            tableWord = docWord.Tables.Add(rangeWord, dataExport.Rows.Count + head, dataExport.Columns.Count, ref miss, ref miss);    
            tableWord.Range.ParagraphFormat.SpaceAfter = 6;
            if (includeHeaders)
            {
                foreach (DataColumn dataCol in dataExport.Columns)
                {
                  column++;
                  tableWord.Cell(rowInfo, column).Range.Text = dataCol.ColumnName;
                }
                rowInfo += 1;
                tableWord.Rows[1].Range.Font.Bold = 1;
                tableWord.Rows[1].Range.Font.Italic = 1;
            }

            for (int i = 0; i < dataExport.Rows.Count; i++)
            {
                for (int j = 0; j < dataExport.Columns.Count; j++)
                {
                  tableWord.Cell(i + rowInfo, j + 1).Range.Text = dataExport.Rows[i][j].ToString();
                }
            }
           

            for (int i = rowInfo; i <= dataExport.Rows.Count + 1; i++)
            {
                tableWord.Rows[i].Range.Font.Bold = 0;
            }
            tableWord.Borders[Word.WdBorderType.wdBorderVertical].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderRight].LineStyle =Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderHorizontal].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
             filePath = path + @"\" + fileName + ".pdf";
            docWord.ExportAsFixedFormat(filePath, Word.WdExportFormat.wdExportFormatPDF);
            docWord.Close(Word.WdSaveOptions.wdDoNotSaveChanges, Type.Missing, Type.Missing);
                    try
                    {
                        foreach (System.Diagnostics.Process proceso in System.Diagnostics.Process.GetProcesses())
                        {
                            if (proceso.ProcessName.Contains("WORD"))
                            {
                                      proceso.Kill();
                            }
                        }
                    }
                    catch (Exception)
                    { }
            
         
            return filePath;
         }
        public string getDocumentPdf2(DataTable dataExport, bool includeHeaders, bool includeTitles, List<string> headerTitles, string path, string fileName)
        {
            string filePath = "";
            object miss = System.Reflection.Missing.Value;
            object end = "\\endofdoc";
            Word.Application appWord = new Word.Application();
            Word.Document docWord;
            docWord = appWord.Documents.Add(miss, miss, miss, miss);
            Word.Paragraph paragWord;
            paragWord = docWord.Content.Paragraphs.Add(miss);
            int fontSize = 16, column = 0, rowInfo = 1, head = 0;
            if (includeTitles)
            {
                for (int i = 0; i < headerTitles.Count; i++)
                {
                    paragWord.Range.Text = headerTitles[i];
                    paragWord.Range.Font.Bold = 1;
                    paragWord.Range.Font.Size = fontSize;
                    paragWord.Format.SpaceAfter = 6;
                    paragWord.Range.InsertParagraphAfter();
                    fontSize = 12;
                }
            }

            if (includeHeaders)
            {
                head = 1;
            }
            object oRng = docWord.Bookmarks.get_Item(ref end).Range;
            Word.Table tableWord;
            Word.Range rangeWord = docWord.Bookmarks.get_Item(ref end).Range;
            tableWord = docWord.Tables.Add(rangeWord, dataExport.Rows.Count + head, dataExport.Columns.Count, ref miss, ref miss);
            tableWord.Range.ParagraphFormat.SpaceAfter = 6;
            if (includeHeaders)
            {
                foreach (DataColumn dataCol in dataExport.Columns)
                {
                    column++;
                    tableWord.Cell(rowInfo, column).Range.Text = dataCol.ColumnName;
                }
                rowInfo += 1;
                tableWord.Rows[1].Range.Font.Bold = 1;
                tableWord.Rows[1].Range.Font.Italic = 1;
            }

            for (int i = 0; i < dataExport.Rows.Count; i++)
            {
                for (int j = 0; j < dataExport.Columns.Count; j++)
                {
                    tableWord.Cell(i + rowInfo, j + 1).Range.Text = dataExport.Rows[i][j].ToString();
                }
            }


            for (int i = rowInfo; i <= dataExport.Rows.Count + 1; i++)
            {
                tableWord.Rows[i].Range.Font.Bold = 0;
            }
            tableWord.Borders[Word.WdBorderType.wdBorderVertical].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderHorizontal].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tableWord.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            filePath = path + @"\" + fileName + ".pdf";
            docWord.ExportAsFixedFormat(filePath, Word.WdExportFormat.wdExportFormatPDF);
            docWord.Close(Word.WdSaveOptions.wdDoNotSaveChanges, Type.Missing, Type.Missing);
            try
            {
                foreach (System.Diagnostics.Process proceso in System.Diagnostics.Process.GetProcesses())
                {
                    if (proceso.ProcessName.Contains("WORD"))
                    {
                        proceso.Kill();
                    }
                }
            }
            catch (Exception)
            { }


            return filePath;
        }
      
    }
}
 