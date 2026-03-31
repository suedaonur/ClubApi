using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vote : BaseEntity
    {
        public int StudentId { get; set; } 
        public int PostId { get; set; } 
        public bool IsUpvote { get; set; } 

        public Student Student { get; set; }
        public Post Post { get; set; }
    }
}
