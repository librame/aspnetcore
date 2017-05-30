#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace LibrameCore.Adaptation
{
    using Utilities;

    /// <summary>
    /// 适配器接口。
    /// </summary>
    public interface IAdapter : IDisposable
    {
        /// <summary>
        /// 选择项。
        /// </summary>
        LibrameOptions Options { get; }

        /// <summary>
        /// 适配器信息。
        /// </summary>
        AdapterInfo Info { get; }

        /// <summary>
        /// 适配器目录。
        /// </summary>
        string BaseDirectory { get; }

        /// <summary>
        /// 适配器配置目录。
        /// </summary>
        string ConfigDirectory { get; }


        /// <summary>
        /// 父级适配器接口。
        /// </summary>
        IAdapter ParentAdapter { get; set; }


        /// <summary>
        /// 导出当前程序集包含的嵌入资源文件到适配器配置目录。
        /// </summary>
        /// <param name="outputRelativeFilePath">给定的输出相对文件路径（相对于 <see cref="ConfigDirectory"/> 适配器配置目录）。</param>
        /// <param name="manifestResourceName">给定的清单资源文件名（可选；默认以输出相对文件路径参考文件名）。</param>
        void ExportManifestResourceFile(string outputRelativeFilePath, string manifestResourceName = null);
        /// <summary>
        /// 导出指定程序集包含的嵌入资源文件到适配器配置目录。
        /// </summary>
        /// <param name="adapterAssembly">给定包含嵌入资源文件的程序集。</param>
        /// <param name="outputRelativeFilePath">给定的输出相对文件路径（相对于 <see cref="ConfigDirectory"/> 适配器配置目录）。</param>
        /// <param name="manifestResourceName">给定的清单资源文件名（可选；默认以输出相对文件路径参考文件名）。</param>
        void ExportManifestResourceFile(Assembly adapterAssembly, string outputRelativeFilePath,
            string manifestResourceName = null);
    }


    /// <summary>
    /// 抽象适配器。
    /// </summary>
    public abstract class AbstractAdapter : IAdapter
    {
        /// <summary>
        /// 构造一个抽象适配器实例。
        /// </summary>
        /// <param name="name">给定的适配器名称。</param>
        /// <param name="options">给定的选择项。</param>
        protected AbstractAdapter(string name, IOptions<LibrameOptions> options)
            : this(new AdapterInfo(name.NotEmpty(nameof(name))), options)
        {
        }
        /// <summary>
        /// 构造一个抽象适配器实例。
        /// </summary>
        /// <param name="info">给定的适配器信息。</param>
        /// <param name="options">给定的选择项。</param>
        protected AbstractAdapter(AdapterInfo info, IOptions<LibrameOptions> options)
        {
            Info = info.NotNull(nameof(info));
            Options = options.NotNull(nameof(options)).Value;
        }


        /// <summary>
        /// 选择项。
        /// </summary>
        public LibrameOptions Options { get; }
        
        /// <summary>
        /// 适配器信息。
        /// </summary>
        public virtual AdapterInfo Info { get; }

        /// <summary>
        /// 适配器目录。
        /// </summary>
        public virtual string BaseDirectory
        {
            get { return Options.BaseDirectory.AppendDirectoryName(LibrameConstants.ADAPTERS); }
        }

        /// <summary>
        /// 适配器配置目录。
        /// </summary>
        public virtual string ConfigDirectory
        {
            get { return BaseDirectory.AppendDirectoryName(Info.Name); }
        }


        /// <summary>
        /// 父级适配器接口。
        /// </summary>
        public IAdapter ParentAdapter { get; set; }


        /// <summary>
        /// 导出当前程序集包含的嵌入资源文件到适配器配置目录。
        /// </summary>
        /// <param name="outputRelativeFilePath">给定的输出相对文件路径（相对于 <see cref="ConfigDirectory"/> 适配器配置目录）。</param>
        /// <param name="manifestResourceName">给定的清单资源文件名（可选；默认以输出相对文件路径参考文件名）。</param>
        public virtual void ExportManifestResourceFile(string outputRelativeFilePath, string manifestResourceName = null)
        {
            ExportManifestResourceFile(AssemblyUtility.CurrentAssembly, outputRelativeFilePath, manifestResourceName);
        }
        /// <summary>
        /// 导出指定程序集包含的嵌入资源文件到适配器配置目录。
        /// </summary>
        /// <param name="adapterAssembly">给定包含嵌入资源文件的程序集。</param>
        /// <param name="outputRelativeFilePath">给定的输出相对文件路径（相对于 <see cref="ConfigDirectory"/> 适配器配置目录）。</param>
        /// <param name="manifestResourceName">给定的清单资源文件名（可选；默认以输出相对文件路径参考文件名）。</param>
        public virtual void ExportManifestResourceFile(Assembly adapterAssembly, string outputRelativeFilePath,
            string manifestResourceName = null)
        {
            outputRelativeFilePath.NotEmpty(nameof(outputRelativeFilePath));

            if (string.IsNullOrEmpty(manifestResourceName))
                manifestResourceName = ToManifestResourceName(adapterAssembly, outputRelativeFilePath);

            // 如果不是以适配器配置目录开始的，则添加配置目录
            if (!outputRelativeFilePath.StartsWith(ConfigDirectory))
            {
                // 支持基础目录
                if (outputRelativeFilePath.StartsWith(BaseDirectory))
                    outputRelativeFilePath = BaseDirectory.AppendPath(outputRelativeFilePath);
                else
                    outputRelativeFilePath = ConfigDirectory.AppendPath(outputRelativeFilePath);
            }

            // 导出嵌入的资源配置文件
            adapterAssembly.ManifestResourceSaveAs(manifestResourceName, outputRelativeFilePath);
        }

        /// <summary>
        /// 将资源路径转换为嵌入的清单资源名。
        /// </summary>
        /// <param name="adapterAssembly">给定包含嵌入资源文件的程序集。</param>
        /// <param name="resourceFilePath">给定的资源文件路径。</param>
        /// <returns>返回清单资源名。</returns>
        protected virtual string ToManifestResourceName(Assembly adapterAssembly, string resourceFilePath)
        {
            adapterAssembly.NotNull(nameof(adapterAssembly));

            var assemblyName = new AssemblyName(adapterAssembly.FullName);

            // 移除基础目录
            var manifestResourceName = resourceFilePath.Replace(BaseDirectory, string.Empty);

            if (manifestResourceName.Contains("\\"))
                manifestResourceName = manifestResourceName.Replace('\\', '.');

            if (manifestResourceName.Contains("/"))
                manifestResourceName = manifestResourceName.Replace('/', '.');
			
			if (manifestResourceName.StartsWith("."))
                manifestResourceName = manifestResourceName.TrimStart('.');

            // 附加命令空间
            if (!manifestResourceName.StartsWith(assemblyName.Name))
                manifestResourceName = (assemblyName.Name + "." + manifestResourceName);

            return manifestResourceName;
        }


        /// <summary>
        /// 释放适配器资源。
        /// </summary>
        public virtual void Dispose()
        {
            // None.
        }

    }
}
