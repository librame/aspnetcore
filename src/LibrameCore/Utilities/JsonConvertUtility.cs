#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Newtonsoft.Json;
using System;
using System.Reflection;

namespace LibrameCore.Utilities
{
    /// <summary>
    /// <see cref="JsonConvert"/> 实用工具。
    /// </summary>
    public static class JsonConvertUtility
    {
        /// <summary>
        /// 内容类型。
        /// </summary>
        public const string CONTENT_TYPE = "application/json";


        /// <summary>
        /// 将指定值类型实例序列化为 JSON 字符串形式。
        /// </summary>
        /// <remarks>支持直接转换值类型和虚转换字符串类型。</remarks>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="value">给定的值。</param>
        /// <param name="formatting">要格式化的方式（可选）。</param>
        /// <param name="serializerSettings">序列化首选项（可选）。</param>
        /// <param name="converters">转换器数组（可选）。</param>
        /// <returns>返回 JSON 字符串。</returns>
        public static string AsJson<TValue>(this TValue value, Formatting formatting = Formatting.None,
            JsonSerializerSettings serializerSettings = null, params JsonConverter[] converters)
        {
            return AsJson((object)value, formatting, serializerSettings, converters);
        }

        /// <summary>
        /// 将指定的对象序列化为 JSON 字符串形式。
        /// </summary>
        /// <remarks>支持直接转换值类型和虚转换字符串类型。</remarks>
        /// <param name="value">给定的值。</param>
        /// <param name="formatting">要格式化的方式（可选）。</param>
        /// <param name="serializerSettings">序列化首选项（可选）。</param>
        /// <param name="converters">转换器数组（可选）。</param>
        /// <returns>返回 JSON 字符串。</returns>
        public static string AsJson(this object value, Formatting formatting = Formatting.None,
            JsonSerializerSettings serializerSettings = null, params JsonConverter[] converters)
        {
            if (value == null)
                return string.Empty;

            var typeInfo = value.GetType().GetTypeInfo();
            if (typeInfo.IsValueType || typeInfo.IsAnsiClass)
                return value.ToString(); // 值类型和字符串类型直接返回

            try
            {
                if (!ReferenceEquals(converters, null) && converters.Length > 0)
                    return JsonConvert.SerializeObject(value, formatting, converters);

                if (!ReferenceEquals(serializerSettings, null))
                    return JsonConvert.SerializeObject(value, formatting, serializerSettings);

                return JsonConvert.SerializeObject(value, formatting);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 将 JSON 字符串形式反序列化为指定值类型实例。
        /// </summary>
        /// <remarks>支持值类型和字符串类型。</remarks>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="json">给定的 JSON 字符串。</param>
        /// <param name="serializerSettings">序列化首选项（可选）。</param>
        /// <param name="converters">转换器数组（可选）。</param>
        /// <returns>返回值类型实例。</returns>
        public static TValue FromJson<TValue>(this string json,
            JsonSerializerSettings serializerSettings = null, params JsonConverter[] converters)
        {
            return (TValue)FromJson(json, typeof(TValue), serializerSettings, converters);
        }

        /// <summary>
        /// 将 JSON 字符串形式反序列化为指定类型对象。
        /// </summary>
        /// <remarks>支持值类型和字符串类型。</remarks>
        /// <param name="json">给定的 JSON 字符串。</param>
        /// <param name="type">给定的类型。</param>
        /// <param name="serializerSettings">序列化首选项（可选）。</param>
        /// <param name="converters">转换器数组（可选）。</param>
        /// <returns>返回值类型实例。</returns>
        public static object FromJson(this string json, Type type,
            JsonSerializerSettings serializerSettings = null, params JsonConverter[] converters)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsValueType || typeInfo.IsAnsiClass)
                return json.AsOrDefault(type); // 值类型和字符串类型使用字符串转换

            try
            {
                if (!ReferenceEquals(serializerSettings, null))
                    return JsonConvert.DeserializeObject(json, type, serializerSettings);

                if (!ReferenceEquals(converters, null) && converters.Length > 0)
                    return JsonConvert.DeserializeObject(json, type, converters);

                return JsonConvert.DeserializeObject(json, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
