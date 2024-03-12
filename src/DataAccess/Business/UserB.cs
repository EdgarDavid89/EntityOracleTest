using System;
using AutoMapper;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Business
{
    public class UserB : IUserB
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;


        public UserB(IRepository<User> userRepository, 
        IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUser(UserDto user)
        {
            user.Id = user.Id > 0 || user.Id < 0  ? 0: user.Id;
            user.DateCreated = DateTime.Now;
            user.DateUpdated = DateTime.Now;
            var userEntity = _mapper.Map<User>(user);
            var newParking = await _userRepository.InsertAsync(userEntity);
            return _mapper.Map<UserDto>(newParking);
        }

        public async Task<UserDto> DeleteUser<TId>(TId id)
        {
            var userDelet = await _userRepository.DeleteAsync(id);
            return _mapper.Map<UserDto>(userDelet);
        }

        public async Task<UserDto> GetUserById<TId>(TId id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> UpdateUser<TId>(UserDto user, TId id)
        {
            var userUpdate = _mapper.Map<User>(user);
            userUpdate.Dateupdated = DateTime.Now;     
            var updateUser = await _userRepository.UpdateAsync(userUpdate, id);
            return _mapper.Map<UserDto>(updateUser);
        }

        public async Task<UserDto> GetUserByUserName(string userName)
        {
            var updateUser = await _userRepository.GetByFilterAsync(u => u.Username == userName);
            return _mapper.Map<UserDto>(updateUser.FirstOrDefault());
        }
        
    }
}