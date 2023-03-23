using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;

namespace T0NKME06.Extensions
{
    public static class ExcelExtensions
    {

        /// <summary>
        /// 獲得DisplayName所設定的名稱
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns>名稱值</returns>
        private static string GetDisplayName(this MemberInfo memberInfo)
        {
            var titleName = string.Empty;
            var attribute = memberInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
            if (attribute != null)
            {
                titleName = (attribute as DisplayNameAttribute).DisplayName;
            }
            else
            {
                //titleName = memberInfo.Name;
            }
            return titleName;
        }
        /// <summary>
        /// 獲得屬性名displayName的特性名
        /// </summary>
        /// <param name="type">類型</param>
        /// <returns>特性名的值</returns>
        private static List<string> GetPropertyDisplayNames(this Type type)
        {
            var titleList = new List<string>();
            var propertyInfos = type.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var titleName = propertyInfo.GetDisplayName();
                titleList.Add(titleName);
            }
            return titleList;
        }

        private static void SetBorderStyle(int startRow, int endRow, int startCol, int endCol, IWorkbook workBook, ISheet workSheet)
        {
            //for (int r = startRow; r <= endRow; r++)
            //{
            //    IRow row = workSheet.GetRow(r);
            //    for (int c = startCol; c <= endCol; c++)
            //    {
            //        ICellStyle style = workBook.CreateCellStyle();
            //        style.BorderBottom = BorderStyle.THIN;
            //        style.BorderLeft = BorderStyle.THIN;
            //        style.BorderRight = BorderStyle.THIN;
            //        style.BorderTop = BorderStyle.THIN;
            //        style.Alignment = HorizontalAlignment.CENTER;
            //        ICell cell = row.GetCell(c);
            //        cell.CellStyle = style;
            //        workSheet.AutoSizeColumn(c);
            //    }
            //}
            for (int r = startRow; r <= endRow; r++)
            {
                IRow row = workSheet.GetRow(r);

                for (int c = startCol; c <= endCol; c++)
                {

                    ICellStyle style = workBook.CreateCellStyle();
                    style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;


                    if (r == 1)
                    {
                        style.Alignment = HorizontalAlignment.Center;
                        //var color = Color.FromArgb(80, 124, 209);
                        //style.FillPattern = FillPatternType.SOLID_FOREGROUND;
                        //style.FillForegroundColor = HSSFColor.RoyalBlue.Index;



                        IFont font = workBook.CreateFont();
                        font.Boldweight = (short)400;
                        font.Color = HSSFColor.White.Index;
                        font.FontHeightInPoints = (short)12;
                        style.SetFont(font);

                    }
                    if (r > 1)
                    {
                        //var color = Color.FromArgb(239, 243, 251);
                        style.WrapText = true;
                        style.VerticalAlignment = VerticalAlignment.Center;
                        if (r % 2 == 0)
                        {
                            //style.FillPattern = FillPatternType.SOLID_FOREGROUND;
                            //style.FillForegroundColor = HSSFColor.LIGHT_CORNFLOWER_BLUE.index;
                        }
                    }

                    ICell cell = row.GetCell(c);
                    cell.CellStyle = style;
                    workSheet.AutoSizeColumn(c);
                }
            }
        }

        /// <summary>
        /// for Report1 use
        /// one class has lots list ,then you got sheets
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static string ExportExcel<T>(this T dataList, string fileName)
        {
            //Create workbook
            var datatype = typeof(T);
            IWorkbook workbook; //= new HSSFWorkbook();
            var extension = Path.GetFileNameWithoutExtension(fileName);
            if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = new XSSFWorkbook();
            }


            //var test = datatype.GetPropertyDisplayNamesMap();
            var subClassMapName = datatype.GetClassNameList();
            Assembly a = Assembly.Load("T0NKME06");

