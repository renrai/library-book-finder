using ProjectChallengeData.Database.Repositories.IRepositories;
using ProjectChallengeDomain.IService;
using ProjectChallengeDomain.Models.Requests;
using ProjectChallengeDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibraryDomain.IService;
using ProjectLibraryDomain.Models;
using ProjectLibraryDomain.Models.Requests;

namespace ProjectLibraryService.Services
{
    public class UserService : IUSerService
    {
        private static IUnitOfWork _unitOfWork;


        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if (user is null)
                return false;

            _unitOfWork.UserRepository.Remove(user);
            _unitOfWork.Commit();

            return true;
        }

        public async Task<List<User>> Get()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return users.ToList();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public bool Post(UserRequestPost request)
        {
            User user = new User();
            user.UserName = request.UserName;
            user.Password = base64Encode(request.Password);
            user.Role = request.Role;
            user.Login = request.Login;
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();
            return true;
        }

        public async Task<bool> Put(UserRequestPut request)
        {
            var user = await _unitOfWork.UserRepository.GetById(request.Id);
            if (user is null)
                return false;

            user.UpdateDate = DateTime.UtcNow.AddHours(-3);

            user.UserName = request.UserName;

            if (!string.IsNullOrEmpty(request.Password))
            {
              user.Password = base64Encode(request.Password);
            }

            user.Role = request.Role;

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();
            return true;
        }

        public static string base64Encode(string sData) // Encode
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
