using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeDomain.Models.Requests
{
    public class BookRequestPost
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Shelf { get; set; }
        public string Genre { get; set; }

        public int Year { get; set; }
    }
}
