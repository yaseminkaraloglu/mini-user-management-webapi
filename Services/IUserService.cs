using MyWebApplication.Dtos;
using MyWebApplication.Models;

namespace MyWebApplication.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllUsers();
        Task<List<UserResponseDto>> GetActiveUsers();
        Task<UserResponseDto?> GetById(int id);
        Task<UserResponseDto> Create(User user);
        Task<UserResponseDto?> Update(int id, User user);
        Task<bool> SoftDelete(int id);
    }
}