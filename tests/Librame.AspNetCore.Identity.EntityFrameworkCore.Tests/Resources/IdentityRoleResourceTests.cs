﻿using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityRoleResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionStringLocalizer<IdentityRoleResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionStringLocalizer<IdentityRoleResource> localizer, string cultureName)
        {
            BuilderGlobalization.RegisterCultureInfos(cultureName);

            var id = localizer[r => r.Id];
            Assert.False(id.ResourceNotFound);

            var normalizedName = localizer[r => r.NormalizedName];
            Assert.False(normalizedName.ResourceNotFound);

            var name = localizer[r => r.Name];
            Assert.False(name.ResourceNotFound);

            var concurrencyStamp = localizer[r => r.ConcurrencyStamp];
            Assert.False(concurrencyStamp.ResourceNotFound);
        }

    }
}