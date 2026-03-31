using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        public int ClubId { get; set; } 
        public string Title { get; set; } 
        public string Content { get; set; } 
        public PostType Type { get; set; } 
        public int VoteScore { get; set; } = 0; 

        public Club Club { get; set; } 
        public ICollection<Vote> Votes { get; set; } 
    }

     public enum PostType { Article, Event, Announcement, Form, Poll } 
}
