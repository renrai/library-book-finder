using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeData.Database.Entities
{
    public class BookEntity : BaseEntity
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Shelf { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
    }
}
