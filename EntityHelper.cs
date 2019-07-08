using System;
using System.Collections.Generic;
using System.Reflection;

namespace CommonLibrary
{
    /// <summary>
    /// 实体对象辅助类
    /// </summary>
    public class EntityHelper
    {
        /// <summary>
        /// 从一个实体类中，复制数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyEntity<T, S>(T destination, S source)
        {
            return CopyEntity(destination, source, source.GetType(), null);
        }

        /// <summary>
        /// 从一个实体类中，复制数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <param name="excludeName">排除下列名称的属性或字段不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyEntity<T, S>(T destination, S source, Type templateType)
        {
            return CopyEntity(destination, source, templateType, null);
        }

        /// <summary>
        /// 从一个实体类中，复制数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <param name="templateType">复制的属性字段模板</param>
        /// <param name="excludeName">排除下列名称的属性或字段不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyEntity<T, S>(T destination, S source, Type templateType, IList<string> excludeName)
        {
            int count = 0;
            count += CopyEntityFields(destination, source, templateType, excludeName);
            count += CopyEntityProperties(destination, source, templateType, excludeName);

            return count;
        }

        /// <summary>
        /// 从一个实体类中，把实体属性字段数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyEntityFields<T, S>(object destination, S source)
        {
            return CopyEntityFields(destination, source, source.GetType(), null);
        }

        /// <summary>
        /// 从一个实体类中，把实体属性字段数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <param name="excludeName">排除下列名称的字段不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyEntityFields<T, S>(object destination, S source, Type templateType)
        {
            return CopyEntityFields(destination, source, templateType, null);
        }

        /// <summary>
        /// 从一个实体类中，把实体属性字段数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <param name="templateType">复制的字段模板</param>
        /// <param name="excludeName">排除下列名称的字段不要复制</param>
        /// <returns>成功复制的值个数</returns>
        private static int CopyEntityFields<T, S>(T destination, S source, Type templateType, IList<string> excludeName)
        {
            if (destination == null || source == null)
                return 0;
            if (excludeName == null)
                excludeName = new List<string>();
            //复制成功数
            int count = 0;
            //目标类型
            Type desType = destination.GetType();
            //逐一进行复制处理
            foreach (FieldInfo mField in templateType.GetFields())
            {
                if (excludeName.Contains(mField.Name))
                    continue;
                try
                {
                    FieldInfo des = desType.GetField(mField.Name);
                    if (des != null && des.FieldType == mField.FieldType)
                    {
                        des.SetValue(destination, mField.GetValue(source));
                        count++;
                    }
                }
                catch
                {
                }
            }
            return count;
        }

        /// <summary>
        /// 从一个实体类中，把实体属性复制数据到另外一个新建的实体类
        /// </summary>
        /// <param name="source">来源</param>
        /// <returns>成功复制的值个数大于0返回新建的实体类，不大于0返回null</returns>
        public static TResult CopyNewEntityProperties<S, TResult>(S source) where S : class where TResult : class, new()
        {
            if (source != null)
            {
                TResult info = new TResult();
                return CopyEntityProperties(info, source) > 0 ? info : null;
            }

            return null;
        }

        /// <summary>
        /// 从一个实体类中，把实体属性复制数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyEntityProperties<T, S>(T destination, S source)
        {
            return CopyEntityProperties(destination, source, source.GetType(), null);
        }

        /// <summary>
        /// 从一个实体类中，把实体属性复制数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyEntityProperties<T, S>(T destination, S source, Type templateType)
        {
            return CopyEntityProperties(destination, source, templateType, null);
        }

        /// <summary>
        /// 从一个实体类中，把实体属性复制数据到另外一个实体类
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <param name="templateType">复制的属性模板</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        private static int CopyEntityProperties<T, S>(T destination, S source, Type templateType, IList<string> excludeName)
        {
            if (destination == null || source == null)
                return 0;

            if (excludeName == null)
                excludeName = new List<string>();

            //复制成功数
            int count = 0;
            //目标类型
            Type desType = destination.GetType();
            //逐一进行复制处理
            foreach (PropertyInfo mProperty in templateType.GetProperties())
            {
                if (excludeName.Contains(mProperty.Name))
                    continue;
                try
                {
                    PropertyInfo des = desType.GetProperty(mProperty.Name);
                    if (des != null && des.PropertyType == mProperty.PropertyType &&
                        des.CanWrite && mProperty.CanRead)
                    {
                        des.SetValue(destination, mProperty.GetValue(source, null), null);
                        //des.SetValue(destination, ChangeType(mProperty.GetValue(source, null), mProperty.PropertyType), null);
                        count++;
                    }
                }
                catch
                {
                }
            }

            return count;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="value">源类型值</param>
        /// <param name="type">目标类型</param>
        /// <returns></returns>
        public static object ChangeType(object value, Type type)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }

        /// <summary>
        /// 转换类型对象
        /// </summary>
        /// <typeparam name="TResult">输出类型</typeparam>
        /// <param name="inputObj">输入对象</param>
        /// <param name="typeObj">输出类型对象</param>
        /// <returns>转换类型后对象</returns>
        public static TResult ConvertEntity<TResult>(object inputObj, TResult typeObj)
        {
            if (inputObj != null)
            {
                return (TResult)inputObj;
            }

            return default;
        }
    }
}
