#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// 认证状态。
    /// </summary>
    [Description("认证状态")]
    public enum AuthenticationStatus
    {

        #region Global

        /// <summary>
        /// 默认。
        /// </summary>
        [Display(Name = "Default",
            ShortName = "Default",
            Description = "Default",
            Prompt = "Default",
            GroupName = "DefaultGroup",
            ResourceType = typeof(Resources.AuthenticationStatus),
            Order = -9
        )]
        Default = 0,

        #endregion


        #region Cookie

        /// <summary>
        /// Cookie。
        /// </summary>
        [Display(Name = "Cookie",
            ShortName = "Cookie",
            Description = "Cookie",
            Prompt = "Cookie",
            GroupName = "DefaultGroup",
            ResourceType = typeof(Resources.AuthenticationStatus)
        )]
        Cookie = 1,

        #endregion


        #region Header

        /// <summary>
        /// 请求头部。
        /// </summary>
        [Display(Name = "Header",
            ShortName = "Header",
            Description = "Header",
            Prompt = "Header",
            GroupName = "DefaultGroup",
            ResourceType = typeof(Resources.AuthenticationStatus)
        )]
        Header = 2,

        #endregion


        #region Query

        /// <summary>
        /// 查询参数。
        /// </summary>
        [Display(Name = "Query",
            ShortName = "Query",
            Description = "Query",
            Prompt = "Query",
            GroupName = "DefaultGroup",
            ResourceType = typeof(Resources.AuthenticationStatus)
        )]
        Query = 4

        #endregion


        //#region Form

        ///// <summary>
        ///// 查询参数。
        ///// </summary>
        //[Display(Name = "Form",
        //    ShortName = "Form",
        //    Description = "Form",
        //    Prompt = "Form",
        //    GroupName = "DefaultGroup",
        //    ResourceType = typeof(Resources.AuthenticationStatus)
        //)]
        //Form = 8

        //#endregion

    }
}
