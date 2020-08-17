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
                pageRoles(index: 1, size: 10) {
                    id
                    name
                    createdTime
                    createdBy
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
                    createdTime
                    createdBy
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
                    createdTime
                    createdBy
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

            // roleClaimId
            var query = @"{
                roleClaimId(id: 1) {
                    id
                    roleId
                    claimType
                    claimValue
                    createdTime
                    createdBy
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // roleClaimRoleId
            query = @$"{{
                roleClaimRoleId(roleId: ""{_testRoleId}"") {{
                    id
                    roleId
                    claimType
                    claimValue
                    createdTime
                    createdBy
                }}
            }}";

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
                pageUsers(index: 1, size: 10) {
                    id
                    userName
                    createdTime
                    createdBy
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // UserId
            query = @$"{{
                userId(id: ""{_testUserId}"") {{
                    id
                    userName
                    createdTime
                    createdBy
                }}
            }}";

            result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // UserName
            query = @$"{{
                userName(name: ""{_testUserName}"") {{
                    id
                    userName
                    createdTime
                    createdBy
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

            // userClaimId
            var query = @"{
                userClaimId(id: 1) {
                    id
                    userId
                    claimType
                    claimValue
                    createdTime
                    createdBy
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);

            // userClaimUserId
            query = @$"{{
                userClaimUserId(userId: ""{_testUserId}"") {{
                    id
                    userId
                    claimType
                    claimValue
                    createdTime
                    createdBy
                }}
            }}";

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
                pageUserLogins(index: 1, size: 10) {
                    userId
                    loginProvider
                    providerDisplayName
                    providerKey
                    createdTime
                    createdBy
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);
        }


        [Fact]
        public async void UserRoleTypeFieldsTest()
        {
            var request = TestServiceProvider.Current.GetRequiredService<IApiRequest>();

            // UserRoles
            var query = @$"{{
                userRoles(userId: ""{_testUserId}"") {{
                    id
                    name
                    createdTime
                    createdBy
                }}
            }}";

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
                    userId
                    loginProvider
                    name
                    value
                    createdTime
                    createdBy
                }
            }";

            var result = await request.ExecuteAsync(query).ConfigureAwait();
            Assert.True(result.Succeeded);

            var json = await request.ExecuteJsonAsync(query).ConfigureAwait();
            Assert.NotEmpty(json);
        }

    }
}
