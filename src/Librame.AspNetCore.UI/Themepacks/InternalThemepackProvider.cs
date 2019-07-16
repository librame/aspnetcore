#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

//using Microsoft.Extensions.Options;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;

//namespace Librame.AspNetCore.UI
//{
//    using Extensions;

//    /// <summary>
//    /// 内部主题提供程序。
//    /// </summary>
//    internal class InternalThemepackProvider : IThemepackProvider
//    {
//        private readonly IEnumerable<IThemepackInfo> _themepackInfos;
//        private readonly UIBuilderOptions _options;


//        /// <summary>
//        /// 构造一个 <see cref="InternalThemepackProvider"/> 实例。
//        /// </summary>
//        /// <param name="themepackInfos">给定的 <see cref="IEnumerable{IThemepackInfo}"/>。</param>
//        /// <param name="options">给定的 <see cref="IOptions{UIBuilderOptions}"/>。</param>
//        public InternalThemepackProvider(IEnumerable<IThemepackInfo> themepackInfos,
//            IOptions<UIBuilderOptions> options)
//        {
//            _themepackInfos = themepackInfos.NotNullOrEmpty(nameof(themepackInfos));
//            _options = options.NotNull(nameof(options)).Value;
//        }


//        /// <summary>
//        /// 主题信息数。
//        /// </summary>
//        public int Count
//            => _themepackInfos.Count();

//        /// <summary>
//        /// 当前主题信息。
//        /// </summary>
//        public IThemepackInfo Current
//        {
//            get
//            {
//                var predicate = _options.Themepacks.DefaultFactory;
//                if (predicate.IsNull())
//                    return _themepackInfos.First();

//                return _themepackInfos.Single(predicate);
//            }
//        }

//        /// <summary>
//        /// 主题信息集合。
//        /// </summary>
//        public IEnumerable<KeyValuePair<string, IThemepackInfo>> ThemepackInfos
//            => _themepackInfos.Select(info => new KeyValuePair<string, IThemepackInfo>(info.Name, info));


//        /// <summary>
//        /// 获取指定名称的主题信息。
//        /// </summary>
//        /// <param name="name">给定的名称。</param>
//        /// <returns>返回 <see cref="IThemepackInfo"/>。</returns>
//        public IThemepackInfo GetThemepackInfo(string name)
//        {
//            name.NotNullOrEmpty(nameof(name));

//            return _themepackInfos.Single(info => info.Name == name);
//        }


//        /// <summary>
//        /// 获取枚举器。
//        /// </summary>
//        /// <returns>返回 <see cref="IEnumerator{IThemepackInfo}"/>。</returns>
//        public IEnumerator<IThemepackInfo> GetEnumerator()
//        {
//            return _themepackInfos.GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }
//}
