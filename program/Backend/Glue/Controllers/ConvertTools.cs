using System;
using System.Globalization;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Glue.Controllers
{
    public class ConvertTools
    {
        public static DateTime? StringConvertToDate(string dateString)
        {
            string[] formats = { "yyyy-M-d", "yyyy-MM-dd",
                "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-M-dTHH:mm:ss.fffZ",
                "yyyy/M/d H:mm:ss"};
            if (DateTime.TryParseExact(dateString, formats, null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                // 'parsedDate' now holds the DateTime value
                //Console.WriteLine(parsedDate.ToString()); // Print the parsed date
                return parsedDate;
            }
            else
            {
                return null;
            }
        }
        public static string DataTableToJson(DataTable table)
        {
            var jsonString = new StringBuilder();

            if (table.Rows.Count > 0)
            {
                jsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Append("{");

                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        jsonString.AppendFormat("\"{0}\":\"{1}\"",
                            table.Columns[j].ColumnName,
                            table.Rows[i][j]);

                        if (j < table.Columns.Count - 1)
                        {
                            jsonString.Append(",");
                        }
                    }

                    jsonString.Append("}");
                    if (i < table.Rows.Count - 1)
                    {
                        jsonString.Append(",");
                    }
                }
                jsonString.Append("]");
            }

            return jsonString.ToString();
        }
        /* 以"￥"结尾（或没有）的字符串转换为double */
        public static bool ConvertCurrencyStringToDouble(string currencyString, out double result)
        {
            result = 0;
            if (double.TryParse(currencyString, out result))
            {
                return true;
            }
            int currencySymbolIndex = currencyString.IndexOf("￥");
            if (currencySymbolIndex != -1 && currencySymbolIndex == currencyString.Length - 1)
            {
                currencyString = currencyString.Replace("￥", "");
                if (double.TryParse(currencyString, out result))
                {
                    return true;
                }
            }
            return false;
        }
        /* 以"小时"/"时"/"h"/"H"结尾（或没有）的字符串转换为double */
        public static bool ConvertHourStringToDouble(string hourString, out double result)
        {
            result = 0;
            //匹配数字（整数或小数），可以有：小时/时/h/H结尾（也可以没有）
            string pattern = @"^(\d+(?:\.\d+)?)(?:小时|时|h|H)?$";

            Match match = Regex.Match(hourString, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string numericPart = match.Groups[1].Value;
                if (double.TryParse(numericPart, out result))
                {
                    return true;
                }
            }

            // If no match or conversion failed, return a default value (e.g., 0)
            return false;
        }
    }
}
