using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Api.Tests
{
    using AspNetCore.Api;
    using Extensions;

    public class IdentityGraphApiQueryTests
    {
        private readonly string _testRoleId = "b38c4952-e791-9981-973f-39f5ce48b046";
        private readonly string _testRoleName = "Administrator";
        private readonly string _testUserId = "5e0a511e-85d0-ad7d-5ef1-39f5ce48b084";
        private readonly string _testUserName = "librame@librame.net";


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
