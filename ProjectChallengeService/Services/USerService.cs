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

        public User Post(UserRequestPost request)
        {
            User user = new User();
            user.UserName = request.UserName;
            user.Password = request.Password;
            user.Role = request.Role;
            user.Login = request.Login;
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();
            return user;
        }

        public async Task<User> Put(UserRequestPut request)
        {
            var user = await _unitOfWork.UserRepository.GetById(request.Id);
            if (user is null)
                return null;

            user.UpdateDate = DateTime.UtcNow.AddHours(-3);
            user.UserName = request.UserName;
            user.Password = request.Password;
            user.Role = request.Role;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();
            return user;
        }
    }
}
