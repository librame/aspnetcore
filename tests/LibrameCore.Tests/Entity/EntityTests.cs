using LibrameStandard.Entity;
using LibrameStandard.Entity.DbContexts;
using LibrameStandard.Entity.Descriptors;
using LibrameStandard.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameStandard.Tests.Entity
{
    public class EntityTests
    {
        [Fact]
        public void UseEntityTest()
        {
            var services = new ServiceCollection();
            
            var connectionString = "Data Source=PC-I74910MQ\\SQLEXPRESS;Initial Catalog=librame_test;Integrated Security=True";

            // 默认使用 SqlServerDbContext
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<SqlServerDbContextReader>(options =>
                {
                    options.UseSqlServer(connectionString, sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                });
                //.AddDbContext<SqlServerDbContextWriter>(options =>
                //{
                //    options.UseSqlServer(connectionString, sql =>
                //    {
                //        sql.UseRowNumberForPaging();
                //        sql.MaxBatchSize(50);
                //    });
                //});

            // 默认实体程序集
            var defaultAssemblies = TypeUtility.AsAssemblyName<Article>().Name;

            // 注册 Librame （非 MVC；默认使用内存配置源）
            var builder = services.AddLibrameByMemory(options =>
            {
                // 修改默认的数据库上下文类型名
                options[EntityAutomappingSetting.GetAutomappingDbContextTypeNameKey(0)]
                    = typeof(SqlServerDbContextReader).AsAssemblyQualifiedNameWithoutVCP();
                options[EntityAutomappingSetting.GetAutomappingDbContextTypeNameKey(1)]
                    = typeof(SqlServerDbContextWriter).AsAssemblyQualifiedNameWithoutVCP();

                //// 默认不启用读写分离
                //options[EntityAdapterSettings.EnableReadWriteSeparationKey]
                //    = false.ToString();

                // 修改默认的实体程序集（读写）
                options[EntityAutomappingSetting.GetAutomappingAssembliesKey(0)]
                    = defaultAssemblies;
                //options[EntityAutomappingSetting.GetAutomappingAssembliesKey(1)]
                //    = defaultAssemblies;
            });

            // 获取实体适配器
            var adapter = builder.GetEntityAdapter();
            
            // 初始化文章
            var article = new Article
            {
                Title = "Test Title",
                Descr = "Test Descr"
            };
            
            var repository = adapter.GetSqlServerRepository<Article>();

            // 标题不能重复
            Article dbArticle;
            if (!repository.Exists(p => p.Title == article.Title, out dbArticle))
            {
                dbArticle = repository.Writer.Create(article);
            }
            else
            {
                article = dbArticle;
            }

            // 对比
            Assert.Equal(article.CreateTime, dbArticle.CreateTime);
        }
    }

    //[Table("Articles")]
    public class Article : AbstractCreateDataIdDescriptor<int>
    {
        public string Title { get; set; }
        
        public string Descr { get; set; }
    }

}
