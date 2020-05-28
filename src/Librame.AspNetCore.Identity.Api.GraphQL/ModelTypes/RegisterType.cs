﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.ModelTypes
{
    using AspNetCore.Api;
    using AspNetCore.Identity.Api.Models;

    /// <summary>
    /// 注册类型。
    /// </summary>
    public class RegisterType : ApiModelInputGraphTypeBase<RegisterApiModel>
    {
        /// <summary>
        /// 构造一个 <see cref="RegisterInputType"/> 实例。
        /// </summary>
        public RegisterType()
            : base()
        {
            this.AddRegisterApiModelFields();
        }

    }
}
