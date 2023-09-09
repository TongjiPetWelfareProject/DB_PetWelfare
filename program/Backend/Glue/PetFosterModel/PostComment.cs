using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.Model
{
    public class PostComment
    {
        public string PID { get; set; }
        public string Post_Title { get; set; }
        public string UID { get; set; }
        public string User_Name { get; set; }
        public string Avatar { get; set; }
        public string Content { get; set; }
        public DateTime Comment_Time { get; set; }
        public PostComment() { }
    }
}
