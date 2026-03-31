using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Regulation : BaseEntity
    {
        public string Title { get; set; } // [cite: 78]
        public string TextContent { get; set; } // AI'nın okuyacağı metin [cite: 79]
        public string Category { get; set; } // [cite: 80]
    }
}
