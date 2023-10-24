﻿using ProjectLibraryDomain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibraryDomain.Models.Requests
{
    public class UserRequestPost
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
