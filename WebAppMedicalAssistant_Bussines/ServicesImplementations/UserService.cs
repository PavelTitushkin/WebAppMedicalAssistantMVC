using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckUserPassword(string email, string password)
        {
            var dbPasswordHash = (await _unitOfWork.User.Get().FirstOrDefaultAsync(user => user.Email.Equals(email)))?.PasswordHash;

            return dbPasswordHash != null && CreateMd5(password).Equals(dbPasswordHash.ToUpper());
        }   

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.User.FindBy(user => user.Email.Equals(email), user => user.Roles).FirstOrDefaultAsync();
            var userDto = _mapper.Map<UserDto>(user);

            return userDto; 
        }

        public async Task<int> RegisterUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.PasswordHash = CreateMd5(userDto.PasswordHash);

            await _unitOfWork.User.AddEntityAsync(user);

            return await _unitOfWork.Commit();
        }

        //преобразование пароля в Hash
        private string CreateMd5(string password)
        {
            //var passwordSalt = _configuration["UserSecrets:PasswordSalt"];

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