            foreach (var item in subClassMapName)
            {
                var dyType = a.GetType(item.Value.FullName);
                var worksheet = workbook.CreateSheet(string.Format("{0}", item.Value.DisplayName));
                var row = worksheet.CreateRow(0);
                var titleListMap = dyType.GetPropertyDisplayNamesMap();


                var cellIdx = 0;
                foreach (var subItem in titleListMap)
                {
                    var cell = row.CreateCell(cellIdx);


                    var title = subItem.Value;
                    //cell.CellStyle = fontStyle;

                    cell.SetCellValue(title.Name);

                    cellIdx++;
                }


                var ttt = datatype.GetProperty(item.Key);
                var t_type = ttt.GetType();
                var sub_dataList = (IList)ttt.GetValue(dataList, null);

                //Insert data values
                InsertDataValues(sub_dataList, workbook, worksheet, titleListMap);
                //自動篩選
                var endRange = IntToAlphabet.IndexToColumn(titleListMap.Count) + "1";
                var headerRange = CellRangeAddress.ValueOf("A1:" + endRange);
                worksheet.SetAutoFilter(headerRange);

                //自動設寬
                for (int i = 1; i < titleListMap.Count + 1; i++)
                {
                    worksheet.AutoSizeColumn(i);
                }
            }


            //Save file
            var savePath = Path.Combine(Path.GetTempPath(), fileName);
            FileStream file = new FileStream(savePath, FileMode.Create);
            workbook.Write(file);
            file.Close();

            return savePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="fileName">檔名: myExcel.xls</param>
        /// <returns>filePath</returns>
        public static string ExportExcel<T>(this IEnumerable<T> dataList, string fileName, string sheetName)
        {
            //Create workbook
            var datatype = typeof(T);

            var extension = Path.GetFileNameWithoutExtension(fileName);

            IWorkbook workbook; //= new HSSFWorkbook();

            if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = new XSSFWorkbook();
            }

            var worksheet = workbook.CreateSheet(string.Format("{0}", sheetName));
            //Insert titles
            var row = worksheet.CreateRow(0);
            var titleListMap = datatype.GetPropertyDisplayNamesMap();
            var wrapRowCount = 0;

            var cellIdx = 0;
            foreach (var item in titleListMap)
            {
                var cell = row.CreateCell(cellIdx);


                var title = item.Value;
                //cell.CellStyle = fontStyle;

                cell.SetCellValue(title.Name);

                cellIdx++;
            }

            //Insert data values
            InsertDataValues(dataList.ToList(), workbook, worksheet, titleListMap);


            //自動篩選
            var endRange = IntToAlphabet.IndexToColumn(titleListMap.Count) + "1";
            var headerRange = CellRangeAddress.ValueOf("A1:" + endRange);
            worksheet.SetAutoFilter(headerRange);

            //自動設寬
            for (int i = 1; i < titleListMap.Count + 1; i++)
            {
                worksheet.AutoSizeColumn(i);
            }

            //Save file
            var savePath = Path.Combine("C:\\", fileName);
            FileStream file = new FileStream(savePath, FileMode.Create);
            workbook.Write(file);
            file.Close();

