using LibrameStandard.Entity;
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
            services.AddEntityFrameworkSqlServer().AddDbContext<SqlServerDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sql =>
                {
                    sql.UseRowNumberForPaging();
                    sql.MaxBatchSize(50);
                });
            });

            // 注册 Librame （非 MVC；默认使用内存配置源）
            var builder = services.AddLibrameByMemory(options =>
            {
                // 修改默认的数据库上下文类型名
                options[EntityAutomappingSetting.DbContextTypeNameKey]
                    = typeof(SqlServerDbContext).AsAssemblyQualifiedNameWithoutVCP();

                // 修改默认的实体映射程序集
                options[EntityAutomappingSetting.AssembliesKey]
                    = TypeUtility.AsAssemblyName<Article>().Name;
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
