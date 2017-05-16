using Librame.Entity;
using Librame.Entity.Descriptors;
using Librame.Entity.Providers;
using Librame.Entity.Repositories;
using Librame.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.Tests.Entity
{
    public class EntityTests
    {
        [Fact]
        public void UseEntityTest()
        {
            var services = new ServiceCollection();

            // 注册实体框架
            var connectionString = "Data Source=(local);Initial Catalog=librame;Integrated Security=False;Persist Security Info=False;User ID=librame;Password=123456";
            //services.AddEntityFrameworkSqlServer().AddDbContext<DbContextProvider>(options =>
            //{
            //    options.UseSqlServer(connectionString, sql =>
            //    {
            //        sql.UseRowNumberForPaging();
            //        sql.MaxBatchSize(50);
            //    });
            //});

            // 注册 Librame （默认使用内存配置源）
            var builder = services.AddLibrameByMemory(source =>
            {
                // 重置实体程序集
                source[EntityOptions.AutomappingAssembliesKey]
                    = TypeUtil.GetAssemblyName<Article>().Name;
            })
            .UseEntity(connectionString: connectionString); // 使用内部集成 SQLSERVER 数据源的实体框架模块
            
            // 初始化文章
            var article = new Article
            {
                Title = "Test Title",
                Descr = "Test Descr"
            };
            
            var repository = builder.GetRepository<Article>();

            // 标题不能重复
            Article dbArticle;
            if (!repository.Exists(p => p.Title == article.Title, out dbArticle))
            {
                dbArticle = repository.Create(article);
            }
            else
            {
                article = dbArticle;
            }

            // 对比
            Assert.Equal(article.CreateTime, dbArticle.CreateTime);
        }
    }

    
    public class Article : AbstractCreateDataIdDescriptor<int>
    {
        public string Title { get; set; }
        
        public string Descr { get; set; }
    }

}