            return savePath;
        }

        public static Dictionary<string, ClassNameValue> GetClassNameList(this Type type)
        {
            var result = new Dictionary<string, ClassNameValue>();
            var propertyInfos = type.GetProperties();
            foreach (var item in propertyInfos)
            {
                var titleName = item.GetDisplayName();
                Regex r1 = new Regex(@"(\[\[)([^,]+)");

                // C
                // Match the input and write results
                Match match = r1.Match(item.PropertyType.FullName);
                if (match.Success)
                {
                    string match_value = match.Groups[0].Value.Replace(@"[[", string.Empty);
                    if (string.IsNullOrEmpty(titleName))
                    {
                        titleName = item.Name;
                    }
                    result.Add(item.Name, new ClassNameValue { DisplayName = titleName, FullName = match_value });
                }

            }


            return result;
        }

        public class ClassNameValue
        {
            public string DisplayName { get; set; }
            public string FullName { get; set; }
        }

        /// <summary>
        /// 取得屬性的顯示名稱 (字典類)
        /// Note:因類名稱規定不能重覆，這邊就不再判斷會不會加入同樣的key
        /// 調整形態判斷規則:
        /// 如果沒有 指定特殊形態 則使用系統內建的
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, DisplayItem> GetPropertyDisplayNamesMap(this Type type)
        {
            var titleListMap = new Dictionary<string, DisplayItem>();
            var propertyInfos = type.GetProperties();
            var isEnableFormat = false;
            foreach (var propertyInfo in propertyInfos)
            {
                var titleName = propertyInfo.GetDisplayName();
                //default
                var cellType = CellType.String;

                var excelType = propertyInfo.GetCustomAttributes(typeof(ExcelDataTypeAttribute), true);
                if (excelType.Length == 1)
                {
                    var checkType = (ExcelDataTypeAttribute)excelType.FirstOrDefault();
                    if (checkType.DataType == DataType.Currency)
                    {
                        cellType = CellType.Numeric;
                        isEnableFormat = true;
                    }
                    else if (checkType.DataType == DataType.DateTime)
                    {
                        cellType = CellType.Formula;
                    }
                }
                else
                {
                    var proType = propertyInfo.PropertyType;
                    //沒有強制指定Column Type則使用預設所偵測到的反射型態
                    switch (proType.Name)
                    {

                        case "Int32":
                        case "Float":
                        case "Decimal":
                            cellType = CellType.Numeric;
                            break;
                    }
                }

                if (string.IsNullOrEmpty(titleName))
                    continue;

                titleListMap.Add(propertyInfo.Name, new DisplayItem { Name = titleName, CellType = cellType, IsEnableFormat = isEnableFormat });
            }

            return titleListMap;
        }

        /// <summary>
        /// 塞資料用的 not pretty
        /// 供上面產出Excel Data 共用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="workbook"></param>
        /// <param name="worksheet"></param>
        /// <param name="titleListMap"></param>
        private static void InsertDataValues(IList dataList, IWorkbook workbook, ISheet worksheet, Dictionary<string, DisplayItem> titleListMap)
        {

            //Insert data values
            for (int i = 1; i < dataList.Count + 1; i++)
            {
                var tmpRow = worksheet.CreateRow(i);
                var valueList = dataList[i - 1].GetPropertyValues(titleListMap);

                for (int j = 0; j < valueList.Count; j++)
                {
                    var rowCell = tmpRow.CreateCell(j);
                    var valueItem = valueList[j];
                    var tempValue = valueItem.Name;

                    switch (valueItem.CellType)
                    {
                        case CellType.Numeric:
                            if (string.IsNullOrEmpty(tempValue) || tempValue == "------" || tempValue == "--")
                            {
                                rowCell.SetCellValue("");
                            }
                            else
                            {
                                var intValue = 0.00;
                                var flag = double.TryParse(tempValue.Replace(",", string.Empty), out intValue);
                                if (flag)
                                    rowCell.SetCellValue(intValue);
                                else
                                    rowCell.SetCellValue(tempValue);
                            }

                            if (valueItem.IsEnableFormat)
                            {
                                var cellStyle = workbook.CreateCellStyle();
                                var format = workbook.CreateDataFormat();
                                cellStyle.DataFormat = format.GetFormat("#,##0.000");
                                rowCell.CellStyle = cellStyle;
                            }

                            break;
                        case CellType.Formula:
                            if (!string.IsNullOrEmpty(tempValue))
                            {
                                DateTime dateValue;
                                var flag = DateTime.TryParseExact(tempValue, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue);
                                if (flag)
                                    rowCell.SetCellValue(dateValue);
                                else
                                    rowCell.SetCellValue(tempValue);
                            }
                            else
                            {
                                rowCell.SetCellValue(tempValue);
                            }
                            var cellStyle2 = workbook.CreateCellStyle();
                            var format2 = workbook.CreateDataFormat();
                            cellStyle2.DataFormat = format2.GetFormat("yyyy/m/d");
                            rowCell.CellStyle = cellStyle2;
                            break;
                        default:
                            rowCell.SetCellValue(tempValue);
                            break;


                    }

                }

            }
        }

        /// <summary>
        /// 將T類型的公共屬性全部轉換成字符串
        /// </summary>
        /// <typeparam name="T">T類型</typeparam>
        /// <param name="data">需要轉換的對象</param>
        /// <returns>公共類型的屬性字符串集合</returns>
        private static List<string> GetPropertyValues<T>(this T data)
        {
            var properValues = new List<string>();
            var properInfos = data.GetType().GetProperties();
            foreach (var properInfoItem in properInfos)
            {
                //var value = properInfoItem.GetValue(data, null).ToString();
                var rowValue = properInfoItem.GetValue(data, null) != null ? properInfoItem.GetValue(data, null).ToString() : "";
                properValues.Add(rowValue);
            }
            return properValues;
        }

        /// <summary>
        /// 取得屬性值
        /// Note:原作者取值的方法，會有排序上的錯亂。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<RowItem> GetPropertyValues<T>(this T data, Dictionary<string, DisplayItem> columnMap)
        {
            var propertyValues = new List<RowItem>();

            var sourceData = data.GetType();
            foreach (var item in columnMap)
            {

                var propertyValue = sourceData.GetProperty(item.Key).GetValue(data, null);
                var rowValue = propertyValue != null ? propertyValue.ToString() : "";

                propertyValues.Add(new RowItem { Name = rowValue, CellType = item.Value.CellType, IsEnableFormat = item.Value.IsEnableFormat });//排除null的情況
            }

            return propertyValues;
        }
    }

    /// <summary>
    /// 自定義欄位名稱
    /// </summary>
    public class ColNames
    {
        public string RootItem { get; set; }
        public List<string> ChildrenItem { get; set; }
        public int Length { get; set; }
        public bool HasChildrenItem()
        {
            if (this.ChildrenItem == null || this.Length == 0)
            {
                Length = 0;
                return false;
            }
            else
            {
                Length = ChildrenItem.Count();
                return true;
            }
        }
        public ColNames(string root, List<string> children)
        {
            RootItem = root;
            if (children == null)
            {
                //ChildrenItem = null;
                Length = 0;
            }
            else
            {
                ChildrenItem = children;
                Length = children.Count();
            }
        }
    }

    public class ColNamesMap
    {
        public int Length { get; set; }
        public List<ColNames> colMap { get; set; }
        public ColNamesMap(List<ColNames> map)
        {
            var lenth = 0;
            colMap = map;
            foreach (var item0 in map)
            {
                if (item0.ChildrenItem != null)
                {
                    foreach (var item1 in item0.ChildrenItem)
                    {
                        lenth += 1;
                    }
                }
                else
                {
                    lenth += 1;
                }
            }
            this.Length = lenth;
        }
    }

    /// <summary>
    /// 自訂Column物件
    /// </summary>
    public class DisplayItem
    {
        public string Name { get; set; }

        public CellType CellType { get; set; }

        /// <summary>
        /// 彈性不夠 要在修
        /// </summary>
        public bool IsEnableFormat { get; set; }
    }

    /// <summary>
    /// 自訂列物件
    /// </summary>
    public class RowItem
    {
        public string Name { get; set; }

        public CellType CellType { get; set; }

        public bool IsEnableFormat { get; set; }
    }

    public class ExcelDataTypeAttribute : Attribute
    {

        public ExcelDataTypeAttribute(DataType dataType)
        {
            this.DataType = dataType;
        }
        public DataType DataType { get; set; }

    }



    public class IntToAlphabet
    {
        const int ColumnBase = 26;
        const int DigitMax = 7; // ceil(log26(Int32.Max))
        const string Digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string IndexToColumn(int index)
        {
            var result = "A";
            try
            {

                if (index <= ColumnBase)
                    return Digits[index - 1].ToString();

                var sb = new StringBuilder().Append(' ', DigitMax);
                var current = index;
                var offset = DigitMax;
                while (current > 0)
                {
                    sb[--offset] = Digits[--current % ColumnBase];
                    current /= ColumnBase;
                }
                result = sb.ToString(offset, DigitMax - offset);
            }
            catch (Exception ex)
            {


            }
            return result;
        }
    }
}
