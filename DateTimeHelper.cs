using System;

namespace CommonLibrary
{
    /// <summary>
    /// 常用时间操作类
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime ConvertTimeByUnix(object timeStamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long time = long.Parse(string.Concat(timeStamp, "0000000"));
            TimeSpan timeSpan = new TimeSpan(time);
            return startTime.Add(timeSpan);
        }
        /// <summary>
        /// 将时间转为时间戳
        /// </summary>
        /// <param name="dateTime">需要转换的时间</param>
        /// <returns></returns>
        public static int ConvertUnixByTime(DateTime dateTime)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(dateTime - startTime).TotalSeconds;
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="unixTime">Unix时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimeToDateTime(object unixTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long time = long.Parse(string.Concat(unixTime, "0000000"));
            TimeSpan timeSpan = new TimeSpan(time);
            return startTime.Add(timeSpan);
        }

        /// <summary>
        /// 将DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns> Unix时间戳</returns>
        public static int DateTimeToUnixTime(DateTime dateTime)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(dateTime - startTime).TotalSeconds;
        }

        /// <summary>
        /// 将日期转换为数字
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>数字</returns>
        public static int DateToInt(DateTime date)
        {
            string strDate = date.ToString("yyyyMMdd");
            return int.Parse(strDate);
        }

        /// <summary>
        /// 将日期字符串转换为数字
        /// </summary>
        /// <param name="strDate">日期字符串</param>
        /// <returns>数字</returns>
        public static int DateToInt(string strDate)
        {
            if (BasicValidatorHelper.IsDate(strDate) == false) return 0;
            DateTime date = Convert.ToDateTime(strDate);
            strDate = date.ToString("yyyyMMdd");
            return int.Parse(strDate);
        }

        /// <summary>
        /// 将时间字符串转换为数字
        /// </summary>
        /// <param name="strDate">日期字符串</param>
        /// <returns>数字</returns>
        public static int TimeToInt(DateTime time)
        {
            string strTime = time.ToString("HHmmss");
            return int.Parse(strTime);
        }

        /// <summary>
        /// 将数字转换为日期字符串
        /// </summary>
        /// <param name="intDate">数字</param>
        /// <returns>日期字符串</returns>
        public static string IntToStr(int intDate)
        {
            string strDate = intDate.ToString("####-##-##");
            return strDate;
        }

        /// <summary>
        /// 将数字转换为日期
        /// </summary>
        /// <param name="intDate">数字</param>
        /// <returns>日期</returns>
        public static DateTime IntToDate(int intDate)
        {
            string strDate = intDate.ToString("####-##-##");
            DateTime date = Convert.ToDateTime(strDate);
            return date;
        }

        /// <summary>
        /// 将数字转换为时间
        /// </summary>
        /// <param name="intDate">数字</param>
        /// <returns>时间</returns>
        public static DateTime IntToTime(int intTime)
        {
            string strDate = intTime.ToString().PadLeft(6, '0');
            DateTime date = Convert.ToDateTime(strDate.Substring(0, 2) + ":" + strDate.Substring(2, 2) + ":" + strDate.Substring(4, 2));
            return date;
        }

        /// <summary>
        /// 整型转换成时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime IntToDateTime(int date, int time)
        {
            string intTime = time.ToString().PadLeft(6, '0');
            return Convert.ToDateTime(string.Format("{0:####-##-##} {1}:{2}:{3}", date, intTime.Substring(0, 2), intTime.Substring(2, 2), intTime.Substring(4, 2)));
        }

        /// <summary>
        /// 当前日期加减n天后的日期
        /// </summary>
        /// <param name="days">天数</param>
        /// <returns>日期</returns>
        public static DateTime DateAfterToday(int days)
        {
            return DateTime.Today.AddDays(days);
        }

        /// <summary>
        /// 给定一个日期，返回加减n天后的日期
        /// </summary>
        /// <param name="date">给定日期</param>
        /// <param name="days">天数</param>
        /// <returns>加减后的日期</returns>
        public static DateTime DateAfterOneDate(DateTime date, int days)
        {
            return date.AddDays(days);
        }

        /// <summary>
        /// 计算两个日期相隔的天数
        /// </summary>
        /// <param name="date1">开始日期</param>
        /// <param name="date2">结束日期</param>
        /// <returns>天数</returns>
        public static int DaysBetweenTwoDate(DateTime date1, DateTime date2)
        {
            TimeSpan ts = date2 - date1;
            return ts.Days;
        }

        /// <summary>
        /// 计算两个日期相隔的天数
        /// </summary>
        /// <param name="strDate1">开始日期</param>
        /// <param name="strDate2">结束日期</param>
        /// <returns>天数</returns>
        public static int DaysBetweenTwoDate(string strDate1, string strDate2)
        {
            DateTime date1 = Convert.ToDateTime(strDate1);
            DateTime date2 = Convert.ToDateTime(strDate2);
            TimeSpan ts = date2 - date1;
            return ts.Days;
        }

        /// <summary>
        /// 计算两个日期相隔的天数
        /// </summary>
        /// <param name="intDate1">开始日期</param>
        /// <param name="intDate2">结束日期</param>
        /// <returns>天数</returns>
        public static int DaysBetweenTwoDate(int intDate1, int intDate2)
        {
            DateTime date1 = IntToDate(intDate1);
            DateTime date2 = IntToDate(intDate2);
            TimeSpan ts = date2 - date1;
            return ts.Days;
        }

        /// <summary>
        /// 给定一个日期,返回当月的第一天
        /// </summary>
        /// <param name="date">给定日期</param>
        /// <returns>当月的第一天</returns>
        public static int GetMonthFirstDate(DateTime date)
        {
            return int.Parse(Convert.ToDateTime(date.ToString("yyyy-MM-") + "01").ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 给定一个日期,返回当月的最后一天
        /// </summary>
        /// <param name="date">给定日期</param>
        /// <returns>当月的最后一天</returns>
        public static int GetMonthLastDate(DateTime date)
        {
            return int.Parse(Convert.ToDateTime(date.ToString("yyyy-MM-") + "01").AddMonths(1).AddDays(-1).ToString("yyyyMMdd"));
        }

        /// <summary>        
        /// 格式化显示时间为几个月,几天前,几小时前,几分钟前,或几秒前        
        /// </summary>        
        /// <param name="date">要格式化显示的时间</param>        
        /// <returns>几个月,几天前,几小时前,几分钟前,或几秒前</returns>        
        public static string DateStringFromNow(DateTime date)
        {
            TimeSpan span = DateTime.Now - date;
            if (span.TotalDays > 60) { return date.ToShortDateString(); }
            else if (span.TotalDays > 30) { return "1个月前"; }
            else if (span.TotalDays > 14) { return "2周前"; }
            else if (span.TotalDays > 7) { return "1周前"; }
            else if (span.TotalDays > 1) { return string.Format("{0}天前", (int)Math.Floor(span.TotalDays)); }
            else if (span.TotalHours > 1) { return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours)); }
            else if (span.TotalMinutes > 1) { return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes)); }
            else if (span.TotalSeconds >= 1) { return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds)); }
            else { return "1秒前"; }
        }
        /// <summary>
        /// 得到本周第一天(以星期一为第一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>
        /// 得到本周最后一天(以星期六为最后一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekLastDaySat(DateTime datetime)
        {
            //星期六为最后一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (7 - weeknow) - 1;

            //本周最后一天
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }
    }
}
