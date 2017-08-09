#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LibrameStandard.Utilities
{
    /// <summary>
    /// <see cref="SelectList"/> 实用工具。
    /// </summary>
    public static class SelectListUtility
    {

        /// <summary>
        /// 当作数据状态选择列表项集合。
        /// </summary>
        /// <param name="status">给定的数据状态。</param>
        /// <returns>返回选择列表项集合。</returns>
        public static IEnumerable<SelectListItem> AsDataStatusItems(this DataStatus status)
        {
            return AsEnumItems<DataStatus>((int)status);
        }


        /// <summary>
        /// 当作枚举选择列表项集合。
        /// </summary>
        /// <typeparam name="TEnum">指定的枚举类型。</typeparam>
        /// <param name="enumValue">给定的枚举常量值。</param>
        /// <returns>返回选择列表项集合。</returns>
        public static IEnumerable<SelectListItem> AsEnumItems<TEnum>(int enumValue)
        {
            var valueGroups = new Dictionary<string, string>();
            var selectedValue = enumValue.ToString();

            var items = typeof(TEnum).AsEnumResultsWithAttribute<DisplayAttribute, SelectListItem>((field, attrib, i) =>
            {
                var group = attrib.GetGroupName();
                var value = ((int)i).ToString();

                if (!valueGroups.ContainsKey(value))
                    valueGroups.Add(value, group);

                return new SelectListItem
                {
                    Selected = (selectedValue == value),
                    Text = attrib.GetName(),
                    Value = value
                };
            }).ToArray();

            var groups = valueGroups.Values.Distinct().Select(name =>
            {
                return new SelectListGroup
                {
                    Name = name
                };
            }).ToArray();

            foreach (var item in items)
            {
                foreach (var group in groups)
                {
                    if (valueGroups[item.Value] == group.Name)
                    {
                        item.Group = group;
                        break;
                    }
                }
            }

            return items;
        }

    }
}
