#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.Identity.UI.Models.ManageViewModels
{
    /// <summary>
    /// 增加电话号码视图模型。
    /// </summary>
    public class AddPhoneNumberViewModel
    {
        /// <summary>
        /// 电话号码。
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = nameof(PhoneNumber), ResourceType = typeof(ViewModelsResource))]
        public string PhoneNumber { get; set; }
    }
}
