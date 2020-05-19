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
    using Models;

    public class TestGuidIdentityServerStoreInitializer : GuidIdentityServerStoreInitializer
    {
        public TestGuidIdentityServerStoreInitializer(SignInManager<DefaultIdentityUser<Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid>> roleMananger,
            IUserStore<DefaultIdentityUser<Guid>> userStore,
            GuidIdentityServerStoreIdentifierGenerator identifier, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, identifier, loggerFactory)
        {
        }


        protected override void InitializeCore<TAudit, TAuditProperty, TEntity, TMigration, TTenant, TIncremId>
            (IStoreHub<TAudit, TAuditProperty, TEntity, TMigration, TTenant, Guid, TIncremId> stores)
        {
            base.InitializeCore(stores);

            if (stores.Accessor is IdentityServerDbContextAccessor dbContextAccessor)
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
