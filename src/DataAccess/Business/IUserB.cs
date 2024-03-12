using System;
using DataAccess.DTO;

namespace DataAccess.Business
{
    public interface IUserB
    {
        Task<UserDto> GetUserById<TId>(TId id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> CreateUser(UserDto worker);
        Task<UserDto> UpdateUser<TId>(UserDto worker, TId id);
        Task<UserDto> DeleteUser<TId>(TId id);
        Task<UserDto> GetUserByUserName(string userName);
    }
}