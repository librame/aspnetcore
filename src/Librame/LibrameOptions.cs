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
using System.Runtime.InteropServices;

namespace Librame
{
    using Utility;

    /// <summary>
    /// Librame 选项接口。
    /// </summary>
    public interface ILibrameOptions
    {
    }


    /// <summary>
    /// Librame 选项。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class LibrameOptions : ILibrameOptions
    {

        #region AuthId

        /// <summary>
        /// 授权编号键名。
        /// </summary>
        public static readonly string AuthIdKey = nameof(AuthId);

        /// <summary>
        /// 默认授权编号。
        /// </summary>
        public static readonly string DefaultAuthId = "2549A5C4BF06F34E9E53847979253E98";

        /// <summary>
        /// 授权编号。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultAuthId"/>。
        /// </value>
        public string AuthId { get; set; } = DefaultAuthId;

        #endregion


        #region BaseDirectory

        /// <summary>
        /// 基础目录键名。
        /// </summary>
        public static readonly string BaseDirectoryKey = nameof(BaseDirectory);

        /// <summary>
        /// 默认基础目录。
        /// </summary>
        public static readonly string DefaultBaseDirectory = AppContext.BaseDirectory;

        /// <summary>
        /// 基础目录。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultBaseDirectory"/>。
        /// </value>
        public string BaseDirectory { get; set; } = DefaultBaseDirectory;

        #endregion


        #region Encoding

        /// <summary>
        /// 字符编码键名。
        /// </summary>
        public static readonly string EncodingKey = nameof(Encoding);

        /// <summary>
        /// 默认字符编码。
        /// </summary>
        public static readonly string DefaultEncoding = "utf-8";

        /// <summary>
        /// 字符编码。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultEncoding"/>。
        /// </value>
        public string Encoding { get; set; } = DefaultEncoding;

        #endregion


        private Algorithm.AlgorithmOptions _algorithm;
        /// <summary>
        /// 算法选项。
        /// </summary>
        public Algorithm.AlgorithmOptions Algorithm
        {
            get
            {
                if (_algorithm == null)
                    _algorithm = new Algorithm.AlgorithmOptions();

                return _algorithm;
            }
            set
            {
                _algorithm = value.NotNull(nameof(value));
            }
        }

        
        private Entity.EntityOptions _entity;
        /// <summary>
        /// 实体选项。
        /// </summary>
        public Entity.EntityOptions Entity
        {
            get
            {
                if (_entity == null)
                    _entity = new Entity.EntityOptions();

                return _entity;
            }
            set
            {
                _entity = value.NotNull(nameof(value));
            }
        }

    }
}
