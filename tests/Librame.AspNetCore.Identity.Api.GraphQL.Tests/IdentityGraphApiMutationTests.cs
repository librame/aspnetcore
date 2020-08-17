using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Librame.AspNetCore.Identity.Api.Tests
{
    using AspNetCore.Api;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using Extensions;

    public class IdentityGraphApiMutationTests
    {
        [Fact]
        public async void LoginTypeFieldTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            var dependency = TestServiceProvider.Current.GetRequiredService<IdentityBuilderDependency>();
            var defaultUser = dependency.Options.Stores.Initialization.DefaultUserEmails[0];
            var defaultPassword = dependency.Options.Stores.Initialization.DefaultPassword;

            // mutation
            var query = @$"{{
                ""query"": ""mutation($user: LoginInput!) {{
                    login(user: $user) {{
                        succeeded
                        isLockedOut
                        isNotAllowed
                        requiresTwoFactor
                    }}
                }}"",
                ""variables"": {{
                     ""user"": {{
                         ""username"": ""{defaultUser}"",
                         ""password"": ""{defaultPassword}"",
                         ""rememberMe"": true
                     }}
                 }}
            }}";

            request.Populate(query);

            var result = await request.ExecuteAsync().ConfigureAwait();
            Assert.False(result.Succeeded); // HttpContext must not be null.
        }


        [Fact]
        public async void RegisterTypeFieldTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            var dependency = TestServiceProvider.Current.GetRequiredService<IdentityBuilderDependency>();
            var defaultUser = dependency.Options.Stores.Initialization.DefaultUserEmails[0];
            var defaultPassword = dependency.Options.Stores.Initialization.DefaultPassword;

            // mutation
            var query = @$"{{
                ""query"": ""mutation($user: RegisterInput!) {{
                    register(user: $user) {{
                        succeeded
                        errors {{
                            code
                            description
                        }}
                    }}
                }}"",
                ""variables"": {{
                     ""user"": {{
                         ""username"": ""{defaultUser}"",
                         ""password"": ""{defaultPassword}"",
                         ""confirmEmailUrl"": null
                     }}
                 }}
            }}";

            request.Populate(query);

            var stores = TestServiceProvider.Current.GetRequiredService<IdentityStoreHub>();

            var result = await request.ExecuteAsync().ConfigureAwait();
            if (stores.Accessor.ContainsInitializationData)
                Assert.False(result.Succeeded);
            else
                Assert.True(result.Succeeded);

            var registerResult = result.LookupDataValue<bool>("register", "succeeded");
            Assert.False(registerResult);

            var errors = (Dictionary<string, object>)result.LookupDataValue<List<object>>("register", "errors")[0];
            Assert.Equal("DuplicateUserName", errors["code"]); // errors["description"]: 用户名称“xxx”已被使用。
        }

    }
}
