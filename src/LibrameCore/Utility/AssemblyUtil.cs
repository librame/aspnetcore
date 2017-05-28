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
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace LibrameCore.Utility
{
    /// <summary>
    /// <see cref="Type"/> 实用工具。
    /// </summary>
    public static class AssemblyUtil
    {
        /// <summary>
        /// 当前程序集。
        /// </summary>
        public readonly static Assembly CurrentAssembly = null;

        static AssemblyUtil()
        {
            if (CurrentAssembly == null)
                CurrentAssembly = TypeUtil.GetAssembly<ILibrameBuilder>();
        }

        
        /// <summary>
        /// 获取程序集标识。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <returns>返回程序集标识。</returns>
        public static AssemblyName GetAssemblyName(this Assembly assembly)
        {
            return new AssemblyName(assembly.NotNull(nameof(assembly)).FullName);
        }


        #region Manifest Resource

        /// <summary>
        /// 将当前程序集中嵌入的清单资源另存为。
        /// </summary>
        /// <param name="manifestResourceName">给定的清单资源名。</param>
        /// <param name="outputFilePath">给定的输出文件路径。</param>
        public static void ManifestResourceSaveAs(string manifestResourceName, string outputFilePath)
        {
            CurrentAssembly.ManifestResourceSaveAs(manifestResourceName, outputFilePath);
        }
        /// <summary>
        /// 将指定程序集中嵌入的清单资源另存为。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="manifestResourceName">给定的清单资源名。</param>
        /// <param name="outputFilePath">给定的输出文件路径。</param>
        public static void ManifestResourceSaveAs(this Assembly assembly,
            string manifestResourceName, string outputFilePath)
        {
            assembly.NotNull(nameof(assembly));

            try
            {
                // 如果文件已存在且不需要覆盖，则不导出
                if (File.Exists(outputFilePath)
                    && !OverwriteOutputFile(assembly, manifestResourceName, outputFilePath))
                {
                    return;
                }

                // 读取嵌入的资源流
                using (var s = assembly.GetManifestResourceStream(manifestResourceName))
                {
                    using (var sr = new StreamReader(s))
                    {
                        var lenth = sr.BaseStream.Length;

                        int i = 0;
                        var buffer = new char[256];

                        // 输出到文件流
                        using (var fs = new FileStream(outputFilePath, FileMode.Create,
                            FileAccess.Write, FileShare.None, 4096, true))
                        {
                            using (var sw = new StreamWriter(fs))
                            {
                                while (!sr.EndOfStream)
                                {
                                    i = sr.Read(buffer, 0, 256);
                                    sw.Write(buffer, 0, i);
                                }

                                sw.Flush();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 异步将指定程序集中嵌入的清单资源另存为。
        /// </summary>
        /// <param name="manifestResourceName">给定的清单资源名。</param>
        /// <param name="outputFilePath">给定的输出文件路径。</param>
        public static Task ManifestResourceSaveAsAsync(string manifestResourceName, string outputFilePath)
        {
            return CurrentAssembly.ManifestResourceSaveAsAsync(manifestResourceName, outputFilePath);
        }
        /// <summary>
        /// 异步将指定程序集中嵌入的清单资源另存为。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="manifestResourceName">给定的清单资源名。</param>
        /// <param name="outputFilePath">给定的输出文件路径。</param>
        public static Task ManifestResourceSaveAsAsync(this Assembly assembly,
            string manifestResourceName, string outputFilePath)
        {
            assembly.NotNull(nameof(assembly));

            try
            {
                // 如果文件已存在且不需要覆盖，则不导出
                if (File.Exists(outputFilePath)
                    && !OverwriteOutputFile(assembly, manifestResourceName, outputFilePath))
                {
                    return Task.CompletedTask;
                }

                // 读取嵌入的资源流
                using (var s = assembly.GetManifestResourceStream(manifestResourceName))
                {
                    using (var sr = new StreamReader(s))
                    {
                        var lenth = sr.BaseStream.Length;

                        int i = 0;
                        var buffer = new char[256];

                        // 输出到文件流
                        using (var fs = new FileStream(outputFilePath, FileMode.Create,
                            FileAccess.Write, FileShare.None, 4096, true))
                        {
                            using (var sw = new StreamWriter(fs))
                            {
                                while (!sr.EndOfStream)
                                {
                                    i = sr.Read(buffer, 0, 256);
                                    sw.WriteAsync(buffer, 0, i);
                                }

                                return sw.FlushAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 覆盖输出文件。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="manifestResourceName">给定的清单资源文件名。</param>
        /// <param name="outputFileName">给定的输出文件名。</param>
        /// <returns>返回是否覆盖的布尔值。</returns>
        private static bool OverwriteOutputFile(Assembly assembly, string manifestResourceName,
            string outputFileName)
        {
            try
            {
                var resourceInfo = assembly.GetManifestResourceInfo(manifestResourceName);
                var resourceFileInfo = new FileInfo(resourceInfo.FileName);
                var outputFileInfo = new FileInfo(outputFileName);

                // 如果资源文件的修改时间比输出文件的修改时间新，则要求覆盖
                return (resourceFileInfo.LastWriteTime > outputFileInfo.LastWriteTime);
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}
