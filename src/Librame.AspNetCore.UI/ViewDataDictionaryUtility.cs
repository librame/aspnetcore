#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// <see cref="ViewDataDictionary"/> 实用工具。
    /// </summary>
    public static class ViewDataDictionaryUtility
    {
        /// <summary>
        /// 获取布尔值。
        /// </summary>
        /// <param name="viewData">给定的 <see cref="ViewDataDictionary"/>。</param>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回布尔值。</returns>
        public static bool GetBool(ViewDataDictionary viewData, string key)
        {
            if (viewData.TryGetValue(key, out object value))
                return (bool)value;

            return false;
        }

    }
}
