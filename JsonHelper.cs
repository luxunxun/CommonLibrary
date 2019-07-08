using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using fastJSON;
using Nancy.Json;

namespace CommonLibrary
{
    /// <summary>
    /// JSON操作类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 快速将对象转为JSON
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string FastToJson(object obj)
        {
            return JSON.ToJSON(obj, new JSONParameters { EnableAnonymousTypes = true, UseEscapedUnicode = true });
        }
        /// <summary>
        /// 快速将对象转为JSON
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="prams">附加属性</param>
        /// <returns></returns>
        public static string FastToJson(object obj, JSONParameters prams)
        {
            return JSON.ToJSON(obj, prams);
        }
        /// <summary>
        /// 快速将JSON转为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static T FastToObject<T>(string json) where T : class
        {
            return JSON.ToObject<T>(json);
        }
        /// <summary>
        /// 将指定的 JSON 字符串转换为 dynamic 类型的对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static dynamic JsonToDynamic(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            return jss.DeserializeObject(json) as dynamic;
        }
        /// <summary>
        /// 将对象转换为 JSON 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetJson<T>(T input)
        {
            string rtn = string.Empty;
            if (input == null) return rtn;
            JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();
            JsonSerializer.RegisterConverters(new JavaScriptConverter[] { new DataTableConverter() });
            rtn = JsonSerializer.Serialize(input);
            rtn = Regex.Replace(rtn, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
            if (!string.IsNullOrEmpty(rtn) && typeof(T) == typeof(System.Data.DataTable))
            {
                Dictionary<string, object> dictionary = ParseFromJson<Dictionary<string, object>>(rtn);
                return GetJson<object>(dictionary["Rows"]);
            }
            return rtn;
        }

        /// <summary>
        /// 将指定的 JSON 字符串转换为 T 类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T ParseFromJson<T>(string input)
        {
            T rtn = default(T);
            if (string.IsNullOrEmpty(input)) return rtn;
            JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();
            JsonSerializer.RegisterConverters(new JavaScriptConverter[] { new DataTableConverter() });
            if (!string.IsNullOrEmpty(input) && typeof(T) == typeof(System.Data.DataTable))
            {
                return JsonSerializer.Deserialize<T>(string.Format("{{\"Rows\":{0}}}", input));
            }
            else
            {
                rtn = JsonSerializer.Deserialize<T>(input);
            }
            return rtn;
        }
        /// <summary>
        /// 获取JSON字符串的键值
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static string GetJsonValue(string json, string key)
        {
            if (string.IsNullOrEmpty(json) || !json.StartsWith("{") || !json.EndsWith("}")) return string.Empty;
            Dictionary<string, object> dictionary = JsonHelper.ParseFromJson<Dictionary<string, object>>(json);
            if (dictionary == null || dictionary.Count == 0) return string.Empty;
            object returnValue = null;
            foreach (KeyValuePair<string, object> dic in dictionary)
            {
                if (string.IsNullOrEmpty(dic.Key)) break;
                if (dic.Key == key)
                {
                    if (dic.Value.GetType() == typeof(Dictionary<string, object>))
                    {
                        returnValue = JsonHelper.GetJson<Dictionary<string, object>>((Dictionary<string, object>)dic.Value);
                    }
                    else
                    {
                        returnValue = dic.Value;
                    }
                    break;
                }
            }
            if (returnValue == null)
            {
                return string.Empty;
            }
            if (returnValue.GetType() == typeof(ArrayList))
            {
                return string.Empty;
            }
            return returnValue.ToString();
        }
        /// <summary>
        /// 获取JSON字符串的键值
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static object GetJsonObjectValue(string json, string key)
        {
            if (string.IsNullOrEmpty(json) || !json.StartsWith("{") || !json.EndsWith("}")) return string.Empty;
            Dictionary<string, object> dictionary = JsonHelper.ParseFromJson<Dictionary<string, object>>(json);
            if (dictionary == null || dictionary.Count == 0) return string.Empty;
            object returnValue = null;
            foreach (KeyValuePair<string, object> dic in dictionary)
            {
                if (dic.Key == key)
                {
                    if (dic.Value.GetType() == typeof(Dictionary<string, object>))
                    {
                        returnValue = JsonHelper.GetJson<Dictionary<string, object>>((Dictionary<string, object>)dic.Value);
                    }
                    else
                    {
                        returnValue = dic.Value;
                    }
                    break;
                }
            }
            if (returnValue == null)
            {
                return string.Empty;
            }
            if (returnValue.GetType() == typeof(System.Collections.ArrayList))
            {
                return returnValue;
            }
            return returnValue.ToString();
        }
        /// <summary>
        /// 获取第一个KEY-VALUE的值
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string GetJsonValue(string json)
        {
            if (string.IsNullOrEmpty(json) || !json.StartsWith("{") || !json.EndsWith("}")) return string.Empty;
            Dictionary<string, object> dictionary = JsonHelper.ParseFromJson<Dictionary<string, object>>(json);
            if (dictionary == null || dictionary.Count == 0) return string.Empty;
            object returnValue = null;
            foreach (KeyValuePair<string, object> dic in dictionary)
            {
                if (string.IsNullOrEmpty(dic.Key)) break;
                if (dic.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    returnValue = JsonHelper.GetJson<Dictionary<string, object>>((Dictionary<string, object>)dic.Value);
                }
                else
                {
                    returnValue = dic.Value;
                }
                break;
            }
            if (returnValue == null)
            {
                return string.Empty;
            }
            if (returnValue.GetType() == typeof(System.Collections.ArrayList))
            {
                return string.Empty;
            }
            return returnValue.ToString();
        }
    }

    public class DataTableConverter : JavaScriptConverter
    {
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataTable dt = obj as DataTable;
            Dictionary<string, object> result = new Dictionary<string, object>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    row.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                rows.Add(row);
            }
            result["Rows"] = rows;
            return result;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            DataTable dt = new DataTable();
            ArrayList array = dictionary["Rows"] as ArrayList;
            for (int i = 0; i < array.Count; i++)
            {
                Dictionary<string, object> info = array[i] as Dictionary<string, object>;
                if (info == null || info.Count == 0) return null;
                if (i == 0)
                {
                    foreach (KeyValuePair<string, object> dic in info)
                    {
                        dt.Columns.Add(dic.Key, dic.Value.GetType());
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (KeyValuePair<string, object> dic in info)
                {
                    dr[dic.Key] = dic.Value.ToString();
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 获取本转换器支持的类型
        /// </summary>
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(DataTable) }; }
        }
    }

    #region DynamicJsonConverter
    public class DynamicJsonConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(object))
            {
                return new DynamicJsonObject(dictionary);
            }

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
        }
    }

    public class DynamicJsonObject : DynamicObject
    {
        private IDictionary<string, object> Dictionary { get; set; }

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.Dictionary[binder.Name];

            if (result is IDictionary<string, object>)
            {
                result = new DynamicJsonObject(result as IDictionary<string, object>);
            }
            else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
            {
                result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
            }
            else if (result is ArrayList)
            {
                result = new List<object>((result as ArrayList).ToArray());
            }

            return this.Dictionary.ContainsKey(binder.Name);
        }
    }
    #endregion 
}
