#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.Api
{
    using Extensions;
    using Models;

    /// <summary>
    /// 抽象 API 模型静态扩展。
    /// </summary>
    public static class AbstractionApiModelExtensions
    {
        /// <summary>
        /// 记录模型日志。
        /// </summary>
        /// <typeparam name="TModel">指定的模型类型。</typeparam>
        /// <param name="model">给定的 <typeparamref name="TModel"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger"/>。</param>
        /// <returns>返回 <typeparamref name="TModel"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static TModel Log<TModel>(this TModel model, ILogger logger)
            where TModel : AbstractApiModel
        {
            model.NotNull(nameof(model));

            if (model.IsError)
            {
                if (model.Errors.Count > 0)
                    logger.LogError(model.Errors.First(), model.Message);
                else
                    logger.LogError(model.Message);
            }
            else
            {
                logger.LogDebug(model.Message);
            }

            return model;
        }

    }
}
