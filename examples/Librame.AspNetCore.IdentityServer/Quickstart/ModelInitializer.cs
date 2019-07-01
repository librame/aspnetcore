using LibrameStandard.Extensions.Entity;
using Librame.AspNet.Identity.Stores;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;

namespace LibrameCore.IdentityServer
{
    public class ModelInitializer
    {

        public static void ReadyRolesAndUsers(IEfCoreRoleStore roles, IEfCoreUserRoleStore users)
        {
            // Roles
            if (!roles.RoleRepository.Exists())
            {
                // Add Data
                var list = new List<IdentityRole>();

                list.Add(new IdentityRole
                {
                    Name = "超级管理员",
                    NormalizedName = "SuperAdministrator"
                });
                list.Add(new IdentityRole
                {
                    Name = "管理员",
                    NormalizedName = "Administrator"
                });
                list.Add(new IdentityRole
                {
                    Name = "注册用户",
                    NormalizedName = "Register"
                });

                roles.RoleRepository.Create(list);
            }

            // Users
            if (!users.UserRepository.Exists())
            {
                // Add Data
                var one = new IdentityUser
                {
                    UserName = "测试用户1",
                    NormalizedUserName = "TestUserOne",
                    Email = "testuserone@domain.com",
                    NormalizedEmail = "testuserone@domain.com",
                    PhoneNumber = "13100000000"
                };
                // Hash Password
                one.PasswordHash = users.PasswordHasher.HashPassword(one, "123456");

                users.CreateAsync(one, CancellationToken.None).Wait();

                // Add Claims
                users.AddClaimsAsync(one, new List<Claim>
                    {
                        new Claim("given_name", "One"),
                        new Claim("family_name", "TestUser")
                    },
                    CancellationToken.None).Wait();

                var two = new IdentityUser
                {
                    UserName = "测试用户2",
                    NormalizedUserName = "TestUserTwo",
                    Email = "testusertwo@domain.com",
                    NormalizedEmail = "testusertwo@domain.com",
                    PhoneNumber = "13200000000"
                };
                // Hash Password
                two.PasswordHash = users.PasswordHasher.HashPassword(two, "123456");

                users.CreateAsync(two, CancellationToken.None).Wait();

                // Add Claims
                users.AddClaimsAsync(two, new List<Claim>
                    {
                        new Claim("given_name", "Two"),
                        new Claim("family_name", "TestUser")
                    },
                    CancellationToken.None).Wait();
            }
        }

    }
}
