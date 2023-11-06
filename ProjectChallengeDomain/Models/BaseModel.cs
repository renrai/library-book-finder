using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeDomain.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserCreation { get; set; }
        public string UserLastUpdate { get; set; }
    }
}
