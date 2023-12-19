using System;
using System.Collections.Generic;

namespace Labb3_Gymnasium.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? PersonalNumber { get; set; }
        public string? Class { get; set; }
    }
}
