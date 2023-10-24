using ProjectChallengeData.Database.Entities;
using ProjectChallengeData.Database.Repositories.IRepositories;
using ProjectChallengeDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeData.Database.Repositories
{
    public class BookRepository : RepositoryBase<Book, BookEntity>, IBookRepository
    {
        private readonly ProjectContextDb _context;

        public BookRepository(ProjectContextDb context) : base(context)
        {
            _context = context;
        }
    }
}
