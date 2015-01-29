// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Utility
{
    /// <summary>
    /// Librame 实体静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameEntityExtensions
    {
        /// <summary>
        /// 将已变化的被提交实体合并到被管理实体中。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="managed">给定的被管理实体。</param>
        /// <param name="posted">给定的被提交实体。</param>
        public static void Merge<T>(this T managed, T posted)
        {
            if (ReferenceEquals(managed, null))
            {
                managed = posted;
            }
            else if (!ReferenceEquals(posted, null))
            {
                var pis = ReflectionUtils.GetProperties<T>();
                foreach (var pi in pis)
                {
                    var mana = pi.GetValue(managed);
                    var post = pi.GetValue(posted);

                    // 如果对象的属性值发生了变化
                    if (!Equals(mana, post))
                    {
                        // 采用变化的属性值
                        pi.SetValue(managed, post);
                    }
                }
            }
            else
            { }
        }

    }
}