#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.IO;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="Path"/> 实用工具。
    /// </summary>
    public static class PathUtil
    {
        /// <summary>
        /// 将指定相对路径附加到当前基础路径。
        /// </summary>
        /// <seealso cref="Path.Combine(string, string)"/>
        /// <param name="basePath">给定的基础路径。</param>
        /// <param name="relativePath">给定的相对路径。</param>
        /// <param name="checkStartWithBasePath">检查相对路径是否以基础路径开始。</param>
        /// <returns>返回路径。</returns>
        public static string AppendPath(this string basePath, string relativePath, bool checkStartWithBasePath = true)
        {
            if (checkStartWithBasePath)
            {
                relativePath.NotNullOrEmpty(nameof(relativePath));

                if (relativePath.StartsWith(basePath))
                    return relativePath;
            }

            return Path.Combine(basePath, relativePath);
        }


        /// <summary>
        /// 创建指定路径中包含的目录信息。
        /// </summary>
        /// <seealso cref="Path.GetDirectoryName(string)"/>
        /// <seealso cref="Directory.CreateDirectory(string)"/>
        /// <param name="path">给定的路径。</param>
        /// <returns>返回完整目录字符串。</returns>
        public static string CreateDirectoryName(this string path)
        {
            var di = path.CreateDirectoryInfo();

            return di.FullName;
        }

        /// <summary>
        /// 创建指定路径中包含的目录信息。
        /// </summary>
        /// <seealso cref="Path.GetDirectoryName(string)"/>
        /// <seealso cref="Directory.CreateDirectory(string)"/>
        /// <param name="path">给定的路径。</param>
        /// <returns>返回目录信息。</returns>
        public static DirectoryInfo CreateDirectoryInfo(this string path)
        {
            string dirctory = Path.GetDirectoryName(path);

            return Directory.CreateDirectory(dirctory);
        }


        /// <summary>
        /// 附加并创建目录。
        /// </summary>
        /// <seealso cref="Path.Combine(string, string)"/>
        /// <seealso cref="Directory.CreateDirectory(string)"/>
        /// <param name="baseDirectory">给定的基础目录。</param>
        /// <param name="relativeDirectory">给定的相对目录。</param>
        /// <param name="checkStartWithBasePath">检查相对路径是否以基础路径开始。</param>
        /// <returns>返回完整目录字符串。</returns>
        public static string AppendDirectoryName(this string baseDirectory, string relativeDirectory,
            bool checkStartWithBasePath = true)
        {
            var di = baseDirectory.AppendDirectoryInfo(relativeDirectory, checkStartWithBasePath);

            return di.FullName;
        }

        /// <summary>
        /// 附加并创建目录。
        /// </summary>
        /// <seealso cref="Path.Combine(string, string)"/>
        /// <seealso cref="Directory.CreateDirectory(string)"/>
        /// <param name="baseDirectory">给定的基础目录。</param>
        /// <param name="relativeDirectory">给定的相对目录。</param>
        /// <param name="checkStartWithBasePath">检查相对路径是否以基础路径开始。</param>
        /// <returns>返回目录信息。</returns>
        public static DirectoryInfo AppendDirectoryInfo(this string baseDirectory, string relativeDirectory,
            bool checkStartWithBasePath = true)
        {
            var directory = baseDirectory.AppendPath(relativeDirectory, checkStartWithBasePath);

            return Directory.CreateDirectory(directory);
        }

    }
}
