using ElevenNote.Data.Entities;
using ElevenNote.Models.Token;
using ElevenNote.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ElevenNote.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _context;
        public TokenService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TokenResponse> GetTokenAsync(TokenRequest model) 
        {
            var userEntity = await GetValidUserAsync(model);
            if (userEntity is null)
                return null;

            return GenerateToken(userEntity);
        }
        private async Task<UserEntity> GetValidUserAsync(TokenRequest model) 
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == model.Username.ToLower());
            if (userEntity is null)
                return null;
            var passwordHasher = new PasswordHasher<UserEntity>();
            var verifyPasswordResult = passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password, model.Password);
            if (verifyPasswordResult == PasswordVerificationResult.Failed)
                return null;

            return userEntity;
        }
        private TokenResponse GenerateToken(UserEntity entity) {}

        private Claim[] GetClaims(UserEntity user)
        {
            var fullName = $"{user.FirstName} {user.LastName}";
            var name = !string.IsNullOrWhiteSpace(fullName) ? fullName : user.Username;

            var claims = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email),
                new Claim("Name", name)
            };

            return claims;
        }
    }
}