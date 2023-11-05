using ProjectChallengeDomain.Models;
using ProjectChallengeDomain.Models.Requests;
using ProjectLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeDomain.IService
{
    public interface IBookService
    {
        Book Post(BookRequestPost request);
        Task<Book> Put(BookRequestPut request);
        Task<List<Book>> Get();
        Task<Book> GetById(Guid id);

        Task<bool> Delete(Guid id);
        Task<List<Book>> Search(PaginationFilter filter);

    }
}
