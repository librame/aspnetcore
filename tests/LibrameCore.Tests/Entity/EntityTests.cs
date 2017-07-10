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
            services.AddLibrameByMemory(entityOptionsAction: opts =>
            {
                // 修改默认的数据库上下文类型名
                opts.Automappings[0].DbContextTypeName
                    = typeof(SqlServerDbContextReader).AsAssemblyQualifiedNameWithoutVCP();
                opts.Automappings[1].DbContextTypeName
                    = typeof(SqlServerDbContextWriter).AsAssemblyQualifiedNameWithoutVCP();

                //// 默认不启用读写分离
                //options[EntityAdapterSettings.EnableReadWriteSeparationKey]
                //    = false.ToString();

                // 修改默认的实体程序集（读写）
                opts.Automappings[0].Assemblies = defaultAssemblies;
                opts.Automappings[1].Assemblies = defaultAssemblies;
            });

            var serviceProvider = services.BuildServiceProvider();

            // 初始化文章
            var article = new Article
            {
                Title = "Test Title",
                Descr = "Test Descr"
            };

            //var repository = serviceProvider.GetLibrameRepository<SqlServerDbContextReader, Article>();
            var repository = serviceProvider.GetLibrameRepositoryReader<Article>();

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
