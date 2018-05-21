using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DAO.DTO;
using DAO.Models;
using DAO.Repositories;

namespace DAO.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHashPasswordService _hashPasswordService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IHashPasswordService hashPasswordService, ITokenService tokenService, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _hashPasswordService = hashPasswordService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public IEnumerable<User> GetAll()
        {
            return _usersRepository.GetAll().ToList();
        }

        public User GetById(int id)
        {
            return _usersRepository.GetById(id);
        }

        public TokenDto Create(SignUpDto signUpDto)
        {
            var userFromDb = _usersRepository.GetAll().FirstOrDefault(x => x.UserName == signUpDto.UserName);
            if (userFromDb != null)
            {
                return null;
            }

            var user = _mapper.Map<SignUpDto, User>(signUpDto);

            _hashPasswordService.AddSaltAndHashPassword(user);

            _usersRepository.Create(user);
            _usersRepository.Save();
            return _tokenService.GenerateToken(_mapper.Map<User, UserDto>(user));
        }

        public void Update(User user)
        {
            _usersRepository.Update(user);
            _usersRepository.Save();
        }

        public void Delete(int id)
        {
            _usersRepository.Delete(id);
            _usersRepository.Save();
        }

        public TokenDto Login(LoginDTO loginDto)
        {
            var user = _usersRepository.GetAll().FirstOrDefault(x => x.UserName == loginDto.UserName);

            if (user != null && _hashPasswordService.PasswordEqualsUserPassword(loginDto.Password, user))
            {
                return _tokenService.GenerateToken(_mapper.Map<User, UserDto>(user));
            }

            return null;
        }
    }
}
