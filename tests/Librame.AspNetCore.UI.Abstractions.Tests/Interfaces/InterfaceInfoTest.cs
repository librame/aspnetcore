using System;
using Microsoft.Extensions.Localization;
using Xunit;

namespace Librame.AspNetCore.UI.Tests
{
    public class InterfaceInfoTest
    {
        class TestInterfaceInfo : AbstractInterfaceInfo
        {
            public TestInterfaceInfo()
                : base(GetService)
            {
            }


            public override string Name => "TestUi";

            public override string Authors => "Test";

            public override string Contact => "Test";

            public override string Copyright => "Test";

            public override IStringLocalizer Localizer => throw new NotImplementedException();

            public static object GetService(Type serviceType)
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void AllTest()
        {
            var info = new TestInterfaceInfo();
            Assert.NotEmpty(info.Name);
            Assert.NotEmpty(info.Authors);
            Assert.NotEmpty(info.Contact);
            Assert.NotEmpty(info.Copyright);
        }
    }
}
