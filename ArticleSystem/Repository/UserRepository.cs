using ArticleSystem.Database;
using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using ArticleSystem.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArticleSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private ArticleDbContext _context;
        private IMapper _mapper;
        private IPasswordHasher<User> _passwordHasher;

        public UserRepository(ArticleDbContext context, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public void AddUser(UserRegisterDto userRegisterDto)
        {
            if (_context.Users.Any(x => x.Login.ToLower() == userRegisterDto.Login.ToLower()))
                throw new UserLoginDuplicateException("User login is busy. Please choose other");

            if (_context.Users.Any(x => x.Email.ToLower() == userRegisterDto.Email.ToLower()))
                throw new UserEmailDuplicateException("Email is already used. Please choose other");

            User newUser = new User();
            newUser = _mapper.Map<User>(userRegisterDto);

            var hashedPassword = _passwordHasher.HashPassword(newUser, userRegisterDto.Password);
            newUser.HashedPassword = hashedPassword;

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public User LoginUser(UserLoginDto userLoginDto)
        {
            User? potentialUser = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Login == userLoginDto.Login);

            if (potentialUser is null) throw new BadLoginException("Wrong login or password.");

            var result = _passwordHasher.VerifyHashedPassword(potentialUser, potentialUser.HashedPassword, userLoginDto.Password);

            if (result != PasswordVerificationResult.Success) throw new BadLoginException("Wrong login or password.");

            return potentialUser;
        }
    }
}
