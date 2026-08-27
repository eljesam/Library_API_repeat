using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Authentication;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using AppUser = Library_API_repeat.Api.Models.User;

namespace Library_API_repeat.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly LibraryDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(
            LibraryDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {
            if (await EmailExistsAsync(dto.Email))
            {
                return false;
            }

            var user = new AppUser
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = "Member"
            };

            var passwordHasher = new PasswordHasher<AppUser>();

            user.PasswordHash = passwordHasher.HashPassword(
                user,
                dto.Password);

            var member = new Member
            {
                User = user,
                MembershipDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.Members.Add(member);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<AuthResponseDTO?> LoginAsync(LoginDTO dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<AppUser>();

            var verificationResult =
                passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    dto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Name),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role, 
                    user.Role)
            };

            var jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException(
                    "JWT key is not configured.");
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
