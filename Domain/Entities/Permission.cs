using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; } // [cite: 56]
        public string Description { get; set; } // [cite: 57]
    }
}
