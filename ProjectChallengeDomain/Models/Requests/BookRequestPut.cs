using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectChallengeDomain.Models.Requests
{
    public class BookRequestPut : BookRequestPost
    {
        public Guid Id { get; set; }
    }
}
