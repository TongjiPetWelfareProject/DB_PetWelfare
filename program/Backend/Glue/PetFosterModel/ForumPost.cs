using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glue.PetFoster.Model
{
    public class ForumPost
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public List<string> urls { get; set; }
        public DateTime Post_time { get; set; }
        public int ReadCount { get; set; }
        public int LikeNum { get; set; }
        public int CommentNum { get; set; }
        //public ForumPost(int BID, int EID, string Heading, string Content, DateTime pd, int rd) { }
        public ForumPost() { }
    }
}
