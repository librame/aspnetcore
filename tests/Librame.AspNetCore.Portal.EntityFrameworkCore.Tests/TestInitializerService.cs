using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Portal.Tests
{
    using Extensions.Data;

    public class TestInitializerService<TAccessor> : InitializerServiceBase<TAccessor, PortalIdentifierService>
        where TAccessor : PortalDbContextAccessor
    {
        private IList<PortalClaim> _claims;
        private IList<PortalCategory> _categories;
        private IList<PortalPane> _panes;
        private IList<PortalSource> _sources;


        public TestInitializerService(IPortalIdentifierService identifier, ILoggerFactory loggerFactory)
            : base(identifier, loggerFactory)
        {
        }


        protected override void InitializeStores(IStoreHub<TAccessor> stores)
        {
            base.InitializeStores(stores);

            InitializeClaims(stores);

            InitializeCategories(stores);

            InitializePanes(stores);

            InitializeSources(stores);
        }

        private void InitializeClaims(IStoreHub<TAccessor> stores)
        {
            if (!stores.Accessor.Claims.Any())
            {
                _claims = new List<PortalClaim>
                {
                    new PortalClaim(typeof(PortalClaim), "门户声明"),
                    new PortalClaim(typeof(PortalCategory), "门户分类"),
                    new PortalClaim(typeof(PortalPane), "门户窗格"),
                    new PortalClaim(typeof(PortalPaneClaim), "门户窗格声明"),
                    new PortalClaim(typeof(PortalTag), "门户标签"),
                    new PortalClaim(typeof(PortalTagClaim), "门户标签声明"),
                    new PortalClaim(typeof(PortalSource), "门户来源"),
                    new PortalClaim(typeof(PortalEditor), "门户编者"),
                    new PortalClaim(typeof(PortalEditorTitle), "门户编者头衔"),
                    new PortalClaim(typeof(PortalSubject), "门户专题"),
                    new PortalClaim(typeof(PortalSubjectBody), "门户专题主体"),
                    new PortalClaim(typeof(PortalSubjectClaim), "门户专题声明")
                };

                stores.Accessor.Claims.AddRange(_claims);
            }
            else
            {
                _claims = stores.Accessor.Claims.ToList();
            }
        }

        private void InitializeCategories(IStoreHub<TAccessor> stores)
        {
            if (!stores.Accessor.Categories.Any())
            {
                _categories = new List<PortalCategory>
                {
                    new PortalCategory("默认", "默认分类"),
                    new PortalCategory("导航栏", "用于导航栏的分类"),
                    new PortalCategory("友情链接", "用于友情链接的分类")
                };

                stores.Accessor.Categories.AddRange(_categories);
            }
            else
            {
                _categories = stores.Accessor.Categories.ToList();
            }
        }

        private void InitializePanes(IStoreHub<TAccessor> stores)
        {
            if (!stores.Accessor.Panes.Any())
            {
                var navCategoryId = 2;

                _panes = new List<PortalPane>
                {
                    new PortalPane("首页", "/")
                    {
                        CategoryId = navCategoryId
                    },
                    new PortalPane("专题", "/subjects")
                    {
                        CategoryId = navCategoryId
                    }
                };

                stores.Accessor.Panes.AddRange(_panes);
            }
            else
            {
                _panes = stores.Accessor.Panes.ToList();
            }
        }

        private void InitializeSources(IStoreHub<TAccessor> stores)
        {
            if (!stores.Accessor.Sources.Any())
            {
                var weblinkCategoryId = 3;

                _sources = new List<PortalSource>
                {
                    new PortalSource("LibrameRepository", "https://github.com/librame/Librame")
                    {
                        CategoryId = weblinkCategoryId
                    },
                    new PortalSource("LibrameCoreRepository", "https://github.com/librame/LibrameCore")
                    {
                        CategoryId = weblinkCategoryId
                    }
                };

                stores.Accessor.Sources.AddRange(_sources);
            }
            else
            {
                _sources = stores.Accessor.Sources.ToList();
            }
        }
    }
}
