using LibrameCore.Utilities;
using System;
using Xunit;

namespace LibrameCore.Tests.Utility
{
    public class EnumerableUtilityTests
    {
        [Fact]
        public void JoinStringTest()
        {
            var days = new string[] { "一", "二", "三", "四", "五", "六", "七" };
            var week = days.JoinString("-");
            Assert.NotEmpty(week);

            var guids = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var str = guids.JoinString(g => g.ToString(), ";");
            Assert.NotEmpty(str);
        }

        [Fact]
        public void InvokeTest()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            items.Invoke(a =>
            {
                Assert.True(a > 0);
            });
        }

    }
}
