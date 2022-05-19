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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportMeetingsApi.Authentication.Models;
using SportMeetingsApi.Persistence;
using SportMeetingsApi.Shared.Models;
using SportMeetingsApi.Shared.Settings;
using System.Linq;

namespace SportMeetingsApi.Authentication.Services;

public class AuthenticationService {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager, JwtSettings jwtSettings) {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<Either<Error, TokenModel>> Login(Login model) {

        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Left(new Error("Incorrect email or password"));

        var authClaims = new List<Claim> {
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
            expires: DateTime.Now.AddMinutes(_jwtSettings.TokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    public async Task<Either<Error, RegisterResponse>> Register(Register model) {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return Left(new Error("User already exists!"));

        User user = new() {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return Left(new Error("User creation failed! Please check user details and try again."));

        return new RegisterResponse(Message: "User created successfully!");
    }

    public async Task<Either<Error, TokenModel>> RefreshToken(RefreshTokenModel refreshTokenModel) {

        string accessToken = refreshTokenModel.AccessToken;
        string refreshToken = refreshTokenModel.RefreshToken;

        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null) {
            return Left(new Error("Invalid access token or refresh token"));
        }
        string? username = principal?.Identity?.Name;

        var user = await _userManager.FindByNameAsync(username);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) {
            return Left(new Error("Invalid access token or refresh token"));
        }

        var newAccessToken = CreateToken(principal.Claims.ToList());
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new TokenModel(
            AccessToken: new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken: newRefreshToken,
            Expiration: newAccessToken.ValidTo
        );
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token) {
        var tokenValidationParameters = new TokenValidationParameters {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
