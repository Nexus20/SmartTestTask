using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace SmartTestTask.API.Authentication;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly string _apiKey;
    
    public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IConfiguration configuration) : base(options, logger, encoder, clock)
    {
        _apiKey = configuration["ApiKey"];
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    { 
        if (!Request.Headers.ContainsKey("ApiKey"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization header missing."));
        }

        var apiKeyFromHeader = Request.Headers["ApiKey"].ToString();

        if(apiKeyFromHeader != _apiKey)
            return Task.FromResult(AuthenticateResult.Fail("Invalid Api Key"));

        var claims = new[] {
            new Claim(ClaimTypes.Name, "User"),
            new Claim(ClaimTypes.Role, "User") 
        };

        var claimsIdentity = new ClaimsIdentity(claims, nameof(ApiKeyAuthenticationHandler));

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
    }
}