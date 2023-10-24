using ProjectChallengeDomain.Models.Requests;
using ProjectChallengeDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibraryDomain.Models;
using ProjectLibraryDomain.Models.Requests;

namespace ProjectLibraryDomain.IService
{
    public interface IUSerService
    {
        User Post(UserRequestPost request);
        Task<User> Put(UserRequestPut request);
        Task<List<User>> Get();
        Task<User> GetById(Guid id);

        Task<bool> Delete(Guid id);
    }
}
