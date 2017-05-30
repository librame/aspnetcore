#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;

namespace LibrameCore.Entity
{
    using Utilities;

    /// <summary>
    /// 实体绑定。
    /// </summary>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    public class EntityBinding<TEntity> : IEntityBinding<TEntity>
        where TEntity : class
    {
        private readonly ConcurrentDictionary<string, BindingMarkup> _properties = null;

        /// <summary>
        /// 构造一个抽象仓库绑定实例。
        /// </summary>
        public EntityBinding()
        {
            _properties = new ConcurrentDictionary<string, BindingMarkup>();
        }


        /// <summary>
        /// 绑定属性。
        /// </summary>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <param name="markup">给定的绑定标记（可选；默认为所有）。</param>
        /// <returns>返回 <see cref="IEntityBinding{T}"/>。</returns>
        public virtual IEntityBinding<TEntity> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression,
            BindingMarkup markup = BindingMarkup.All)
        {
            var propertyName = ExpressionUtility.AsPropertyName(propertyExpression);
            _properties.AddOrUpdate(propertyName, markup, (key, value) => value);

            return this;
        }


        /// <summary>
        /// 清空绑定的属性集合。
        /// </summary>
        public virtual void ClearProperties()
        {
            _properties.Clear();
        }


        /// <summary>
        /// 导出绑定的属性集合。
        /// </summary>
        /// <param name="markup">给定要导出的绑定标记（可选；默认为所有）。</param>
        /// <returns>返回属性名称数组。</returns>
        public virtual string[] ExportProperties(BindingMarkup markup = BindingMarkup.All)
        {
            return _properties.Where(pair => pair.Value == markup).Select(s => s.Key).ToArray();
        }

    }
}
