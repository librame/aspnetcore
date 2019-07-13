using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;
    using Extensions.Core;
    using Extensions.Data;

    public class TestIdentifierService : IdentifierServiceBase
    {
        public TestIdentifierService(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }


        public virtual Task<string> GetRoleIdAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                string roleId = GuIdentifier.New();
                Logger.LogInformation($"Get RoleId: {roleId}");

                return roleId;
            });
        }

        public virtual Task<string> GetUserIdAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                string userId = GuIdentifier.New();
                Logger.LogInformation($"Get UserId: {userId}");

                return userId;
            });
        }

    }
}
