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
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.AspNetCore.Mvc.Razor
{
    /// <summary>
    /// ������ͼλ����չ����
    /// </summary>
    /// <example>
    /// For the default case with no areas, views are generated with the following patterns (assuming controller is
    /// "Home", action is "Index" and language is "en")
    /// Views/Home/en/Action
    /// Views/Home/Action
    /// Views/Shared/en/Action
    /// Views/Shared/Action
    /// </example>
    public class ResetLanguageViewLocationExpander : IViewLocationExpander
    {
        private const string ValueKey = "language";
        private readonly LanguageViewLocationExpanderFormat _format;
        private readonly bool _lookupParentCultureInfo;

        /// <summary>
        /// ����һ�� <see cref="ResetLanguageViewLocationExpander"/> Ĭ��ʵ����
        /// </summary>
        public ResetLanguageViewLocationExpander()
            : this(LanguageViewLocationExpanderFormat.Suffix, false)
        {
        }

        /// <summary>
        /// ����һ�� <see cref="ResetLanguageViewLocationExpander"/> ʵ����
        /// </summary>
        /// <param name="format">������ <see cref="LanguageViewLocationExpanderFormat"/>��</param>
        /// <param name="lookupParentCultureInfo">�Ƿ����������Ļ���Ϣ��</param>
        public ResetLanguageViewLocationExpander(LanguageViewLocationExpanderFormat format,
            bool lookupParentCultureInfo)
        {
            _format = format;
            _lookupParentCultureInfo = lookupParentCultureInfo;
        }


        /// <inheritdoc />
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Using CurrentUICulture so it loads the locale specific resources for the views.
            context.Values[ValueKey] = CultureInfo.CurrentUICulture.Name;
        }


        /// <inheritdoc />
        public virtual IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            context.Values.TryGetValue(ValueKey, out var value);

            if (!string.IsNullOrEmpty(value))
            {
                CultureInfo culture;
                try
                {
                    culture = new CultureInfo(value);
                }
                catch (CultureNotFoundException)
                {
                    return viewLocations;
                }

                return ExpandViewLocationsCore(viewLocations, culture);
            }

            return viewLocations;
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations, CultureInfo cultureInfo)
        {
            foreach (var location in viewLocations)
            {
                if (_lookupParentCultureInfo)
                {
                    // ׷�ݸ�ϵ�Ļ���Ϣ��Դ��λ
                    var temporaryCultureInfo = cultureInfo;

                    while (temporaryCultureInfo != temporaryCultureInfo.Parent)
                    {
                        if (_format == LanguageViewLocationExpanderFormat.SubFolder)
                        {
                            yield return location.Replace("{0}", temporaryCultureInfo.Name + "/{0}");
                        }
                        else
                        {
                            yield return location.Replace("{0}", "{0}." + temporaryCultureInfo.Name);
                        }

                        temporaryCultureInfo = temporaryCultureInfo.Parent;
                    }
                }
                else
                {
                    if (_format == LanguageViewLocationExpanderFormat.SubFolder)
                    {
                        yield return location.Replace("{0}", cultureInfo.Name + "/{0}");
                    }
                    else
                    {
                        yield return location.Replace("{0}", "{0}." + cultureInfo.Name);
                    }
                }

                yield return location;
            }
        }

    }
}