#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace System.Collections.Generic
{
    /// <summary>
    /// 分页助手。
    /// </summary>
    public class PagingHelper
    {
        /// <summary>
        /// 根据索引创建分页信息。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="total">给定的总条数。</param>
        /// <returns>返回分页信息。</returns>
        public static PagingInfo CreateInfoByIndex(int index, int size, int total)
        {
            if (index < 1) index = 1;
            if (size < 1) size = 1;

            var info = new PagingInfo();

            info.Size = size;
            info.Index = index;

            // 计算跳过的条数
            if (info.Index > 1)
            {
                info.Skip = ((info.Index - 1) * info.Size);
            }
            else
            {
                // 当前页索引小于等于1表示不跳过
                info.Skip = 0;
            }

            // 如果总条数存在
            info.Total = total;

            if (info.Total > 0)
            {
                // 计算总索引数
                info.Pages = info.Total / info.Size + (info.Total % info.Size > 0 ? 1 : 0);
            }

            return info;
        }


        /// <summary>
        /// 根据跳过条数创建分页信息。
        /// </summary>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的获取条数。</param>
        /// <param name="total">给定的总条数。</param>
        /// <returns>返回分页信息。</returns>
        public static PagingInfo CreateInfoBySkip(int skip, int take, int total)
        {
            if (take < 1) take = 1;

            var info = new PagingInfo();

            info.Index = (((int)Math.Round((double)skip / take)) + 1);
            info.Skip = skip;
            info.Size = take;

            // 如果总条数存在
            info.Total = total;

            if (info.Total > 0)
            {
                // 计算总页数
                info.Pages = info.Total / take + (info.Total % take > 0 ? 1 : 0);
            }

            return info;
        }

    }
}
