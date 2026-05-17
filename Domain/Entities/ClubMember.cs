using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClubMember : BaseEntity
    {
        public int StudentId { get; set; } 
        public int ClubId { get; set; } 
        public bool IsAdmin { get; set; }
        public bool IsWriteable { get; set; }
        public bool IsRemoveableMember { get; set; }

        public Student Student { get; set; }
        public Club Club { get; set; }
    }
}
