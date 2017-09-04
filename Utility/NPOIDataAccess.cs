using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utility
{
    public static class NPOIDataAccess
    {
        /// <summary>
        /// 读取excel 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName)
        {
            DataTable dt = new DataTable();
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheetAt(0);
            if (sheet.LastRowNum != 0)
                dt = ImportDt(sheet, 0, true);
            RemoveEmptyRow(dt);
            return dt;
        }
        static DataTable ImportDt(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable table = new DataTable();
            IRow headerRow;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                DataColumn column = new DataColumn(Convert.ToString(i));
                                table.Columns.Add(column);
                            }

                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        IRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i);
                        }
                        else
                        {
                            row = sheet.GetRow(i);
                        }

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j <= cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;
                                        case CellType.Formula:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.String:
                                                    string strFORMULA = row.GetCell(j).StringCellValue;
                                                    if (strFORMULA != null && strFORMULA.Length > 0)
                                                    {
                                                        dataRow[j] = strFORMULA.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.Numeric:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.Boolean:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.Error:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;
                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                throw;
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch (Exception exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception exception)
            {
                throw;
            }
            return table;
        }
        public static List<string> getSheetName(string strFileName)
        {
            List<string> list = new List<string>();
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            for (int i = 0; i < wb.NumberOfSheets; i++)
            {
                ISheet sheet = wb.GetSheetAt(i);
                list.Add(sheet.SheetName);
            }

            return list;
        }
        /// <summary>
        /// 移除导入时模版产生的空行数据
        /// </summary>
        /// <param name="source"></param>
        private static void RemoveEmptyRow(DataTable source)
        {
            if (null != source && source.Rows.Count > 0)
            {
                DataRow row = source.Rows[source.Rows.Count - 1];
                string rowValue = string.Empty;
                foreach (DataColumn col in source.Columns)
                {
                    rowValue += row[col.ColumnName].ToString();
                }

                if (rowValue == string.Empty)
                {
                    source.Rows.Remove(row);
                    RemoveEmptyRow(source);
                }
            }
        }
        public static void ExcelOut(DataTable dt, string fullXlsFilePath)
        {
            //创建Excel文件的对象
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            NPOI.SS.UserModel.IRow headRow = sheet1.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                headRow.CreateCell(i).SetCellValue(dt.Columns[i].ToString());
            }

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    rowtemp.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());

                }
            }

            //加上设置大小下载下来的.xlsx文件打开时才没有错误
            FileStream fileStream = new FileStream(fullXlsFilePath, FileMode.Create, FileAccess.Write);
            book.Write(fileStream);//调用这个后会关于文件流，在HSSFWorkbook不会关闭所以在处理时应注意               
            FileStream fs = new FileStream(fullXlsFilePath, FileMode.Open, FileAccess.Read);
            long fileSize = fs.Length;
            //加上设置大小下载下来的.xlsx文件打开时才没有错误
            HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            byte[] fileBuffer = new byte[fileSize]; fs.Read(fileBuffer, 0, (int)fileSize); HttpContext.Current.Response.BinaryWrite(fileBuffer); fs.Close();
             
        }
        /// <summary>
        /// 动态表头导出
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="list"></param>
        /// <param name="fullXlsFilePath"></param>
        public static void ExcelOut(DataTable dt, Dictionary<string, string> list, string fullXlsFilePath)
        {
            if (!System.IO.Directory.Exists(fullXlsFilePath))
            {
                var str = fullXlsFilePath.Substring(0, fullXlsFilePath.LastIndexOf('\\'));

                System.IO.Directory.CreateDirectory(str);
            }
            //创建Excel文件的对象
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            NPOI.SS.UserModel.IRow headRow = sheet1.CreateRow(0);
            int headnum = 0;
            foreach (var item in list)
            {
                headRow.CreateCell(headnum).SetCellValue(item.Value);
                headnum++;
            }
            int num = 1;
            foreach (DataRow item in dt.Rows)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(num);
                int connum = 0;
                foreach (var key in list)
                {
                    rowtemp.CreateCell(connum).SetCellValue(item[key.Key] == DBNull.Value ? "" : item[key.Key].ToString());
                    connum++;
                }

                num++;
            }

            //加上设置大小下载下来的.xlsx文件打开时才没有错误
            FileStream fileStream = new FileStream(fullXlsFilePath, FileMode.Create, FileAccess.Write);
            book.Write(fileStream);//调用这个后会关于文件流，在HSSFWorkbook不会关闭所以在处理时应注意               
            FileStream fs = new FileStream(fullXlsFilePath, FileMode.Open, FileAccess.Read);
            long fileSize = fs.Length;
            //加上设置大小下载下来的.xlsx文件打开时才没有错误
            HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            byte[] fileBuffer = new byte[fileSize]; fs.Read(fileBuffer, 0, (int)fileSize); HttpContext.Current.Response.BinaryWrite(fileBuffer); fs.Close();

        }
        /// <summary>
        /// 向excel添加图片
        /// </summary>
        /// <param name="XlsFilePath"></param>
        /// <param name="pictureData"></param>
        /// <param name="sheetName"></param>
        public static void AddPicture(string XlsFilePath,byte[] pictureData,bool isNewSheet,string sheetName= "PicTure")
        {
            FileStream fs = File.OpenRead(XlsFilePath);
            //IWorkbook workbook = new XSSFWorkbook(fs);
            
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook(fs); // (NPOI.XSSF.UserModel.XSSFWorkbook)WorkbookFactory.Create(XlsFilePath);
            fs.Close();
            int pictureIdx=  book.AddPicture(pictureData, PictureType.JPEG);
            NPOI.SS.UserModel.ISheet sheet = null;
            if (!isNewSheet)
            {
                sheet = book.GetSheetAt(0);
            }
            else
            {
                sheet = book.CreateSheet(sheetName);
            }
            IDrawing patriarch = sheet.CreateDrawingPatriarch();
            NPOI.XSSF.UserModel.XSSFClientAnchor anchor = new NPOI.XSSF.UserModel.XSSFClientAnchor(0, 0, 1023, 0, 0, 0, 1, 3);
            IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
            pict.Resize();
          
            using (FileStream fileStream = File.Open(XlsFilePath,
            FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                book.Write(fileStream);
                fileStream.Close();
            }
        }
        /// <summary>
        /// 导出多个sheet的Excel
        /// </summary>
        /// <param name="dt1">数据源1</param>
        /// <param name="dt2">数据源2</param>
        /// <param name="list1">需要导出字段1</param>
        /// <param name="list2">需要导出字段2</param>
        /// <param name="sheet1Name">sheet1名称</param>
        /// <param name="sheet2Name">sheet2名称</param>
        /// <param name="fullXlsFilePath">路径</param>
        public static void ExcelNSheetOut(DataTable dt1,DataTable dt2, Dictionary<string, string> list1, Dictionary<string, string> list2,string sheet1Name,string sheet2Name, string fullXlsFilePath)
        {
            if (!Directory.Exists(fullXlsFilePath))
            {
                var str = fullXlsFilePath.Substring(0, fullXlsFilePath.LastIndexOf('\\'));
                Directory.CreateDirectory(str);
            }
            //创建Excel文件的对象
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();
            //添加第一个sheet
            ISheet sheet1 = book.CreateSheet(sheet1Name);
            IRow headRow = sheet1.CreateRow(0);
            int headnum = 0;
            foreach (var item in list1)
            {
                headRow.CreateCell(headnum).SetCellValue(item.Value);
                headnum++;
            }
            int num = 1;
            foreach (DataRow item in dt1.Rows)
            {
                IRow rowtemp = sheet1.CreateRow(num);
                int connum = 0;
                foreach (var key in list1)
                {
                    rowtemp.CreateCell(connum).SetCellValue(item[key.Key] == DBNull.Value ? "" : item[key.Key].ToString());
                    connum++;
                }
                num++;
            }
            //添加第二个sheet
            ISheet sheet2 = book.CreateSheet(sheet2Name);
            IRow headRow2 = sheet2.CreateRow(0);
            int headnum2 = 0;
            foreach (var item in list2)
            {
                headRow2.CreateCell(headnum2).SetCellValue(item.Value);
                headnum2++;
            }
            int num2 = 1;
            foreach (DataRow item in dt2.Rows)
            {
                IRow rowtemp2 = sheet2.CreateRow(num2);
                int connum2 = 0;
                foreach (var key in list2)
                {
                    rowtemp2.CreateCell(connum2).SetCellValue(item[key.Key] == DBNull.Value ? "" : item[key.Key].ToString());
                    connum2++;
                }
                num2++;
            }
            //end 第二个sheet
            //加上设置大小下载下来的.xlsx文件打开时才没有错误
            FileStream fileStream = new FileStream(fullXlsFilePath, FileMode.Create, FileAccess.Write);
            book.Write(fileStream);//调用这个后会关于文件流，在HSSFWorkbook不会关闭所以在处理时应注意               
            FileStream fs = new FileStream(fullXlsFilePath, FileMode.Open, FileAccess.Read);
            long fileSize = fs.Length;
            //加上设置大小下载下来的.xlsx文件打开时才没有错误
            HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            byte[] fileBuffer = new byte[fileSize]; fs.Read(fileBuffer, 0, (int)fileSize); HttpContext.Current.Response.BinaryWrite(fileBuffer); fs.Close();
        }

    }
}
