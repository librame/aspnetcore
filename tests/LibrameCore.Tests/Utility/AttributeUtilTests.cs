using LibrameCore.Utility;
using System.ComponentModel;
using System.Reflection;
using Xunit;

namespace LibrameCore.Tests.Utility
{
    public class AttributeUtilTests
    {
        internal const string DESCRIPTION = "测试属性";
        internal const int DEFAULT_VALUE = 1;

        [Fact]
        public void AttributeTest()
        {
            var type = typeof(TestAttribute);

            var description = type.Attribute<DescriptionAttribute>();
            Assert.NotNull(description);
            Assert.Equal(description.Description, DESCRIPTION);

            var defaultValue = type.GetProperty("Id")?.Attribute<DefaultValueAttribute>();
            Assert.NotNull(defaultValue);
            Assert.Equal((int)defaultValue.Value, DEFAULT_VALUE);
        }

    }

    [Description(AttributeUtilTests.DESCRIPTION)]
    public class TestAttribute
    {
        [DefaultValue(AttributeUtilTests.DEFAULT_VALUE)]
        public int Id { get; set; }
    }

}
