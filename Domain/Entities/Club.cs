using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Club : BaseEntity
    {
        public string Name { get; set; } 
        public string Description { get; set; } 
        public int PresidentId { get; set; } 
        public ClubStatus Status { get; set; } 

       
        public ICollection<ClubMember> Members { get; set; } 
        public ICollection<Post> Posts { get; set; } 
    }

    public enum ClubStatus { Pending = 0, Active = 1, Closed = 2 } 
}
