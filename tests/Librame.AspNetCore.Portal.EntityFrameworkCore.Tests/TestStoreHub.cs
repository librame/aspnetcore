using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Portal.Tests
{
    using Extensions.Data;

    public interface ITestStoreHub : IPortalStoreHub<PortalDbContextAccessor>
    {
        IList<PortalClaim> GetClaims();

        IList<PortalCategory> GetCategories();

        IList<PortalPane> GetPanes();

        IList<PortalSource> GetSources();


        /// <summary>
        /// 供手动切换读取写入库测试。
        /// </summary>
        /// <returns></returns>
        ITestStoreHub UseWriteDbConnection();

        /// <summary>
        /// 供手动切换读取写入库测试。
        /// </summary>
        /// <returns></returns>
        ITestStoreHub UseDefaultDbConnection();
    }


    public class TestStoreHub : PortalStoreHub<PortalDbContextAccessor>, ITestStoreHub
    {
        public TestStoreHub(IPortalIdentifierService identifierService, IAccessor accessor) // or PortalDbContextAccessor
            : base(accessor)
        {
        }


        public IList<PortalClaim> GetClaims()
        {
            return Accessor.Claims.ToList();
        }

        public IList<PortalCategory> GetCategories()
        {
            return Accessor.Categories.ToList();
        }

        public IList<PortalPane> GetPanes()
        {
            return Accessor.Panes.ToList();
        }

        public IList<PortalSource> GetSources()
        {
            return Accessor.Sources.ToList();
        }


        public ITestStoreHub UseDefaultDbConnection()
        {
            Accessor.TryChangeDbConnection(t => t.DefaultConnectionString);
            return this;
        }

        public ITestStoreHub UseWriteDbConnection()
        {
            Accessor.TryChangeDbConnection(t => t.WriteConnectionString);
            return this;
        }
    }
}
