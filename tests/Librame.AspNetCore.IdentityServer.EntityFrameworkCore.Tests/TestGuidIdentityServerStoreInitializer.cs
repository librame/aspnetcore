using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Accessors;
    using AspNetCore.IdentityServer.Stores;
    using Extensions.Data.Stores;
    using Extensions.Data.ValueGenerators;
    using Models;

    public class TestGuidIdentityServerStoreInitializer : GuidIdentityServerStoreInitializer
    {
        public TestGuidIdentityServerStoreInitializer(SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IUserStore<DefaultIdentityUser<Guid, Guid>> userStore,
            IDefaultValueGenerator<Guid> createdByGenerator,
            IStoreIdentifierGenerator<Guid> identifierGenerator, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, createdByGenerator, identifierGenerator, loggerFactory)
        {
        }


        protected override void InitializeData(IDataStoreHub<DataAudit<Guid, Guid>, DataAuditProperty<int, Guid>,
            DataEntity<Guid, Guid>, DataMigration<Guid, Guid>, DataTenant<Guid, Guid>, Guid> dataStores)
        {
            base.InitializeData(dataStores);

            if (dataStores.Accessor is IdentityServerDbContextAccessor dbContextAccessor)
            {
                InitializeApiResources(dbContextAccessor);

                InitializeClients(dbContextAccessor);

                InitializeIdentityResources(dbContextAccessor);
            }
        }


        private void InitializeApiResources(IdentityServerDbContextAccessor stores)
        {
            if (!stores.ApiResources.Any())
            {
                stores.ApiResources.Add(IdentityServerConfiguration.DefaultApiResource.ToEntity());

                RequiredSaveChanges = true;
            }
        }

        private void InitializeClients(IdentityServerDbContextAccessor stores)
        {
            if (!stores.Clients.Any())
            {
                stores.Clients.Add(IdentityServerConfiguration.DefaultClient.ToEntity());

                RequiredSaveChanges = true;
            }
        }

        private void InitializeIdentityResources(IdentityServerDbContextAccessor stores)
        {
            if (!stores.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.DefaultIdentityResources)
                {
                    stores.IdentityResources.Add(resource.ToEntity());
                }

                RequiredSaveChanges = true;
            }
        }

    }
}
