#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Authentication.OpenIdConnect
{
    using Extensions;

    /// <summary>
    /// Librame <see cref="OpenIdConnectHandler"/>。
    /// </summary>
    public class LibrameOpenIdConnectHandler : OpenIdConnectHandler
    {
        private OpenIdConnectConfiguration _configuration;


        /// <summary>
        /// 构造一个 <see cref="LibrameOpenIdConnectHandler"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptionsMonitor{TOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILoggerFactory"/>。</param>
        /// <param name="htmlEncoder">给定的 <see cref="HtmlEncoder"/>。</param>
        /// <param name="encoder">给定的 <see cref="UrlEncoder"/>。</param>
        /// <param name="clock">给定的 <see cref="ISystemClock"/>。</param>
        public LibrameOpenIdConnectHandler(IOptionsMonitor<OpenIdConnectOptions> options, ILoggerFactory logger, HtmlEncoder htmlEncoder, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, htmlEncoder, encoder, clock)
        {
        }


        /// <summary>
        /// 异步处理请求。
        /// </summary>
        /// <returns>返回一个带布尔值的异步操作。</returns>
        public override Task<bool> HandleRequestAsync()
        {
            if (Options.RemoteSignOutPath.HasValue && Options.RemoteSignOutPath == Request.Path)
            {
                return HandleRemoteSignOutAsync();
            }
            else if (Options.SignedOutCallbackPath.HasValue && Options.SignedOutCallbackPath == Request.Path)
            {
                return HandleSignOutCallbackAsync();
            }

            return BaseHandleRequestAsync();
        }


        /// <summary>
        /// Fix RemoteAuthenticationHandler{OpenIdConnectOptions}.HandleRequestAsync()。
        /// </summary>
        /// <returns>返回一个带布尔值的异步操作。</returns>
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        [SuppressMessage("Design", "CA1031:不捕获常规异常类型")]
        private async Task<bool> BaseHandleRequestAsync()
        {
            if (!await ShouldHandleRequestAsync().ConfigureAndResultAsync())
            {
                return false;
            }

            AuthenticationTicket ticket = null;
            Exception exception = null;
            AuthenticationProperties properties = null;

            try
            {
                var authResult = await HandleRemoteAuthenticateAsync().ConfigureAndResultAsync();
                if (authResult == null)
                {
                    exception = new InvalidOperationException("Invalid return state, unable to redirect.");
                }
                else if (authResult.Handled)
                {
                    return true;
                }
                else if (authResult.Skipped || authResult.None)
                {
                    return false;
                }
                else if (!authResult.Succeeded)
                {
                    exception = authResult.Failure ?? new InvalidOperationException("Invalid return state, unable to redirect.");
                    properties = authResult.Properties;
                }

                ticket = authResult?.Ticket;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                //Logger.RemoteAuthenticationError(exception.Message);
                var errorContext = new RemoteFailureContext(Context, Scheme, Options, exception)
                {
                    Properties = properties
                };
                await Events.RemoteFailure(errorContext).ConfigureAndWaitAsync();

                if (errorContext.Result != null)
                {
                    if (errorContext.Result.Handled)
                    {
                        return true;
                    }
                    else if (errorContext.Result.Skipped)
                    {
                        return false;
                    }
                    else if (errorContext.Result.Failure != null)
                    {
                        throw new Exception("An error was returned from the RemoteFailure event.", errorContext.Result.Failure);
                    }
                }

                if (errorContext.Failure != null)
                {
                    throw new Exception("An error was encountered while handling the remote login.", errorContext.Failure);
                }
            }

            // We have a ticket if we get here
            var ticketContext = new TicketReceivedContext(Context, Scheme, Options, ticket)
            {
                ReturnUri = ticket.Properties.RedirectUri
            };

            ticket.Properties.RedirectUri = null;

            // Mark which provider produced this identity so we can cross-check later in HandleAuthenticateAsync
            // RemoteAuthenticationHandler<OpenIdConnectOptions>.AuthSchemeKey
            ticketContext.Properties.Items[".AuthScheme"] = Scheme.Name;

            await Events.TicketReceived(ticketContext).ConfigureAndWaitAsync();

            if (ticketContext.Result != null)
            {
                if (ticketContext.Result.Handled)
                {
                    //Logger.SignInHandled();
                    return true;
                }
                else if (ticketContext.Result.Skipped)
                {
                    //Logger.SignInSkipped();
                    return false;
                }
            }

            await Context.SignInAsync(SignInScheme, ticketContext.Principal, ticketContext.Properties).ConfigureAndWaitAsync();

            // 解决当配置了 OpenIdConnectOptions.CallbackPath 路径却重定向到 AuthenticationHandler<OpenIdConnectOptions>.OriginalPath (默认路径)的问题
            if (ticketContext.Properties.Items.TryGetValue(OpenIdConnectDefaults.RedirectUriForCodePropertiesKey, out string callbackUri)
                && !callbackUri.Equals(ticketContext.ReturnUri, StringComparison.OrdinalIgnoreCase))
            {
                if (callbackUri.IsAbsoluteUri(out Uri uri))
                    ticketContext.ReturnUri = uri.AbsolutePath;
                else
                    ticketContext.ReturnUri = callbackUri;
            }

            // Default redirect path is the base path
            if (string.IsNullOrEmpty(ticketContext.ReturnUri))
            {
                ticketContext.ReturnUri = "/";
            }

            Response.Redirect(ticketContext.ReturnUri);
            return true;
        }


        /// <summary>
        /// Invoked to process incoming OpenIdConnect messages.
        /// </summary>
        /// <returns>An <see cref="HandleRequestResult"/>.</returns>
        [SuppressMessage("Design", "CA1031:不捕获常规异常类型")]
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        protected override async Task<HandleRequestResult> HandleRemoteAuthenticateAsync()
        {
            Logger.EnteringOpenIdAuthenticationHandlerHandleRemoteAuthenticateAsync(GetType().FullName);

            OpenIdConnectMessage authorizationResponse = null;

            if (string.Equals(Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
            {
                authorizationResponse = new OpenIdConnectMessage(Request.Query.Select(pair => new KeyValuePair<string, string[]>(pair.Key, pair.Value)));

                // response_mode=query (explicit or not) and a response_type containing id_token
                // or token are not considered as a safe combination and MUST be rejected.
                // See http://openid.net/specs/oauth-v2-multiple-response-types-1_0.html#Security
                if (!string.IsNullOrEmpty(authorizationResponse.IdToken) || !string.IsNullOrEmpty(authorizationResponse.AccessToken))
                {
                    if (Options.SkipUnrecognizedRequests)
                    {
                        // Not for us?
                        return HandleRequestResult.SkipHandler();
                    }
                    return HandleRequestResult.Fail("An OpenID Connect response cannot contain an " +
                            "identity token or an access token when using response_mode=query");
                }
            }
            // assumption: if the ContentType is "application/x-www-form-urlencoded" it should be safe to read as it is small.
            else if (string.Equals(Request.Method, "POST", StringComparison.OrdinalIgnoreCase)
              && !string.IsNullOrEmpty(Request.ContentType)
              // May have media/type; charset=utf-8, allow partial match.
              && Request.ContentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase)
              && Request.Body.CanRead)
            {
                var form = await Request.ReadFormAsync().ConfigureAndResultAsync();
                authorizationResponse = new OpenIdConnectMessage(form.Select(pair => new KeyValuePair<string, string[]>(pair.Key, pair.Value)));
            }

            if (authorizationResponse == null)
            {
                if (Options.SkipUnrecognizedRequests)
                {
                    // Not for us?
                    return HandleRequestResult.SkipHandler();
                }
                return HandleRequestResult.Fail("No message.");
            }

            AuthenticationProperties properties = null;
            try
            {
                properties = ReadPropertiesAndClearState(authorizationResponse);

                var messageReceivedContext = await RunMessageReceivedEventAsync(authorizationResponse, properties).ConfigureAndResultAsync();
                if (messageReceivedContext.Result != null)
                {
                    return messageReceivedContext.Result;
                }
                authorizationResponse = messageReceivedContext.ProtocolMessage;
                properties = messageReceivedContext.Properties;

                if (properties == null || properties.Items.Count == 0)
                {
                    // Fail if state is missing, it's required for the correlation id.
                    if (string.IsNullOrEmpty(authorizationResponse.State))
                    {
                        // This wasn't a valid OIDC message, it may not have been intended for us.
                        Logger.NullOrEmptyAuthorizationResponseState();

                        // 解决当请求路径是 CallbackPath 且未认证时，会抛出 OpenIdConnectAuthenticationHandler: message.State is null or empty. 的异常
                        // ShouldHandleRequestAsync() => Options.CallbackPath == Request.Path
                        if (Options.SkipUnrecognizedRequests || await ShouldHandleRequestAsync().ConfigureAndResultAsync())
                        {
                            return HandleRequestResult.SkipHandler();
                        }
                        return HandleRequestResult.Fail("OpenIdConnectAuthenticationHandler: message.State is null or empty.");
                    }

                    properties = ReadPropertiesAndClearState(authorizationResponse);
                }

                if (properties == null)
                {
                    Logger.UnableToReadAuthorizationResponseState();
                    if (Options.SkipUnrecognizedRequests)
                    {
                        // Not for us?
                        return HandleRequestResult.SkipHandler();
                    }

                    // if state exists and we failed to 'unprotect' this is not a message we should process.
                    return HandleRequestResult.Fail("Unable to unprotect the message.State.");
                }

                if (!ValidateCorrelationId(properties))
                {
                    return HandleRequestResult.Fail("Correlation failed.", properties);
                }

                // if any of the error fields are set, throw error null
                if (!string.IsNullOrEmpty(authorizationResponse.Error))
                {
                    // Note: access_denied errors are special protocol errors indicating the user didn't
                    // approve the authorization demand requested by the remote authorization server.
                    // Since it's a frequent scenario (that is not caused by incorrect configuration),
                    // denied errors are handled differently using HandleAccessDeniedErrorAsync().
                    // Visit https://tools.ietf.org/html/rfc6749#section-4.1.2.1 for more information.
                    if (string.Equals(authorizationResponse.Error, "access_denied", StringComparison.Ordinal))
                    {
                        var result = await HandleAccessDeniedErrorAsync(properties).ConfigureAndResultAsync();
                        if (!result.None)
                        {
                            return result;
                        }
                    }

                    return HandleRequestResult.Fail(CreateOpenIdConnectProtocolException(authorizationResponse, response: null), properties);
                }

                if (_configuration == null && Options.ConfigurationManager != null)
                {
                    Logger.UpdatingConfiguration();
                    _configuration = await Options.ConfigurationManager.GetConfigurationAsync(Context.RequestAborted).ConfigureAndResultAsync();
                }

                PopulateSessionProperties(authorizationResponse, properties);

                ClaimsPrincipal user = null;
                JwtSecurityToken jwt = null;
                string nonce = null;
                var validationParameters = Options.TokenValidationParameters.Clone();

                // Hybrid or Implicit flow
                if (!string.IsNullOrEmpty(authorizationResponse.IdToken))
                {
                    Logger.ReceivedIdToken();
                    user = ValidateToken(authorizationResponse.IdToken, properties, validationParameters, out jwt);

                    nonce = jwt.Payload.Nonce;
                    if (!string.IsNullOrEmpty(nonce))
                    {
                        nonce = ReadNonceCookie(nonce);
                    }

                    var tokenValidatedContext = await RunTokenValidatedEventAsync(authorizationResponse, null, user, properties, jwt, nonce).ConfigureAndResultAsync();
                    if (tokenValidatedContext.Result != null)
                    {
                        return tokenValidatedContext.Result;
                    }
                    authorizationResponse = tokenValidatedContext.ProtocolMessage;
                    user = tokenValidatedContext.Principal;
                    properties = tokenValidatedContext.Properties;
                    jwt = tokenValidatedContext.SecurityToken;
                    nonce = tokenValidatedContext.Nonce;
                }

                Options.ProtocolValidator.ValidateAuthenticationResponse(new OpenIdConnectProtocolValidationContext()
                {
                    ClientId = Options.ClientId,
                    ProtocolMessage = authorizationResponse,
                    ValidatedIdToken = jwt,
                    Nonce = nonce
                });

                OpenIdConnectMessage tokenEndpointResponse = null;

                // Authorization Code or Hybrid flow
                if (!string.IsNullOrEmpty(authorizationResponse.Code))
                {
                    var authorizationCodeReceivedContext = await RunAuthorizationCodeReceivedEventAsync(authorizationResponse, user, properties, jwt).ConfigureAndResultAsync();
                    if (authorizationCodeReceivedContext.Result != null)
                    {
                        return authorizationCodeReceivedContext.Result;
                    }
                    authorizationResponse = authorizationCodeReceivedContext.ProtocolMessage;
                    user = authorizationCodeReceivedContext.Principal;
                    properties = authorizationCodeReceivedContext.Properties;
                    var tokenEndpointRequest = authorizationCodeReceivedContext.TokenEndpointRequest;
                    // If the developer redeemed the code themselves...
                    tokenEndpointResponse = authorizationCodeReceivedContext.TokenEndpointResponse;
                    jwt = authorizationCodeReceivedContext.JwtSecurityToken;

                    if (!authorizationCodeReceivedContext.HandledCodeRedemption)
                    {
                        tokenEndpointResponse = await RedeemAuthorizationCodeAsync(tokenEndpointRequest).ConfigureAndResultAsync();
                    }

                    var tokenResponseReceivedContext = await RunTokenResponseReceivedEventAsync(authorizationResponse, tokenEndpointResponse, user, properties).ConfigureAndResultAsync();
                    if (tokenResponseReceivedContext.Result != null)
                    {
                        return tokenResponseReceivedContext.Result;
                    }

                    authorizationResponse = tokenResponseReceivedContext.ProtocolMessage;
                    tokenEndpointResponse = tokenResponseReceivedContext.TokenEndpointResponse;
                    user = tokenResponseReceivedContext.Principal;
                    properties = tokenResponseReceivedContext.Properties;

                    // no need to validate signature when token is received using "code flow" as per spec
                    // [http://openid.net/specs/openid-connect-core-1_0.html#IDTokenValidation].
                    validationParameters.RequireSignedTokens = false;

                    // At least a cursory validation is required on the new IdToken, even if we've already validated the one from the authorization response.
                    // And we'll want to validate the new JWT in ValidateTokenResponse.
                    var tokenEndpointUser = ValidateToken(tokenEndpointResponse.IdToken, properties, validationParameters, out var tokenEndpointJwt);

                    // Avoid reading & deleting the nonce cookie, running the event, etc, if it was already done as part of the authorization response validation.
                    if (user == null)
                    {
                        nonce = tokenEndpointJwt.Payload.Nonce;
                        if (!string.IsNullOrEmpty(nonce))
                        {
                            nonce = ReadNonceCookie(nonce);
                        }

                        var tokenValidatedContext = await RunTokenValidatedEventAsync(authorizationResponse, tokenEndpointResponse, tokenEndpointUser, properties, tokenEndpointJwt, nonce).ConfigureAndResultAsync();
                        if (tokenValidatedContext.Result != null)
                        {
                            return tokenValidatedContext.Result;
                        }
                        authorizationResponse = tokenValidatedContext.ProtocolMessage;
                        tokenEndpointResponse = tokenValidatedContext.TokenEndpointResponse;
                        user = tokenValidatedContext.Principal;
                        properties = tokenValidatedContext.Properties;
                        jwt = tokenValidatedContext.SecurityToken;
                        nonce = tokenValidatedContext.Nonce;
                    }
                    else
                    {
                        if (!string.Equals(jwt.Subject, tokenEndpointJwt.Subject, StringComparison.Ordinal))
                        {
                            throw new SecurityTokenException("The sub claim does not match in the id_token's from the authorization and token endpoints.");
                        }

                        jwt = tokenEndpointJwt;
                    }

                    // Validate the token response if it wasn't provided manually
                    if (!authorizationCodeReceivedContext.HandledCodeRedemption)
                    {
                        Options.ProtocolValidator.ValidateTokenResponse(new OpenIdConnectProtocolValidationContext()
                        {
                            ClientId = Options.ClientId,
                            ProtocolMessage = tokenEndpointResponse,
                            ValidatedIdToken = jwt,
                            Nonce = nonce
                        });
                    }
                }

                if (Options.SaveTokens)
                {
                    SaveTokens(properties, tokenEndpointResponse ?? authorizationResponse);
                }

                if (Options.GetClaimsFromUserInfoEndpoint)
                {
                    return await GetUserInformationAsync(tokenEndpointResponse ?? authorizationResponse, jwt, user, properties).ConfigureAndResultAsync();
                }
                else
                {
                    using (var payload = JsonDocument.Parse("{}"))
                    {
                        var identity = (ClaimsIdentity)user.Identity;
                        foreach (var action in Options.ClaimActions)
                        {
                            action.Run(payload.RootElement, identity, ClaimsIssuer);
                        }
                    }
                }

                return HandleRequestResult.Success(new AuthenticationTicket(user, properties, Scheme.Name));
            }
            catch (Exception exception)
            {
                Logger.ExceptionProcessingMessage(exception);

                // Refresh the configuration for exceptions that may be caused by key rollovers. The user can also request a refresh in the event.
                if (Options.RefreshOnIssuerKeyNotFound && exception is SecurityTokenSignatureKeyNotFoundException)
                {
                    if (Options.ConfigurationManager != null)
                    {
                        Logger.ConfigurationManagerRequestRefreshCalled();
                        Options.ConfigurationManager.RequestRefresh();
                    }
                }

                var authenticationFailedContext = await RunAuthenticationFailedEventAsync(authorizationResponse, exception).ConfigureAndResultAsync();
                if (authenticationFailedContext.Result != null)
                {
                    return authenticationFailedContext.Result;
                }

                return HandleRequestResult.Fail(exception, properties);
            }
        }

        private AuthenticationProperties ReadPropertiesAndClearState(OpenIdConnectMessage message)
        {
            AuthenticationProperties properties = null;
            if (!string.IsNullOrEmpty(message.State))
            {
                properties = Options.StateDataFormat.Unprotect(message.State);

                if (properties != null)
                {
                    // If properties can be decoded from state, clear the message state.
                    properties.Items.TryGetValue(OpenIdConnectDefaults.UserstatePropertiesKey, out var userstate);
                    message.State = userstate;
                }
            }
            return properties;
        }

        private void PopulateSessionProperties(OpenIdConnectMessage message, AuthenticationProperties properties)
        {
            if (!string.IsNullOrEmpty(message.SessionState))
            {
                properties.Items[OpenIdConnectSessionProperties.SessionState] = message.SessionState;
            }

            if (!string.IsNullOrEmpty(_configuration.CheckSessionIframe))
            {
                properties.Items[OpenIdConnectSessionProperties.CheckSessionIFrame] = _configuration.CheckSessionIframe;
            }
        }

        /// <summary>
        /// Save the tokens contained in the <see cref="OpenIdConnectMessage"/> in the <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="properties">The <see cref="AuthenticationProperties"/> in which tokens are saved.</param>
        /// <param name="message">The OpenID Connect response.</param>
        private void SaveTokens(AuthenticationProperties properties, OpenIdConnectMessage message)
        {
            var tokens = new List<AuthenticationToken>();

            if (!string.IsNullOrEmpty(message.AccessToken))
            {
                tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = message.AccessToken });
            }

            if (!string.IsNullOrEmpty(message.IdToken))
            {
                tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.IdToken, Value = message.IdToken });
            }

            if (!string.IsNullOrEmpty(message.RefreshToken))
            {
                tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = message.RefreshToken });
            }

            if (!string.IsNullOrEmpty(message.TokenType))
            {
                tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.TokenType, Value = message.TokenType });
            }

            if (!string.IsNullOrEmpty(message.ExpiresIn))
            {
                if (int.TryParse(message.ExpiresIn, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
                {
                    var expiresAt = Clock.UtcNow + TimeSpan.FromSeconds(value);
                    // https://www.w3.org/TR/xmlschema-2/#dateTime
                    // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx
                    tokens.Add(new AuthenticationToken { Name = "expires_at", Value = expiresAt.ToString("o", CultureInfo.InvariantCulture) });
                }
            }

            properties.StoreTokens(tokens);
        }


        /// <summary>
        /// Searches <see cref="HttpRequest.Cookies"/> for a matching nonce.
        /// </summary>
        /// <param name="nonce">the nonce that we are looking for.</param>
        /// <returns>echos 'nonce' if a cookie is found that matches, null otherwise.</returns>
        /// <remarks>Examine <see cref="IRequestCookieCollection.Keys"/> of <see cref="HttpRequest.Cookies"/> that start with the prefix: 'OpenIdConnectAuthenticationDefaults.Nonce'.
        /// <see cref="ISecureDataFormat{TData}.Unprotect(string)"/> of <see cref="OpenIdConnectOptions.StringDataFormat"/> is used to obtain the actual 'nonce'. If the nonce is found, then <see cref="IResponseCookies.Delete(string)"/> of <see cref="HttpResponse.Cookies"/> is called.</remarks>
        [SuppressMessage("Design", "CA1031:不捕获常规异常类型")]
        private string ReadNonceCookie(string nonce)
        {
            if (nonce == null)
            {
                return null;
            }

            foreach (var nonceKey in Request.Cookies.Keys)
            {
                if (nonceKey.StartsWith(Options.NonceCookie.Name, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var nonceDecodedValue = Options.StringDataFormat.Unprotect(nonceKey.Substring(Options.NonceCookie.Name.Length, nonceKey.Length - Options.NonceCookie.Name.Length));
                        if (nonceDecodedValue == nonce)
                        {
                            var cookieOptions = Options.NonceCookie.Build(Context, Clock.UtcNow);
                            Response.Cookies.Delete(nonceKey, cookieOptions);
                            return nonce;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.UnableToProtectNonceCookie(ex);
                    }
                }
            }

            return null;
        }

        private async Task<MessageReceivedContext> RunMessageReceivedEventAsync(
            OpenIdConnectMessage message, AuthenticationProperties properties)
        {
            Logger.MessageReceived(message.BuildRedirectUrl());
            var context = new MessageReceivedContext(Context, Scheme, Options, properties)
            {
                ProtocolMessage = message,
            };

            await Events.MessageReceived(context).ConfigureAndWaitAsync();
            if (context.Result != null)
            {
                if (context.Result.Handled)
                {
                    Logger.MessageReceivedContextHandledResponse();
                }
                else if (context.Result.Skipped)
                {
                    Logger.MessageReceivedContextSkipped();
                }
            }

            return context;
        }

        private async Task<TokenValidatedContext> RunTokenValidatedEventAsync(
            OpenIdConnectMessage authorizationResponse, OpenIdConnectMessage tokenEndpointResponse,
            ClaimsPrincipal user, AuthenticationProperties properties, JwtSecurityToken jwt, string nonce)
        {
            var context = new TokenValidatedContext(Context, Scheme, Options, user, properties)
            {
                ProtocolMessage = authorizationResponse,
                TokenEndpointResponse = tokenEndpointResponse,
                SecurityToken = jwt,
                Nonce = nonce,
            };

            await Events.TokenValidated(context).ConfigureAndWaitAsync();
            if (context.Result != null)
            {
                if (context.Result.Handled)
                {
                    Logger.TokenValidatedHandledResponse();
                }
                else if (context.Result.Skipped)
                {
                    Logger.TokenValidatedSkipped();
                }
            }

            return context;
        }

        private async Task<AuthorizationCodeReceivedContext> RunAuthorizationCodeReceivedEventAsync(
            OpenIdConnectMessage authorizationResponse, ClaimsPrincipal user,
            AuthenticationProperties properties, JwtSecurityToken jwt)
        {
            Logger.AuthorizationCodeReceived();

            var tokenEndpointRequest = new OpenIdConnectMessage()
            {
                ClientId = Options.ClientId,
                ClientSecret = Options.ClientSecret,
                Code = authorizationResponse.Code,
                GrantType = OpenIdConnectGrantTypes.AuthorizationCode,
                EnableTelemetryParameters = !Options.DisableTelemetry,
                RedirectUri = properties.Items[OpenIdConnectDefaults.RedirectUriForCodePropertiesKey]
            };

            // PKCE https://tools.ietf.org/html/rfc7636#section-4.5, see HandleChallengeAsyncInternal
            if (properties.Items.TryGetValue(OAuthConstants.CodeVerifierKey, out var codeVerifier))
            {
                tokenEndpointRequest.Parameters.Add(OAuthConstants.CodeVerifierKey, codeVerifier);
                properties.Items.Remove(OAuthConstants.CodeVerifierKey);
            }

            var context = new AuthorizationCodeReceivedContext(Context, Scheme, Options, properties)
            {
                ProtocolMessage = authorizationResponse,
                TokenEndpointRequest = tokenEndpointRequest,
                Principal = user,
                JwtSecurityToken = jwt,
                //Backchannel = Backchannel
            };
            typeof(AuthorizationCodeReceivedContext).GetProperty(nameof(context.Backchannel)).SetValue(context, Backchannel);

            await Events.AuthorizationCodeReceived(context).ConfigureAndWaitAsync();
            if (context.Result != null)
            {
                if (context.Result.Handled)
                {
                    Logger.AuthorizationCodeReceivedContextHandledResponse();
                }
                else if (context.Result.Skipped)
                {
                    Logger.AuthorizationCodeReceivedContextSkipped();
                }
            }

            return context;
        }

        private async Task<TokenResponseReceivedContext> RunTokenResponseReceivedEventAsync(
            OpenIdConnectMessage message,
            OpenIdConnectMessage tokenEndpointResponse,
            ClaimsPrincipal user,
            AuthenticationProperties properties)
        {
            Logger.TokenResponseReceived();
            var context = new TokenResponseReceivedContext(Context, Scheme, Options, user, properties)
            {
                ProtocolMessage = message,
                TokenEndpointResponse = tokenEndpointResponse,
            };

            await Events.TokenResponseReceived(context).ConfigureAndWaitAsync();
            if (context.Result != null)
            {
                if (context.Result.Handled)
                {
                    Logger.TokenResponseReceivedHandledResponse();
                }
                else if (context.Result.Skipped)
                {
                    Logger.TokenResponseReceivedSkipped();
                }
            }

            return context;
        }

        private async Task<AuthenticationFailedContext> RunAuthenticationFailedEventAsync(
            OpenIdConnectMessage message, Exception exception)
        {
            var context = new AuthenticationFailedContext(Context, Scheme, Options)
            {
                ProtocolMessage = message,
                Exception = exception
            };

            await Events.AuthenticationFailed(context).ConfigureAndWaitAsync();
            if (context.Result != null)
            {
                if (context.Result.Handled)
                {
                    Logger.AuthenticationFailedContextHandledResponse();
                }
                else if (context.Result.Skipped)
                {
                    Logger.AuthenticationFailedContextSkipped();
                }
            }

            return context;
        }

        // Note this modifies properties if Options.UseTokenLifetime
        private ClaimsPrincipal ValidateToken(string idToken, AuthenticationProperties properties,
            TokenValidationParameters validationParameters, out JwtSecurityToken jwt)
        {
            if (!Options.SecurityTokenValidator.CanReadToken(idToken))
            {
                Logger.UnableToReadIdToken(idToken);
                throw new SecurityTokenException(string.Format(CultureInfo.InvariantCulture, "Unable to validate the 'id_token', no suitable ISecurityTokenValidator was found for: '{0}'.", idToken));
            }

            if (_configuration != null)
            {
                var issuer = new[] { _configuration.Issuer };
                validationParameters.ValidIssuers = validationParameters.ValidIssuers?.Concat(issuer) ?? issuer;

                validationParameters.IssuerSigningKeys = validationParameters.IssuerSigningKeys?.Concat(_configuration.SigningKeys)
                    ?? _configuration.SigningKeys;
            }

            var principal = Options.SecurityTokenValidator.ValidateToken(idToken, validationParameters, out SecurityToken validatedToken);
            jwt = validatedToken as JwtSecurityToken;
            if (jwt == null)
            {
                Logger.InvalidSecurityTokenType(validatedToken?.GetType().ToString());
                throw new SecurityTokenException(string.Format(CultureInfo.InvariantCulture, "The Validated Security Token must be of type JwtSecurityToken, but instead its type is: '{0}'.", validatedToken?.GetType()));
            }

            if (validatedToken == null)
            {
                Logger.UnableToValidateIdToken(idToken);
                throw new SecurityTokenException(string.Format(CultureInfo.InvariantCulture, "Unable to validate the 'id_token', no suitable ISecurityTokenValidator was found for: '{0}'.", idToken));
            }

            if (Options.UseTokenLifetime)
            {
                var issued = validatedToken.ValidFrom;
                if (issued != DateTime.MinValue)
                {
                    properties.IssuedUtc = issued;
                }

                var expires = validatedToken.ValidTo;
                if (expires != DateTime.MinValue)
                {
                    properties.ExpiresUtc = expires;
                }
            }

            return principal;
        }

        private OpenIdConnectProtocolException CreateOpenIdConnectProtocolException(OpenIdConnectMessage message,
            HttpResponseMessage response)
        {
            var description = message.ErrorDescription ?? "error_description is null";
            var errorUri = message.ErrorUri ?? "error_uri is null";

            if (response != null)
            {
                Logger.ResponseErrorWithStatusCode(message.Error, description, errorUri, (int)response.StatusCode);
            }
            else
            {
                Logger.ResponseError(message.Error, description, errorUri);
            }

            var ex = new OpenIdConnectProtocolException(string.Format(
                CultureInfo.InvariantCulture,
                "Message contains error: '{0}', error_description: '{1}', error_uri: '{2}'.",
                message.Error,
                description,
                errorUri));
            ex.Data["error"] = message.Error;
            ex.Data["error_description"] = description;
            ex.Data["error_uri"] = errorUri;

            return ex;
        }

    }
}
