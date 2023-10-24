using ProjectChallengeDomain.Models;
using ProjectLibraryDomain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibraryDomain.Models
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
