using System;
using System.Data;
using System.Text.RegularExpressions;

namespace CommonLibrary
{
    /// <summary>
    /// 常用的基础验证
    /// </summary>
    public class BasicValidatorHelper
    {
        /// <summary>
        /// 验证传入值是否为数字（数字：000000为合法数字）
        /// </summary>
        /// <param name="input">待验证值</param>
        /// <returns>是否为数字</returns>
        public static bool IsNumeric(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return Regex.IsMatch(input, @"^[0-9]+$");
        }

        /// <summary>
        /// 验证传入值是否为自然数（大于等于0数字，数字前面不可出现+符号）
        /// </summary>
        /// <param name="input">待验证值</param>
        /// <returns>是否为自然数</returns>
        public static bool IsNaturalNum(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return Regex.IsMatch(input, @"^(0|[1-9][0-9]*)$");
        }

        /// <summary>
        ///  验证传入值是否为正整数（大于0的数字，数字前面可出现+符号）
        /// </summary> 
        /// <param name="input">待验证值</param>
        /// <returns>是否为正整数</returns>
        public static bool IsPositiveInteger(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return Regex.IsMatch(input, @"^+?[1-9][0-9]*$");
        }

        /// <summary>
        ///  验证传入值是否为负整数（小于0的数字，数字前面可出现-符号）
        /// </summary> 
        /// <param name="input">待验证值</param>
        /// <returns>是否为负整数</returns>
        public static bool IsNegativeInteger(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return Regex.IsMatch(input, @"^-?[1-9][0-9]*$");
        }

        /// <summary>
        ///  验证传入值是否Int32整数
        /// </summary>
        /// <param name="input">待验证值</param>
        /// <returns>是否为Int32整数</returns>
        public static bool IsInt32(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            int result = 0;
            return int.TryParse(input, out result);
        }

        /// <summary>
        ///  验证传入值是否Int64长整数
        /// </summary>
        /// <param name="input">待验证值</param>
        /// <returns>是否为Int64长整数</returns>
        public static bool IsInt64(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            long result = 0;
            return long.TryParse(input, out result);
        }

        /// <summary>
        ///  验证传入值是否float类型值
        /// </summary> 
        /// <param name="input">待验证值</param>
        /// <returns>是否为float类型值</returns>
        public static bool IsFloat(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            float result = 0;
            return float.TryParse(input, out result);
        }

        /// <summary>
        ///  验证传入值是否double类型值
        /// </summary>
        /// <typeparam name="T">待验证值类型</typeparam>
        /// <param name="input">待验证值</param>
        /// <returns>是否为double类型值</returns>
        public static bool IsDouble(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            double result = 0;
            return double.TryParse(input, out result);
        }

        /// <summary>
        ///  验证传入值是否decimal类型值
        /// </summary>
        /// <param name="input">待验证值</param>
        /// <returns>是否为decimal类型值</returns>
        public static bool IsDecimal(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            decimal result = 0;
            return decimal.TryParse(input, out result);
        }

        /// <summary>
        /// 判断是否为以分隔符整隔开的整数字符串
        /// 分隔符必须指定，不能为空。格式：1,2,3,4,5,6
        /// </summary>
        /// <param name="input">待字符串集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns>是否为以分隔符整隔开的整数字符串</returns>
        public static bool IsNumericArray(string input, string separator)
        {
            if (string.IsNullOrEmpty(input)) return false;
            if (Regex.IsMatch(input, @"^([-+]?[0-9]+([" + separator + "][-+]?[0-9]+)*)$") == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证是否为有效身份证号
        /// </summary>
        /// <param name="input">待验证身份证号码</param>
        /// <returns>是否为有效身份证号</returns>
        public static bool IsIDCard(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            int intLen = input.Length;
            long n = 0;

            if (intLen == 18)
            {
                if (long.TryParse(input.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(input.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(input.Remove(2)) == -1)
                {
                    return false;//省份验证
                }
                string birth = input.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证
                }
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = input.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                if (arrVarifyCode[y] != input.Substring(17, 1).ToLower())
                {
                    return false;//校验码验证
                }
                return true;//符合GB11643-1999标准
            }
            else if (intLen == 15)
            {
                if (long.TryParse(input, out n) == false || n < Math.Pow(10, 14))
                {
                    return false;//数字验证
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(input.Remove(2)) == -1)
                {
                    return false;//省份验证
                }
                string birth = input.Substring(6, 6).Insert(4, "-").Insert(2, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证
                }
                return true;//符合15位身份证标准
            }
            else
            {
                return false;//位数不对
            }
        }

        /// <summary>
        /// 是否为邮政编码
        /// </summary>
        /// <param name="input">待验证邮政编码</param>
        /// <returns>是否为邮政编码</returns>
        public static bool IsPostcode(string input)
        {
            return (IsNumeric(input) && (input.Length == 6));
        }

        /// <summary>
        /// 是否为手机号码
        /// </summary>
        /// <param name="input">需验证手机号码</param>
        /// <returns>是否为手机号码</returns>
        public static bool IsMobile(string input)
        {
            return Regex.IsMatch(input, @"^1[0-9]{10}$");
        }

        /// <summary>
        /// 验证传入IP，是否为有效IP格式
        /// </summary>
        /// <param name="ip">待验证IP</param>
        /// <returns>是否为有效IP格式</returns>
        public static bool IsIP(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 是否为URL网址
        /// </summary>
        /// <param name="input">需验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsURL(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }
        /// <summary>
        /// 验证字符串是否日期[2004-2-29|||2004-02-29 10:29:39 pm|||2004/12/31]
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsDate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            DateTime result;
            return DateTime.TryParse(input, out result);
        }

        /// <summary>
        /// 是否为时间
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }
        /// <summary>
        /// 检查内存数据库不为null，并且数据库内表个数大于0
        /// </summary>
        /// <param name="ds">内存数据库</param>
        /// <returns></returns>
        public static bool IsFillDataSetTable(DataSet ds)
        {
            return ds != null && ds.Tables != null && ds.Tables.Count > 0;
        }

        /// <summary>
        /// 检查表数据不为null，并且表数据行数个数大于0
        /// </summary>
        /// <param name="dt">表数据</param>
        /// <returns></returns>
        public static bool IsFillDataTableRow(DataTable dt)
        {
            return dt != null && dt.Rows.Count > 0;
        }
    }
}
