using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportMeetingsApi.Authentication.Models;
using SportMeetingsApi.Persistence;
using SportMeetingsApi.Shared.Models;
using SportMeetingsApi.Shared.Settings;
using System.Linq;
using Microsoft.Extensions.Options;
using SportMeetingsApi.Authentication.Settings;
using SportMeetingsApi.Shared.Services;

namespace SportMeetingsApi.Authentication.Services;

public class AuthenticationService {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;

    private readonly IContext _context;

    public AuthenticationService(
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSettings> jwtSettings, IContext context) {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings.Value;
        _context = context;
    }

    public async Task<Either<Error, TokenModel>> Login(Login model) {
        try {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || user.IsDeleted || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Left(new Error("Incorrect email or password"));

            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in await _userManager.GetRolesAsync(user)) {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = CreateToken(authClaims);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSettings.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return new TokenModel(
                AccessToken: new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken: refreshToken,
                Expiration: token.ValidTo
            );
        }
        catch (Exception ex) {
            return Left(new Error(ex.Message));
        }
    }

    private static string GenerateRefreshToken() {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims) {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    public async Task<Either<Error, RegisterResponse>> Register(Register model) {
        try {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return Left(new Error("User already exists!"));

            User user = new() {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                IsDeleted = false
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return Left(new Error("User creation failed! Please check user details and try again."));

            if (await _roleManager.RoleExistsAsync(UserRole.User)) {
                await _userManager.AddToRoleAsync(user, UserRole.User);
            }

            return new RegisterResponse(Message: "User created successfully!");
        }
        catch (Exception ex) {
            return Left(new Error(ex.Message));
        }
    }

    public async Task<Either<Error, TokenModel>> RefreshToken(RefreshTokenModel refreshTokenModel) {
        try {
            string accessToken = refreshTokenModel.AccessToken;
            string refreshToken = refreshTokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal.IsNone)
                return Left(new Error("Invalid access token or refresh token"));

            string username = principal.Match<string>(
                Some: v => v.Identity?.Name is null ?
                    string.Empty :
                    v.Identity.Name,
                None: () => string.Empty
            );

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.IsDeleted || user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
                return Left(new Error("Invalid access token or refresh token"));

            var claims = principal.Match<List<Claim>>(
                Some: v => v.Claims is null ?
                    new List<Claim>() :
                    v.Claims.ToList(),
                None: () => new List<Claim>()
            );

            var newAccessToken = CreateToken(claims);
            user.RefreshToken = GenerateRefreshToken();

            await _userManager.UpdateAsync(user);

            return new TokenModel(
                AccessToken: new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken: user.RefreshToken,
                Expiration: newAccessToken.ValidTo
            );
        }
        catch (Exception ex) {
            return Left(new Error(ex.Message));
        }
    }

    private Option<ClaimsPrincipal> GetPrincipalFromExpiredToken(string? token) {
        var tokenValidationParameters = new TokenValidationParameters {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (principal is null || securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return None;

        return principal;
    }

    public async Task<string> Test() {
        var user = await _userManager.FindByIdAsync(_context.UserId);
        var roles = await _userManager.GetRolesAsync(user);
        return _context.UserId + ": " + (roles is null ? string.Empty : string.Join(", ", roles));
    }
}
