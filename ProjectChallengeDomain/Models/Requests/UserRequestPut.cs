using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibraryDomain.Models.Requests
{
    public class UserRequestPut : UserRequestPost
    {
        public Guid Id { get; set; }
    }
}
