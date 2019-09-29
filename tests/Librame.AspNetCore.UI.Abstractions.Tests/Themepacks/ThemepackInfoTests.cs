using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using System;
using Xunit;

namespace Librame.AspNetCore.UI.Tests
{
    public class ThemepackInfoTests
    {
        class TestThemepackInfo : AbstractThemepackInfo
        {
            public override string Name => "TestThemepack";

            public override string Authors => "Test";

            public override string Contact => "Test";

            public override string Copyright => "Test";

            public override IStringLocalizer Localizer => throw new NotImplementedException();

            public override IFileProvider GetStaticFileProvider()
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void AllTest()
        {
            var info = new TestThemepackInfo();
            Assert.NotEmpty(info.Name);
            Assert.NotEmpty(info.Authors);
            Assert.NotEmpty(info.Contact);
            Assert.NotEmpty(info.Copyright);
            Assert.Throws<NotImplementedException>(() => info.GetStaticFileProvider());
        }

    }
}
