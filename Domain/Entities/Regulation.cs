using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Regulation : BaseEntity
    {
        public string Title { get; set; } 
        public string TextContent { get; set; } 
        public string Category { get; set; } 
    }
}
