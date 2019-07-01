using IdentityModel;
using IdentityServer4.Stores;
using Librame.AspNet.Identity.Stores;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LibrameCore.IdentityServer
{
    public static class Extensions
    {

        /// <summary>
        /// Determines whether the client is configured to use PKCE.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="client_id">The client identifier.</param>
        /// <returns></returns>
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
        {
            if (!string.IsNullOrWhiteSpace(client_id))
            {
                var client = await store.FindEnabledClientByIdAsync(client_id);
                return client?.RequirePkce == true;
            }

            return false;
        }


        public static bool ValidateCredentials(this IEfCoreUserRoleStore users, string username, string password,
            out Librame.AspNet.Identity.Stores.IdentityUser user)
        {
            user = users.FindByNameAsync(username, CancellationToken.None).Result;
            if (user.IsNull()) return false;

            var validResult = users.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (validResult == PasswordVerificationResult.Failed)
            {
                user = null;
                return false;
            }

            if (validResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                // Update Password
                var hashedPassword = users.PasswordHasher.HashPassword(user, password);
                users.SetPasswordHashAsync(user, hashedPassword, CancellationToken.None).Wait();
            }

            // Add Login
            var loginInfo = new UserLoginInfo("Local", "LocalAppKey", "LibramePassport");
            users.AddLoginAsync(user, loginInfo, CancellationToken.None).Wait();

            return true;
        }

        /// <summary>
        /// Finds the user by external provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Librame.AspNet.Identity.Stores.IdentityUser FindByExternalProvider(this IEfCoreUserRoleStore users, string provider, string userId)
        {
            //return users.FirstOrDefault(x =>
            //    x.ProviderName == provider &&
            //    x.ProviderSubjectId == userId);

            return users.FindByIdAsync(userId, CancellationToken.None).Result;
        }

        /// <summary>
        /// Automatically provisions a user.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        public static Librame.AspNet.Identity.Stores.IdentityUser AutoProvisionUser(this IEfCoreUserRoleStore users, string provider, string userId, List<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();

            foreach (var claim in claims)
            {
                // if the external system sends a display name - translate that to the standard OIDC name claim
                if (claim.Type == ClaimTypes.Name)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
                }
                // if the JWT handler has an outbound mapping to an OIDC claim use that
                else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                {
                    filtered.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
                }
                // copy the claim as-is
                else
                {
                    filtered.Add(claim);
                }
            }

            // if no display name was provided, try to construct by first and/or last name
            if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
            {
                var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
                var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // create a new unique subject id
            //var sub = CryptoRandom.CreateUniqueId();

            // check if a display name is available, otherwise fallback to subject id
            var name = filtered.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value ?? userId;

            // create new user
            var user = new Librame.AspNet.Identity.Stores.IdentityUser
            {
                //SubjectId = sub,
                //Username = name,
                //ProviderName = provider,
                //ProviderSubjectId = userId,
                //Claims = filtered
                UserName = name,
                NormalizedUserName = name,
                Email = "testuserone@domain.com",
                NormalizedEmail = "testuserone@domain.com",
                PhoneNumber = "13100000000"
            };
            // Hash Password
            user.PasswordHash = users.PasswordHasher.HashPassword(user, "123456");

            // add user to in-memory store
            users.CreateAsync(user, CancellationToken.None).Wait();

            // Add Claims
            users.AddClaimsAsync(user, filtered, CancellationToken.None).Wait();

            return user;
        }

    }
}
