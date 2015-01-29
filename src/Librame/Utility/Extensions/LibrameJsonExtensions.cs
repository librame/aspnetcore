// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace System
{
    /// <summary>
    /// Librame JSON 静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameJsonExtensions
    {
        /// <summary>
        /// JSON 反序列化。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="json">给定的 JSON 字符串。</param>
        /// <returns>返回对象。</returns>
        public static T JsonDeserialize<T>(this string json)
        {
            if (!json.StartsWith("["))
                json = "[" + json; // 转换为数组

            if (!json.EndsWith("]"))
                json = json + "]"; // 转换为数组

            return JsonConvert.DeserializeObject<T>(json);

            //Regex r = new Regex(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{3}Z");
            //if (r.IsMatch(json))
            //{
            //    MatchEvaluator me = new MatchEvaluator(DateStringToJsonDate);
            //    json = r.Replace(json, me);
            //}

            //object obj = null;

            //using (var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            //{
            //    var dcjs = new DataContractJsonSerializer(typeof(T));
            //    obj = dcjs.ReadObject(ms);
            //}

            //return (T)obj;
        }
        //private static string DateStringToJsonDate(Match m)
        //{
        //    DateTime dt = DateTime.Parse(m.Groups[0].Value).ToUniversalTime();
        //    TimeSpan ts = dt - DateTime.Parse("1970-01-01");
        //    return String.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
        //}

        /// <summary>
        /// JSON 序列化。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="t">给定的对象。</param>
        /// <returns>返回 JSON 字符串。</returns>
        public static string JsonSerializer<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);

            //string json = String.Empty;

            //using (var ms = new System.IO.MemoryStream())
            //{
            //    var dcjs = new DataContractJsonSerializer(typeof(T));
            //    dcjs.WriteObject(ms, t);

            //    json = System.Text.Encoding.UTF8.GetString(ms.ToArray());
            //}

            //return json;
        }

    }
}