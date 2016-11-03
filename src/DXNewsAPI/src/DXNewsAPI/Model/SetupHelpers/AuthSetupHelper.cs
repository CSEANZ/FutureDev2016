using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace DXNewsAPI.Model.SetupHelpers
{
    public static class AuthSetupHelper
    {
        public static IApplicationBuilder UseDxAuth(this IApplicationBuilder app, 
            IServiceProvider serviceProvider, IConfigurationRoot configuration,
            AuthCallbackHandler callbackHandler)
        {
            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                ClientId = configuration["AzureAd:ClientId"],
                Authority = String.Format(configuration["AzureAd:AadInstance"], configuration["AzureAd:Tenant"]),
                ResponseType = OpenIdConnectResponseType.IdToken,
                PostLogoutRedirectUri = configuration["AzureAd:PostLogoutRedirectUri"],
                Events = new OpenIdConnectEvents
                {
                    OnRemoteFailure = callbackHandler.RemoteFailure,
                    OnTokenValidated = callbackHandler.TokenValidated
                },
                TokenValidationParameters = new TokenValidationParameters
                {
                    // Instead of using the default validation (validating against
                    // a single issuer value, as we do in line of business apps), 
                    // we inject our own multitenant validation logic
                    ValidateIssuer = false,

                    NameClaimType = "name"
                }
            });

            return app;
        }


    }
}
