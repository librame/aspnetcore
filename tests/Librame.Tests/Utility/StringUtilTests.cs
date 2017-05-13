using Librame.Utility;
using System;
using Xunit;

namespace Librame.Tests.Utility
{
    public class StringUtilTests
    {
        [Fact]
        public void AsTest()
        {
            // Default: false
            var b = true.ToString();
            Assert.True(b.AsBool());

            // Default: DateTime.MaxValue
            var dt = DateTime.Now.ToString();
            Assert.NotEqual(dt.AsDateTime(), DateTime.MaxValue);

            // Default: Guid.Empty
            var g = Guid.NewGuid().ToString();
            Assert.NotEqual(g.AsGuid(), Guid.Empty);

            // Default: float.NaN
            var f = float.MaxValue.ToString();
            Assert.NotEqual(f.AsFloat(), float.NaN);

            // Default: double.NaN
            var d = double.MaxValue.ToString();
            Assert.NotEqual(d.AsDouble(), double.NaN);

            // Default: 0L
            var l = long.MaxValue.ToString();
            Assert.NotEqual(l.AsLong(), 0L);

            // Default: 0
            var i = int.MaxValue.ToString();
            Assert.NotEqual(i.AsLong(), 0);

            // Default: string.Empty
            var s = "test";
            Assert.NotEqual(s.As(), string.Empty);
        }


        #region Singular & Plural

        private readonly string _word = "aphorism";

        [Fact]
        public void AsSingularizeTest()
        {
            var singular = _word.AsSingularize();

            Assert.Equal(singular, "aphorisms");
        }

        [Fact]
        public void AsPluralizeTest()
        {
            var plural = "aphorisms".AsPluralize();

            Assert.Equal(plural, _word);
        }

        #endregion


        #region Naming Conventions

        private readonly string[] _words = "one,two,three,four,five,six,seven,eight,nine,ten".Split(',');

        [Fact]
        public void AsPascalCasingTest()
        {
            var casing = _words.AsPascalCasing();

            Assert.NotEmpty(casing);
        }

        [Fact]
        public void AsCamelCasingTest()
        {
            var casing = _words.AsCamelCasing();

            Assert.NotEmpty(casing);
        }

        #endregion

    }
}
