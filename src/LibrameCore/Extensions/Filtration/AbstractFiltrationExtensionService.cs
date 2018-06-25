#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Abstractions;
using Microsoft.Extensions.Options;

namespace LibrameCore.Extensions.Filtration
{
    /// <summary>
    /// 抽象过滤扩展模块。
    /// </summary>
    /// <typeparam name="TFiltration">指定的过滤类型。</typeparam>
    public class AbstractFiltrationExtensionService<TFiltration> : AbstractExtensionService<FiltrationExtensionOptions>, IFiltrationExtensionService
        where TFiltration : IFiltrationExtensionService
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractFiltrationExtensionService{TFiltration}"/> 实例。
        /// </summary>
        /// <param name="options">给定的过滤选项。</param>
        public AbstractFiltrationExtensionService(IOptionsMonitor<FiltrationExtensionOptions> options)
            : base(options)
        {
        }

    }
}
