using ProjectChallengeData.Database.Repositories.IRepositories;
using ProjectChallengeDomain.Models;
using ProjectLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibraryData.Database.Repositories.IRepositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}
