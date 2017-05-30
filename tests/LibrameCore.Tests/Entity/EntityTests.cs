using LibrameCore.Entity;
using LibrameCore.Entity.Descriptors;
using LibrameCore.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameCore.Tests.Entity
{
    public class EntityTests
    {
        [Fact]
        public void UseEntityTest()
        {
            var services = new ServiceCollection();

            // 注册实体框架
            var connectionString = "Data Source=PC-I74910MQ\\SQLEXPRESS;Initial Catalog=librame_test;Integrated Security=True";
            
            //// 默认实体模块使用 DbContextProvider，也可更换为自己的需要，但需同时修改下面配置源
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
                //// 修改默认的 DbContextProvider
                //source[EntityOptions.EntityProviderTypeNameKey]
                //    = typeof(DbContextProvider).AssemblyQualifiedNameWithoutVcp();

                // 重置实体程序集
                source[EntityOptions.AutomappingAssembliesKey]
                    = TypeUtility.GetAssemblyName<Article>().Name;
            });

            // 获取实体适配器（因之前未注册 AddEntityFrameworkSqlServer，此处使用内部集成注册，因此连接字符串不能为空）
            var adapter = builder.GetEntityAdapter(connectionString);
            
            // 初始化文章
            var article = new Article
            {
                Title = "Test Title",
                Descr = "Test Descr"
            };
            
            var repository = adapter.GetRepository<Article>();

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
