using SmartFit.Model;
using System.Data;
using Dapper;
using SmartFit.Interfaces;

namespace SmartFit.Services
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _dbConnection;

        public UserService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Users WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            string sql = "SELECT * FROM Users";
            return await _dbConnection.QueryAsync<User>(sql);
        }

        public async Task AddAsync(User user)
        {
            string sql = @"INSERT INTO Users (Id, Name, Age, Height, CurrentWeight, WeightGoal, Email, CreatedAt)
                           VALUES (@Id, @Name, @Age, @Height, @CurrentWeight, @WeightGoal, @Email, @CreatedAt)";
            await _dbConnection.ExecuteAsync(sql, user);
        }

        public async Task UpdateAsync(User user)
        {
            string sql = @"UPDATE Users 
                           SET Name = @Name, Age = @Age, Height = @Height, 
                               CurrentWeight = @CurrentWeight, WeightGoal = @WeightGoal, Email = @Email
                           WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, user);
        }

        public async Task DeleteAsync(Guid id)
        {
            string sql = "DELETE FROM Users WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
