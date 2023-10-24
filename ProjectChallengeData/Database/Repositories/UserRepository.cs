using ProjectChallengeData.Database.Entities;
using ProjectChallengeData.Database.Repositories.IRepositories;
using ProjectChallengeData.Database.Repositories;
using ProjectChallengeData.Database;
using ProjectChallengeDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibraryData.Database.Entities;
using ProjectLibraryDomain.Models;
using ProjectLibraryData.Database.Repositories.IRepositories;

namespace ProjectLibraryData.Database.Repositories
{
    public class UserRepository : RepositoryBase<User, UserEntity>, IUserRepository
    {
        private readonly ProjectContextDb _context;

        public UserRepository(ProjectContextDb context) : base(context)
        {
            _context = context;
        }
    }
}
