using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Core.DTOs;
using User.Core.Entities;
using User.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace User.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserRequest req);
        Task<UserDto?> GetAsync(Guid id);
        Task<IEnumerable<UserDto>> ListAsync();
        Task UpdateAsync(Guid id, UpdateUserRequest req);
        Task DeleteAsync(Guid id);
        Task<UserDto?> AuthenticateAsync(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<UserDto> CreateAsync(CreateUserRequest req)
        {
            // simple hash (demo). Replace with PBKDF2/Argon2 in prod.
            var hash = ComputeHash(req.Password);
            var user = new User { Email = req.Email, PasswordHash = hash };
            var created = await _repo.AddAsync(user);
            return new UserDto(created.Id, created.Email, created.CreatedAt);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<UserDto?> GetAsync(Guid id)
        {
            var u = await _repo.GetAsync(id);
            return u == null ? null : new UserDto(u.Id, u.Email, u.CreatedAt);
        }

        public async Task<IEnumerable<UserDto>> ListAsync()
        {
            var list = await _repo.ListAsync();
            return list.Select(u => new UserDto(u.Id, u.Email, u.CreatedAt));
        }

        public async Task UpdateAsync(Guid id, UpdateUserRequest req)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) throw new KeyNotFoundException("User not found");
            if (req.Email != null) existing.Email = req.Email;
            if (req.Password != null) existing.PasswordHash = ComputeHash(req.Password);
            await _repo.UpdateAsync(existing);
        }

        public async Task<UserDto?> AuthenticateAsync(string email, string password)
        {
            var u = await _repo.GetByEmailAsync(email);
            if (u == null) return null;
            if (u.PasswordHash != ComputeHash(password)) return null;
            return new UserDto(u.Id, u.Email, u.CreatedAt);
        }

        private static string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var h = sha.ComputeHash(bytes);
            return Convert.ToBase64String(h);
        }
    }
}
