using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA1
{
    public class NewsFeed
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Photos { get; set; }
        public int UserID {  get; set; }
        
        public NewsFeed(int userid,string content,string author,string photos,int id) 
        {
            UserID = userid;
            Content = content;
            Author = author;
            Photos = photos;
            UserID = id;
        }

        public NewsFeed() { }
    }
}
