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
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 应用上下文静态扩展。
    /// </summary>
    public static class ApplicationContextExtensions
    {
        private static readonly string LibrameCore
            = nameof(LibrameCore);

        private static readonly string LibrameCoreArchitecture
            = $"{nameof(Librame)}.{nameof(AspNetCore)}";


        /// <summary>
        /// 底部版权信息。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="displayMiniInfo">显示迷你信息（可选；默认显示完整信息）。</param>
        /// <returns>返回字符串。</returns>
        public static string GetFooterCopyrightInfo(this IApplicationContext context, bool displayMiniInfo = false)
        {
            context.NotNull(nameof(context));

            var localizer = context.ServiceFactory.GetRequiredService<IExpressionLocalizer<CopyrightInfoResource>>();
            var interInfo = context.CurrentInterfaceInfo;
            var themeInfo = context.CurrentThemepackInfo;

            var sb = new StringBuilder();
            sb.Append($"{GetCopyrightString(localizer)} {GetNuGetLink(localizer, LibrameCoreArchitecture, LibrameCore)} {GetPoweredByString(interInfo, themeInfo, localizer)}; {GetCultureString(localizer)}");
            
            if (!displayMiniInfo)
                sb.AppendLine($"<br />{GetApplicationString(interInfo, localizer)} / {GetThemepackString(themeInfo, localizer)}");
            
            return sb.ToString();
        }


        private static string GetCopyrightString(IExpressionLocalizer<CopyrightInfoResource> localizer)
            => $"{localizer[r => r.Copyright]} © {DateTime.UtcNow.ToString("yyyy", CultureInfo.CurrentCulture)}";

        private static string GetPoweredByString(IInterfaceInfo interInfo, IThemepackInfo themeInfo, IExpressionLocalizer<CopyrightInfoResource> localizer)
        {
            string framework;
            if (interInfo.Framework.Equals(themeInfo.Framework, StringComparison.OrdinalIgnoreCase))
                framework = GetFrameworkString(interInfo.Framework);
            else
                framework = $"{GetFrameworkString(interInfo.Framework)} / {GetFrameworkString(themeInfo.Framework)}";

            return localizer[r => r.PoweredBy, GetFrameworkLink(localizer, framework), RuntimeInformation.OSDescription];

            string GetFrameworkString(string framework)
            {
                // 格式：.NETCoreApp,Version=v3.0
                var pair = framework.SplitPair(",");
                var version = pair.Value.TrimStart("Version=v");

                if (pair.Key.StartsWith(".netcore", StringComparison.OrdinalIgnoreCase))
                    return $".NET Core {version}";

                if (pair.Key.StartsWith(".netstandard", StringComparison.OrdinalIgnoreCase))
                    return $".NET Standard {version}";

                return $"{pair.Key} {version}";
            }
        }

        private static string GetCultureString(IExpressionLocalizer<CopyrightInfoResource> localizer)
            => $"{localizer[r => r.Culture]}: {CultureInfo.CurrentUICulture.DisplayName}";


        private static string GetApplicationString(IInterfaceInfo interInfo, IExpressionLocalizer<CopyrightInfoResource> localizer)
            => $"{localizer[r => r.Application]}: {GetNuGetLink(localizer, interInfo.AssemblyName.Name, interInfo.Name)} {interInfo.Version} [{interInfo.Authors}]";

        private static string GetThemepackString(IThemepackInfo themeInfo, IExpressionLocalizer<CopyrightInfoResource> localizer)
            => $"{localizer[r => r.Themepack]}: {GetNuGetLink(localizer, themeInfo.AssemblyName.Name, themeInfo.Name)} {themeInfo.Version} [{themeInfo.Authors}]";


        private static string GetNuGetLink(IExpressionLocalizer<CopyrightInfoResource> localizer, string assemblyName, string displayName = null)
            => $"<a href='https://www.nuget.org/packages?q={assemblyName}' title='{localizer[r => r.SearchInNuget]}' target='_blank'>{displayName ?? assemblyName}</a>";

        private static string GetFrameworkLink(IExpressionLocalizer<CopyrightInfoResource> localizer, string framework)
        {
            if (framework.Contains("Standard", StringComparison.OrdinalIgnoreCase))
                return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/standard/net-standard' title='{localizer[r => r.GotoMicrosoft]}' target='_blank'>{framework}</a>";

            if (framework.Contains("Core", StringComparison.OrdinalIgnoreCase))
                return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/core/' title='{localizer[r => r.GotoMicrosoft]}' target='_blank'>{framework}</a>";

            return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/framework/' title='{localizer[r => r.GotoMicrosoft]}' target='_blank'>{framework}</a>";
        }
    }
}
