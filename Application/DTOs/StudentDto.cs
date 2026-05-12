using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public string StudentNumber { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsObsVerified { get; set; }
}
