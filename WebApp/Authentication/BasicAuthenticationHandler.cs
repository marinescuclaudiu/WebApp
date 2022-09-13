using DatabaseAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace WebApp.Authentication
{
    public class BasicAuthenticationHandler :AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserManager<ApplicationUser> _userManager;
         public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            UserManager<ApplicationUser>userManager
            ):base(options, logger, encoder, clock)
        {
            _userManager = userManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var authHeader = Request.Headers.Authorization[0];
                var token = authHeader.Substring("Basic ".Length).Trim();
                var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialString.Split(':');
                var user = await _userManager.FindByNameAsync(credentials[0]);

                if (user == null)
                {
                    return AuthenticateResult.Fail("Invalid user name");
                }

                bool isPasswordOk = await _userManager.CheckPasswordAsync(user, credentials[1]);

                if (isPasswordOk == false)
                {
                    return AuthenticateResult.Fail("Invalid password");
                }

                var claims = new[] { new Claim("name", user.UserName), new Claim(ClaimTypes.NameIdentifier, user.Id) };
                var identity = new ClaimsIdentity(claims, "Basic");
                var claimsPrincipal = new ClaimsPrincipal(identity);

                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
            }
            catch
            {
                return AuthenticateResult.Fail("Could not read auth header");
            }
          
        }
    }
}
