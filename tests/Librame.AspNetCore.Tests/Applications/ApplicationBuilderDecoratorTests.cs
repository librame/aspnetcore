using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using Xunit;

namespace Librame.AspNetCore.Tests
{
    using Applications;
    using Builders;
    using Extensions;

    public class ApplicationBuilderDecoratorTests
    {
        [Fact]
        public void AllTest()
        {
            var builder = new TestApplicationBuilder();

            var baseDecoratorType = typeof(IApplicationBuilderDecorator);
            var decorator = typeof(AspNetCoreCoreBuilderDependency).Assembly
                .GetType($"{baseDecoratorType.Namespace}.{baseDecoratorType.Name.TrimStart('I')}")
                .EnsureCreate<IApplicationBuilderDecorator>(builder);

            Assert.NotNull(decorator);
            Assert.NotNull(decorator.Source);
        }


        class TestApplicationBuilder : IApplicationBuilder
        {
            public IServiceProvider ApplicationServices
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            public IDictionary<string, object> Properties
                => throw new NotImplementedException();

            public IFeatureCollection ServerFeatures
                => throw new NotImplementedException();

            public RequestDelegate Build()
                => throw new NotImplementedException();

            public IApplicationBuilder New()
                => throw new NotImplementedException();

            public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
                => throw new NotImplementedException();
        }
    }
}
