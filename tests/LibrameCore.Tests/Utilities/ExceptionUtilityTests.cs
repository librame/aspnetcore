using LibrameCore.Utilities;
using System;
using Xunit;

namespace LibrameCore.Tests.Utility
{
    public class ExceptionUtilityTests
    {
        [Fact]
        public void NotOutOfRangeTest()
        {
            try
            {
                var i = 30;

                i = i.NotOutOfRange(1, 9, nameof(i));
            }
            catch (Exception ex)
            {
                Assert.NotEmpty(ex.AsInnerMessage());
            }
        }

        [Fact]
        public void CanAssignableFromTypeTest()
        {
            var baseType = typeof(IBaseType);
            var fromType = typeof(BaseType);

            var resultType = baseType.CanAssignableFromType(fromType);

            Assert.Equal(fromType, resultType);
        }


        public void SameTypeTest()
        {
            object obj = new BaseType();

            var result = obj.SameType<BaseType>(nameof(obj));

            Assert.Equal(obj, result);
        }

    }


    public interface IBaseType
    {
    }

    public class BaseType : IBaseType
    {
    }
}
