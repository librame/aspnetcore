#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 应用上下文静态扩展。
    /// </summary>
    public static class ApplicationContextExtensions
    {
        private static readonly string LibrameArchitecture = $"{nameof(Librame)}.{nameof(AspNetCore)}";


        /// <summary>
        /// 底部版权信息。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="displayMiniInfo">显示迷你信息（可选；默认显示完整信息）。</param>
        /// <returns>返回字符串。</returns>
        public static string GetFooterCopyrightInfo(this IApplicationContext context, bool displayMiniInfo = false)
        {
            var interInfo = context.CurrentInterfaceInfo;
            var themeInfo = context.CurrentThemepackInfo;

            string framework;
            if (interInfo.Framework.Equals(themeInfo.Framework))
                framework = interInfo.Framework.Replace(",Version=v", " ");
            else
                framework = $"{interInfo.Framework.Replace(",Version=v", " ")} / {themeInfo.Framework.Replace(",Version=v", " ")}";

            var sb = new StringBuilder();
            sb.Append($"Copyright © {DateTime.Now.ToString("yyyy")} {GetNuGetLink(LibrameArchitecture)} Powered by {GetFrameworkLink(framework)} on {RuntimeInformation.OSDescription}; {nameof(CultureInfo.CurrentUICulture)}: {CultureInfo.CurrentUICulture.Name}");
            
            if (!displayMiniInfo)
                sb.AppendLine($"<br />Application: {GetNuGetLink(interInfo.AssemblyName.Name, interInfo.Name)} {interInfo.Version} [{interInfo.Authors}] / Themepack: {GetNuGetLink(themeInfo.AssemblyName.Name, themeInfo.Name)} {themeInfo.Version} [{themeInfo.Authors}]");
            
            return sb.ToString();
        }

        private static string GetNuGetLink(string assemblyName, string displayName = null)
            => $"<a href='https://www.nuget.org/packages?q={assemblyName}' title='Search in NuGet.org' target='_blank'>{displayName ?? assemblyName}</a>";

        private static string GetFrameworkLink(string framework)
        {
            if (framework.Contains("Standard"))
                return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/standard/net-standard' title='Goto microsoft.com' target='_blank'>{framework}</a>";

            if (framework.Contains("Core"))
                return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/core/' title='Goto microsoft.com' target='_blank'>{framework}</a>";

            return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/framework/' title='Goto microsoft.com' target='_blank'>{framework}</a>";
        }
    }
}
