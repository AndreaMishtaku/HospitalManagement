using AutoMapper;
using Cryptography;
using Entities.Configuration;
using Entities.Models;
using Exceptions;
using IRepository;
using IService;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO.Request;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

namespace Service;

public class AuthenticationService : IAuthenticationService
{

    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IOptions<JwtConfiguration> _configuration;
    private readonly JwtConfiguration _jwtConfiguration;
    //private readonly IEmailSender _emailSender;
    private readonly IRepositoryManager _repositoryManager;
    private readonly DefaultConfiguration _defaultConfig;
    private readonly SignInManager<User> _signInManager;
    private readonly ICryptoUtils _cryptoUtils;


    public AuthenticationService(ILoggerManager logger
    , IMapper mapper
    , UserManager<User> userManager
    , IOptions<JwtConfiguration> configuration
    //, IEmailSender emailSender
    , IRepositoryManager repositoryManager
    , DefaultConfiguration defaultConfig
    , SignInManager<User> signInManager
    , ICryptoUtils cryptoUtils)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _jwtConfiguration = _configuration.Value;
        _repositoryManager = repositoryManager;
        _defaultConfig = defaultConfig;
        _signInManager = signInManager;
        _cryptoUtils = cryptoUtils;
    }
    public async Task<IdentityResult> CreateUserAsync(UserRegisterDTO userRegister, string role)
    {
        try
        {
            var user = _mapper.Map<User>(userRegister);
            user.UserName = userRegister.Email;
            

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new BadRequestException(result.Errors.ToString());
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CreateUserAsync), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<TokenDTO> RefreshToken(TokenDTO tokenDto)
    {
        try
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

            var currentUserEmail = principal.Claims.Where(x => x.Type == "Email").FirstOrDefault();
            if (currentUserEmail is null)
                throw new BadRequestException("Invalid client request. The tokenDto has some invalid values.");

            var user = await _userManager.FindByEmailAsync(currentUserEmail.Value);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new BadRequestException("Invalid client request. The tokenDto has some invalid values.");

            return await CreateToken(user, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(RefreshToken), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<TokenDTO> ValidateUserAndCreateToken(UserLoginDTO userLogin)
    {
        try
        {
            User currentUser = await _userManager.FindByNameAsync(userLogin.Email);

            if (currentUser is null)
                throw new BadRequestException($"Përdoruesi me email {userLogin.Email} nuk ekziston.");


            var validateUser = await _signInManager.PasswordSignInAsync(currentUser, userLogin.Password, false, lockoutOnFailure: true);

       
            if (!validateUser.Succeeded)
             {
                  _logger.LogWarn(string.Format("{0}: Authentication failed.", nameof(ValidateUserAndCreateToken)));
                  throw new BadRequestException("E-mail ose fjalëkalimi është i pasaktë!");
                    
             }
             else
              {
                  var tokenDto = await CreateToken(currentUser, true);
                  return tokenDto;
              }
        
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(ValidateUserAndCreateToken), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }


    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        int refreshTokenExpire = Convert.ToInt32(_jwtConfiguration.RefreshTokenExpire);
        int tokenExpire = Convert.ToInt32(_jwtConfiguration.Expires);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = false,
            ClockSkew = TimeSpan.FromMinutes(refreshTokenExpire - tokenExpire),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey)),
            ValidIssuer = _jwtConfiguration.ValidIssuer,
            ValidAudience = _jwtConfiguration.ValidAudience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null)
            throw new SecurityTokenException("Token jo i vlefshëm!");

        return principal;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private async Task<ClaimsIdentity> GetClaims(User currentUser, string tokenHash)
    {
        var claims = new List<Claim>
             {
                new Claim("Id", currentUser.Id.ToString()),
                new Claim("Email", currentUser.Email),
                new Claim("FirstName", currentUser.FirstName),
                new Claim("LastName", currentUser.LastName),
                new Claim("TokenHash", tokenHash)
            };

        var roles = await _userManager.GetRolesAsync(currentUser);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return new ClaimsIdentity(claims);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature);
    }

    private async Task<TokenDTO> CreateToken(User? currentUser, bool populateExp)
    {
        try
        {
            if (currentUser is not null)
            {
                var signingCredentials = GetSigningCredentials();
                var tokenHash = _cryptoUtils.Encrypt($"{currentUser.Id}{currentUser.Email}{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}");
                var claims = await GetClaims(currentUser, tokenHash);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    NotBefore = DateTime.UtcNow,
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                    SigningCredentials = signingCredentials,
                    Audience = _jwtConfiguration.ValidAudience,
                    Issuer = _jwtConfiguration.ValidIssuer
                };

                var refreshToken = GenerateRefreshToken();
                currentUser.RefreshToken = refreshToken;
                currentUser.TokenHash = tokenHash;

                if (populateExp)
                    currentUser.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.RefreshTokenExpire));

                await _userManager.UpdateAsync(currentUser);

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(token);

                return new TokenDTO(accessToken, refreshToken);
            }
            return new TokenDTO("", "");
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CreateToken), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

}
