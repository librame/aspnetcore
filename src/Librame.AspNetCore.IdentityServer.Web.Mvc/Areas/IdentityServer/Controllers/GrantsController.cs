// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Web.Controllers
{
    using AspNetCore.IdentityServer.Web.Models;
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// This sample controller allows a user to revoke grants given to clients
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    [Area(IdentityServerRouteBuilderExtensions.AreaName)]
    [Route(IdentityServerRouteBuilderExtensions.Template)]
    public class GrantsController : Controller
    {
        [InjectionService]
        private IIdentityServerInteractionService _interaction = null;

        [InjectionService]
        private IClientStore _clients = null;

        [InjectionService]
        private IResourceStore _resources = null;

        [InjectionService]
        private IEventService _events = null;


        /// <summary>
        /// 构造一个 <see cref="GrantsController"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        public GrantsController(IInjectionService injectionService)
        {
            injectionService.NotNull(nameof(injectionService)).Inject(this);
        }


        /// <summary>
        /// Show list of grants
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await BuildViewModelAsync().ConfigureAwait());
        }

        /// <summary>
        /// Handle postback to revoke a client
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revoke(string clientId)
        {
            await _interaction.RevokeUserConsentAsync(clientId).ConfigureAwait();
            await _events.RaiseAsync(new GrantsRevokedEvent(User.GetSubjectId(), clientId)).ConfigureAwait();

            return RedirectToAction("Index");
        }

        private async Task<GrantsViewModel> BuildViewModelAsync()
        {
            var grants = await _interaction.GetAllUserGrantsAsync().ConfigureAwait();

            var list = new List<GrantViewModel>();
            foreach(var grant in grants)
            {
                var client = await _clients.FindClientByIdAsync(grant.ClientId).ConfigureAwait();
                if (client != null)
                {
                    var resources = await _resources.FindResourcesByScopeAsync(grant.Scopes).ConfigureAwait();

                    var item = new GrantViewModel()
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName ?? client.ClientId,
                        ClientLogoUrl = client.LogoUri,
                        ClientUrl = client.ClientUri,
                        Created = grant.CreationTime,
                        Expires = grant.Expiration,
                        IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                        ApiGrantNames = resources.ApiResources.Select(x => x.DisplayName ?? x.Name).ToArray()
                    };

                    list.Add(item);
                }
            }

            return new GrantsViewModel
            {
                Grants = list
            };
        }
    }
}