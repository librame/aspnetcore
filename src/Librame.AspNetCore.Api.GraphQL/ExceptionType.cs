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
    /// 异常类型。
    /// </summary>
    public class ExceptionType : ObjectGraphType<Exception>
    {
        /// <summary>
        /// 构造一个 <see cref="ExceptionType"/>。
        /// </summary>
        public ExceptionType()
        {
            Field(f => f.HelpLink, true);
            Field(f => f.Message, true);
            Field(f => f.Source, true);
            Field(f => f.StackTrace, true);
            Field(f => f.InnerException, true);
        }
    }
}
