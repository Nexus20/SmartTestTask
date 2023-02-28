using Microsoft.AspNetCore.Authorization;
using SmartTestTask.API.Constants;

namespace SmartTestTask.API.Authentication;

public class ApiKeyAuthorizeAttribute : AuthorizeAttribute
{
    public ApiKeyAuthorizeAttribute()
    {
        Policy = CustomAuthenticationPolicies.ApiKeyAuthenticationPolicy;
        AuthenticationSchemes = CustomAuthenticationSchemes.ApiKeyAuthentication;
    }
}