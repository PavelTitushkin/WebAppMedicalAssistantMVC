using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core;
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

        public async Task<bool> CheckUserPasswordAsync(string email, string password)
        {
            var dbPasswordHash = (await _unitOfWork.User.Get().FirstOrDefaultAsync(user => user.Email.Equals(email)))?.PasswordHash;

            return dbPasswordHash != null && CreateMd5(password).Equals(dbPasswordHash.ToUpper());
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.User.FindBy(entity => entity.Id.Equals(id))
                    .Include(include => include.DoctorVisits)
                    .Include(include => include.TransferredDiseases)
                    .Include(include => include.Analyzes)
                    .Include(include => include.Vaccinations)
                    .Include(include => include.MedicalExaminations)
                    .Include(include => include.PrescribedMedication)
                    .Include(include => include.Fluorographies)
                    .Include(include => include.PhysicalTherapies)
                    .FirstOrDefaultAsync();

                _unitOfWork.User.Remove(entity);
                await _unitOfWork.Commit();
            }

            catch (Exception)
            {
                throw new ArgumentException("User for removing doesn't exist", nameof(id));
            }
        }

        public async Task<List<UserDto>> GetAllUserAsync()
        {
            try
            {
                var usersDto = await _unitOfWork.User.Get()
                    .Include(entity=>entity.Roles)
                    .Select(entities => _mapper.Map<UserDto>(entities)).ToListAsync();

                return usersDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.User
                .FindBy(user => user.Email.Equals(email), user => user.Roles)
                .FirstOrDefaultAsync();
            var userDto = _mapper.Map<UserDto>(user);

            return userDto; 
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            var isExit = await _unitOfWork.User.FindBy(user => user.Email.Equals(email)).FirstOrDefaultAsync();

            return isExit != null;
        }

        public async Task<int> RegisterUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.PasswordHash = CreateMd5(userDto.PasswordHash);
            user.Email = user.Email.ToLower();

            await _unitOfWork.User.AddEntityAsync(user);

            return await _unitOfWork.Commit();
        }

        public async Task<int> UpdateUserAsync(UserDto dto, int id)
        {
            try
            {
                var sourceDto = await GetUserByEmailAsync(dto.Email);
                var patchList = new List<PatchModel>();

                if (dto.LastName != sourceDto.LastName)
                {
                    patchList.Add(new PatchModel()
                    {
                        PropertyName = nameof(dto.LastName),
                        PropertyValue = dto.LastName
                    });
                }
                if (dto.FirstName != sourceDto.FirstName)
                {
                    patchList.Add(new PatchModel()
                    {
                        PropertyName = nameof(dto.FirstName),
                        PropertyValue = dto.FirstName
                    });
                }
                if (dto.Birthday != sourceDto.Birthday)
                {
                    patchList.Add(new PatchModel()
                    {
                        PropertyName = nameof(dto.Birthday),
                        PropertyValue = dto.Birthday
                    });
                }
                if (dto.Email != sourceDto.Email)
                {
                    patchList.Add(new PatchModel()
                    {
                        PropertyName = nameof(dto.Email),
                        PropertyValue = dto.Email
                    });
                }
                if (dto.Avatar != sourceDto.Avatar)
                {
                    patchList.Add(new PatchModel()
                    {
                        PropertyName = nameof(dto.Avatar),
                        PropertyValue = dto.Avatar
                    });
                }

                await _unitOfWork.User.PatchAsync(id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {

                throw;
            }
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
