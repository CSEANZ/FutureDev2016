using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace DXNewsAPI.Model.SetupHelpers
{
    public class AuthCallbackHandler
    {
        public Task TokenValidated(TokenValidatedContext context)
        {

            string issuer = context.SecurityToken.Issuer;
            string subject = context.SecurityToken.Subject;
            string tenantID = context.Ticket.Principal.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

            // Build a dictionary of approved tenants
            IEnumerable<string> approvedTenantIds = new List<string>
            {
                "43d9f969-d7ef-4f3f-a55b-50a48f7ae65f",
                "9188040d-6c67-4c5b-b112-36a304b66dad" // MSA Tenant
            };

            if (!approvedTenantIds.Contains(tenantID))
                throw new SecurityTokenValidationException();

            /* ---------------------
            // Replace this with your logic to validate the issuer/tenant
               ---------------------       
            // Retriever caller data from the incoming principal
            string issuer = context.SecurityToken.Issuer;
            string subject = context.SecurityToken.Subject;
            string tenantID = context.Ticket.Principal.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

            // Build a dictionary of approved tenants
            IEnumerable<string> approvedTenantIds = new List<string>
            {
                "<Your tenantID>",
                "9188040d-6c67-4c5b-b112-36a304b66dad" // MSA Tenant
            };

            if (!approvedTenantIds.Contains(tenantID))
                throw new SecurityTokenValidationException();
              --------------------- */

            return Task.FromResult(0);
        }

        // Handle sign-in errors differently than generic errors.
        public Task RemoteFailure(FailureContext context)
        {
            context.HandleResponse();
            context.Response.Redirect("/Home/Error?message=" + context.Failure.Message);
            return Task.FromResult(0);
        }
    }
}
