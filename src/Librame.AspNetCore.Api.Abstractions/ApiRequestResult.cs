#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.Api
{
    using Extensions;

    /// <summary>
    /// API 请求结果。
    /// </summary>
    public class ApiRequestResult
    {
        /// <summary>
        /// 构造一个 <see cref="ApiRequestResult"/>。
        /// </summary>
        /// <param name="data">给定的结果数据（可选）。</param>
        public ApiRequestResult(object data = null)
        {
            Data = data;
        }


        /// <summary>
        /// 结果数据。
        /// </summary>
        public object Data { get; protected set; }


        /// <summary>
        /// 请求成功。
        /// </summary>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// 实体错误集合。
        /// </summary>
        /// <value>返回 <see cref="IReadOnlyList{Exception}"/>。</value>
        public IReadOnlyList<Exception> Errors { get; protected set; }


        /// <summary>
        /// 按层级名称集合的先后顺序查找结果数据中包含的值。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="levelNames">给定的层级名称数组。</param>
        /// <returns>返回 <typeparamref name="TValue"/>。</returns>
        public virtual TValue LookupDataValue<TValue>(params string[] levelNames)
            => (TValue)LookupDataValue(levelNames);

        /// <summary>
        /// 按层级名称集合的先后顺序查找结果数据中包含的值。
        /// </summary>
        /// <param name="levelNames">给定的层级名称数组。</param>
        /// <returns>返回对象。</returns>
        public virtual object LookupDataValue(params string[] levelNames)
        {
            var value = Data;

            foreach (var name in levelNames)
            {
                value = LookupLevel(value, name, out var isContinue);

                if (!isContinue)
                    break;
            }

            return value;

            // LookupLevel
            object LookupLevel(object levelValue, string levelName, out bool isContinue)
            {
                if (levelValue is Dictionary<string, object> dictionary)
                {
                    if (isContinue = dictionary.TryGetValue(levelName, out var value))
                        return value;

                    return null;
                }

                isContinue = false;
                return levelValue;
            }
        }


        /// <summary>
        /// 表示成功的请求结果。
        /// </summary>
        /// <param name="data">给定的结果数据（可选）。</param>
        /// <returns>返回 <see cref="ApiRequestResult"/>。</returns>
        public static ApiRequestResult Success(object data)
            => new ApiRequestResult(data)
            {
                Succeeded = true
            };


        /// <summary>
        /// 表示失败的请求结果。
        /// </summary>
        /// <param name="data">给定的结果数据。</param>
        /// <param name="errors">给定的 <see cref="Exception"/> 数组。</param>
        /// <returns>返回 <see cref="ApiRequestResult"/>。</returns>
        public static ApiRequestResult Failed(object data,
            params Exception[] errors)
        {
            var result = new ApiRequestResult(data)
            {
                Succeeded = false,
                Errors = errors
            };

            return result;
        }

        /// <summary>
        /// 表示失败的请求结果。
        /// </summary>
        /// <typeparam name="TException">指定的异常类型。</typeparam>
        /// <param name="data">给定的结果数据。</param>
        /// <param name="errors">给定的 <typeparamref name="TException"/> 集合。</param>
        /// <returns>返回 <see cref="ApiRequestResult"/>。</returns>
        public static ApiRequestResult Failed<TException>(object data,
            IEnumerable<TException> errors)
            where TException : Exception
        {
            var result = new ApiRequestResult(data)
            {
                Succeeded = false,
                Errors = errors.AsReadOnlyList()
            };

            return result;
        }

    }
}
