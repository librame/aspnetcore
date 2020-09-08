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

namespace Librame.AspNetCore.Api.Models
{
    /// <summary>
    /// 异常模型类型。
    /// </summary>
    public class ExceptionModelType : ModelTypeBase<Exception>
    {
        /// <summary>
        /// 构造一个 <see cref="ExceptionModelType"/>。
        /// </summary>
        public ExceptionModelType()
        {
            Field(f => f.HResult, true);
            Field(f => f.HelpLink, true);
            Field(f => f.Message, true);
            Field(f => f.Source, true);
            Field(f => f.StackTrace, true);

            Field(f => f.InnerException, type: typeof(ExceptionModelType), nullable: true);
        }

    }
}
