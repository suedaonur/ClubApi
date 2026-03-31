using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        public string StudentNumber { get; set; } 
        public string FullName { get; set; } 
        public string Email { get; set; } 
        public bool IsObsVerified { get; set; } 

        public ICollection<ClubMember> ClubMemberships { get; set; } 
    }
}
