using LibrameCore.Extensions.Authentication;
using LibrameCore.Extensions.Authentication.Descriptors;
using LibrameStandard.Extensions.Entity;
using LibrameStandard.Extensions.Entity.DbContexts;
using LibrameStandard.Extensions.Entity.Descriptors;
using LibrameStandard.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace LibrameCore.Tests.Extensions.Authentication.Repositories
{
    public class AuthenticationRepositoryTests
    {
        [Fact]
        public async void UseUserManagerTest()
        {
            var services = new ServiceCollection();

            // 默认使用 SqlServerDbContext
            var connectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_core;Integrated Security=True";
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<SqlServerDbContext>(options =>
                {
                    options.UseSqlServer(connectionString, sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                })
                .AddDbContext<SqlServerDbContextWriter>(options =>
                {
                    options.UseSqlServer(connectionString, sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                });

            // 默认实体程序集
            var defaultAssemblies = TypeUtility.AsAssemblyName<User>().Name;

            services.AddLibrameCore(options =>
            {
                options.PostConfigureEntity = opts =>
                {
                    opts.Automappings.Add(new EntityExtensionOptions.AutomappingOptions
                    {
                        // 修改默认的自映射实体程序集
                        DbContextAssemblies = defaultAssemblies,
                        // 修改默认的数据库上下文类型名为 SQLServer
                        DbContextTypeName = typeof(SqlServerDbContext).AsAssemblyQualifiedNameWithoutVCP(),
                        DbContextWriterTypeName = typeof(SqlServerDbContextWriter).AsAssemblyQualifiedNameWithoutVCP(),
                        // 启用读写分离（默认不启用）
                        ReadWriteSeparation = true
                    });
                };
            });

            var serviceProvider = services.BuildServiceProvider();

            // Test
            var repository = serviceProvider.GetRequiredService<IAuthenticationRepository<Role,
                User, UserRole, int, int, int>>();
            Assert.NotNull(repository);

            var user = new User
            {
                Name = "TestUser",
                Passwd = repository.PasswordManager.Encode("123456"),
                Email = "test@librame.net",
                Phone = "13600000000"
            };

            // 可能会出现数据重复
            var result = await repository.TryCreateUserAsync(user);
            var identity = await repository.ValidateUserAsync(user.Name, user.Passwd);
            Assert.NotNull(identity.User);

            // Default: Administrator
            var roles = await repository.GetRolesAsync(user);

            foreach (var r in roles)
            {
                Assert.NotNull(r);
            }
        }

    }


    /// <summary>
    /// 测试用户。
    /// </summary>
    [DisplayName("用户")]
    public class User : AbstractCIdDataDescriptor<int>, IUserDescriptor<int>
    {
        /// <summary>
        /// 名称。
        /// </summary>
        [DisplayName("名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        [StringLength(500)]
        public string Passwd { get; set; }

        /// <summary>
        /// 邮箱。
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [DisplayName("邮箱")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 电话。
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [DisplayName("电话")]
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 标识。
        /// </summary>
        [DisplayName("标识")]
        [Required]
        [StringLength(50)]
        public string UniqueId { get; set; } = Guid.Empty.ToString();
    }


    /// <summary>
    /// 测试角色。
    /// </summary>
    [DisplayName("角色")]
    public class Role : AbstractCIdDataDescriptor<int>, IRoleDescriptor<int>
    {
        /// <summary>
        /// 名称。
        /// </summary>
        [DisplayName("名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 说明。
        /// </summary>
        [DisplayName("说明")]
        [StringLength(200)]
        public string Descr { get; set; }
    }


    /// <summary>
    /// 测试用户角色。
    /// </summary>
    [DisplayName("用户角色")]
    public class UserRole : AbstractCIdDataDescriptor<int>, IUserRoleDescriptor<int, int, int>
    {
        /// <summary>
        /// 角色。
        /// </summary>
        [DisplayName("角色")]
        [Required]
        public int RoleId { get; set; } = default;

        /// <summary>
        /// 用户。
        /// </summary>
        [DisplayName("用户")]
        [Required]
        public int UserId { get; set; } = default;
    }

}
