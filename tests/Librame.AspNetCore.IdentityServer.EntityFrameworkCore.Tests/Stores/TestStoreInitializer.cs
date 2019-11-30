using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using Extensions.Data;
    using Models;
    using Identity;

    public class TestStoreInitializer : IdentityServerStoreInitializer
    {
        public TestStoreInitializer(SignInManager<DefaultIdentityUser<string>> signInManager,
            RoleManager<DefaultIdentityRole<string>> roleMananger,
            IUserStore<DefaultIdentityUser<string>> userStore,
            IdentityServerStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, identifier, loggerFactory)
        {
        }


        protected override void InitializeCore<TAccessor, TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId>
            (StoreHub<TAccessor, TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId> stores)
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
