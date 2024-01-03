using System;
using System.Collections.Generic;

namespace Labb3_Gymnasium.Models
{
    public partial class EmployeeDepartmentInfo
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int Salary { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
    }
}
