#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;
using System;

namespace Librame.AspNetCore.Api
{
    /// <summary>
    /// 异常 Graph 类型。
    /// </summary>
    public class ExceptionGraphType : ObjectGraphType<Exception>
    {
        /// <summary>
        /// 构造一个 <see cref="ExceptionGraphType"/>。
        /// </summary>
        public ExceptionGraphType()
        {
            Field(f => f.HResult, true);
            Field(f => f.HelpLink, true);
            Field(f => f.Message, true);
            Field(f => f.Source, true);
            Field(f => f.StackTrace, true);
            Field<ExceptionGraphType>(nameof(Exception.InnerException));
        }
    }
}
