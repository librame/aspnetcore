using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Librame.AspNetCore.Identity.Api.Tests
{
    using AspNetCore.Api;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using Extensions;

    public class IdentityGraphApiQueryTests
    {
        private string _testRoleId;
        private string _testRoleName;
        private string _testUserId;
        private string _testUserName;


        public IdentityGraphApiQueryTests()
        {
            var dependency = TestServiceProvider.Current.GetRequiredService<IdentityBuilderDependency>();
            _testRoleName = dependency.Options.Stores.Initialization.DefaultRoleNames[0];
            _testUserName = dependency.Options.Stores.Initialization.DefaultUserEmails[0];

            var stores = TestServiceProvider.Current.GetRequiredService<IdentityStoreHub>();
            _testRoleId = stores.Roles.First(p => p.Name == _testRoleName).Id.ToString();
            _testUserId = stores.Users.First(p => p.UserName == _testUserName).Id.ToString();
        }


        [Fact]
        public async void RoleTypeFieldsTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            // PageRoles
            var query = @"{
                pageRoles(index: 1, size: 10, includeRoleClaims: false) {
                    id
                    name
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // RoleId
            query = @$"{{
                roleId(id: ""{_testRoleId}"") {{
                    id
                    name
                }}
            }}";

            result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // RoleName
            query = @$"{{
                roleName(name: ""{_testRoleName}"") {{
                    id
                    name
                }}
            }}";

            result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);
        }


        [Fact]
        public async void RoleClaimTypeFieldsTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            // PageRoleClaims
            var query = @"{
                pageRoleClaims(index: 1, size: 10, includeRole: false) {
                    id
                    claimType
                    claimValue
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // roleClaimId
            query = @"{
                roleClaimId(id: 1, includeRole: true) {
                    id
                    claimType
                    claimValue
                    role {
                        id
                        name
                    }
                }
            }";

            result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);
        }


        [Fact]
        public async void UserTypeFieldsTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            // PageUsers
            var query = @"{
                pageUsers(index: 1, size: 10, includeRoles: false, includeUserClaims: false, includeUserLogins: false, includeUserTokens: false) {
                    id
                    userName
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // UserId
            query = @$"{{
                userId(id: ""{_testUserId}"", includeRoles: false, includeUserClaims: false, includeUserLogins: false, includeUserTokens: false) {{
                    id
                    userName
                }}
            }}";

            result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // UserName
            query = @$"{{
                userName(name: ""{_testUserName}"", includeRoles: false, includeUserClaims: false, includeUserLogins: false, includeUserTokens: false) {{
                    id
                    userName
                }}
            }}";

            result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);
        }


        [Fact]
        public async void UserClaimTypeFieldsTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            // PageUserClaims
            var query = @"{
                pageUserClaims(index: 1, size: 10, includeUser: false) {
                    id
                    claimType
                    claimValue
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // userClaimId
            query = @"{
                userClaimId(id: 1, includeUser: true) {
                    id
                    claimType
                    claimValue
                    user {
                        id
                        userName
                    }
                }
            }";

            result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);
        }


        [Fact]
        public async void UserLoginTypeFieldsTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            // PageUserLogins
            var query = @"{
                pageUserLogins(index: 1, size: 10, includeUser: false) {
                    loginProvider
                    providerDisplayName
                    providerKey
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);
        }


        [Fact]
        public async void UserTokenTypeFieldsTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            // PageUserTokens
            var query = @"{
                pageUserTokens(index: 1, size: 10) {
                    loginProvider
                    name
                    value
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);
        }

    }
}
