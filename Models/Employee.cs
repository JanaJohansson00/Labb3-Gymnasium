﻿using System;
using System.Collections.Generic;

namespace Labb3_Gymnasium.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Courses = new HashSet<Course>();
        }

        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
