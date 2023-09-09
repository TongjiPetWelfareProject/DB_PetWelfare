using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.Model
{
    public class Bulletin
    {
        public string Id { get; set; }
        public int EmployeeID { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public DateTime published_date { get; set; }
        public int ReadCount { get; set; }
        public Bulletin(int BID, int EID, string Heading, string Content, DateTime pd, int rd) { }
        public Bulletin() { }
    }
}
