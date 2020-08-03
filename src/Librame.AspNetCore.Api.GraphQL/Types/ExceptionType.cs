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

namespace Librame.AspNetCore.Api.Types
{
    /// <summary>
    /// 异常类型。
    /// </summary>
    public class ExceptionType : ApiTypeBase<Exception>
    {
        /// <summary>
        /// 构造一个 <see cref="ExceptionType"/>。
        /// </summary>
        public ExceptionType()
        {
            Field(f => f.HResult, true);
            Field(f => f.HelpLink, true);
            Field(f => f.Message, true);
            Field(f => f.Source, true);
            Field(f => f.StackTrace, true);

            Field<ExceptionType>(nameof(Exception.InnerException));
        }

    }
}
