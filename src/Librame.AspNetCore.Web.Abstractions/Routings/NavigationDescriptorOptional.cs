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

namespace Librame.AspNetCore.Web.Routings
{
    internal class NavigationDescriptorOptional : INavigationDescriptorOptional
    {
        public string Icon { get; private set; }
            = "glyphicon glyphicon-link";

        public List<AbstractNavigationDescriptor> Children { get; private set; }


        public Func<dynamic, AbstractNavigationDescriptor, bool> VisibilityFactory { get; private set; }
            = (page, nav) => true;

        public Func<dynamic, AbstractNavigationDescriptor, string> ActiveCssClassNameFactory { get; private set; }
            = (page, nav) => string.Empty;

        public Func<dynamic, AbstractNavigationDescriptor, string> ActiveStyleFactory { get; private set; }
            = (page, nav) => string.Empty;


        public string TagId { get; private set; }

        public string TagName { get; private set; }

        public string TagTarget { get; private set; }

        public string TagTitle { get; private set; }


        public INavigationDescriptorOptional ChangeIcon(string newIcon)
        {
            Icon = newIcon;
            return this;
        }

        public INavigationDescriptorOptional ChangeChildren(List<AbstractNavigationDescriptor> newChildren)
        {
            Children = newChildren;
            return this;
        }


        public INavigationDescriptorOptional ChangeVisibilityFactory(Func<dynamic, AbstractNavigationDescriptor, bool> newVisibilityFactory)
        {
            VisibilityFactory = newVisibilityFactory;
            return this;
        }

        public INavigationDescriptorOptional ChangeActiveCssClassNameFactory(Func<dynamic, AbstractNavigationDescriptor, string> newActiveCssClassNameFactory)
        {
            ActiveCssClassNameFactory = newActiveCssClassNameFactory;
            return this;
        }

        public INavigationDescriptorOptional ChangeActiveStyleFactory(Func<dynamic, AbstractNavigationDescriptor, string> newActiveStyleFactory)
        {
            ActiveStyleFactory = newActiveStyleFactory;
            return this;
        }


        public INavigationDescriptorOptional ChangeTagId(string newTagId)
        {
            TagId = newTagId;
            return this;
        }

        public INavigationDescriptorOptional ChangeTagName(string newTagName)
        {
            TagName = newTagName;
            return this;
        }

        public INavigationDescriptorOptional ChangeTagTarget(string newTagTarget)
        {
            TagTarget = newTagTarget;
            return this;
        }

        public INavigationDescriptorOptional ChangeTagTitle(string newTagTitle)
        {
            TagTitle = newTagTitle;
            return this;
        }

    }
}
