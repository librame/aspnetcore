#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Librame.AspNetCore.Web.Services
{
    using AspNetCore.Web.Projects;
    using AspNetCore.Web.Resources;
    using AspNetCore.Web.Themepacks;
    using Extensions;
    using Extensions.Core.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class CopyrightService : AbstractService, ICopyrightService
    {
        private const string LibrameCore
            = nameof(LibrameCore);

        private static readonly string LibrameCoreArchitecture
            = $"{nameof(Librame)}.{nameof(AspNetCore)}";


        private IStringLocalizer<CopyrightServiceResource> _localizer;
        private IProjectContext _project;
        private IThemepackContext _themepack;


        public CopyrightService(IStringLocalizer<CopyrightServiceResource> localizer,
            IProjectContext project, IThemepackContext themepack)
            : base()
        {
            _localizer = localizer.NotNull(nameof(localizer));
            _project = project.NotNull(nameof(project));
            _themepack = themepack.NotNull(nameof(themepack));
        }


        public string GetHtmlCode(bool displayMiniInfo = false)
        {
            var sb = new StringBuilder();
            sb.Append($"{GetCopyrightYearPart()} {FormatNuGetLink(LibrameCoreArchitecture, LibrameCore, _localizer.GetString(r => r.SearchInNuget))}; {GetPoweredByPart()}");

            if (!displayMiniInfo)
                sb.AppendLine($"<br />{GetApplicationPart()} / {GetThemepackPart()}; {GetCulturePart()}");

            return sb.ToString();
        }


        private string GetPoweredByPart()
        {
            var projInfo = _project.Current.Info;
            var themeInfo = _themepack.CurrentInfo;

            string frameworkVersion;
            if (projInfo.Framework.Equals(themeInfo.Framework, StringComparison.OrdinalIgnoreCase))
                frameworkVersion = FormatFrameworkVersion(projInfo.Framework);
            else
                frameworkVersion = $"{FormatFrameworkVersion(projInfo.Framework)} / {FormatFrameworkVersion(themeInfo.Framework)}";

            var frameworkLink = FormatFrameworkLink(frameworkVersion, _localizer.GetString(r => r.GotoMicrosoft));
            return _localizer.GetString(r => r.PoweredBy, frameworkLink, FormatOSVersion());
        }

        private string GetCopyrightYearPart()
            => $"{_localizer.GetString(r => r.Copyright)} © {DateTime.UtcNow.ToString("yyyy", CultureInfo.CurrentCulture)}";

        private string GetCulturePart()
            => $"{_localizer.GetString(r => r.Culture)}: {CultureInfo.CurrentUICulture.DisplayName}";

        private string GetApplicationPart()
        {
            var info = _project.Current.Info;
            return $"{_localizer.GetString(r => r.Application)}: {FormatNuGetLink(info.AssemblyName.Name, info.DisplayName, _localizer.GetString(r => r.SearchInNuget))} {info.Version}";
        }

        private string GetThemepackPart()
        {
            var info = _themepack.CurrentInfo;
            return $"{_localizer.GetString(r => r.Themepack)}: {FormatNuGetLink(info.AssemblyName.Name, info.DisplayName, _localizer.GetString(r => r.SearchInNuget))} {info.Version}";
        }


        private static string FormatNuGetLink(string packageName, string displayName, string title)
            => $"<a href='https://www.nuget.org/packages?q={packageName}' title='{title}' target='_blank'>{displayName ?? packageName}</a>";

        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "framework")]
        private static string FormatFrameworkLink(string frameworkVersion, string title)
        {
            frameworkVersion.NotEmpty(nameof(frameworkVersion));
            
            if (frameworkVersion.Contains("Standard", StringComparison.OrdinalIgnoreCase))
                return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/standard/net-standard' title='{title}' target='_blank'>{frameworkVersion}</a>";

            if (frameworkVersion.Contains("Core", StringComparison.OrdinalIgnoreCase))
                return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/core/' title='{title}' target='_blank'>{frameworkVersion}</a>";

            return $"<a href='https://docs.microsoft.com/zh-cn/dotnet/framework/' title='{title}' target='_blank'>{frameworkVersion}</a>";
        }

        private static string FormatFrameworkVersion(string framework)
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

        private static string FormatOSVersion()
        {
            var descr = RuntimeInformation.OSDescription;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                descr = descr.TrimStart("Microsoft ");

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                descr = descr.SplitPair('-').Key;

            descr += $" {RuntimeInformation.ProcessArchitecture}";
            return descr;
        }

    }
}
