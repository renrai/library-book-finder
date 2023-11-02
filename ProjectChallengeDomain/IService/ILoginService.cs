using ProjectChallengeDomain.Models.Requests;
using ProjectChallengeDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibraryDomain.Models;
using ProjectLibraryDomain.Models.DTO;

namespace ProjectLibraryDomain.IService
{
    public interface ILoginService
    {
        Task<RefreshToken> Login (LoginDTO request);

    }
}
