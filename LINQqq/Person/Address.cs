using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQqq.Person
{
    public class Address
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}
