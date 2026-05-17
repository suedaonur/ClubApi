using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StudentClubDto
    {
        public int Id { get; set; } // Üyelik tablosunun ID'si
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsWriteable { get; set; }
        public bool IsRemoveableMember { get; set; }
    }
}
