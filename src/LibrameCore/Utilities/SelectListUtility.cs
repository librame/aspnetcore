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
using LibrameStandard.Entity.Descriptors;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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

        #region AsEnumItems

        /// <summary>
        /// 当作数据状态选择列表项集合。
        /// </summary>
        /// <param name="status">给定的数据状态。</param>
        /// <returns>返回选择列表项集合。</returns>
        public static SelectListItem[] AsDataStatusSelectListItems(this DataStatus status)
        {
            return AsEnumSelectListItems<DataStatus>((int)status);
        }


        /// <summary>
        /// 当作枚举选择列表项集合。
        /// </summary>
        /// <typeparam name="TEnum">指定的枚举类型。</typeparam>
        /// <param name="enumValue">给定的枚举常量值。</param>
        /// <returns>返回选择列表项集合。</returns>
        public static SelectListItem[] AsEnumSelectListItems<TEnum>(int enumValue)
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
            })
            .ToArray();

            var groups = valueGroups.Values.Distinct().Select(name =>
            {
                return new SelectListGroup
                {
                    Name = name
                };
            })
            .ToArray();

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

        #endregion


        #region AsSelectListItems

        /// <summary>
        /// 转换为选择列表项集合。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TId">指定的编号类型。</typeparam>
        /// <param name="entities">给定的实体集合。</param>
        /// <param name="textFactory">给定的文本工厂方法。</param>
        /// <param name="selectedValue">给定的选中值。</param>
        /// <param name="optionLabel">给定的选项标签。</param>
        /// <param name="defaultValue">给定的默认值。</param>
        /// <returns>返回选择列表项集合。</returns>
        public static IList<SelectListItem> AsSelectListItems<TEntity, TId>(this IEnumerable<TEntity> entities,
            Func<TEntity, string> textFactory, string selectedValue = null,
            string optionLabel = null, string defaultValue = null)
            where TEntity : class, IIdDescriptor<TId>
        {
            optionLabel = optionLabel.AsOrDefault("默认");
            defaultValue = defaultValue.AsOrDefault("0");

            return entities.AsSelectListItems(textFactory, v => v.Id.ToString(),
                selectedValue, optionLabel, defaultValue);
        }

        /// <summary>
        /// 转换为选择列表项集合。
        /// </summary>
        /// <typeparam name="TSource">指定的来源类型。</typeparam>
        /// <param name="sources">给定的来源集合。</param>
        /// <param name="textFactory">给定的文本工厂方法。</param>
        /// <param name="valueFactory">给定的值工厂方法。</param>
        /// <param name="selectedValue">给定的选中值。</param>
        /// <param name="optionLabel">给定的选项标签。</param>
        /// <param name="defaultValue">给定的默认值。</param>
        /// <returns>返回选择列表项集合。</returns>
        public static IList<SelectListItem> AsSelectListItems<TSource>(this IEnumerable<TSource> sources,
            Func<TSource, string> textFactory, Func<TSource, string> valueFactory,
            string selectedValue = null, string optionLabel = null, string defaultValue = null)
            where TSource : class
        {
            var items = sources.Select(s =>
            {
                var value = valueFactory.Invoke(s);
                var text = textFactory.Invoke(s);

                return new SelectListItem
                {
                    Text = text,
                    Value = value,
                    Selected = (string.IsNullOrEmpty(selectedValue) ? false : value == selectedValue)
                };
            })
            .ToList();

            if (!string.IsNullOrEmpty(optionLabel))
            {
                items.Insert(0, new SelectListItem
                {
                    Text = optionLabel,
                    Value = defaultValue
                });
            }

            return items;
        }

        #endregion


        #region SelectListItemsWithGroup
        
        /// <summary>
        /// 分组转换为选择列表项集合。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TId">指定的编号类型。</typeparam>
        /// <param name="entities">给定的实体集合。</param>
        /// <param name="textFactory">给定的文本工厂方法。</param>
        /// <param name="groupLabel">给定的组标签。</param>
        /// <param name="selectedValue">给定的选中值。</param>
        /// <param name="groupFactory">断定组的工厂方法。</param>
        /// <param name="optionLabel">给定的选项标签。</param>
        /// <param name="defaultValue">给定的默认值。</param>
        /// <returns>返回选择列表项集合。</returns>
        public static IList<SelectListItem> AsSelectListItemsWithGroup<TEntity, TId>(this IEnumerable<TEntity> entities,
            Func<TEntity, string> textFactory, string groupLabel,
            string selectedValue = null, Func<TEntity, bool> groupFactory = null,
            string optionLabel = null, string defaultValue = null)
            where TEntity : class, IParentIdDescriptor<TId>
        {
            if (groupFactory == null)
            {
                groupFactory = g => g.ParentId.Equals(default(TId));
            }

            var allItems = new List<SelectListItem>();

            // Root
            var rootGroup = new SelectListGroup
            {
                Name = groupLabel
            };

            var groups = entities.Where(groupFactory).Select(s =>
            {
                var value = s.Id.ToString();
                var text = textFactory.Invoke(s);

                allItems.Add(new SelectListItem
                {
                    Text = text,
                    Value = value,
                    Selected = (string.IsNullOrEmpty(selectedValue) ? false : value == selectedValue),
                    Group = rootGroup
                });

                var group = new SelectListGroup
                {
                    Name = text
                };

                return new KeyValuePair<TId, SelectListGroup>(s.Id, group);
            })
            .ToDictionary(k => k.Key);

            // All
            groups.Invoke(g =>
            {
                var items = entities.Where(p => p.ParentId.Equals(g.Key)).Select(s =>
                {
                    var value = s.Id.ToString();
                    var text = textFactory.Invoke(s);

                    return new SelectListItem
                    {
                        Text = text,
                        Value = value,
                        Selected = (string.IsNullOrEmpty(selectedValue) ? false : value == selectedValue),
                        Group = g.Value.Value
                    };
                });

                allItems.AddRange(items);
            });

            if (!string.IsNullOrEmpty(optionLabel))
            {
                allItems.Insert(0, new SelectListItem
                {
                    Text = optionLabel,
                    Value = defaultValue
                });
            }

            return allItems;
        }

        #endregion

    }
}
