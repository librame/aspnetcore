﻿using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameCore.Mvc.Tests.Authentication
{
    public class AuthenticationTests
    {
        [Fact]
        public void UseAuthenticationTest()
        {
            var services = new ServiceCollection();

            // 注册 Librame MVC （默认使用内存配置源）
            var builder = services.AddLibrameMvcByMemory();

            // 获取认证适配器
            var adapter = builder.GetAuthenticationAdapter();
            Assert.NotNull(adapter);
            Assert.NotNull(adapter.TokenGenerator);
            Assert.NotNull(adapter.UserAuthentication);

            var tokenGenerateOptions = adapter.TokenGenerator.Options;
            Assert.NotNull(tokenGenerateOptions);
            Assert.NotNull(tokenGenerateOptions.SigningCredentials);
        }

    }
}