using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SportMeetingsApi.Shared.Services;

public class HttpCurrentContext : IContext {
    public string UserId => _lazyUserId.Value;
    private readonly Lazy<string> _lazyUserId;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpCurrentContext(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
        _lazyUserId = new Lazy<string>(
            () => {
                var currentUser =
                    _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                    throw new InvalidOperationException("Cannot get current user id");

                return currentUser;
            }
        );
    }

}
