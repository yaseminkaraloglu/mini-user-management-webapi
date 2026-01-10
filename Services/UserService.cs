using MyWebApplication.Models;
using MyWebApplication.Data;
using MyWebApplication.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MyWebApplication.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            return await _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    IsActive = u.IsActive
                })
                .ToListAsync();
        }

        public async Task<List<UserResponseDto>> GetActiveUsers()
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    IsActive = u.IsActive
                })
                .ToListAsync();
        }

        public async Task<UserResponseDto?> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive
            };
        }

        public async Task<UserResponseDto> Create(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.IsActive = true;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive
            };
        }

        public async Task<UserResponseDto?> Update(int id, User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null) return null;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.IsActive = user.IsActive;

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email,
                IsActive = existingUser.IsActive
            };
        }

        public async Task<bool> SoftDelete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            user.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}