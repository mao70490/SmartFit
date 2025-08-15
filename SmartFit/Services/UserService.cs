using SmartFit.Model;
using System.Data;
using Dapper;
using SmartFit.Interfaces;
using Microsoft.AspNetCore.Identity.Data;

namespace SmartFit.Services
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _db;

        public UserService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var exists = await _db.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM Users WHERE Email = @Email",
                new { request.Email }
            );

            if (exists > 0) throw new Exception("Email 已被註冊");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var sql = @"
            INSERT INTO Users (Id, Name, Email, PasswordHash, Age, Height, CurrentWeight, WeightGoal, CreatedAt)
            VALUES (@Id, @Name, @Email, @PasswordHash, @Age, @Height, @CurrentWeight, @WeightGoal, GETDATE())";

            var rows = await _db.ExecuteAsync(sql, new
            {
                Id = Guid.NewGuid(),
                request.Name,
                request.Email,
                PasswordHash = passwordHash,
                request.Age,
                request.Height,
                request.CurrentWeight,
                request.WeightGoal
            });

            return rows > 0;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Email = @Email",
                new { Email = email }
            );
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _db.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Id = @Id",
                new { Id = id }
            );
        }
    }
}
