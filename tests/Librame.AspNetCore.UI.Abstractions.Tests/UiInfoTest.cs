using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xunit;

namespace Librame.AspNetCore.UI.Tests
{
    using Extensions.Core;

    public class UiInfoTest
    {
        class TestUiInfo : AbstractUiInfo
        {
            public override string Name => "TestUi";

            public override string Title => "测试用户界面";

            public override string Author => "Test";

            public override string Contact => "Test";

            public override string Copyright => "Test";

            public override void AddLocalizers(ref ConcurrentDictionary<string, IStringLocalizer> localizers, ServiceFactoryDelegate serviceFactory)
            {
                throw new NotImplementedException();
            }

            public override void AddNavigations(ref ConcurrentDictionary<string, List<NavigationDescriptor>> navigations, ServiceFactoryDelegate serviceFactory)
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void AllTest()
        {
            var info = new TestUiInfo();
            Assert.NotEmpty(info.Name);
            Assert.NotEmpty(info.Title);
            Assert.NotEmpty(info.Author);
            Assert.NotEmpty(info.Contact);
            Assert.NotEmpty(info.Copyright);

            var localizers = new ConcurrentDictionary<string, IStringLocalizer>();
            var navigations = new ConcurrentDictionary<string, List<NavigationDescriptor>>();
            Assert.Throws<NotImplementedException>(() => info.AddLocalizers(ref localizers, null));
            Assert.Throws<NotImplementedException>(() => info.AddNavigations(ref navigations, null));
        }
    }
}
