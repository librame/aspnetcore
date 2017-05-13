#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Librame.Adaptation
{
    using Utility;

    ///// <summary>
    ///// 适配器集合接口。
    ///// </summary>
    //public interface IAdapterCollection : ICollection<IAdapter>, IEnumerable<IAdapter>
    //{
    //    /// <summary>
    //    /// 服务集合。
    //    /// </summary>
    //    IServiceCollection Services { get; }
        
    //    /// <summary>
    //    /// 服务提供程序。
    //    /// </summary>
    //    IServiceProvider ServiceProvider { get; }

    //    /// <summary>
    //    /// 适配器集合。
    //    /// </summary>
    //    IEnumerable<IAdapter> Adapters { get; }


    //    /// <summary>
    //    /// 添加适配器集合。
    //    /// </summary>
    //    /// <param name="adapters">给定的适配器接口集合。</param>
    //    /// <returns>返回适配器集合。</returns>
    //    IAdapterCollection Add(IEnumerable<IAdapter> adapters);

    //    /// <summary>
    //    /// 添加适配器集合。
    //    /// </summary>
    //    /// <param name="adapters">给定的适配器接口集合。</param>
    //    /// <returns>返回适配器集合。</returns>
    //    bool Remove(IEnumerable<IAdapter> adapters);
    //}


    ///// <summary>
    ///// 适配器集合。
    ///// </summary>
    //public class AdapterCollection : IAdapterCollection
    //{
    //    /// <summary>
    //    /// 构造一个 <see cref="AdapterCollection"/> 实例。
    //    /// </summary>
    //    /// <param name="services">给定的服务集合接口。</param>
    //    public AdapterCollection(IServiceCollection services)
    //    {
    //        Services = services.NotNull(nameof(services));

    //        // 添加自身
    //        Services.AddSingleton<IAdapterCollection>(this);
    //    }


    //    /// <summary>
    //    /// 服务集合。
    //    /// </summary>
    //    public IServiceCollection Services { get; }

    //    /// <summary>
    //    /// 服务提供程序。
    //    /// </summary>
    //    public IServiceProvider ServiceProvider => Services.BuildServiceProvider();

    //    /// <summary>
    //    /// 适配器集合。
    //    /// </summary>
    //    public IEnumerable<IAdapter> Adapters => ServiceProvider.GetServices<IAdapter>();


    //    /// <summary>
    //    /// 当前适配器总数。
    //    /// </summary>
    //    public int Count => Adapters.Count();

    //    /// <summary>
    //    /// 是否只读。
    //    /// </summary>
    //    public bool IsReadOnly => Services.IsReadOnly;


    //    /// <summary>
    //    /// 添加适配器集合。
    //    /// </summary>
    //    /// <param name="adapters">给定的适配器接口集合。</param>
    //    /// <returns>返回适配器集合。</returns>
    //    public virtual IAdapterCollection Add(IEnumerable<IAdapter> adapters)
    //    {
    //        adapters.Invoke(a =>
    //        {
    //            Services.AddSingleton(a);
    //        });

    //        return this;
    //    }
    //    /// <summary>
    //    /// 添加适配器。
    //    /// </summary>
    //    /// <param name="adapter">给定的适配器接口。</param>
    //    public virtual void Add(IAdapter adapter)
    //    {
    //        Services.AddSingleton(adapter);
    //    }


    //    /// <summary>
    //    /// 清空所有适配器。
    //    /// </summary>
    //    public virtual void Clear()
    //    {
    //        var baseType = typeof(IAdapter);
    //        var descriptors = Services.Where(p => baseType.IsAssignableFrom(p.ServiceType)).ToArray();

    //        descriptors.Invoke(d =>
    //        {
    //            Services.Remove(d);
    //        });
    //    }


    //    /// <summary>
    //    /// 是否包含指定适配器。
    //    /// </summary>
    //    /// <param name="adapter">给定的适配器接口。</param>
    //    /// <returns>返回布尔值。</returns>
    //    public virtual bool Contains(IAdapter adapter)
    //    {
    //        var obj = ServiceProvider.GetService(adapter.NotNull(nameof(adapter)).GetType());
            
    //        return (obj != null);
    //    }


    //    /// <summary>
    //    /// 将适配器数组从指定索引处开始复制到服务集合中。
    //    /// </summary>
    //    /// <param name="array">给定的适配器数组。</param>
    //    /// <param name="arrayIndex">给定的数组索引。</param>
    //    public virtual void CopyTo(IAdapter[] array, int arrayIndex)
    //    {
    //        array.NotNull(nameof(array));

    //        if (arrayIndex < 0)
    //            arrayIndex = 0;

    //        if (array.Length < 1)
    //            return;

    //        if (arrayIndex > array.Length - 1)
    //            return;

    //        array.Invoke(a =>
    //        {
    //            Services.AddSingleton(a);
    //        });
    //    }


    //    /// <summary>
    //    /// 获取枚举器。
    //    /// </summary>
    //    /// <returns>返回适配器枚举器接口。</returns>
    //    public virtual IEnumerator<IAdapter> GetEnumerator()
    //    {
    //        return Adapters.GetEnumerator();
    //    }
    //    /// <summary>
    //    /// 获取枚举器。
    //    /// </summary>
    //    /// <returns>返回适配器枚举器接口。</returns>
    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }


    //    /// <summary>
    //    /// 添加适配器集合。
    //    /// </summary>
    //    /// <param name="adapters">给定的适配器接口集合。</param>
    //    public virtual bool Remove(IEnumerable<IAdapter> adapters)
    //    {
    //        adapters.Invoke(a =>
    //        {
    //            var baseType = a.GetType();
    //            var descriptor = Services.FirstOrDefault(p => p.ServiceType == baseType);

    //            if (descriptor != null)
    //                Services.Remove(descriptor);
    //        });
            
    //        return true;
    //    }
    //    /// <summary>
    //    /// 移除指定服务描述符。
    //    /// </summary>
    //    /// <param name="adapter">给定的适配器接口。</param>
    //    /// <returns>返回移除是否成功的布尔值。</returns>
    //    public virtual bool Remove(IAdapter adapter)
    //    {
    //        var descriptor = Services.FirstOrDefault(p => p.ServiceType == adapter.GetType());

    //        if (descriptor != null)
    //            return Services.Remove(descriptor);

    //        return false;
    //    }

    //}
}
