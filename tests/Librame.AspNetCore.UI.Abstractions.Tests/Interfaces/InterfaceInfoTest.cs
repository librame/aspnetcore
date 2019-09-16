using System;
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

            public override string Title => "²âÊÔ UI ";

            public override string Author => "Test";

            public override string Contact => "Test";

            public override string Copyright => "Test";

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
            Assert.NotEmpty(info.Title);
            Assert.NotEmpty(info.Author);
            Assert.NotEmpty(info.Contact);
            Assert.NotEmpty(info.Copyright);
        }
    }
}
